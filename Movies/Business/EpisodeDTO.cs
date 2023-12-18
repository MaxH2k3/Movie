namespace Movies.Business
{
    public class EpisodeDTO
    {
        public int EpisodeId { get; set; }
        public int EpisodeNumber { get; set; }
        public string? Name { get; set; }
        public string? Video { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
