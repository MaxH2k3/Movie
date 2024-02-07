using MongoDB.Bson;
using System.Net;

namespace Movies.Business.anothers
{
    public class BlackIP
    {
        public ObjectId Id { get; set; }
        public IPAddress IP { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
