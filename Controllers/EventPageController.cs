using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApp.Models;

public class EventPageController : Controller
{
    private readonly MongoContext _mongoContext;

    public EventPageController(MongoContext mongoContext)
    {
        _mongoContext = mongoContext;
    }

    public IActionResult Index(string id)
    {
        Console.WriteLine("id: " + id);
        var Event = _mongoContext.GetCollection<Event>("events").Find(ev => ev.Id == ObjectId.Parse(id)).FirstOrDefault();
        //var User = _mongoContext.GetCollection<User>("users").Find(u => u.Id == ObjectId.Parse(id)).FirstOrDefault();
        if (true)
        {
            ViewBag.IsAttending = false;
        }
        else
        {
            ViewBag.IsAttending = true;
        }
        return View(Event);
    }

    // [HttpPost]
    // public IActionResult Attend(Event Event)
    // {
    //     var Event = _mongoContext.GetCollection<Event>("events").Find(ev => ev.Id == ObjectId.Parse(id)).FirstOrDefault();
    //     return RedirectToAction("Index");
    // }

    // [HttpPost]
    // public IActionResult Unattend(Event Event)
    // {
    //     var Event = _mongoContext.GetCollection<Event>("events").Find(ev => ev.Id == ObjectId.Parse(id)).FirstOrDefault();
    //     return RedirectToAction("Index");
    // }
}