using Movies.Models;

namespace Movies.Interface
{
    public interface IFeatureRepository
    {
        IEnumerable<FeatureFilm> GetFeatures();
    }
}
