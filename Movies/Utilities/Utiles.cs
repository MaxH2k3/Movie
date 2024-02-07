namespace Movies.Utilities
{
    public class Utiles
    {
        public void Hello()
        {
            // Lấy múi giờ hiện tại của server
            DateTime serverTime = DateTime.UtcNow;

            // Tìm thông tin múi giờ của Việt Nam
            TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

            // Chuyển múi giờ của server sang múi giờ của Việt Nam
            DateTime vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(serverTime, vietnamTimeZone);
            
            Console.WriteLine("Time: " + vietnamTime);
        }
        
        public static DateTime GetNow()
        {
            // Lấy múi giờ hiện tại của server
            DateTime serverTime = DateTime.UtcNow;

            // Tìm thông tin múi giờ của Việt Nam
            TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

            // Chuyển múi giờ của server sang múi giờ của Việt Nam
            DateTime vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(serverTime, vietnamTimeZone);

            return vietnamTime;
        }

        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}
