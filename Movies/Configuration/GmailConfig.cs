namespace Movies.Configuration;

public class GmailSetting
{
    public string DisplayName { get; set; }
    public string SmtpServer { get; set; }
    public int Port { get; set; }
    public string Mail { get; set; }
    public string Password { get; set; }

    public override string? ToString()
    {
        return "DisplayName: " + DisplayName + "\n" +
               "SmtpServer: " + SmtpServer + "\n" +
               "Port: " + Port + "\n" +
               "Mail: " + Mail + "\n" +
               "Password: " + Password + "\n";
    }
}

public class GmailConfig
{
    public GmailSetting GmailSetting { get; set; }

    public GmailConfig()
    {
        IConfiguration config = new ConfigurationBuilder()

                .SetBasePath(Directory.GetCurrentDirectory())

                .AddJsonFile("appsettings.json", true, true)

                .Build();

        config.GetSection("GmailSetting");

        GmailSetting = new GmailSetting()
        {
            DisplayName = config["GmailSetting:DisplayName"],
            SmtpServer = config["GmailSetting:SmtpServer"],
            Port = int.Parse(config["GmailSetting:Port"]),
            Mail = config["GmailSetting:Mail"],
            Password = config["GmailSetting:Password"]
        };
    }

}
