using Movies.Business;
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
        Movie? GetMovieNewest();
        Task<ResponseDTO> CreateMovie(MovieDetail movieDetail);
        Task<ResponseDTO> UpdateMovie(MovieDetail movieDetail);
        Task<ResponseDTO> DeleteMovie(Guid id);
    }
}
