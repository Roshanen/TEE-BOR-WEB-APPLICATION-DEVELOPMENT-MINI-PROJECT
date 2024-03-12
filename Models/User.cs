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
    public string PasswordHash { get; set; }
    public string ProfilePicture { get; set; }
    public string Address { get; set; }
    public string Bio { get; set; }
    public string Contact { get; set; }

    public User()
    {
        Id = new ObjectId();
        UserName = string.Empty;
        Email = string.Empty;
        PasswordHash = string.Empty;
        ProfilePicture = string.Empty;
        Address = string.Empty;
        Bio = string.Empty;
        Contact = string.Empty;
    }

}
