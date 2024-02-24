using Movies.Business.movies;
using Movies.Models;

namespace Movies.Repository;

public interface IAnalystService
{
    Task<string> AddViewerMovie(Guid movieId);
    Task ConvertToPrevious();
    Task<List<Movie>> GetTopMovies();
}
