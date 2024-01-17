using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;

namespace Movies.Models
{
    public partial class Person
    {
        public Person()
        {
            Casts = new HashSet<Cast>();
        }

        public Guid PersonId { get; set; }
        public string? Thumbnail { get; set; }
        public string? NamePerson { get; set; }
        public string? NationId { get; set; }
        public string? Role { get; set; }
        public DateTime? DoB { get; set; }

        public virtual Nation? Nation { get; set; }
        public virtual ICollection<Cast> Casts { get; set; }
        public override string ToString()
        {
            return $"ActorId: {PersonId}, LinkImage: {Thumbnail}, NameActor: {NamePerson}, NationId: {NationId}, DoB: {DoB}";
        }
    }
}
