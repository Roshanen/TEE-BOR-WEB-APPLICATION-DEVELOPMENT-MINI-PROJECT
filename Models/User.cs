using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class User
{
    [Required]
    public ObjectId Id { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string UserName { get; set; }
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    [Required]
    public string PasswordHash { get; set; } = string.Empty;
    public string? ProfilePicture { get; set; }
    public string Address { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string Contact { get; set; } = string.Empty;

}
