using MongoDB.Bson;

namespace Movies.Business.users
{
    public class RegisterUser
    {
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
    }
}
