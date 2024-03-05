using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApp.Models;

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
        var event = _mongoContext.GetCollection<Event>("events").Find(ev => ev.Id == ObjectId.Parse(id)).FirstOrDefault();
        return View(event);
    }

    [HttpPost]
    public IActionResult Edit(string id, Event updatedEvent)
    {
        var eventIdFilter = Builders<Event>.Filter.Eq(ev => ev.Id, ObjectId.Parse(id));
        var updateDefinition = Builders<Event>.Update
            .Set(ev => ev.Name, updatedEvent.Name)
            .Set(ev => ev.Place, updatedEvent.Place);

        _mongoContext.GetCollection<Event>("events").UpdateOne(filter, update);

        return RedirectToAction("Index");
    }
}
