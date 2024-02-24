using Google.Cloud.Storage.V1;
using Movies.Interface;
using Movies.Models;
using System.Diagnostics;

namespace Movies.Repository
{
    public class StorageService : IStorageService
    {
        private readonly GCPContext _gcpContext;

        public StorageService(GCPContext gcpContext)
        {
            _gcpContext = gcpContext;
        }

        public StorageService()
        {
            //_gcpContext = new GCPContext();
        }

        public async Task<Stream> GetFile(string fileName)
        {
            /*var stream = new MemoryStream();
            await _gcpContext.StorageClient.DownloadObjectAsync(_gcpContext.GCPStorageBucket, fileName, stream);
            stream.Position = 0;
            return stream;*/
            return null;
        }

        public async Task UploadFile(IFormFile file, string filePath)
        {
            /*var stream = new MemoryStream();
            file.CopyTo(stream);
            var storageObject = await _gcpContext.StorageClient.UploadObjectAsync(_gcpContext.GCPStorageBucket, filePath, file.ContentType, stream,
                new UploadObjectOptions
                {
                    PredefinedAcl = PredefinedObjectAcl.PublicRead
                });
            if(storageObject == null)
            {
                throw new Exception("Upload file failed");
            }*/
        }

        public async Task<bool> DeleteFile(string fileName)
        {
            /*try
            {
                if(await _gcpContext.StorageClient.GetObjectAsync(_gcpContext.GCPStorageBucket, fileName) != null)
                {
                    await _gcpContext.StorageClient.DeleteObjectAsync(_gcpContext.GCPStorageBucket, fileName);
                }
                
            } catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }*/
            return true;
        }
    }
}
