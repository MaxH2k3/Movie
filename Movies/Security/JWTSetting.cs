namespace Movies.Security
{
    public class JWTSetting
    {
        public string? SecurityKey { get; set; }
        public double? ExpiryMinutes { get; set; }

        public JWTSetting()
        {
            SecurityKey = GetConnectionString();
            ExpiryMinutes = 20;
        }

        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()

            .SetBasePath(Directory.GetCurrentDirectory())

            .AddJsonFile("appsettings.json", true, true)

            .Build();

            var strConn = config["JWTSetting:securitykey"];

            return strConn;

        }
    }
}
