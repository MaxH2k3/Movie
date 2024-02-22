using Movies.Business.movies;
using Movies.Models;

namespace Movies.Repository;

public interface IAnalystRepository
{
    Task<string> AddViewerMovie(Guid movieId);
    Task ConvertToPrevious();
    Task<List<Movie>> GetTopMovies();
}
