namespace Movies.Business.users
{
    public class UserDetail
    {
        public Guid UserId { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Role { get; set; }
        public string? Status { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
