using MongoDB.Bson;
using MongoDB.Driver;
namespace WebApp.Models;

public class Place
{
    public ObjectId Id { get; set; }
    
    public string? MapUrl { get; set; }
    public string? Place { get; set; }
    public string? Province { get; set; }
    public string? District { get; set; }
    public string? SubDistrict { get; set; }
}
