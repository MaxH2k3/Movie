namespace Movies.Utilities
{
    public class IDUtils
    {
        public static string GenerateNumId()
        {
            return DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
        }
    }
}
