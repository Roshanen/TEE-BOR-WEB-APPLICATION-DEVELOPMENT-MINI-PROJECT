using MongoDB.Bson;
using MongoDB.Driver;

namespace WebApp.Models
{
    public class EventViewModel
    {
        public string? EventName { get; set; }
        public string? HostImg { get; set; }
        public string? HostName { get; set; }
        public string? EventImg { get; set; }
        public string? EventDetails { get; set; }
        public string? Tags { get; set; }
        public List<User>? Attendees { get; set; }
        public DateTime? Time { get; set; }
        public string? Place { get; set; }
        public string? MapUrl { get; set; }
    }
}