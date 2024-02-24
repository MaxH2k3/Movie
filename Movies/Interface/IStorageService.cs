namespace Movies.Interface
{
    public interface IStorageService
    {
        Task<Stream> GetFile(string fileName);
        Task UploadFile(IFormFile file, string filePath);
        Task<Boolean> DeleteFile(string fileName);
    }
}
