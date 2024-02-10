using Microsoft.AspNetCore.Mvc;
using Movies.Repository;
using System.Net;
using Movies.Utilities;

namespace Movies.Controllers;

[ApiController]
public class AnalystController : Controller
{
    private readonly IAnalystRepository _analystService;

    public AnalystController(IAnalystRepository analystRepository)
    {
        _analystService = analystRepository;
    }

    [HttpPost("Analyst/AddViewerMovie")]
    public async Task<IActionResult> AddViewerMovie(Guid movieId)
    {
        var result = await _analystService.AddViewerMovie(movieId);
        return Ok(result);
    }

    [HttpGet("Analyst/GetViewerMovie")]
    public async Task<IActionResult> ConvertToPrevious()
    {
        var movies = await _analystService.GetTopMovies();
        return Ok(movies);
    }
}
