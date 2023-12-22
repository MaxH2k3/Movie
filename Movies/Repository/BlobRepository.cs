using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Movies.Interface;
using Movies.Models;

namespace Movies.Repository;

public class BlobRepository : IBlobRepository
{
    private readonly BlobServiceClient _blobServiceClient;
    private BlobContainerClient client;

    public BlobRepository(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
        client = _blobServiceClient.GetBlobContainerClient("movie");
    }

    public async Task<string> DeleteBlob(string filePath)
    {
        var blobClient = client.GetBlobClient(filePath);
        if(await blobClient.ExistsAsync())
        {
            await blobClient.DeleteAsync();
            return string.Empty;
        }
        return $"{filePath} is not existed!";
    }

    public async Task<Stream> GetBlob(string filePath)
    {
        var blobClient = client.GetBlobClient(filePath);
        if(await blobClient.ExistsAsync())
        {
            var downloadContent = await blobClient.DownloadAsync();
            return downloadContent.Value.Content;
        }
        return Stream.Null;
    }

    public async Task<string> UploadBlob(string filePath, IFormFile file)
    {
        var blobClient = client.GetBlobClient(filePath);
        if(await blobClient.ExistsAsync())
        {
            return string.Empty;
        }

        await blobClient.UploadAsync(file.OpenReadStream());


        return blobClient.Uri.AbsolutePath;
    }
}
