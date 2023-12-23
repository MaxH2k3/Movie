using Movies.Business.persons;
using Movies.Models;
using System.ComponentModel.DataAnnotations;

namespace Movies.Business.movies
{
    public class NewMovie
    {
        public Guid? MovieId { get; set; }
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
        public IFormFile? Thumbnail { get; set; }
        [Required]
        public string? Trailer { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        [Required]
        public Guid? ProducerId { get; set; }
        [Required]
        public string? NationId { get; set; }
        [Required]
        public int? FeatureId { get; set; }
    }
}
