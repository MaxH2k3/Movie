using Hangfire.Mongo.Migration.Strategies.Backup;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo;
using Hangfire;

namespace Movies.Configuration
{
    public static class HangfireExtension
    {
        public static void AddHangfireService(this IServiceCollection services)
        {
            var options = new MongoStorageOptions
            {
                MigrationOptions = new MongoMigrationOptions
                {
                    MigrationStrategy = new DropMongoMigrationStrategy(),
                    BackupStrategy = new NoneMongoBackupStrategy()
                }
            };

            services.AddHangfire(x =>
                x.UseMongoStorage(GetConnectionString(), options));

            services.AddHangfireServer();
        }

        private static string GetConnectionString()
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
