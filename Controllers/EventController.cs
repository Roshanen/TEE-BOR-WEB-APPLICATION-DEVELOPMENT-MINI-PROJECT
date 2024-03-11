using System;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApp.Models;

namespace WebApp.Controllers;

public class EventController : BaseController
{
    private readonly ILogger<EventController> _logger;
    private new readonly MongoContext _mongoContext;

    public EventController(ILogger<EventController> logger, MongoContext mongoContext)
        : base(mongoContext)
    {
        _logger = logger;
        _mongoContext = mongoContext;
    }

    public IActionResult Index()
    {
        _SetUserDataInViewData();
        var events = _mongoContext.GetCollection<Event>("events").Find(ev => true).ToList();

        return View(events);
    }

    public IActionResult Create()
    {
        String userIdString = _SetUserDataInViewData();

        if (userIdString is null)
        {
            return RedirectToAction("login", "account");
        }

        return View();
    }

    [HttpPost]
    public IActionResult Create(CreateEvent createEvent)
    {
        String userIdString = _SetUserDataInViewData();

        if (userIdString is null)
        {
            return RedirectToAction("login", "account");
        }

        var userId = new ObjectId(userIdString);

        Event eventModel = new Event();
        Place placeModel = new Place();

        float defaultRating = 0.0f;
        eventModel.CurrentMember = 1;

        // can be shared between edit and create
        DateTime today = DateTime.Today;
        eventModel.StartDate = createEvent.StartDate;
        eventModel.EndDate = createEvent.EndDate;
        eventModel.LastModifiedDate = today;
        eventModel.EventName = createEvent.EventName;
        eventModel.EventImg = createEvent.EventImg;
        eventModel.EventDetails = createEvent?.EventDetails.ToString();
        eventModel.MaxMember = createEvent.MaxMember;
        eventModel.Rating = defaultRating;

        placeModel.MapUrl = createEvent.MapUrl;
        placeModel.ActualPlace = createEvent.ActualPlace;
        placeModel.Province = createEvent.Province;
        placeModel.District = createEvent.District;
        placeModel.SubDistrict = createEvent.SubDistrict;

        eventModel.CategoryName = createEvent.CategoryName;
        // end of sharing

        _mongoContext.GetCollection<Place>("places").InsertOne(placeModel);

        eventModel.HostId = userId;
        eventModel.PlaceId = placeModel.Id;

        _mongoContext.GetCollection<Event>("events").InsertOne(eventModel);
        _mongoContext
            .GetCollection<JoinEvent>("joinEvents")
            .InsertOne(
                new JoinEvent
                {
                    UserId = userId,
                    EventId = eventModel.Id,
                    JoinDate = DateTime.Now,
                    BringFriends = 0
                }
            );

        return RedirectToAction("Index");
    }

    public IActionResult Edit(string id)
    {
        _SetUserDataInViewData();
        // Handle user not login
        String userIdString = _SetUserDataInViewData();
        if (userIdString is null)
        {
            return RedirectToAction("login", "account");
        }
        // Handle user not the owner of event
        var Event = _mongoContext
            .GetCollection<Event>("events")
            .Find(ev => ev.Id == ObjectId.Parse(id))
            .FirstOrDefault();
        var userId = new ObjectId(userIdString);
        if (Event.HostId != userId)
        {
            return RedirectToAction("login", "account");
        }

        CreateEvent createEvent = new CreateEvent();
        //Place
        var Place = _mongoContext
            .GetCollection<Place>("places")
            .Find(p => p.Id == (Event.PlaceId))
            .FirstOrDefault();
        createEvent.ActualPlace = Place.ActualPlace;
        createEvent.Province = Place.Province;
        createEvent.District = Place.District;
        createEvent.SubDistrict = Place.SubDistrict;
        createEvent.MapUrl = Place.MapUrl;
        //Tag
        createEvent.CategoryName = Event.CategoryName;
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
    public IActionResult Edit(string id, CreateEvent createEvent)
    {
        String userIdString = _SetUserDataInViewData();
        if (userIdString is null)
        {
            return RedirectToAction("login", "account");
        }
        var Event = _mongoContext
            .GetCollection<Event>("events")
            .Find(ev => ev.Id == ObjectId.Parse(id))
            .FirstOrDefault();
        
        var userId = new ObjectId(userIdString);
        if (Event.HostId != userId)
        {
            return RedirectToAction("login", "account");
        }

        var eventModel = _mongoContext
            .GetCollection<Event>("events")
            .Find(ev => ev.Id == ObjectId.Parse(id))
            .FirstOrDefault();
        var placeModel = _mongoContext
            .GetCollection<Place>("places")
            .Find(p => p.Id == eventModel.PlaceId)
            .FirstOrDefault();

        DateTime today = DateTime.Today;
        eventModel.StartDate = createEvent.StartDate;
        eventModel.EndDate = createEvent.EndDate;
        eventModel.LastModifiedDate = today;
        eventModel.EventName = createEvent.EventName;
        eventModel.EventImg = createEvent.EventImg;
        eventModel.EventDetails = createEvent.EventDetails;
        eventModel.MaxMember = createEvent.MaxMember;
        eventModel.CategoryName = createEvent.CategoryName;

        placeModel.MapUrl = createEvent.MapUrl;
        placeModel.ActualPlace = createEvent.ActualPlace;
        placeModel.Province = createEvent.Province;
        placeModel.District = createEvent.District;
        placeModel.SubDistrict = createEvent.SubDistrict;

        _mongoContext
            .GetCollection<Event>("events")
            .ReplaceOne(ev => ev.Id == eventModel.Id, eventModel);
        _mongoContext
            .GetCollection<Place>("places")
            .ReplaceOne(ev => ev.Id == placeModel.Id, placeModel);

        return RedirectToAction("Index");
    }
}
