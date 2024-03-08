using MongoDB.Bson;
using MongoDB.Driver;
namespace WebApp.Models;

public class Attendee
{
    public User user { get; set; }
    public int? friend { get; set; }
}
