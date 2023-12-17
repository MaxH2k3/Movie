using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Movies.Models
{
    public class Player
    {
        public ObjectId Id { get; set; }
        [BsonElement("title")]
        public string? Title { get; set; }
        [BsonElement("name")]
        public string? Name { get; set; }

        public override string? ToString()
        {
            return "id: " + Id + " title: " + Title + " name: " + Name + "\n";
        }
    }
}
