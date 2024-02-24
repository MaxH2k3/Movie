using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace Movies.DTO.anothers
{
    public class GeminiKey
    {
        [JsonIgnore]
        public ObjectId Id { get; set; }
        public string APIKey { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
