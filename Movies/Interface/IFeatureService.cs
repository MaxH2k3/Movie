using Movies.Models;

namespace Movies.Interface
{
    public interface IFeatureService
    {
        IEnumerable<FeatureFilm> GetFeatures();
    }
}
