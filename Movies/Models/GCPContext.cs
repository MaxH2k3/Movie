using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;

namespace Movies.Models;

public class GCPContext
{
    private readonly GoogleCredential _googleCredential;
    public string? GCPStorageAuthFile { get; set; }
    public string? GCPStorageBucket { get; set; }
    public StorageClient StorageClient { get; set; }

    public GCPContext()
    {
        GetConnectionString();
        _googleCredential = GoogleCredential.FromFile(GCPStorageAuthFile);
        StorageClient = StorageClient.Create(_googleCredential);
    }

    private void GetConnectionString()
    {

        IConfiguration config = new ConfigurationBuilder()

        .SetBasePath(Directory.GetCurrentDirectory())

        .AddJsonFile("appsettings.json", true, true)

        .Build();

        GCPStorageAuthFile = config["GoogleCloud:GCPStorageAuthFile"];
        GCPStorageBucket = config["GoogleCloud:GCPStorageBucket"];

    }

}
