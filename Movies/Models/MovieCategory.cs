using System;
using System.Collections.Generic;

namespace Movies.Models
{
    public partial class MovieCategory
    {
        public int? CategoryId { get; set; }
        public Guid? MovieId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Movie? Movie { get; set; }
    }
}
