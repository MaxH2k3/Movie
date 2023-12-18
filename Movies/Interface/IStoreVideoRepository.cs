﻿using Movies.Business;
using Movies.Models;

namespace Movies.Interface;

public interface IStoreVideoRepository
{
    public Task<ResponseDTO> UploadMovie(IFormFile videoFile, string videoName);
    public Task<bool> FileExistAsync(string fileName);
    public Task<bool> DeleteVideo(string filename);
    public Task<Stream> GetVideo(string movie);
}
