using Microsoft.EntityFrameworkCore;
using Movies.Interface;
using Movies.Models;
using Movies.Utilities;

namespace Movies.Repository
{
    public class SeasonService : ISeasonRepository
    {
        private readonly MOVIESContext _context;

        public SeasonService(MOVIESContext context)
        {
            _context = context;
        }

        public SeasonService()
        {
            _context = new MOVIESContext();
        }

        public IEnumerable<Season> GetSeasons()
        {
            return _context.Seasons.Include(s => s.Episodes);
        }

        public IEnumerable<Season> GetSeasonsByMovie(Guid movieId)
        {
            return GetSeasons().Where(s => s.MovieId.Equals(movieId)).ToList()
                .Select(s =>
                {
                    s.Episodes = s.Episodes.OrderBy(e => e.EpisodeNumber).ToList();
                    return s;
                });
        }

        public IEnumerable<Season> GetSeasonsByMovieAndNumber(Guid movieId, int? seasonNumber)
        {
            return GetSeasons().Where(s => s.MovieId.Equals(movieId) && s.SeasonNumber == seasonNumber).ToList()
                .Select(s =>
                {
                    s.Episodes = s.Episodes.OrderBy(e => e.EpisodeNumber).ToList();
                    return s;
                });
        }
    }
}
