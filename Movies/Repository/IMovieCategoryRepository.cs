using Movies.Business.globals;

namespace Movies.Repository
{
    public interface IMovieCategoryRepository
    {
        Task<ResponseDTO> CreateMovieCategory(Guid movieId, IEnumerable<int> MovieCategories);
    }
}
