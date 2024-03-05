using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login model)
        {
            if (ModelState.IsValid)
            {
                // Your authentication logic here
                // For example, check if the provided credentials are valid
                // If valid, redirect the user to the dashboard or home page
                // Otherwise, return an error message
            }

             // If the model state is invalid, return the view with validation errors
            return View(model);
        }

        // GET: /Account/Register
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Signup model)
        {
            if (ModelState.IsValid)
            {
                // Your registration logic here
                // For example, create a new user account with the provided information
                // Redirect the user to the login page after successful registration
            }

           // If the model state is invalid, return the view with validation errors
            return View(model);
         }
    }
}