using Microsoft.AspNetCore.Mvc;
using Movies.Interface;
using System.Diagnostics;

namespace Movies.Controllers;


[ApiController]
public class FileController : Controller
{
    private readonly IBlobRepository _blobRepository;
    public FileController(IBlobRepository blobRepository)
    {
        _blobRepository = blobRepository;
    }


    [HttpGet("image/{fileName}")]
    public async Task<IActionResult> getThumbnail(string fileName)
    {

        var image = await _blobRepository.GetBlob(fileName);
        if(image == Stream.Null)
        {
            return NotFound($"{fileName} is not existed!");
        }
        return File(image, "image/*");
    }

    [HttpPost("image")]
    public async Task<IActionResult> upload(string pathName, IFormFile file)
    {
        var path = await _blobRepository.UploadBlob(pathName, file);
        if(string.IsNullOrEmpty(path))
        {
            return Conflict($"{pathName} is existed! Please, change your fileName");
        }
        return Ok(path);
    }

    [HttpDelete("image/{fileName}")]
    public async Task<IActionResult> delete(string fileName)
    {
        var path = await _blobRepository.DeleteBlob(fileName);
        if(string.IsNullOrEmpty(path))
        {
            return NotFound($"{fileName} is not existed!");
        }
        return Ok(path);
    }
}
