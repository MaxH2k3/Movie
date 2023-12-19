using Microsoft.EntityFrameworkCore;
using Movies.Interface;
using Movies.Models;

namespace Movies.Repository
{
    public class EpisodeRepository : IEpisodeRepository
    {
        private readonly MOVIESContext _context;

        public EpisodeRepository(MOVIESContext context)
        {
            _context = context;
        }

        public EpisodeRepository()
        {
            _context = new MOVIESContext();
        }

        public IEnumerable<Episode> GetEpisodes()
        {
            return _context.Episodes.Include(e => e.Season);
        }

        public IEnumerable<Episode> GetEpisodesBySeason(string seasonId)
        {
            return GetEpisodes().Where(e => e.SeasonId.Equals(seasonId)).ToList();
        }
    }
}
