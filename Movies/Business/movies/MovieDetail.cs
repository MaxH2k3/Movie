using Movies.Business.persons;
using Movies.Models;
using System.ComponentModel.DataAnnotations;

namespace Movies.Business.movies
{
    public class MovieDetail
    {
        public Guid MovieId { get; set; }
        public double? Mark { get; set; }
        public int? Time { get; set; }
        public int? Viewer { get; set; }
        [MinLength(20)]
        public string? Description { get; set; }
        [MaxLength(100)]
        [MinLength(2)]
        [Required]
        public string? EnglishName { get; set; }
        [MaxLength(100)]
        [MinLength(2)]
        [Required]
        public string? VietnamName { get; set; }
        [Required]
        public string? Thumbnail { get; set; }
        [Required]
        public string? Trailer { get; set; }
        public DateTime? ProducedDate { get; set; }
        public virtual Nation? Nation { get; set; }
        public virtual FeatureFilm? Feature { get; set; }
        public int? TotalSeasons { get; set; }
        public int? TotalEpisodes { get; set; }
        public string? Status { get; set; }
        public virtual ICollection<CastCharacter>? CastCharacteries { get; set; }
        public virtual ICollection<Category>? Categories { get; set; }
    }
}
