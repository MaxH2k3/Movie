using MongoDB.Driver;
using Movies.Business.movies;
using Movies.Models;
using Movies.Repository;
using MongoDB.Bson;
using Microsoft.EntityFrameworkCore;

namespace Movies.Service
{
    public class AnalystService : IAnalystService
    {
        private readonly MovieMongoContext _context;
        private readonly MOVIESContext _contextsql;

        public AnalystService(MovieMongoContext context, MOVIESContext contextsql)
        {
            _context = context;
            _contextsql = contextsql;
        }

        public AnalystService()
        {
            _context = new MovieMongoContext();
            _contextsql = new MOVIESContext();
        }

        public async Task<string> AddViewerMovie(Guid movieId)
        {
            var movie = await _contextsql.Movies.FindAsync(movieId);
            if (movie == null)
            {
                return "Movie not found";
            }
            var filter = Builders<AnalystMovie>.Filter.Eq("MovieId", movieId);
            var update = Builders<AnalystMovie>.Update.Inc("Viewer", 1);
            var options = new UpdateOptions { IsUpsert = true };
            await _context.CurrentTopMovies.UpdateOneAsync(filter, update, options);
            return "Saved successfully";
        }

        public async Task ConvertToPrevious()
        {
            var pipeline = new List<BsonDocument>
            {
                new BsonDocument("$sort", new BsonDocument("Viewer", -1)),
                new BsonDocument("$limit", 15),
                new BsonDocument("$out", "PreviousTopMovie")
            };
            var list = await _context.CurrentTopMovies.AggregateAsync<BsonDocument>(pipeline).Result.ToListAsync();
            if(list.Count > 0)
            {
                await _context.CurrentTopMovies.DeleteManyAsync(new BsonDocument());
            }
        }

        public async Task<List<Movie>> GetTopMovies()
        {
            var list = await (await _context.PreviousTopMovies.FindAsync(new BsonDocument())).ToListAsync();
            var listId = list.Select(x => x.MovieId).ToList();
            var movies = await _contextsql.Movies
                .Include(x => x.Feature)
                .Where(x => listId.Contains(x.MovieId))
                .ToListAsync();
            return movies;
        }
    }
}
