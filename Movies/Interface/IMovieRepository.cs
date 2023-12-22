using Movies.Business.globals;
using Movies.Business.movies;
using Movies.Models;

namespace Movies.Interface
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetMovies();
        Movie? GetMovieById(Guid id);
        IEnumerable<Movie> GetMovieByName(string name);
        IEnumerable<Movie> GetRecentUpdateMovies(int featureId);
        IEnumerable<Movie> GetMovieByCategory(int categoryId);
        IEnumerable<Movie> GetMovieByActor(string actorId);
        IEnumerable<Movie> GetMovieByProducer(string producerId);
        IEnumerable<Movie> GetMovieByFeature(int featureId);
        IEnumerable<Movie> GetMovieByNation(string nationId);
        Movie? GetMovieNewest();
        Task<ResponseDTO> CreateMovie(MovieDetail movieDetail);
        Task<ResponseDTO> UpdateMovie(MovieDetail movieDetail);
        Task<ResponseDTO> DeleteMovie(Guid id);
    }
}
