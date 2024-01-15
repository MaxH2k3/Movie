using System.ComponentModel.DataAnnotations;

namespace Movies.Business.seasons
{
    public class NewSeason
    {
        [Required]
        public Guid? MovieId { get; set; }
        [Required]
        public string? Name { get; set; }
    }
}
