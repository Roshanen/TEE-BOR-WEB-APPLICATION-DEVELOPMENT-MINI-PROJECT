using MongoDB.Bson;
using MongoDB.Driver;
using WebApp.Models;

namespace WebApp.Models
{
    public class PresentCondition
    {
        public string CurrentDate {get; set;}
        public string Member {get; set;}
        public string Canceled {get; set;}
    }
}