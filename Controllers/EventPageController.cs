using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApp.Models;

namespace WebApp.Controllers;

public class EventPageController : Controller
{
    private readonly MongoContext _mongoContext;

    public EventPageController(MongoContext mongoContext)
    {
        _mongoContext = mongoContext;
    }

    public IActionResult ViewId(string id)
    {

        var Event = _mongoContext.GetCollection<Event>("events").Find(ev => ev.Id == ObjectId.Parse(id)).FirstOrDefault();
        var Host = _mongoContext.GetCollection<User>("users").Find(u => u.Id == (Event.HostId)).FirstOrDefault();
        var Category = _mongoContext.GetCollection<Category>("tags").Find(t => t.Id == (Event.CategoryId)).FirstOrDefault();
        var Place = _mongoContext.GetCollection<Place>("places").Find(p => p.Id == (Event.PlaceId)).FirstOrDefault();

        var Joins = _mongoContext.GetCollection<JoinEvent>("joinEvents").Find(j => j.Id == ObjectId.Parse(id)).ToList();
        var Attendees = new List<User>();
        foreach (var join in Joins)
        {
            var attendee = _mongoContext.GetCollection<User>("users").Find(u => u.Id == (join.UserId)).FirstOrDefault();
            Attendees.Add(attendee);
        }

        // EventViewModel eventView = new EventViewModel(Event.EventName, Host.UserName, 
        // Host.ProfilePicture, Event.EventImg, Event.EventDetails, Category.TagName, 
        // Attendees, Event.EndDate, Place.ActualPlace, Place.MapUrl);

        EventViewModel eventView = new EventViewModel();

        eventView.EventName = Event.EventName;
        eventView.HostImg = Host.ProfilePicture;
        eventView.HostName = Host.UserName;
        eventView.EventImg = Event.EventImg;
        eventView.EventDetails = Event.EventDetails;
        eventView.Tags = Category.CategoryName;
        eventView.Attendees = Attendees;
        eventView.Time = Event.EndDate;
        eventView.Place = Place.ActualPlace;
        eventView.MapUrl = Place.MapUrl;

        if (true)
        {
            ViewBag.IsAttending = false;
        }
        else
        {
            ViewBag.IsAttending = true;
        }

        return View(eventView);
    }

    // [HttpPost]
    // public IActionResult Attend(Event Event)
    // {
    //     var Event = _mongoContext.GetCollection<Event>("events").Find(ev => ev.Id == ObjectId.Parse(id)).FirstOrDefault();
    //     return RedirectToAction("Index");
    // }

    [HttpPost]
    public IActionResult Cancle()
    {
        // var Event = _mongoContext.GetCollection<Event>("events").Find(ev => ev.Id == ObjectId.Parse(id)).FirstOrDefault();
        return View();
    }
}