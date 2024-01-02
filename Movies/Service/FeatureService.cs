using Movies.Interface;
using Movies.Models;

namespace Movies.Repository
{
    public class FeatureService : IFeatureRepository
    {
        private readonly MOVIESContext _context;

        public FeatureService(MOVIESContext context)
        {
            _context = context;
        }

        public FeatureService()
        {
            _context = new MOVIESContext();
        }

        public IEnumerable<FeatureFilm> GetFeatures()
        {
            return _context.FeatureFilms.ToList();
        }
    }
}
