namespace Movies.Business.seasons
{
    public class NewEpisode
    {
        public Guid SeasonId { get; set; }
        public string? Name { get; set; }
        public string? Video { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
