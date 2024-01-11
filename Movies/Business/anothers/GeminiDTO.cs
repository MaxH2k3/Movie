namespace Movies.Business.anothers
{
    public class GeminiDTO
    {
        public string Content { get; set; }
        public int? AmountWord { get; set; }

        public override string ToString()
        {
            return $"Summary {Content} in {AmountWord} word without breaks and break lines";
        }
    }
}
