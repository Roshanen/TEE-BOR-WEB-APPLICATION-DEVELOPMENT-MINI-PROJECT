using MongoDB.Bson;
using MongoDB.Driver;
namespace WebApp.Models;

public class YourEventViewModel
{
    public List<Event> HostEvent { get; set; }
    public  List<Event> PastEvent { get; set; }
    public List<Event> AttendEvent { get; set; }
}
