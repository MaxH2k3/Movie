using Movies.Business.globals;
using Movies.Business.seasons;
using Movies.Models;

namespace Movies.Interface
{
    public interface IEpisodeService
    {
        IEnumerable<Episode> GetEpisodes();
        IEnumerable<Episode> GetEpisodesBySeason(Guid seasonId);
        Episode CreateEpisode(NewEpisode newEpisode, int episodeNumber, Guid seasonId);
        Task<ResponseDTO> CreateEpisodes(IEnumerable<NewEpisode> newEpisodes, Guid seasonId);
        Episode? GetEpisode(Guid episodeId);
        Task<ResponseDTO> DeleteEpisode(Guid episodeId);
        Task<ResponseDTO> DeleteEpisodeBySeason(Guid seasonId);
        Task<ResponseDTO> UpdateEpisode(NewEpisode newEpisode, Guid episodeId);
        Task<ResponseDTO> UpdateEpisodes(Guid seasonId, IEnumerable<EpisodeDTO> newEpisodes);
        int GetTotalEpisodes(Guid seasonId);
    }
}
