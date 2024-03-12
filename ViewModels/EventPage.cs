using MongoDB.Bson;

namespace WebApp.Models;

public class EventPage
{
    public ObjectId Id { get; set; }
    public string HostImg { get; set; }
    public string HostName { get; set; }
    public string EventName { get; set; }
    public string EventImg { get; set; }
    public string EventDetails { get; set; }
    public List<string> Tags { get; set; }
    public List<User> Attendees { get; set; }
    public string Time {get; set;}
    public string Place {get; set;}
    public string MapUrl {get; set;}
    public bool IsAttending {get; set;}

    public EventPage()
    {
        Id = new ObjectId();
        HostImg = string.Empty;
        HostName = string.Empty;
        EventName = string.Empty;
        EventImg = string.Empty;
        EventDetails = string.Empty;
        Tags = new List<string>();
        Attendees = new List<User>();
        Time = string.Empty;
        Place = string.Empty;
        MapUrl = string.Empty;
        IsAttending = false;
    }
}