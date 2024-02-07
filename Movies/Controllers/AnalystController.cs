using Microsoft.AspNetCore.Mvc;
using Movies.Repository;
using System.Net;
using Movies.Utilities;

namespace Movies.Controllers;

[ApiController]
public class AnalystController : Controller
{
    private readonly IAnalystRepository _analystService;
    private readonly IIPService _ipService;

    public AnalystController(IAnalystRepository analystRepository, IIPService ipService)
    {
        _analystService = analystRepository;
        _ipService = ipService;
    }

    [HttpPost("analyst/addViewerMovie")]
    public async Task<IActionResult> AddViewerMovie(Guid movieId)
    {
        await _analystService.AddViewerMovie(movieId);
        return Ok("Record Successfully!");
    }

    [HttpGet("analyst/convertToPrevious")]
    public IActionResult ConvertToPrevious()
    {
        _analystService.ConvertToPrevious();
        return Ok("Record Successfully!");
    }
}
