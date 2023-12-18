using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.Business;
using Movies.Interface;
using Movies.Models;

namespace Movies.Controllers;

[ApiController]
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

    [HttpGet("Movies")]
    public IActionResult Movies()
    {
        var movie = _mapper.Map<IEnumerable<MoviePreview>>(_movieRepository.GetMovies());
        return Ok(movie);
    }

    [HttpGet("Movie/{MovieId}")]
    public IActionResult Movie(int MovieId)
    {
        var movie = _mapper.Map<MovieDetail>(_movieRepository.GetMovieById(MovieId));
        return Ok(movie);
    }

    [HttpGet("Movie/{MovieName}")]
    public IActionResult SearchByName(string MovieName)
    {
        if(!ModelState.IsValid)
        {
            BadRequest("Invalid the name");
        }
        var movie = _mapper.Map<IEnumerable<MoviePreview>>(_movieRepository.GetMovieByName(MovieName.Trim().ToLower()));
        return Ok(movie);
    }

    [HttpGet("Movie/RecentUpdate")]
    public IActionResult RecentUpdate(int featureId)
    {
        var movie = _mapper.Map<IEnumerable<MoviePreview>>(_movieRepository.GetRecentUpdateMovies(featureId));
        return Ok(movie);
    }

    [HttpPut("Movie/{MovieId}")]
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

    [HttpPost("Movie")]
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

    [HttpDelete("Movie/{id}")]
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