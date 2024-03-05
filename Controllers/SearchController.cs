using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Random()
        {
            var movie = new Search() {Name = "hello", Id=1};
            
            var customers = new List<Customer>
            {
                new Customer { Name = "Customer 1" }
                new Customer { Name = "Customer 1" }
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            }
            // return View(movie);
            // return Content("Hello world!");
            // return new EmptyResult();
            return RedirectToAction("Index", "Search", new {hello=1 , soy=1});
        }
    }
}