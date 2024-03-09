using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics;
using WebApp.Models;

namespace WebApp.Controllers;

public class HomeController : BaseController
{
    private readonly ILogger<HomeController> _logger;
    private new readonly MongoContext _mongoContext;

    public HomeController(ILogger<HomeController> logger, MongoContext mongoContext) : base(mongoContext)
    {
        _logger = logger;
        _mongoContext = mongoContext;
    }


    public IActionResult Index(PresentCondition presentCondition)
    {
        _SetUserDataInViewData();
        DateTime dateTimeNow = DateTime.Now;
        var events = _mongoContext.GetCollection<Event>("events").Find(e => true).ToList();
        List<WebApp.Models.Event> activeEvents = new List<WebApp.Models.Event>();
        foreach (var e in events){
            if ((DateTime.Compare(e.EndDate, dateTimeNow )> 0) & (e.CurrentMember < e.MaxMember) ){
                activeEvents.Add(e);
            }
        }


        return View(activeEvents);
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

