using Movies.Models;

namespace Movies.Interface
{
    public interface IEpisodeRepository
    {
        IEnumerable<Episode> GetEpisodes();
        IEnumerable<Episode> GetEpisodesBySeason(int seasonId);
    }
}
