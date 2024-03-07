using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApp.Models;
using System;

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
    public IActionResult Create(CreateEvent createEvent)
    {
        Event eventModel = new Event();
        Place placeModel = new Place();
        Category categoryModel = new Category();
        User userModel = new User(); // how to get current user

        float defaultRating = 5.0f;
        eventModel.CurrentMember = 1;

        // can be shared between edit and create
        DateTime today = DateTime.Today;
        eventModel.StartDate = createEvent.StartDate;
        eventModel.EndDate = createEvent.EndDate;
        eventModel.LastModifiedDate = today;
        eventModel.EventName = createEvent.EventName;
        eventModel.EventImg = createEvent.EventImg;
        eventModel.EventDetails = createEvent.EventDetails;
        eventModel.MaxMember = createEvent.MaxMember;
        eventModel.Rating = defaultRating;

        placeModel.MapUrl = createEvent.MapUrl;
        placeModel.ActualPlace = createEvent.ActualPlace;
        placeModel.Province = createEvent.Province;
        placeModel.District = createEvent.District;
        placeModel.SubDistrict = createEvent.SubDistrict;

        categoryModel.CategoryName = createEvent.CategoryName;
        // end of sharing

        _mongoContext.GetCollection<Place>("events").InsertOne(placeModel);
        _mongoContext.GetCollection<Category>("events").InsertOne(categoryModel);
        _mongoContext.GetCollection<Event>("events").InsertOne(eventModel);
        return RedirectToAction("Index");
    }

    public IActionResult Edit(string id)
    {
        var Event = _mongoContext.GetCollection<Event>("events").Find(ev => ev.Id == ObjectId.Parse(id)).FirstOrDefault();
        return View(Event);
    }

    [HttpPost]
    public IActionResult Edit(string id, CreateEvent createEvent)
    {
        // var filter = Builders<Event>.Filter.Eq("_id", ObjectId.Parse(id));
        // var update = Builders<Event>.Update
        //     .Set("Name", updatedEvent.EventName)
        //     .Set("Place", updatedEvent.Place);

        // _mongoContext.GetCollection<Event>("events").UpdateOne(filter, update);
        return RedirectToAction("Index");
    }

}
