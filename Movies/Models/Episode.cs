using System;
using System.Collections.Generic;

namespace Movies.Models
{
    public partial class Episode
    {

        public Guid? EpisodeId { get; set; }
        public Guid? SeasonId { get; set; }
        public int? EpisodeNumber { get; set; }
        public string? Name { get; set; }
        public string? Video { get; set; }
        public string? Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }

        public virtual Season? Season { get; set; }
    }
}
