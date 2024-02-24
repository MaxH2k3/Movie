using Microsoft.AspNetCore.Mvc;
using Movies.Repository;
using System.Net;
using Movies.Utilities;
using AutoMapper;
using Movies.Business.movies;

namespace Movies.Controllers;

[ApiController]
public class AnalystController : Controller
{
    private readonly IAnalystService _analystService;
    private readonly IMapper _mapper;

    public AnalystController(IAnalystService analystRepository, 
        IMapper mapper)
    {
        _analystService = analystRepository;
        _mapper = mapper;
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
        var result = _mapper.Map<IEnumerable<MoviePreview>>(movies);
        return Ok(result);
    }
}
