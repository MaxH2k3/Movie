using Movies.Models;

namespace Movies.Interface;

public interface IStoreVideoRepository
{
    public Task<bool> UploadMovie(IFormFile videoFile);
    public Task<bool> FileExistAsync(string fileName);
    public Task<bool> DeleteVideo(string filename);
    public Task<Stream> GetVideo(string movie);
}
