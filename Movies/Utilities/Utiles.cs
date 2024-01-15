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
    }
}
