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

    private new readonly MongoContext _Context2;

    public HomeController(ILogger<HomeController> logger, MongoContext mongoContext) : base(mongoContext)
    {
        _logger = logger;
        _mongoContext = mongoContext;
        _Context2 = mongoContext;
    }


    public IActionResult Index(PresentCondition presentCondition)
    {
        _SetUserDataInViewData();
        DateTime dateTimeNow = DateTime.Now;
        var Events = _Context2.GetCollection<JoinEvent>("joinEvents").Find(j => j.EventId == ObjectId.Parse("65e7f50923f62e18cc7dcc24")).ToList();
        foreach (var e in Events){
        Console.WriteLine("HIHIHIHIHIH");
        Console.WriteLine(e.JoinDate);
        }
        var events = _mongoContext.GetCollection<Event>("events");
        var ongoingcheck = events.Find(e => true).ToList();


        List<WebApp.Models.Event> names = new List<WebApp.Models.Event>();


        foreach (var e in ongoingcheck){
            if ((DateTime.Compare(e.EndDate, dateTimeNow )> 0) & (e.CurrentMember < e.MaxMember) ){
                names.Add(e);
            }

        }
        return View(names);
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

