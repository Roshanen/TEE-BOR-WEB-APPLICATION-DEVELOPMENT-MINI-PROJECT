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
        var eventId = rating.EventId;

        var ratingFound = _mongoContext.GetCollection<Rating>("ratings")
            .Find(r => r.EventId == eventId && r.UserId == userId).FirstOrDefault();
        

        if(ratingFound is null){
            rating.UserId = userId;
            rating.LastModifiedDate = DateTime.Now;
            _mongoContext.GetCollection<Rating>("ratings").InsertOne(rating);
        }
        else {
            ratingFound.Score = rating.Score;
            ratingFound.Comment = rating.Comment;
            ratingFound.LastModifiedDate = DateTime.Now;
            _mongoContext.GetCollection<Rating>("ratings").ReplaceOne(r => (r.Id == ratingFound.Id), ratingFound);
        }

        return RedirectToAction("viewid", "eventpage", new {id = eventId});
    }
}
