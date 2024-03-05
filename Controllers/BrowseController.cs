using Microsoft.AspNetCore.Mvc;
// using WebApp.Models;

namespace WebApp.Controllers
{
    public class BrowseController : Controller
    {
        public IActionResult Index()
        {

            return View(events);
        }
        public IActionResult Cancelled()
        {

            private readonly MongoContext _mongoContext;

            // var events = _mongoContext.GetCollection<Event>("events").Find(ev => true).ToList(); เเก้เป็น เอา event ที่เเคนเซิลเเล้ว
            return View();
        }
        public IActionResult Ended()
        {
            private readonly MongoContext _mongoContext;

            // var events = _mongoContext.GetCollection<Event>("events").Find(ev => true).ToList(); เเก้เป็น เอา event ที่จบไปเเล้ว

            return View();
        }

    }
}