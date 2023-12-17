using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using Movies.Interface;
using Movies.Models;
using System.Diagnostics;

namespace Movies.Repository;

public class StoreVideoRepository : IStoreVideoRepository
{
    private readonly StoreVideoContext _context;

    public StoreVideoRepository(StoreVideoContext storeVideoContext)
    {
        _context = storeVideoContext;
    }

    public StoreVideoRepository()
    {
        _context = new StoreVideoContext();
    }

    public async Task<bool> UploadMovie(IFormFile videoFile)
    {
        // Kiểm tra tệp tin video
        if (videoFile == null || videoFile.Length == 0)
        {
            return false;
        }

        // Tạo một tên duy nhất cho video
        string videoName = $"{Path.GetRandomFileName()}{Path.GetExtension(videoFile.FileName)}";

        // Lưu trữ video vào GridFS
        using (var stream = videoFile.OpenReadStream())
        {
            await _context.gridFSBucket.UploadFromStreamAsync(videoName, stream);
        }

        // Tiếp tục xử lý lưu trữ video (ví dụ: lưu tên tệp tin vào cơ sở dữ liệu)

        return true;
    }

    public async Task<bool> FileExistAsync(string fileName)
    {
        var filter = Builders<GridFSFileInfo<ObjectId>>.Filter.Eq(x => x.Filename, fileName);
        var options = new GridFSFindOptions
        {
            Limit = 1
        };
        var cursor = await _context.gridFSBucket.FindAsync(filter, options);
        return await cursor.AnyAsync();
    }


    public async Task<bool> DeleteVideo(string filename)
    {
        var filter = Builders<GridFSFileInfo<ObjectId>>.Filter.Eq(x => x.Filename, filename);
        var options = new GridFSFindOptions { Limit = 1 };
        var cursor = await _context.gridFSBucket.FindAsync(filter, options);
        var fileInfo = await cursor.FirstOrDefaultAsync();

        if (fileInfo == null)
        {
            return false;
        }

        var id = fileInfo.Id;
        //delete video from GridFS
        await _context.gridFSBucket.DeleteAsync(id);

        return true;
    }

    public async Task<Stream> GetVideo(string movie)
    {
        var filter = Builders<GridFSFileInfo<ObjectId>>.Filter.Eq(x => x.Filename, movie);
        var options = new GridFSFindOptions { Limit = 1 };
        var cursor = await _context.gridFSBucket.FindAsync(filter, options);
        var fileInfo = await cursor.FirstOrDefaultAsync();

        if(fileInfo == null)
        {
            throw new FileNotFoundException($"File '{movie}' not found.");
        }

        return await _context.gridFSBucket.OpenDownloadStreamAsync(fileInfo.Id);
    }

}
