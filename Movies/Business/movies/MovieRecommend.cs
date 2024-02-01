using Movies.Models;

namespace Movies.Business.movies
{
    public class MovieRecommend
    {
        public Guid MovieId { get; set; }
        public int? Time { get; set; }
        public string? VietnamName { get; set; }
        public string? EnglishName { get; set; }
        public string? Thumbnail { get; set; }
        public string? ProducedDate { get; set; }
    }
}
