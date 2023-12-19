using System;
using System.Collections.Generic;

namespace Movies.Models
{
    public partial class Cast
    {
        public Guid ActorId { get; set; }
        public Guid MovieId { get; set; }
        public string CharacterName { get; set; } = null!;

        public virtual Person Actor { get; set; } = null!;
        public virtual Movie Movie { get; set; } = null!;
    }
}
