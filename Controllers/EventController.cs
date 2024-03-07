using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApp.Models;

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
    public IActionResult Create(Event Event)
    {
        _mongoContext.GetCollection<Event>("events").InsertOne(Event);
        return RedirectToAction("Index");
    }

    // public IActionResult Edit(string id)
    // {
    //     var Event = _mongoContext.GetCollection<Event>("events").Find(ev => ev.Id == ObjectId.Parse(id)).FirstOrDefault();
    //     return View(Event);
    // }

    // [HttpPost]
    // public IActionResult Edit(string id, Event updatedEvent)
    // {
    //     var filter = Builders<Event>.Filter.Eq("_id", ObjectId.Parse(id));
    //     var update = Builders<Event>.Update
    //         .Set("Name", updatedEvent.Name)
    //         .Set("Place", updatedEvent.Place);

    //     _mongoContext.GetCollection<Event>("events").UpdateOne(filter, update);
    //     return RedirectToAction("Index");
    // }

}
