using System;
using System.Collections.Generic;

namespace Movies.Models
{
    public partial class Video
    {
        public int VideoId { get; set; }
        public int? EpisodeId { get; set; }
        public string? Link { get; set; }
        public string? Status { get; set; }

        public virtual Episode? Episode { get; set; }
    }
}
