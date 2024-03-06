using MongoDB.Bson;
using MongoDB.Driver;
using WebApp.Models;

namespace WebApp.Models
{
    public class Search
    {
        public string? Name {get; set;} //dbcollection("events").EventName
        public string? Place { get; set; } //dbcollection("places").ActualPlace
        public string? DateChoice { get; set; } //dbcollection("events").EndDate //any, today, this week, next week
        public string? Type { get; set; } //dbcollection("events").EventType //any, indoor, outdoor, online
        public string? Category { get; set; } //dbcollection("events").TagId => Enum<dbcollection("tags").TagName> //any, art, game, dancing
    }
}
