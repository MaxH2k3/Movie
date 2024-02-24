using MongoDB.Driver;
using Movies.Business.anothers;
using Movies.Business.movies;
using Movies.Business.users;
using Movies.DTO.anothers;

namespace Movies.Models
{
    public class GeminiMongoContext
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;

        public IMongoCollection<GeminiKey> GeminiKeys { get; set; }

        public GeminiMongoContext()
        {
            // Create a MongoClient with the connection string
            _client = new MongoClient(GetConnectionString());

            // Access a specific database
            _database = _client.GetDatabase("Movie");

            // Access a specific collection
            GeminiKeys = _database.GetCollection<GeminiKey>("GeminiKey");

        }

        private string GetConnectionString()
        {

            IConfiguration config = new ConfigurationBuilder()

                    .SetBasePath(Directory.GetCurrentDirectory())

                    .AddJsonFile("appsettings.json", true, true)

                    .Build();

            var strConn = config["ConnectionStrings:Hangfire"];

            return strConn;

        }
    }
}
