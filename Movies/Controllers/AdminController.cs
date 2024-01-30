using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.Business.persons;
using Movies.Interface;
using Movies.Repository;

namespace Movies.Controllers;

[ApiController]
public class AdminController : Controller
{
    private readonly IMovieRepository _movieService;

    public AdminController(IMovieRepository movieRepository)
    {
        _movieService = movieRepository;
    }

    [HttpGet("Admin/Statistics")]
    [ProducesResponseType(typeof(Dictionary<string, int>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Statistics()
    {
        var result = await _movieService.GetStatistic();
        return Ok(result);
    }

    /*[HttpGet("Admin/Movies")]
    [ProducesResponseType(typeof(IEnumerable<MoviePreview>), StatusCodes.Status200OK)]
    public IActionResult Movies(string? name, string? status = null )
    {
        if(status != null && !Constraint.StatusMovie.ALL.Contains(status))
        {
            return BadRequest("Invalid status!");
        }

        var movies = _mapper.Map<IEnumerable<MoviePreview>>(_movieService.FilterMovie(name, status));

        return Ok(movies);
    }*/

    [HttpGet("Admin/Features")]
    public async Task<IActionResult> StatisticFeatures()
    {
        return Ok(await _movieService.GetStatisticFeature());
    }

    [HttpGet("Admin/Categories")]
    public async Task<IActionResult> StatisticCategories()
    {
        return Ok(await _movieService.GetStatisticCategory());
    }

}
