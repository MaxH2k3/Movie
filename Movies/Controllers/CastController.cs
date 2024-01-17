using Microsoft.AspNetCore.Mvc;
using Movies.Business.persons;
using Movies.Repository;

namespace Movies.Controllers;

[ApiController]
public class CastController : Controller
{

    private readonly ILogger<CastController> _logger;
    private readonly ICastRepository _castService;

    public CastController(ILogger<CastController> logger, ICastRepository castService)
    {
        _logger = logger;
        _castService = castService;
    }

    [HttpPost("/Cast")]
    public async Task<IActionResult> CreateCast([FromBody] IEnumerable<NewCast> newCasts, Guid movieId)
    {
        var result = await _castService.CreateCast(movieId, newCasts);
        return Ok(result);
    }

    [HttpPut("/Cast/{movieId}")]
    public async Task<IActionResult> UpdateCast([FromBody] IEnumerable<NewCast> newCasts, Guid movieId)
    {
        var result = await _castService.UpdateCast(movieId, newCasts);
        return Ok(result);
    }

}
