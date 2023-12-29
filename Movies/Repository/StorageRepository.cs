﻿using Google.Cloud.Storage.V1;
using Movies.Interface;
using Movies.Models;

namespace Movies.Repository
{
    public class StorageRepository : IStorageRepository
    {
        private readonly GCPContext _gcpContext;

        public StorageRepository(GCPContext gcpContext)
        {
            _gcpContext = gcpContext;
        }

        public StorageRepository()
        {
            _gcpContext = new GCPContext();
        }

        public async Task<Stream> GetFile(string fileName)
        {
            var stream = new MemoryStream();
            await _gcpContext.StorageClient.DownloadObjectAsync(_gcpContext.GCPStorageBucket, fileName, stream);
            stream.Position = 0;
            return stream;

        }

        public async Task UploadFile(IFormFile file, string filePath)
        {
            var stream = new MemoryStream();
            file.CopyTo(stream);
            var storageObject = await _gcpContext.StorageClient.UploadObjectAsync(_gcpContext.GCPStorageBucket, filePath, file.ContentType, stream);
            if(storageObject == null)
            {
                throw new Exception("Upload file failed");
            }
        }

        public async Task DeleteFile(string fileName)
        {
            await _gcpContext.StorageClient.DeleteObjectAsync(_gcpContext.GCPStorageBucket, fileName);
        }
    }
}