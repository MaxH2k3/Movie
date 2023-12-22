using Microsoft.AspNetCore.Mvc;
using Movies.Interface;
using System.IO;

namespace Movies.Controllers;

[ApiController]
public class FileController : Controller
{
    private readonly IStorageRepository _storageRepository;

    public FileController(IStorageRepository storageRepository)
    {
        _storageRepository = storageRepository;
    }

    [HttpGet]
    [Route("file")]
    public async Task<IActionResult> GetFile(string fileName)
    {
        Console.WriteLine("file name: " + fileName);
        var imageStream = await _storageRepository.GetFile(fileName);
        if(imageStream == Stream.Null)
        {
            return NotFound("Not found");
        }

        return File(imageStream, "image/*");
    }

    [HttpPost]
    [Route("file")]
    public async Task<IActionResult> UploadFile(IFormFile file, string filePath)
    {
        if(file.Length == 0)
        {
            return BadRequest("File is empty");
        }
        await _storageRepository.UploadFile(file, filePath);
        return Ok(filePath);
    }

    [HttpDelete]
    [Route("file")]
    public async Task<IActionResult> DeleteFile(string fileName)
    {
        await _storageRepository.DeleteFile(fileName);
        return Ok("ok");
    }
}
