using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;

namespace Movies.Models
{
    public class StoreVideoContext
    {

        public MongoClient client { get; set; }

        public IMongoDatabase database { get; set; }

        public string BucketName { get; set; }

        public GridFSBucket gridFSBucket { get; set;}

        public IMongoCollection<Player> CollectionPlayer { get; set; }
        public StoreVideoContext()
        {
            BucketName = GetBucketName();

            // Create a MongoClient with the connection string
            client = new MongoClient(GetConnectionString());

            // Access a specific database
            database = client.GetDatabase("Movie");

            // Create GridFSBucket
            gridFSBucket = new GridFSBucket(database, new GridFSBucketOptions
            {
                BucketName = BucketName
            });

            // Access a specific collection
            CollectionPlayer = database.GetCollection<Player>("Players");
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

        private string GetBucketName()
        {
            IConfiguration config = new ConfigurationBuilder()

                .SetBasePath(Directory.GetCurrentDirectory())

                .AddJsonFile("appsettings.json", true, true)

                .Build();

            var strConn = config["GridFS:BucketName"];

            return strConn;

        }
        
    }
}
