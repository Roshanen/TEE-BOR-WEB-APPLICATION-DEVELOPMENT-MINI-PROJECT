using MongoDB.Bson;

namespace WebApp.Models;

public class Event
{
    public ObjectId Id { get; set; }
    public string? Name { get; set; }
    public string? Place { get; set; }
}
