using MongoDB.Bson;
using MongoDB.Driver;
namespace WebApp.Models;
using System.ComponentModel.DataAnnotations;

public class CreateEvent
{   
    // place
    [Required]
    public string? ActualPlace { get; set; }
    [Required]
    public string? Province { get; set; }
    [Required]
    public string? District { get; set; }
    [Required]
    public string? SubDistrict { get; set; }
    [Required]
    public string? MapUrl { get; set; }
    // category
    [Required]
    public string? CategoryName { get; set; }
    // date time
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    // event
    [Required]
    public string? EventName { get; set; }
    [Required]
    public string? EventImg { get; set; }
    [Required]
    [Range(50, 200)]
    public string? EventDetails { get; set; }
    // member count
    [Required]
    public int? MaxMember { get; set; }
}