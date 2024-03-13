using MongoDB.Bson;
using MongoDB.Driver;

namespace WebApp.Models
{
    public class EventViewModel
    {
        public string EventName { get; set; }
        public string HostImg { get; set; }
        public string HostName { get; set; }
        public string EventImg { get; set; }
        public string EventDetails { get; set; }
        public string Tags { get; set; }
        public string Type { get; set; }
        public string Place { get; set; }
        public string MapUrl { get; set; }
        public string Status { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public DateTime RecruitStartDate { get; set; }
        public DateTime RecruitEndDate { get; set; }

        public List<dynamic> Attendees { get; set; }
        public List<Rating> Rating { get; set; }
        public List<float> RatingProb { get; set; }
        public List<User> RatingOwner { get; set; }

        public EventViewModel()
        {
            EventName = string.Empty;
            HostImg = string.Empty;
            HostName = string.Empty;
            EventImg = string.Empty;
            EventDetails = string.Empty;
            Tags = string.Empty;
            Type = string.Empty;
            Place = string.Empty;
            MapUrl = string.Empty;
            Status = string.Empty;
            EventStartDate = DateTime.Now;
            EventEndDate = DateTime.Now;
            RecruitStartDate = DateTime.Now;
            RecruitEndDate = DateTime.Now;
            Attendees = new List<dynamic>();
            Rating = new List<Rating>();
            RatingProb = new List<float>();
            RatingOwner = new List<User>();
        }
    }
}