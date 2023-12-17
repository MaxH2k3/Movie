namespace Movies.Business
{
    public class MoviePreview
    {
        public int MovieId { get; set; }
        public double? Mark { get; set; }
        public int? Time { get; set; }
        public string? VietnamName { get; set; }
        public string? EnglishName { get; set; }
        public string? LinkThumbnail { get; set; }
        public string? Status { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
