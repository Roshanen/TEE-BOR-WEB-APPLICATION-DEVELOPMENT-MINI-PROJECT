using MongoDB.Bson;
using MongoDB.Driver;

namespace WebApp.Models;

public class Event
{
    public ObjectId Id { get; set; }
    public ObjectId PlaceId { get; set; }
    public ObjectId HostId { get; set; }

    public DateTime EventStartDate { get; set; }
    public DateTime EventEndDate { get; set; }
    public DateTime RecruitStartDate { get; set; }
    public DateTime RecruitEndDate { get; set; }
    public DateTime LastModifiedDate { get; set; }

    public string CategoryName { get; set; }
    public string EventName { get; set; }
    public string EventImg { get; set; }
    public string EventDetails { get; set; }
    public int CurrentMember { get; set; }
    public int MaxMember { get; set; }
    public float Rating { get; set; }
    public string Type { get; set; } 
    public string Status { get; set; } = "Active";

    public Event()
    {
        Id = new ObjectId();
        PlaceId = new ObjectId();
        HostId = new ObjectId();

        EventStartDate = DateTime.Now;
        EventEndDate = DateTime.Now;
        RecruitStartDate = DateTime.Now;
        RecruitEndDate = DateTime.Now;
        LastModifiedDate = DateTime.Now;

        CategoryName = string.Empty;
        EventName = string.Empty;
        EventImg = string.Empty;
        EventDetails = string.Empty;

        Type = string.Empty;
        Status = "Active";
    }
}
