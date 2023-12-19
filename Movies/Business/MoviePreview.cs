using Movies.Models;

namespace Movies.Business
{
    public class MoviePreview
    {
        public Guid MovieId { get; set; }
        public double? Mark { get; set; }
        public int? Time { get; set; }
        public string? Thumbnail { get; set; }
        public virtual FeatureFilm? Feature { get; set; }
        public int? TotalSeasons { get; set; }
        public int? TotalEpisodes { get; set; }
        public DateTime? DateCreated { get; set; }
        public virtual ICollection<Category>? Categories { get; set; }
    }
}
