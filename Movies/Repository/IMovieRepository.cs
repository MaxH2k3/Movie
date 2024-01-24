using Movies.Business.globals;
using Movies.Business.movies;
using Movies.Models;

namespace Movies.Interface
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetMovies(string? status = null);
        Movie? GetMovieById(Guid id, string? status = null);
        IEnumerable<Movie> GetMovieByName(string name, string status);
        IEnumerable<Movie> GetRecentUpdateMovies(int featureId, string status);
        IEnumerable<Movie> GetMovieByCategory(int categoryId, string status);
        IEnumerable<Movie> GetMovieByActor(string actorId, string status);
        IEnumerable<Movie> GetMovieByProducer(string producerId, string status);
        IEnumerable<Movie> GetMovieByFeature(int featureId, string status);
        IEnumerable<Movie> GetMovieByNation(string nationId, string status);
        Movie? GetMovieNewest();
        Task<ResponseDTO> CreateMovie(NewMovie newMovie);
        Task<ResponseDTO> UpdateMovie(NewMovie newMovie);
        Task<ResponseDTO> DeleteMovie(Guid id);
        int? GetFeatureIdByMovieId(Guid movieId);
        Dictionary<string, int> GetStatistic();
        Task<ResponseDTO> UpdateStatusMovie(Guid movieId, string status);
        IEnumerable<Movie> FilterMovie(string? name, string? status = null);
        Task<ResponseDTO> DeleteMovieByStatus(string status);
    }
}
