using Movies.Models;

namespace Movies.Repository
{
    public interface INationService
    {
        IEnumerable<Nation> GetNations();
    }
}
