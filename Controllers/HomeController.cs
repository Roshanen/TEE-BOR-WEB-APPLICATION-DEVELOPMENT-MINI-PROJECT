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
        var jwtToken = HttpContext.Session.GetString("JwtToken");
        var userId = JwtHelper.GetUserIdFromToken(jwtToken!);
        var connectionUri = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING");
        var settings = MongoClientSettings.FromConnectionString(connectionUri);
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        var client = new MongoClient(settings);
        try
        {
            var result = client
                .GetDatabase("admin")
                .RunCommand<BsonDocument>(new BsonDocument("ping", 1));
            Console.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        var events = _mongoContext.GetCollection<Event>("events").Find(ev => true).ToList();
        DateTime dateTimeNow = DateTime.Now;

        foreach (var e in events)
        {  
        Console.WriteLine(e.EndDate);
        }
        // if (DateTime.Compare(presentCondition.CurrentDate, dateTimeNow) < 0)
        // {
        //     Console.WriteLine(presentCondition.CurrentDate)
        // }





        return View();
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

