using System.ComponentModel.DataAnnotations;

namespace Movies.Business.persons
{
    public class NewPerson
    {
        public Guid? PersonId { get; set; }
        public IFormFile? Thumbnail { get; set; }
        public string? NamePerson { get; set; }
        public string? NationId { get; set; }
        public string? Role { get; set; }
        public string? DoB { get; set; }
    }
}
