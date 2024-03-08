using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApp.Models;

namespace WebApp.Controllers;

public class EventPageController : BaseController
{
    private new readonly MongoContext _mongoContext;

    public EventPageController(MongoContext mongoContext) : base(mongoContext)
    {
        _mongoContext = mongoContext;
    }

    public IActionResult ViewId(string id)
    {
        var Event = _mongoContext.GetCollection<Event>("events").Find(ev => ev.Id == ObjectId.Parse(id)).FirstOrDefault();
        var Host = _mongoContext.GetCollection<User>("users").Find(u => u.Id == (Event.HostId)).FirstOrDefault();
        var Category = _mongoContext.GetCollection<Category>("tags").Find(t => t.Id == (Event.CategoryId)).FirstOrDefault();
        var Place = _mongoContext.GetCollection<Place>("places").Find(p => p.Id == (Event.PlaceId)).FirstOrDefault();

        var Joins = _mongoContext.GetCollection<JoinEvent>("joinEvents").Find(j => j.EventId == ObjectId.Parse(id)).ToList();
        var Attendees = new List<object>();
        var debug = new List<dynamic>();
        
        foreach (var join in Joins)
        {
            var person = _mongoContext.GetCollection<User>("users").Find(u => u.Id == (join.UserId)).FirstOrDefault();
            if (person == Host)
            {
                ViewData["host-friend"] = join.BringFriends;
            }
            else
            {
                var friend = join.BringFriends;
                Attendee attendee = new Attendee();
                attendee.user = person;
                attendee.friend = friend;
                Attendees.Add(attendee);
            }
        }

        Console.WriteLine(debug);
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
        ViewBag.EventId = id;
        ViewBag.MaxCapacity = Event.MaxMember - Event.CurrentMember - 1;
        eventView.StartDate = Event.StartDate;

        DateTime dateTimeNow = DateTime.Now;
        if (DateTime.Compare(Event.EndDate, dateTimeNow) < 0)
        {
            eventView.Status = "ended";
        }
        else if (Event.CurrentMember >= Event.MaxMember)
        {
            eventView.Status = "full";
        }
        else
        {
            eventView.Status = "available";
        }

        var userId = JwtHelper.GetUserIdFromToken(HttpContext.Session.GetString("JwtToken")!);
        ViewData["userID"] = userId;
        if (userId != null)
        {
            var userName = _mongoContext.GetCollection<User>("users").Find(u => u.Id == ObjectId.Parse(userId)).FirstOrDefault();
            ViewData["userName"] = userName?.UserName;
            ViewData["userProfile"] = userName?.ProfilePicture;
            var existingJoinEvent = _mongoContext.GetCollection<JoinEvent>("joinEvents").Find(je => je.UserId == userName.Id && je.EventId == Event.Id).FirstOrDefault();
            
            if (existingJoinEvent != null)
            {
                eventView.Status = "available";
                ViewBag.IsAttending = true;
            }
            else
            {
                ViewBag.IsAttending = false;
            }

        }
        else
        {
            ViewBag.IsAttending = false;
        }

        return View(eventView);
    }

    [HttpPost]
    public IActionResult Attend(string userId, string eventId, int friend)
    {
        try
        {
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var userIdObj = ObjectId.Parse(userId);
            var eventIdObj = ObjectId.Parse(eventId);

            // Check if user and event exist
            var user = _mongoContext.GetCollection<User>("users").Find(u => u.Id == userIdObj).FirstOrDefault();
            var ev = _mongoContext.GetCollection<Event>("events").Find(e => e.Id == eventIdObj).FirstOrDefault();

            if (user == null || ev == null)
            {
                return NotFound("User or event not found.");
            }

            // Check if the user is already joined to the event
            var existingJoinEvent = _mongoContext.GetCollection<JoinEvent>("joinEvents").Find(je => je.UserId == userIdObj && je.EventId == eventIdObj).FirstOrDefault();
            if (existingJoinEvent != null)
            {
                return BadRequest("User is already joined to the event.");
            }

            // Create JoinEvent document
            JoinEvent joinEvent = new JoinEvent();
            joinEvent.UserId = userIdObj;
            joinEvent.EventId = eventIdObj;
            joinEvent.BringFriends = friend;
            joinEvent.JoinDate = DateTime.Now;

            // Insert JoinEvent document
            _mongoContext.GetCollection<JoinEvent>("joinEvents").InsertOne(joinEvent);
            _mongoContext.GetCollection<Event>("events").UpdateOne(e => e.Id == eventIdObj, Builders<Event>.Update.Inc(e => e.CurrentMember, friend + 1));
            return RedirectToAction("ViewId", new { id = eventId });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Cancel(string userId, string eventId)
    {
        try
        {
            var userIdObj = ObjectId.Parse(userId);
            var eventIdObj = ObjectId.Parse(eventId);

            // Check if user and event exist
            var user = await _mongoContext.GetCollection<User>("users").Find(u => u.Id == userIdObj).FirstOrDefaultAsync();
            var ev = await _mongoContext.GetCollection<Event>("events").Find(e => e.Id == eventIdObj).FirstOrDefaultAsync();

            if (user == null || ev == null)
            {
                return NotFound("User or event not found.");
            }

            // Check if the user is already joined to the event
            var existingJoinEvent = await _mongoContext.GetCollection<JoinEvent>("joinEvents").Find(je => je.UserId == userIdObj && je.EventId == eventIdObj).FirstOrDefaultAsync();

            // Remove JoinEvent document from the collection
            await _mongoContext.GetCollection<JoinEvent>("joinEvents").DeleteOneAsync(je => je.Id == existingJoinEvent.Id);
            await _mongoContext.GetCollection<Event>("events").UpdateOneAsync(e => e.Id == eventIdObj, Builders<Event>.Update.Inc(e => e.CurrentMember, -(existingJoinEvent.BringFriends + 1)));
            return RedirectToAction("ViewId", new { id = eventId });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString()); // Log the exception details
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}