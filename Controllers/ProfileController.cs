using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApp.Models;

namespace WebApp.Controllers;

public class ProfileController : BaseController
{
    private new readonly MongoContext _mongoContext;

    public ProfileController(MongoContext mongoContext)
        : base(mongoContext)
    {
        _mongoContext = mongoContext;
    }

    public IActionResult Debug()
    {
        _SetUserDataInViewData();
        var users = _mongoContext.GetCollection<User>("users").Find(ev => true).ToList();

        return View(users);
    }

    public IActionResult ViewId(string Id)
    {
        var currentId = _SetUserDataInViewData();

        ViewData["isProfileOwner"] = currentId == Id ? "true" : null;

        if (currentId == null){
            return RedirectToAction("login", "account");
        }

        var user = _mongoContext
            .GetCollection<User>("users")
            .Find(u => u.Id == ObjectId.Parse(Id))
            .FirstOrDefault();
        if (user == null)
        {
            return NotFound();
        }

        Profile profile = new Profile();
        profile.User = user;

        var joins = _mongoContext
            .GetCollection<JoinEvent>("joinEvents")
            .Find(j => j.UserId == ObjectId.Parse(Id))
            .ToList();
        List<Event> events = new List<Event>();

        foreach (var join in joins){
            events.Add(_mongoContext
            .GetCollection<Event>("events")
            .Find(e => e.Id == join.EventId)
            .FirstOrDefault());
        }

        profile.AttendEvent = events;
        return View(profile);
    }

    public async Task<ActionResult> Edit(string id)
    {
        var objectId = ObjectId.Parse(id);
        var currentId = _SetUserDataInViewData();

        if (currentId == null || objectId != ObjectId.Parse(currentId)){
            return RedirectToAction("login", "account");
        }

        var userProfile = await _mongoContext
            .GetCollection<User>("users")
            .Find(u => u.Id == objectId)
            .FirstOrDefaultAsync();
        
        if (userProfile == null)
        {
            return NotFound();
        }

        return View(userProfile);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(User model)
    {
        var currentId = _SetUserDataInViewData();

        if (currentId == null || model.Id != ObjectId.Parse(currentId)){
            return RedirectToAction("login", "account");
        }

        var filter = Builders<User>.Filter.Eq(u => u.Id, model.Id);
        var updateBuilder = Builders<User>.Update;
        var update = updateBuilder
            .Set(u => u.UserName, model.UserName)
            .Set(u => u.ProfilePicture, model.ProfilePicture)
            .Set(u => u.Address, model.Address)
            .Set(u => u.Bio, model.Bio)
            .Set(u => u.Contact, model.Contact);

        if (model.UserName != "" && model.UserName != null)
        {
            update = update.Set(u => u.UserName, model.UserName);
        }
        if (model.ProfilePicture != "" && model.ProfilePicture != null)
        {
            update = update.Set(u => u.ProfilePicture, model.ProfilePicture);
        }
        if (model.Address != "" && model.Address != null)
        {
            update = update.Set(u => u.Address, model.Address);
        }
        if (model.Bio != "" && model.Bio != null)
        {
            update = update.Set(u => u.Bio, model.Bio);
        }
        if (model.Contact != "" && model.Contact != null)
        {
            update = update.Set(u => u.Contact, model.Contact);
        }

        await _mongoContext.GetCollection<User>("users").UpdateOneAsync(filter, update);

        return RedirectToAction("viewid", "profile", new { Id = model.Id });
    }
}
