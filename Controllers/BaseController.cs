using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApp.Models;

public class BaseController : Controller
{
    protected readonly MongoContext _mongoContext;

    public BaseController(MongoContext mongoContext)
    {
        _mongoContext = mongoContext;
    }

    protected string? _SetUserDataInViewData()
    {
        var userId = JwtHelper.GetUserIdFromToken(HttpContext.Session.GetString("JwtToken")!);
        ViewData["userID"] = userId;

        if (userId != null)
        {
            var userName = _mongoContext
                .GetCollection<User>("users")
                .Find(u => u.Id == ObjectId.Parse(userId))
                .FirstOrDefault();
            ViewData["userName"] = userName?.UserName;
            ViewData["userProfile"] = userName?.ProfilePicture;
        }

        return userId;
    }
}
