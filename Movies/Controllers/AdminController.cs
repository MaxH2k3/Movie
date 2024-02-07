using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.Business.persons;
using Movies.Configuration;
using Movies.Interface;
using Movies.Repository;
using Movies.Service;
using Movies.Utilities;
using Quartz;
using Quartz.Impl;

namespace Movies.Controllers;

[ApiController]
public class AdminController : Controller
{
    private readonly IMovieRepository _movieService;
    private readonly IQuartzRepository _quartzService;

    public AdminController(IMovieRepository movieRepository, IQuartzRepository quartzRepository)
    {
        _movieService = movieRepository;
        _quartzService = quartzRepository;
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
    
    [HttpGet("Admin/Job")]
    public async Task<IActionResult> GetCurrentJob()
    {
        var jobDetails = await _quartzService.GetCurrentJob();
        return Ok(jobDetails);
    }

    [HttpPut("Admin/Job")]
    public async Task<IActionResult> UpdateJob(int time, string action)
    {
        
        var result = await _quartzService.ControlTask(time, action);
        return Ok(result);
    }
    
    [HttpGet("Admin/ExecuteJob")]
    public async Task<IActionResult> ExecuteJob()
    {
        var result = await _quartzService.ExecuteJob();
        return Ok(result);
    }

}
