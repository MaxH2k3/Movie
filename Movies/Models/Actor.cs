using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;

namespace Movies.Models
{
    public partial class Actor
    {
        public Actor()
        {
            Casts = new HashSet<Cast>();
        }

        public int ActorId { get; set; }
        public string? Image { get; set; }
        public string? NameActor { get; set; }
        public string? NationId { get; set; }
        public DateTime? DoB { get; set; }

        public virtual Nation? Nation { get; set; }
        public virtual ICollection<Cast> Casts { get; set; }
        public override string ToString()
        {
            return $"ActorId: {ActorId}, LinkImage: {Image}, NameActor: {NameActor}, NationId: {NationId}, DoB: {DoB}";
        }
    }
}
