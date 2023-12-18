using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using Movies.Business;
using Movies.Interface;
using Movies.Models;
using System.Net;

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
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadMovie(IFormFile videoFile, string videoName)
        {
            ResponseDTO response = await _storeVideoRepository.UploadMovie(videoFile, videoName);
            if(response.Status != HttpStatusCode.Created)
            {
                return BadRequest(response);
            }

            return Ok(response.Message);
        }

        [HttpGet("Watch")]
        [ProducesResponseType(typeof(File), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVideo(string movie)
        {
            //check video exist in GridFs (Mongo)
            if (!_storeVideoRepository.FileExistAsync(movie).Result)
            {
                return NotFound("Video Not Found");
            }

            //get video from GridFS
            Stream videoStream = await _storeVideoRepository.GetVideo(movie);

            return File(videoStream, "video/mp4");
        }

        [HttpDelete("StoreVideo/{filename}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVideo(string filename)
        {
            if(!(await _storeVideoRepository.DeleteVideo(filename)))
            {
                return NotFound("Video Not Found");
            }

            return Ok("Video deleted successfully!");
        }
    }
}
