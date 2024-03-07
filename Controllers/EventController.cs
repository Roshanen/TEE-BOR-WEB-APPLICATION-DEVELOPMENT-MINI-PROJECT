using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApp.Models;
using System;

namespace WebApp.Controllers;

public class EventController : Controller
{
    private readonly ILogger<EventController> _logger;
    private readonly MongoContext _mongoContext;

    public EventController(ILogger<EventController> logger, MongoContext mongoContext)
    {
        _logger = logger;
        _mongoContext = mongoContext;
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

        _mongoContext.GetCollection<Place>("places").InsertOne(placeModel);
        _mongoContext.GetCollection<Category>("tags").InsertOne(categoryModel);

        var userId = new ObjectId(JwtHelper.GetUserIdFromToken(HttpContext.Session.GetString("JwtToken")!));

        eventModel.HostId = userId;
        eventModel.PlaceId = placeModel.Id;
        eventModel.CategoryId = categoryModel.Id;

        _mongoContext.GetCollection<Event>("events").InsertOne(eventModel);

        return RedirectToAction("Index");
    }

    public IActionResult Edit(string id)
    {
        var Event = _mongoContext.GetCollection<Event>("events").Find(ev => ev.Id == ObjectId.Parse(id)).FirstOrDefault();
        CreateEvent createEvent = new CreateEvent();
        //Place
        var Place = _mongoContext.GetCollection<Place>("places").Find(p => p.Id == (Event.PlaceId)).FirstOrDefault();
        createEvent.ActualPlace = Place.ActualPlace;
        createEvent.Province = Place.Province;
        createEvent.District = Place.District;
        createEvent.SubDistrict = Place.SubDistrict;
        createEvent.MapUrl = Place.MapUrl;
        //Tag
        var Category = _mongoContext.GetCollection<Category>("tags").Find(t => t.Id == (Event.CategoryId)).FirstOrDefault();
        createEvent.CategoryName = Category.CategoryName;
        //DateTime
        createEvent.StartDate = Event.StartDate;
        createEvent.EndDate = Event.EndDate;
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
