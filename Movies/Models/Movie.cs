using System;
using System.Collections.Generic;

namespace Movies.Models
{
    public partial class Movie
    {
        public Movie()
        {
            Casts = new HashSet<Cast>();
            Seasons = new HashSet<Season>();
            MovieCategories = new HashSet<MovieCategory>();
        }

        public Guid MovieId { get; set; }
        public int? FeatureId { get; set; }
        public string? NationId { get; set; }
        public double? Mark { get; set; }
        public int? Time { get; set; }
        public int? Viewer { get; set; }
        public string? Description { get; set; }
        public string? EnglishName { get; set; }
        public string? VietnamName { get; set; }
        public string? Thumbnail { get; set; }
        public string? Trailer { get; set; }
        public string? Status { get; set; }
        public int? TotalSeasons { get; set; }
        public int? TotalEpisodes { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateDeleted { get; set; }
        public DateTime? ProducedDate { get; set; }
        public virtual FeatureFilm? Feature { get; set; }
        public virtual Nation? Nation { get; set; }
        public virtual ICollection<Cast> Casts { get; set; }
        public virtual ICollection<Season> Seasons { get; set; }
        public virtual ICollection<MovieCategory> MovieCategories { get; set; }
    }
}
