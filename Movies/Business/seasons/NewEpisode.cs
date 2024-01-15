using System.ComponentModel.DataAnnotations;

namespace Movies.Business.seasons
{
    public class NewEpisode
    {
        public string? Name { get; set; }
        [Required]
        public string? Video { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
