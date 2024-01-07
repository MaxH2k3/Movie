using Movies.Models;

namespace Movies.Repository
{
    public interface INationRepository
    {
        IEnumerable<Nation> GetNations();
    }
}
