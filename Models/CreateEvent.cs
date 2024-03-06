using MongoDB.Bson;
using MongoDB.Driver;
namespace WebApp.Models;

public class CreateEvent
{   
    // place
    public string? Place { get; set; }
    public string? Province { get; set; }
    public string? District { get; set; }
    public string? SubDistrict { get; set; }
    public string? MapUrl { get; set; }
    // tag
    public string? Tag { get; set; }
    // date time
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    // event
    public string? EventName { get; set; }
    public string? EventImg { get; set; }
    public string? EventDetails { get; set; }
    // member count
    public int? MaxMember { get; set; }
}