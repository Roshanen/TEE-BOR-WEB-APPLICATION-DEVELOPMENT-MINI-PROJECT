using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index(Search search)
        {
            Console.WriteLine("params", search.EventName, search.EventPlace);
            return View();
        }
    }
}