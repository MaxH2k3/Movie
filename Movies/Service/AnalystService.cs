using MongoDB.Driver;
using Movies.Business.movies;
using Movies.Models;
using Movies.Repository;
using MongoDB.Bson;

namespace Movies.Service
{
    public class AnalystService : IAnalystRepository
    {
        private readonly MovieMongoContext _context;

        public AnalystService(MovieMongoContext context)
        {
            _context = context;
        }

        public AnalystService()
        {
            _context = new MovieMongoContext();
        }

        public async Task AddViewerMovie(Guid movieId)
        {
            var filter = Builders<AnalystMovie>.Filter.Eq("MovieId", movieId);
            var update = Builders<AnalystMovie>.Update.Inc("Viewer", 1);
            var options = new UpdateOptions { IsUpsert = true };
            await _context.CurrentTopMovies.UpdateOneAsync(filter, update, options);
        }

        public async Task ConvertToPrevious()
        {
            var pipeline = new List<BsonDocument>
            {
                new BsonDocument("$sort", new BsonDocument("Viewer", -1)),
                new BsonDocument("$limit", 10),
                new BsonDocument("$out", "PreviousTopMovie")
            };
            var list = await _context.CurrentTopMovies.AggregateAsync<BsonDocument>(pipeline).Result.ToListAsync();
            if(list.Count > 0)
            {
                await _context.CurrentTopMovies.DeleteManyAsync(new BsonDocument());
            }
        }

    }
}
