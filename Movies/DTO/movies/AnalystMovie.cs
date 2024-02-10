using MongoDB.Bson;

namespace Movies.Business.movies
{
    public class AnalystMovie
    {
        public ObjectId Id { get; set; }
        public Guid MovieId { get; set; }
        public int Viewer { get; set; }
    }
}
