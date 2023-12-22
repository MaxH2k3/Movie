namespace Movies.Interface
{
    public interface IStorageRepository
    {
        Task<Stream> GetFile(string fileName);
        Task UploadFile(IFormFile file, string filePath);
        Task DeleteFile(string fileName);
    }
}
