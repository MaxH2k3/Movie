using System;
using System.Collections.Generic;

namespace Movies.Models
{
    public partial class Season
    {
        public Season()
        {
            Episodes = new HashSet<Episode>();
        }

        public int SeasonId { get; set; }
        public int MovieId { get; set; }
        public int SeasonNumber { get; set; }
        public string? Name { get; set; }
        public string? Status { get; set; }

        public virtual Movie Movie { get; set; } = null!;
        public virtual ICollection<Episode> Episodes { get; set; }
    }
}
