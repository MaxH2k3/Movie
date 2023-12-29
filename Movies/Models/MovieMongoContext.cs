using MongoDB.Driver;
using Movies.Business.users;
using System.Numerics;

namespace Movies.Models;

public class MovieMongoContext
{
    public MongoClient client { get; set; }

    public IMongoDatabase database { get; set; }

    public IMongoCollection<UserTemporary> Users { get; set; }
    public IMongoCollection<VerifyToken> Tokens { get; set;} 

    public MovieMongoContext()
    {

        // Create a MongoClient with the connection string
        client = new MongoClient(GetConnectionString());

        // Access a specific database
        database = client.GetDatabase("Movie");

        // Access a specific collection
        Users = database.GetCollection<UserTemporary>("User");
        Tokens = database.GetCollection<VerifyToken>("Token");

    }

    private string GetConnectionString()
    {
        IWebHostEnvironment? environment = new HttpContextAccessor().HttpContext?.RequestServices
                                    .GetRequiredService<IWebHostEnvironment>();

        IConfiguration config = new ConfigurationBuilder()

                .SetBasePath(Directory.GetCurrentDirectory())

                .AddJsonFile("appsettings.json", true, true)

                .Build();

        var strConn = "";
        if (environment?.IsProduction() ?? true)
        {
            strConn = config["ConnectionStrings:MongoDB"];
        } else
        {
            strConn = config["LocalDB:MongoDB"];
        }

        return strConn;

    }
}
