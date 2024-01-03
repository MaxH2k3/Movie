using Movies.Business.globals;
using Movies.Business.seasons;
using Movies.Models;

namespace Movies.Interface
{
    public interface IEpisodeRepository
    {
        IEnumerable<Episode> GetEpisodes();
        IEnumerable<Episode> GetEpisodesBySeason(Guid seasonId);
        ResponseDTO CreateEpisode(NewEpisode newEpisode, Guid seasonId);
        IEnumerable<ResponseDTO> CreateEpisodes(IEnumerable<NewEpisode> newEpisodes, Guid seasonId);
        Episode? GetEpisode(Guid episodeId);
        Task<ResponseDTO> DeleteEpisode(Guid episodeId);
        IEnumerable<ResponseDTO> DeleteEpisodeBySeason(Guid seasonId);
    }
}
