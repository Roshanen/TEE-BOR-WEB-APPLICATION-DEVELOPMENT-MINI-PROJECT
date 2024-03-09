using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApp.Models;

namespace WebApp.Controllers;

public class YourEventController : BaseController
{
    private new readonly MongoContext _mongoContext;

    public YourEventController(MongoContext mongoContext) : base(mongoContext)
    {
        _mongoContext = mongoContext;
    }

    public IActionResult index()
    {
        var userId = _SetUserDataInViewData();
        if (userId is null) return RedirectToAction("login", "account");
        var Events = _mongoContext.GetCollection<JoinEvent>("joinEvents").Find(j => j.UserId == ObjectId.Parse(userId)).ToList();
        List<Event> hostevent = new List<Event>{}; 
        List<Event> pastevent = new List<Event>{}; 
        List<Event> attendevent = new List<Event>{};
        foreach (var e in Events)
        {
            var Event = _mongoContext.GetCollection<Event>("events").Find(ei => ei.Id == e.EventId).FirstOrDefault();
            if (Event != null)
            {
                if (Event.HostId == ObjectId.Parse(userId))
                {
                    hostevent.Add(Event);
                    continue;
                }
                if (Event.StartDate < DateTime.Now)
                {
                    pastevent.Add(Event);
                    continue;                   
                }
                attendevent.Add(Event);
            }
        }

        var events = new YourEventViewModel
        {
            HostEvent = hostevent,
            PastEvent = pastevent,
            AttendEvent = attendevent
        };
        return View(events);
    }
}