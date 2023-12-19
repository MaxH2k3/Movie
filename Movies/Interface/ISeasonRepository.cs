using Movies.Models;

namespace Movies.Interface
{
    public interface ISeasonRepository
    {
        IEnumerable<Season> GetSeasons();
        IEnumerable<Season> GetSeasonsByMovie(Guid movieId);
    }
}
