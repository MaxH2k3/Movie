using Movies.Models;
using System.ComponentModel.DataAnnotations;

namespace Movies.Business
{
    public class MovieDetail
    {
        public int MovieId { get; set; }
        [MaxLength(5)]
        public string? NationId { get; set; }
        public double? Mark { get; set; }
        public int? Time { get; set; }
        public int? Viewer { get; set; }
        [MinLength(20)]
        public string? Description { get; set; }
        [MaxLength(100)]
        [MinLength(2)]
        public string? EnglishName { get; set; }
        [MaxLength(100)]
        [MinLength(2)]
        public string? VietnamName { get; set; }
        [Required]
        public string? Thumbnail { get; set; }
        [Required]
        public string? Trailer { get; set; }
        public string? Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public virtual Nation? Nation { get; set; }
        public virtual FeatureFilm? Feature { get; set; }
        public virtual ICollection<CastCharacter>? CastCharacteries { get; set; }
        // public virtual ICollection<Season> Seasons { get; set; }
        public virtual ICollection<Category>? Categories { get; set; }
    }
}
