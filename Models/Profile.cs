using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class Profile
{
    public User? User { get; set; }
    public List<Event>? AttendEvent { get; set; }

}

