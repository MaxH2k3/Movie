using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.Business;
using Movies.Interface;
using Movies.Models;

namespace Movies.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : Controller
{
    private readonly IMovieRepository _movieRepository;
    private readonly IMapper _mapper;

    public MovieController(IMovieRepository movieRepository,
        IMapper mapper)
    {
        _movieRepository = movieRepository;
        _mapper = mapper;
    }

    [HttpGet("GetMovies")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Movie>))]
    public IActionResult GetMovies()
    {
        var movie = _mapper.Map<IEnumerable<MoviePreview>>(_movieRepository.GetMovies());
        return Ok(movie);
    }

    [HttpGet("GetMovie")]
    [ProducesResponseType(200, Type = typeof(Movie))]
    public IActionResult GetName(int id)
    {
        var movie = _mapper.Map<MovieDetail>(_movieRepository.GetMovieById(id));
        return Ok(movie);
    }

    [HttpGet("SearchByName")]
    public IActionResult SearchByName(string name)
    {
        if(!ModelState.IsValid)
        {
            BadRequest("Invalid the name");
        }
        var movie = _mapper.Map<IEnumerable<MoviePreview>>(_movieRepository.GetMovieByName(name.Trim().ToLower()));
        return Ok(movie);
    }

    [HttpGet("RecentUpdate")]
    public IActionResult RecentUpdate(int featureId)
    {
        var movie = _mapper.Map<IEnumerable<MoviePreview>>(_movieRepository.GetRecentUpdateMovies(featureId));
        return Ok(movie);
    }

    [HttpPatch("UpdateMovie")]
    public IActionResult UpdateMovie([FromForm] Movie movie)
    {
        if(!ModelState.IsValid)
        {
            BadRequest("Invalid the movie");
        }
        //if(_movieRepository.UpdateMovie(movie))
        //{
        //    return Ok("Update Sucessfully!");
        //}
        return BadRequest("Update movie failed");
    }

    [HttpPost("CreateMovie")]
    public IActionResult CreateMovie([FromForm] Movie movie)
    {
        if(!ModelState.IsValid)
        {
            BadRequest("Invalid the movie");
        }
        //if(_movieRepository.CreateMovie(movie))
        //{
        //    return Ok("Create Sucessfully!");
        //}
        return BadRequest("Create movie failed");
    }

    [HttpDelete("DeleteMovie")]
    public IActionResult DeleteMovie(int id)
    {
        if(!ModelState.IsValid)
        {
            BadRequest("Invalid the movie");
        }
        //if(_movieRepository.DeleteMovie(id))
        //{
        //    return Ok("Delete Sucessfully!");
        //}
        return BadRequest("Delete movie failed");
    }

}