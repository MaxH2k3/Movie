namespace Movies.Business
{
    public class MovieNewest
    {
        public Guid MovieId { get; set; }
        public string? EnglishName { get; set; }
        public string? VietnamName { get; set; }
        public string? Description { get; set; }
        public string? Thumbnail { get; set; }
        public string? Trailer { get; set; }
    }
}
