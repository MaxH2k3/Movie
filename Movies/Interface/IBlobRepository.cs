using Movies.Models;

namespace Movies.Interface
{
    public interface IBlobRepository
    {
        Task<Stream> GetBlob(string filePath);
        Task<string> UploadBlob(string filePath, IFormFile file);
        Task<string> DeleteBlob(string filePath);
    }
}
