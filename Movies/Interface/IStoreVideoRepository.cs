using Microsoft.AspNetCore.Mvc;
using Movies.Business.globals;
using Movies.Models;

namespace Movies.Interface;

public interface IStoreVideoRepository
{
    public Task<ResponseDTO> UploadMovie(IFormFile videoFile, string videoName);
    public Task<bool> FileExistAsync(string fileName);
    public Task<bool> DeleteVideo(string filename);
    public Task<FileStreamResult> GetVideo(string movie);
}
