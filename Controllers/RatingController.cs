using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApp.Models;
using System;

namespace WebApp.Controllers;

public class RatingController : BaseController
{
    private new readonly MongoContext _mongoContext;

    public RatingController(MongoContext mongoContext) : base(mongoContext)
    {
        _mongoContext = mongoContext;
    }

    [HttpPost]
    public IActionResult Create(Rating rating)
    {
        String userIdString = _SetUserDataInViewData();
        if (userIdString is null) return RedirectToAction("login", "account");
        var userId = new ObjectId(userIdString);

        rating.UserId = ObjectId.Parse(ViewBag.UserId);
        rating.LastModifiedDate = DateTime.Now;

        _mongoContext.GetCollection<Rating>("ratings").InsertOne(rating);
        Console.WriteLine("ASDASDASD");
        return RedirectToAction("index", "home");
    }
}
