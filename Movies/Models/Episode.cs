using System;
using System.Collections.Generic;

namespace Movies.Models
{
    public partial class Episode
    {
        public Episode()
        {
            Videos = new HashSet<Video>();
        }

        public int EpisodeId { get; set; }
        public int? SeasonId { get; set; }
        public int EpisodeNumber { get; set; }
        public string? Name { get; set; }
        public string? Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }

        public virtual Season? Season { get; set; }
        public virtual ICollection<Video> Videos { get; set; }
    }
}
