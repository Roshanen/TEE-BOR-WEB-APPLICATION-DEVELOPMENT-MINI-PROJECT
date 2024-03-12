using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApp.Models;

namespace WebApp.Controllers;

public class YourEventController : BaseController
{
    private new readonly MongoContext _mongoContext;

    public YourEventController(MongoContext mongoContext)
        : base(mongoContext)
    {
        _mongoContext = mongoContext;
    }

    public IActionResult index()
    {
        var userId = _SetUserDataInViewData();
        if (userId is null)
        {
            return RedirectToAction("login", "account");
        }

        var Events = _mongoContext
            .GetCollection<JoinEvent>("joinEvents")
            .Find(j => j.UserId == ObjectId.Parse(userId))
            .ToList();
        List<Event> hostevent = new List<Event> { };
        List<Event> pastevent = new List<Event> { };
        List<Event> attendevent = new List<Event> { };
        foreach (var e in Events)
        {
            var Event = _mongoContext
                .GetCollection<Event>("events")
                .Find(ei => ei.Id == e.EventId && ei.Status == true)
                .FirstOrDefault();
            if (Event != null)
            {
                if (Event.EndDate < DateTime.Now)
                {
                    pastevent.Add(Event);
                    continue;
                }
                if (Event.HostId == ObjectId.Parse(userId))
                {
                    hostevent.Add(Event);
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

    [HttpPost]
    public async Task<IActionResult> Cancel(string eventId)
    {
        try
        {
            var eventIdObj = ObjectId.Parse(eventId);
            var ev = await _mongoContext
                .GetCollection<Event>("events")
                .Find(e => e.Id == eventIdObj)
                .FirstOrDefaultAsync();

            if (ev == null)
            {
                return NotFound("User or event not found.");
            }

            // Check if the user is already joined to the event
            // Remove JoinEvent document from the collection
            await _mongoContext
                .GetCollection<Event>("events")
                .UpdateOneAsync(
                    e => e.Id == eventIdObj,
                    Builders<Event>.Update.Set(e => e.Status, false));

            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString()); // Log the exception details
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
