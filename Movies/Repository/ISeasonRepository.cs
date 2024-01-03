using Movies.Business.globals;
using Movies.Business.seasons;
using Movies.Models;

namespace Movies.Interface
{
    public interface ISeasonRepository
    {
        IEnumerable<Season> GetSeasons();
        Season? GetSeason(Guid seasonId);
        IEnumerable<Season> GetSeasonsByMovie(Guid movieId);
        IEnumerable<Season> GetSeasonsByMovieAndNumber(Guid movieId, int? seasonNumber);
        Task<ResponseDTO> CreateSeason(NewSeason newSeason);
        Task<ResponseDTO> DeleteSeason(Guid seasonId);
    }
}
