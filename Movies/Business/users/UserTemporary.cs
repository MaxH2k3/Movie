using MongoDB.Bson;

namespace Movies.Business.users
{
    public class UserTemporary
    {
        public ObjectId Id { get; set; }
        public Guid UserId { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public byte[]? Password { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string? Role { get; set; }
        public string? Status { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
