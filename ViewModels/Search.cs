using MongoDB.Bson;
using MongoDB.Driver;
using WebApp.Models;

namespace WebApp.Models
{
    public class Search
    {
        public string Name {get; set;}
        public string Status { get; set; }
        public string DateChoice { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string Sort { get; set; }

        public Search()
        {
            Name = string.Empty;
            Status = string.Empty;
            DateChoice = string.Empty;
            Type = string.Empty;
            Category = string.Empty;
            Sort = string.Empty;
        }
    }
}