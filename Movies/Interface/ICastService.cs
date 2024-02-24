using Movies.Business.globals;
using Movies.Business.persons;
using Movies.Models;

namespace Movies.Repository
{
    public interface ICastService
    {
        Task<ResponseDTO> CreateCast(Guid movieId, IEnumerable<NewCast> newCasts);
        Task<ResponseDTO> UpdateCast(Guid movieId, IEnumerable<NewCast> newCasts);
        Task<ResponseDTO> DeleteCast(int id);
    }
}
