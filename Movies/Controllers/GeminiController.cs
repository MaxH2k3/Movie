using Microsoft.AspNetCore.Mvc;
using Movies.Repository;
using Movies.Service;
using System.Net;

namespace Movies.Controllers;

[ApiController]
public class GeminiController : Controller
{

    private readonly IGeminiService _geminiService;

    public GeminiController(IGeminiService geminiService)
    {
        _geminiService = geminiService;
    }

    [HttpGet("Chat")]
    public async Task<IActionResult> Chat(string content, string? nation = null)
    {
        var res = await _geminiService.Chat(content, nation);

        if(_geminiService.CheckNull(res))
        {
            return StatusCode(500, "Server Error! Please, Try Again.");
        }

        if (_geminiService.IsJson(res))
        {
            return Ok(res);
        }
        

        return NotFound($"{content} Not Found!");
    }

    [HttpPost("AddGeminiKey")]
    public async Task<IActionResult> AddGeminiKey([FromBody] string apiKey)
    {
        var res = await _geminiService.AddGeminiKey(apiKey);
        if(res.Status == HttpStatusCode.Created)
        {
            return Ok(res.Message);
        } else if(res.Status == HttpStatusCode.Conflict)
        {
            return Conflict(res.Message);
        }

        return BadRequest(res);
    }

    [HttpDelete("DeleteGeminiKey/{key}")]
    public async Task<IActionResult> DeleteGeminiKey(string key)
    {
        var res = await _geminiService.DeleteGeminiKey(key);
        if(res.Status == HttpStatusCode.OK)
        {
            return Ok(res.Message);
        } else if(res.Status == HttpStatusCode.NotFound)
        {
            return NotFound(res.Message);
        }

        return Ok(res);
    }

    [HttpGet("GetGeminiKey")]
    public async Task<IActionResult> GetGeminiKey()
    {
        var res = await _geminiService.GetGeminiKeys();

        return Ok(res);
    }
}
