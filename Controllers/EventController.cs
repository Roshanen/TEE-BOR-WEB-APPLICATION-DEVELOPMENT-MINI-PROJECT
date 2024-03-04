using Microsoft.AspNetCore.Mvc;
using Event.Models;

namespace WebApp.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [ActionName("Create")]
        public IActionResult CreateGet()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public IActionResult CreatePost()
        {
            try
            {
                EventModel evModel = new EventModel();
                // UpdateModel(evModel);

                ViewData["Name"] = evModel.Name;
                ViewData["Place"] = evModel.Place;

                return View("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        [ActionName("Edit")]
        public IActionResult EditGet()
        {
            return View();
        }

        [HttpPut]
        [ActionName("Edit")]
        public IActionResult EditPut()
        {
            // try
            // {
            //     PartyModel ptmodel = new PartyModel();
            //     UpdateModel(ptmodel);

            //     ViewData["Name"] = ptmodel.Name;
            //     ViewData["Place"] = ptmodel.Place;

            //     return View("Index");
            // }
            // catch
            {
                return View();
            }
        }
    }
}