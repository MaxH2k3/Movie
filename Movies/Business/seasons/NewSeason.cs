namespace Movies.Business.seasons
{
    public class NewSeason
    {
        public Guid? SeasonId { get; set; }
        public Guid? MovieId { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<NewEpisode>? Episodes { get; set; }
    }
}
