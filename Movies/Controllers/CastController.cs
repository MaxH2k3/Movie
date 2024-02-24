using Microsoft.AspNetCore.Mvc;
using Movies.Business.persons;
using Movies.Repository;

namespace Movies.Controllers;

[ApiController]
public class CastController : Controller
{

    private readonly ICastService _castService;

    public CastController(ICastService castService)
    {
        _castService = castService;
    }

    [HttpPost("/Cast/{movieId}")]
    public async Task<IActionResult> CreateCast([FromBody] IEnumerable<NewCast> newCasts, Guid movieId)
    {
        var result = await _castService.CreateCast((Guid)movieId, newCasts);
        return Ok(result);
    }

    [HttpPut("/Cast/{movieId}")]
    public async Task<IActionResult> UpdateCast([FromBody] IEnumerable<NewCast> newCasts, Guid movieId)
    {
        var result = await _castService.UpdateCast(movieId, newCasts);
        return Ok(result);
    }

}
