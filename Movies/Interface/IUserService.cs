using Movies.Business.globals;
using Movies.Business.users;
using Movies.Models;

namespace Movies.Interface
{
    public interface IUserService
    {
        User? GetUser(string username);
        IEnumerable<User> GetUsers();
        Task<ResponseDTO> Register(RegisterUser registerUser);
        ResponseDTO? Login(UserDTO userDTO);
        Task<ResponseDTO> VerifyAccount(string token, Guid userId);
        Task<ResponseDTO> ResendToken(Guid userId);
    }
}
