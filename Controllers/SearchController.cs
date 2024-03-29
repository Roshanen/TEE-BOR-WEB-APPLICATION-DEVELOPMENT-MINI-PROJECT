using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApp.Models;

namespace WebApp.Controllers;

public class SearchController : BaseController
{
    private new readonly MongoContext _mongoContext;

    public SearchController(MongoContext mongoContext)
        : base(mongoContext)
    {
        _mongoContext = mongoContext;
    }

    public IActionResult Index(Search search)
    {
        _SetUserDataInViewData();

        return View();
    }

    public IActionResult ViewResult(Search search)
    {
        try
        {
            List<Event> events = searchFilters(search);

            return View(events);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    public List<Event> searchFilters(Search search)
    {
        var filterBuilder = Builders<Event>.Filter;
        var filter = filterBuilder.Empty;

        DateTime currentDate = DateTime.UtcNow.Date;

        if (!string.IsNullOrEmpty(search.Name))
        {
            filter &= filterBuilder.Regex("EventName", new BsonRegularExpression(search.Name, "i"));
        }

        if (!string.IsNullOrEmpty(search.Status) && search.Status != "any")
        {
            if (search.Status == "Available")
            {
                filter &= filterBuilder.Eq("Status", "Active")
                    & filterBuilder.Gte("RecruitEndDate", currentDate);
            }
            else if (search.Status == "Past")
            {
                filter &= filterBuilder.Lt("RecruitEndDate", currentDate);
            }
            else if (search.Status == "Cancelled")
            {
                filter &= filterBuilder.Eq("Status", "Cancelled");
            }
            else if (search.Status == "Closed")
            {
                filter &= filterBuilder.Eq("Status", "Close");
            }
        }

        if (!string.IsNullOrEmpty(search.DateChoice) && search.DateChoice != "any")
        {
            switch (search.DateChoice.ToLower())
            {
                case "today":
                    filter &=
                        filterBuilder.Gte("RecruitEndDate", currentDate)
                        & filterBuilder.Lt("RecruitEndDate", currentDate.AddDays(1));
                    break;
                case "this week":
                    filter &=
                        filterBuilder.Gte("RecruitEndDate", currentDate)
                        & filterBuilder.Lt("RecruitEndDate", currentDate.AddDays(7));
                    break;
                case "next week":
                    filter &=
                        filterBuilder.Gte("RecruitEndDate", currentDate.AddDays(7))
                        & filterBuilder.Lt("RecruitEndDate", currentDate.AddDays(14));
                    break;
            }
        }

        if (!string.IsNullOrEmpty(search.Type) && search.Type != "any")
        {
            filter &= filterBuilder.Eq("Type", search.Type);
        }

        if (!string.IsNullOrEmpty(search.Category) && search.Category != "any")
        {
            filter &= filterBuilder.Eq("CategoryName", search.Category);
        }

        var events = _mongoContext.GetCollection<Event>("events").Find(filter).ToList();

        if (!string.IsNullOrEmpty(search.Status) && search.Status != "any")
        {
            if (search.Status == "Available")
            {
                events = events.Where(ev => ev.CurrentMember < ev.MaxMember).ToList();
            }
            if (search.Status == "Full")
            {
                events = events.Where(ev => ev.CurrentMember >= ev.MaxMember).ToList();
            }
        }

        if (!string.IsNullOrEmpty(search.Sort))
        {
            if (search.Sort == "Newest")
            {
                events = events.OrderByDescending(ev => ev.EventStartDate).ToList();
            }
            if (search.Sort == "Oldest")
            {
                events = events.OrderBy(ev => ev.EventStartDate).ToList();
            }
        }

        return events;
    }

}
