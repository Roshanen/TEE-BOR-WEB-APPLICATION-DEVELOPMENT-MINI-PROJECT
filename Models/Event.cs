using MongoDB.Bson;
using MongoDB.Driver;

namespace WebApp.Models;

public class Event
{
    public ObjectId Id { get; set; }
    public ObjectId PlaceId { get; set; }
    public ObjectId HostId { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
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
}
