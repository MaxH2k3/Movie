using Movies.Interface;
using Movies.Models;

namespace Movies.Repository
{
    public class FeatureRepository : IFeatureRepository
    {
        private readonly MOVIESContext _context;

        public FeatureRepository(MOVIESContext context)
        {
            _context = context;
        }

        public FeatureRepository()
        {
            _context = new MOVIESContext();
        }

        public IEnumerable<FeatureFilm> GetFeatures()
        {
            return _context.FeatureFilms.ToList();
        }
    }
}
