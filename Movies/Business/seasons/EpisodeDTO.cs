namespace Movies.Business.seasons
{
    public class EpisodeDTO
    {
        public Guid EpisodeId { get; set; }
        public int EpisodeNumber { get; set; }
        public string? Name { get; set; }
        public string? Video { get; set; }
        public string? Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
