namespace Movies.Business.movies;

public class MovieGemini
{
    public int? FeatureId { get; set; }
    public string? NationId { get; set; }
    public double? Mark { get; set; }
    public int? Time { get; set; }
    public int? Viewer { get; set; }
    public string? Description { get; set; }
    public DateTime? ProducedDate { get; set; }
    public List<int>? Categories { get; set; }

    public override string? ToString()
    {
        return "{ " +
                "\n FeatureId: int," +
                "\n NationId: string," +
                "\n Mark: int," +
                "\n Time: int," +
                "\n Viewer: int," +
                "\n Description: summarize content of film ranging from 100 to 200 words, " +
                "\n ProducedDate: date of produced have format YYYY/MM/DD," +
                "\n Categories: list int of category" +
                "\n}";
    }
}
