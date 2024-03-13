using MongoDB.Bson;
using MongoDB.Driver;
namespace WebApp.Models;
using System.ComponentModel.DataAnnotations;

public class CreateEvent
{   
    // place
    [Required][StringLength(64)]
    public string? ActualPlace { get; set; }
    [Required][StringLength(64)]
    public string? Province { get; set; }
    [Required][StringLength(64)]
    public string? District { get; set; }
    [Required][StringLength(64)]
    public string? SubDistrict { get; set; }
    [Required]
    public string? MapUrl { get; set; }
    // category
    [Required]
    public string? CategoryName { get; set; }
    // date time
    [Required]
    public DateTime EventStartDate { get; set; }
    [Required]
    public DateTime EventEndDate { get; set; }
    [Required]
    public DateTime RecruitStartDate { get; set; }
    [Required]
    public DateTime RecruitEndDate { get; set; }
    // event
    [Required][StringLength(32)]
    public string? EventName { get; set; }
    [Required]
    public string? EventImg { get; set; }
    [Required][StringLength(248, MinimumLength = 16)]
    public string? EventDetails { get; set; }
    // member count
    [Required]
    public int MaxMember { get; set; }
    [Required]
    public string Type { get; set; }
}