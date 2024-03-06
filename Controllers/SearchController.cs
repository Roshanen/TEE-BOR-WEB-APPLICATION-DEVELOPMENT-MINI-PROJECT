using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApp.Models;

namespace WebApp.Controllers;

public class SearchController : Controller
{
    private readonly MongoContext _mongoContext;

    public SearchController(MongoContext mongoContext)
    {
        _mongoContext = mongoContext;
    }

    public IActionResult Index(Search search)
    {
        try
        {
            var eventsCollection = _mongoContext.GetCollection<Event>("events");

            var filterBuilder = Builders<Event>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrEmpty(search.Name))
            {
                filter &= filterBuilder.Regex("EventName", new BsonRegularExpression(search.Name, "i"));
            }

            if (!string.IsNullOrEmpty(search.Place))
            {
                var placesCollection = _mongoContext.GetCollection<Place>("places");
                var place =  placesCollection.Find(p => p.ActualPlace == search.Place).FirstOrDefault();

                if (place != null)
                {
                    filter &= filterBuilder.Eq("PlaceId", place.Id);
                }
                else
                {
                    return View("events", new List<Event>());
                }
            }

            if (!string.IsNullOrEmpty(search.DateChoice))
            {
                DateTime currentDate = DateTime.UtcNow.Date;
                switch (search.DateChoice.ToLower())
                {
                    case "today":
                        filter &= filterBuilder.Gte("EndDate", currentDate) & filterBuilder.Lt("EndDate", currentDate.AddDays(1));
                        break;
                    case "this week":
                        filter &= filterBuilder.Gte("EndDate", currentDate) & filterBuilder.Lt("EndDate", currentDate.AddDays(7));
                        break;
                    case "next week":
                        filter &= filterBuilder.Gte("EndDate", currentDate.AddDays(7)) & filterBuilder.Lt("EndDate", currentDate.AddDays(14));
                        break;
                }
            }

            if (!string.IsNullOrEmpty(search.Type))
            {
                filter &= filterBuilder.Eq("EventType", search.Type);
            }

            if (!string.IsNullOrEmpty(search.Category))
            {
                var tag =  _mongoContext.GetCollection<Category>("tags").Find(t => t.TagName == search.Category).FirstOrDefault();
                if (tag != null)
                {
                    filter &= filterBuilder.Eq("TagId", tag.Id);
                }
            }

            var events =  eventsCollection.Find(filter).ToList();

            return View(events);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
