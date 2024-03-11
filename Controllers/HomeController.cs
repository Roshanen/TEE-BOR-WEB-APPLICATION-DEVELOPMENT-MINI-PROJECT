using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApp.Models;

namespace WebApp.Controllers;

public class HomeController : BaseController
{
    private readonly ILogger<HomeController> _logger;
    private new readonly MongoContext _mongoContext;

    public HomeController(ILogger<HomeController> logger, MongoContext mongoContext)
        : base(mongoContext)
    {
        _logger = logger;
        _mongoContext = mongoContext;
    }

    public IActionResult Index(PresentCondition presentCondition)
    {
        _SetUserDataInViewData();
<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> 3a7c3b96dd91b6ebf25ffb82a00ab704c3b8a182
        var Events = _mongoContext
            .GetCollection<Event>("events")
            .Find(j => j.EndDate > DateTime.Now)
            .ToList();
            List<EventViewModel> listEventview = new List<EventViewModel>();
<<<<<<< HEAD

        foreach (var e in Events)
        {
            var Host = _mongoContext
                .GetCollection<User>("users")
                .Find(u => u.Id == e.HostId)
                .FirstOrDefault();
=======
        var Events = _mongoContext.GetCollection<Event>("events").Find(j => j.EndDate <= DateTime.Now).ToList();
        List<EventViewModel> listEventview = new List<EventViewModel>();


        foreach (var e in Events){
            var Host = _mongoContext.GetCollection<User>("users").Find(u => u.Id == e.HostId).FirstOrDefault();
>>>>>>> bb3b121d13b8ca4f243fd93876cc7e7e1c137168
=======

        foreach (var e in Events)
        {
            var Host = _mongoContext
                .GetCollection<User>("users")
                .Find(u => u.Id == e.HostId)
                .FirstOrDefault();
>>>>>>> 3a7c3b96dd91b6ebf25ffb82a00ab704c3b8a182
            EventViewModel eventView = new EventViewModel();
            eventView.EventName = e.EventName;
            eventView.HostName = Host.UserName;
            eventView.EventImg = e.EventImg;
            eventView.Tags = e.Id.ToString();
            eventView.EndDate = e.EndDate;
            listEventview.Add(eventView);
        }

        return View(listEventview);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(
            new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }
        );
    }
}
