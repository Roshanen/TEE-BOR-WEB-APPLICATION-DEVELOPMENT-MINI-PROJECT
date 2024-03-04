using MongoDB.Bson;

namespace WebApp.Models;

public class Event
{
    public ObjectId Id { get; set; }
    public string? HostImg { get; set; }
    public string? HostName { get; set; }
    public string? EventName { get; set; }
    public string? EventImg { get; set; }
    public string? EventDetails { get; set; }
    public List<string>? Tags { get; set; }
    public User Host { get; set; }
    public List<User>? Attendees { get; set; }
    public string? Time {get; set;}
    public string? Place {get; set;}
    public string? MapUrl {get; set;}
    public bool? IsAttending {get; set;}
}
