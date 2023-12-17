using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using Movies.Interface;
using Movies.Models;

namespace Movies.Controllers
{
    public class StoreVideoController : Controller
    {
        private readonly IStoreVideoRepository _storeVideoRepository;
        private readonly StoreVideoContext _context;

        public StoreVideoController(IStoreVideoRepository storeVideoRepository,
                    StoreVideoContext context)
        {
            _storeVideoRepository = storeVideoRepository;
            _context = context;
        }

        [HttpPost("StoreVideo")]
        public async Task<IActionResult> UploadMovie(IFormFile videoFile, string videoName)
        {
            if(!(await _storeVideoRepository.UploadMovie(videoFile, videoName)))
            {
                return NotFound("Video Not Found");
            }

            return Ok("Video uploaded successfully.");
        }

        [HttpGet("Watch")]
        public async Task<IActionResult> GetVideo(string movie)
        {
            //check video exist in GridFs (Mongo)
            if(!_storeVideoRepository.FileExistAsync(movie).Result)
            {
                return NotFound("Video Not Found");
            }

            //get video from GridFS
            Stream videoStream = await _storeVideoRepository.GetVideo(movie);

            return File(videoStream, "video/mp4");
        }

        [HttpDelete("StoreVideo/{filename}")]
        public async Task<IActionResult> DeleteVideo(string filename)
        {
            if(!(await _storeVideoRepository.DeleteVideo(filename)))
            {
                return NotFound("Video Not Found");
            }

            return Ok("OK");
        }
    }
}
