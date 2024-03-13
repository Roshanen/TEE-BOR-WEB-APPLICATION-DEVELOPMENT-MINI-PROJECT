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
        var Events = _mongoContext
            .GetCollection<Event>("events")
            .Find(j => j.RecruitEndDate > DateTime.Now | true)
            .ToList();
        List<EventViewModel> listEventview = new List<EventViewModel>();

        foreach (var e in Events)
        {
            var Host = _mongoContext
                .GetCollection<User>("users")
                .Find(u => u.Id == e.HostId)
                .FirstOrDefault();
            EventViewModel eventView = new EventViewModel();
            eventView.EventName = e.EventName;
            eventView.HostName = Host.UserName;
            eventView.EventImg = e.EventImg;
            eventView.Tags = e.Id.ToString();
            eventView.EventEndDate = e.EventEndDate;
            eventView.EventDetails = e.EventDetails;

            listEventview.Add(eventView);
        }

        return View(listEventview);
    }

    public IActionResult Privacy()
    {
        _SetUserDataInViewData();
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
