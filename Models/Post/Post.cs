using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Image_Generating_APIs.Models.Post
{
    public class Post
    {
        [BsonId]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Prompt { get; set; }
        public string? PhotoUrl { get; set; }
    }
}
