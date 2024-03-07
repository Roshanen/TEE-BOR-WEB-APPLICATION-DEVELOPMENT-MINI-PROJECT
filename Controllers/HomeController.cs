using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics;
using WebApp.Models;

namespace WebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly MongoContext _mongoContext;

    public HomeController(ILogger<HomeController> logger, MongoContext mongoContext)
    {
        _logger = logger;
        _mongoContext = mongoContext;
    }


    public IActionResult Index()
    {
        var userId = JwtHelper.GetUserIdFromToken(HttpContext.Session.GetString("JwtToken")!);
        ViewData["userID"] = userId;
        if (userId != null)
        {
            var userName = _mongoContext.GetCollection<User>("users").Find(u => u.Id == ObjectId.Parse(userId)).FirstOrDefault();
            ViewData["userName"] = userName.UserName;
        }

        // var connectionUri = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING");
        // var settings = MongoClientSettings.FromConnectionString(connectionUri);
        // settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        // var client = new MongoClient(settings);
        // try
        // {
        //     var result = client
        //         .GetDatabase("admin")
        //         .RunCommand<BsonDocument>(new BsonDocument("ping", 1));
        //     Console.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");
        // }
        // catch (Exception ex)
        // {
        //     Console.WriteLine(ex);
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
