using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}