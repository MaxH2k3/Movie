using Microsoft.AspNetCore.Mvc;
using Movies.Service;

namespace Movies.Controllers;

[ApiController]
public class GeminiController : Controller
{

    private readonly GeminiService _genminiService;

    public GeminiController(GeminiService geminiService)
    {
        _genminiService = geminiService;
    }

    [HttpGet("Chat")]
    public async Task<IActionResult> Chat(string content, string? nation = null)
    {
        var res = await _genminiService.Chat(content, nation);

        if(_genminiService.CheckNull(res))
        {
            return StatusCode(500, "Server Error! Please, Try Again.");
        }

        if (_genminiService.IsJson(res))
        {
            return Ok(res);
        }
        

        return NotFound($"{content} Not Found!");
    }
}
