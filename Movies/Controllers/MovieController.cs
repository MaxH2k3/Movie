using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.Business;
using Movies.Interface;
using Movies.Models;
using Movies.Utilities;
using static Movies.Utilities.Constraint;

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
    public IActionResult Movies(string? filterBy, string? key)
    {
        IEnumerable<MoviePreview> movie;
        if (Constraint.FilterName.RECENT_UPDATE.Equals(filterBy?.Trim().ToLower()))
        {
            if (int.TryParse(key, out int id))
            {
                movie = _mapper.Map<IEnumerable<MoviePreview>>(_movieRepository.GetRecentUpdateMovies(id));
            } else
            {
                return BadRequest("Invalid your key! Key is a featureId (int)");
            }
        } else if (Constraint.FilterName.CATEGORY.Equals(filterBy?.Trim().ToLower()))
        {
            if (int.TryParse(key, out int id))
            {
                movie = _mapper.Map<IEnumerable<MoviePreview>>(_movieRepository.GetMovieByCategory(id));
            } else
            {
                return BadRequest("Invalid your key! Key is a categoryId (int)");
            }
        } else if(String.IsNullOrEmpty(filterBy) && !String.IsNullOrEmpty(key))
        {
            movie = _mapper.Map<IEnumerable<MoviePreview>>(_movieRepository.GetMovieByName(key.Trim().ToLower()));
        } else if(String.IsNullOrEmpty(filterBy))
        {
            movie = _mapper.Map<IEnumerable<MoviePreview>>(_movieRepository.GetMovies());
        } else
        {
            return NotFound("Your filter did not existed!");
        }
        
        return Ok(movie);
    }

    [HttpGet("Movie/{MovieId}")]
    public IActionResult Movie(int MovieId)
    {
        var movie = _mapper.Map<MovieDetail>(_movieRepository.GetMovieById(MovieId));
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