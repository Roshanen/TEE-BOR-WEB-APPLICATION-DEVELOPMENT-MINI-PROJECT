using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApp.Models;

namespace WebApp.Controllers;

public class EventController : Controller
{
    private readonly MongoContext _mongoContext;

    public EventController(MongoContext mongoContext)
    {
        _mongoContext = mongoContext;
    }

    public IActionResult Index()
    {
        var events = _mongoContext.GetCollection<Event>("events").Find(ev => true).ToList();
        return View(events);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Event Event)
    {
        _mongoContext.GetCollection<Event>("events").InsertOne(Event);
        return RedirectToAction("Index");
    }

    public IActionResult Edit(string id)
    {
        var Event = _mongoContext.GetCollection<Event>("events").Find(ev => ev.Id == ObjectId.Parse(id)).FirstOrDefault();
        CreateEvent createEvent = new CreateEvent();
        //Place
        var Place = _mongoContext.GetCollection<Place>("places").Find(p => p.Id == (Event.PlaceId)).FirstOrDefault();
        createEvent.Place = Place.ActualPlace;
        createEvent.Province = Place.Province;
        createEvent.District = Place.District;
        createEvent.SubDistrict = Place.SubDistrict;
        createEvent.MapUrl = Place.MapUrl;
        //Tag
        var Category = _mongoContext.GetCollection<Category>("tags").Find(t => t.Id == (Event.TagId)).FirstOrDefault();
        createEvent.Tag = Category.CategoryName;
        //DateTime
        createEvent.StartDate = Event.StartDate.ToString();
        createEvent.EndDate = Event.EndDate.ToString();
        //Event
        createEvent.EventName = Event.EventName;
        createEvent.EventImg = Event.EventImg;
        createEvent.EventDetails = Event.EventDetails;
        //MemberCount
        createEvent.MaxMember = Event.MaxMember;
        return View(createEvent);
    }

    [HttpPost]
    public IActionResult Edit(string id, Event updatedEvent)
    {
        _mongoContext.GetCollection<Event>("events").ReplaceOne(ev => ev.Id == ObjectId.Parse(id), updatedEvent);
        return RedirectToAction("Index");
    }

}
