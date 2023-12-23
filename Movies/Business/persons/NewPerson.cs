using System.ComponentModel.DataAnnotations;

namespace Movies.Business.persons
{
    public class NewPerson
    {
        public Guid? PersonId { get; set; }
        public IFormFile? Thumbnail { get; set; }
        [Required]
        public string? NamePerson { get; set; }
        [Required]
        public string? NationId { get; set; }
        [Required]
        public string? Role { get; set; }
        public string? DoB { get; set; }
    }
}
