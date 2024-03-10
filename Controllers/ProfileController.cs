using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace WebApp.Controllers;

public class ProfileController : BaseController
{
    private new readonly MongoContext _mongoContext;

    public ProfileController(MongoContext mongoContext) : base(mongoContext)
    {
        _mongoContext = mongoContext;
    }

    public async Task<ActionResult> Index()
    {

        var userProfile = await _mongoContext.GetCollection<User>("users")
                                         .Find(u => true)
                                         .FirstOrDefaultAsync();
        if (userProfile == null)
        {
            return NotFound();
        }

        return View(userProfile);
    }

    public async Task<ActionResult> ViewId(ObjectId id)
    {
        var currentId = _SetUserDataInViewData();

        var userProfile = await _mongoContext.GetCollection<User>("users")
                                         .Find(u => u.Id == id)
                                         .FirstOrDefaultAsync();
        if (userProfile == null)
        {
            return NotFound();
        }

        return View(userProfile);
    }

    public async Task<ActionResult> Edit(string id)
    {
        var userProfile = await _mongoContext.GetCollection<UserProfile>("UserProfiles")
                                         .Find(u => u.Id == id)
                                         .FirstOrDefaultAsync();
        if (userProfile == null)
        {
            return NotFound();
        }

        return View(userProfile);
    }

    // POST: Profile/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(UserProfile model)
    {
        if (ModelState.IsValid)
        {
            var update = Builders<UserProfile>.Update
                .Set(u => u.UserName, model.UserName)
                .Set(u => u.Email, model.Email)
                .Set(u => u.ProfilePicture, model.ProfilePicture)
                .Set(u => u.Address, model.Address)
                .Set(u => u.Bio, model.Bio)
                .Set(u => u.JoinDate, model.JoinDate);

            await _mongoContext.GetCollection<UserProfile>("UserProfiles")
                          .UpdateOneAsync(u => u.Id == model.Id, update);
            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }
}

