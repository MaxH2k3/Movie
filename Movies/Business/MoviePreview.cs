﻿using Movies.Models;

namespace Movies.Business
{
    public class MoviePreview
    {
        public int MovieId { get; set; }
        public double? Mark { get; set; }
        public int? Time { get; set; }
        public string? VietnamName { get; set; }
        public string? EnglishName { get; set; }
        public string? Thumbnail { get; set; }
        public string? Description { get; set; }
        public virtual Nation? Nation { get; set; }
        public virtual FeatureFilm? Feature { get; set; }
        public DateTime? DateCreated { get; set; }
        public virtual ICollection<Category>? Categories { get; set; }
    }
}
