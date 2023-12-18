using Microsoft.EntityFrameworkCore;
using Movies.Interface;
using Movies.Models;

namespace Movies.Repository
{
    public class SeasonRepository : ISeasonRepository
    {
        private readonly MOVIESContext _context;

        public SeasonRepository(MOVIESContext context)
        {
            _context = context;
        }

        public SeasonRepository()
        {
            _context = new MOVIESContext();
        }

        public IEnumerable<Season> GetSeasons()
        {
            return _context.Seasons.Include(s => s.Episodes);
        }

        public IEnumerable<Season> GetSeasonsByMovie(int movieId)
        {
            return GetSeasons().Where(s => s.MovieId == movieId).ToList()
                .Select(s =>
                {
                    s.Episodes = s.Episodes.OrderBy(e => e.EpisodeNumber).ToList();
                    return s;
                });
        }
    }
}
