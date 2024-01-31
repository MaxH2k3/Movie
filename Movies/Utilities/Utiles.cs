namespace Movies.Utilities
{
    public class Utiles
    {
        public static DateTime ConvertToDateTime(double? value)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds((long)value);
            DateTime expirationDateTime = dateTimeOffset.UtcDateTime;
            return expirationDateTime;
        }

        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}
