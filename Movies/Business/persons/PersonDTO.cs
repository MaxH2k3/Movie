namespace Movies.Business.persons
{
    public class PersonDTO
    {
        public Guid PersonId { get; set; }
        public string? Thumbnail { get; set; }
        public string? NamePerson { get; set; }
        public string? NationId { get; set; }
        public string? NationName { get; set; }
        public string? Role { get; set; }
        public string? DoB { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
