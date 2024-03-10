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
            string userIdString = JwtHelper.GetUserIdFromToken(HttpContext.Session.GetString("JwtToken")!);

            if (userIdString == null) return RedirectToAction("login", "account");

            var userId = new ObjectId(userIdString);

            var userProfile = await _context.GetCollection<User>("users")
                                             .Find(u => u.Id == userId)
                                             .FirstOrDefaultAsync();

            return View(userProfile);
        }


        public async Task<ActionResult> Edit(string id)
        {
            var objectId = ObjectId.Parse(id);
            var userProfile = await _context.GetCollection<User>("users")
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
            var filter = Builders<User>.Filter.Eq(u => u.Id, model.Id);
            var updateBuilder = Builders<User>.Update;
            var update = updateBuilder.Set(u => u.UserName, model.UserName)
                                       .Set(u => u.ProfilePicture, model.ProfilePicture)
                                       .Set(u => u.Address, model.Address)
                                       .Set(u => u.Bio, model.Bio)
                                       .Set(u => u.Contact, model.Contact);

            if (model.UserName != null) update = update.Set(u => u.UserName, model.UserName);
            if (model.ProfilePicture != null) update = update.Set(u => u.ProfilePicture, model.ProfilePicture);
            if (model.Address != null) update = update.Set(u => u.Address, model.Address);
            if (model.Bio != null) update = update.Set(u => u.Bio, model.Bio);
            if (model.Contact != null) update = update.Set(u => u.Contact, model.Contact);

            await _context.GetCollection<User>("users").UpdateOneAsync(filter, update);

            return RedirectToAction("Index");
        }

    }
}
