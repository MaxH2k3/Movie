using Movies.Business.movies;

namespace Movies.Repository;

public interface IAnalystRepository
{
    Task<string> AddViewerMovie(Guid movieId);
    Task ConvertToPrevious();
    Task<List<AnalystMovie>> GetTopMovies();
}
