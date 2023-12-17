namespace Movies.Models
{
    public class StoreVideo
    {
        public int? StoreVideoId { get; set; }
        public int? VideoId { get; set; }
        public virtual Video? Video { get; set; }
        public byte[]? VideoData { get; set; }
        public int? Index { get; set; }

        public override string? ToString()
        {
            return " VideoId: " + VideoId + " VideoData: " + VideoData + " Index: " + Index;
        }
    }
}
