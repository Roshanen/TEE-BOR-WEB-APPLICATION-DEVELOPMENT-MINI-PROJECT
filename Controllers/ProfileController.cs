using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class ProfileController : Controller
    {
        private readonly MongoContext _context;

        public ProfileController(MongoContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Index()
        {

            var userProfile = await _context.GetCollection<User>("users")
                                             .Find(u => u.UserName == "MiaMartin")
                                             .FirstOrDefaultAsync();
            if (userProfile == null)
            {
                return NotFound();
            }

            return View(userProfile);
        }

        public async Task<ActionResult> Edit(string id)
        {
            var userProfile = await _context.GetCollection<UserProfile>("UserProfiles")
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

                await _context.GetCollection<UserProfile>("UserProfiles")
                              .UpdateOneAsync(u => u.Id == model.Id, update);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
    }
}
