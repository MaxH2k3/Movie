using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.Business.globals;
using Movies.Business.movies;
using Movies.Interface;
using Movies.Models;
using Movies.Utilities;
using System.Net;
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

    /// <summary>
    /// Get movies by filter
    /// </summary>
    /// <param name="filterBy">The filter option. Possible values:
    ///    <para>- category: take all movies by category</para>
    ///    <para>- feature: take all movies by feature</para>
    ///    <para>- actor: take all movie cast by actor </para>
    ///    <para>- producer: take all movie published by producer </para>
    ///    <para>- nation: take all movie by nation </para>
    ///    <pra> Get all movies if filterBy is empty </pra>
    /// </param>
    /// <param name="key">The value option. Possible values:
    ///    <para>- recentupdate: featureId (int)</para>
    ///    <para>- category: categoryId (int)</para>
    ///    <para>- empty filterBy: search by englishName and vietnamName (string) </para>
    /// </param>
    /// <returns></returns>

    [HttpGet("Movies")]
    [ProducesResponseType(typeof(IEnumerable<MoviePreview>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IActionResult Movies(string? filterBy, string? key, int page = 1, int eachPage = 6)
    {
        IEnumerable<MoviePreview> movies;
        
        if (Constraint.FilterName.CATEGORY.Equals(filterBy?.Trim().ToLower()))
        {
            if (int.TryParse(key, out int id))
            {
                movies = _mapper.Map<IEnumerable<MoviePreview>>(_movieRepository.GetMovieByCategory(id));
            } else
            {
                return BadRequest("Invalid your key! Key is a categoryId (int)");
            }
        } else if (Constraint.FilterName.FEATURE.Equals(filterBy?.Trim().ToLower()))
        {
            if (int.TryParse(key, out int id))
            {
                movies = _mapper.Map<IEnumerable<MoviePreview>>(_movieRepository.GetMovieByFeature(id));
            } else
            {
                return BadRequest("Invalid your key! Key is a featureId (int)");
            }
        } else if (Constraint.FilterName.NATION.Equals(filterBy?.Trim().ToLower()))
        {
            movies = _mapper.Map<IEnumerable<MoviePreview>>(_movieRepository.GetMovieByNation(key));
        } else if (Constraint.FilterName.ACTOR.Equals(filterBy?.Trim().ToLower()))
        {
            movies = _mapper.Map<IEnumerable<MoviePreview>>(_movieRepository.GetMovieByActor(key));
        } else if (Constraint.FilterName.PRODUCER.Equals(filterBy?.Trim().ToLower()))
        {
            movies = _mapper.Map<IEnumerable<MoviePreview>>(_movieRepository.GetMovieByProducer(key));
        } else if(String.IsNullOrEmpty(filterBy) && !String.IsNullOrEmpty(key))
        {
            movies = _mapper.Map<IEnumerable<MoviePreview>>(_movieRepository.GetMovieByName(key.Trim().ToLower()));
        } else if(String.IsNullOrEmpty(filterBy))
        {
            movies = _mapper.Map<IEnumerable<MoviePreview>>(_movieRepository.GetMovies());
        } else
        {
            return NotFound("Your filter did not existed!");
        }
        movies = movies.OrderByDescending(m => m.DateCreated).Skip((page - 1) * eachPage).Take(eachPage);
        return Ok(movies);
    }

    [HttpGet("Movies/Newest")]
    [ProducesResponseType(typeof(MovieNewest), StatusCodes.Status200OK)]
    public IActionResult MoviesNewest()
    {
        var movies = _mapper.Map<MovieNewest>(_movieRepository.GetMovieNewest());
        return Ok(movies);
    }

    [HttpGet("Movie/{MovieId}")]
    [ProducesResponseType(typeof(MovieDetail), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public IActionResult Movie(Guid MovieId)
    {
        var movie = _mapper.Map<MovieDetail>(_movieRepository.GetMovieById(MovieId));
        if (movie == null)
        {
            return NotFound("The movie did not existed");
        }
        return Ok(movie);
    }

    [HttpPut("Movie")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateMovie([FromForm] NewMovie newMovie)
    {
        if(!ModelState.IsValid)
        {
            BadRequest("Invalid the movie");
        }
        ResponseDTO responseDTO = await _movieRepository.UpdateMovie(newMovie);
        if (responseDTO.Status == HttpStatusCode.OK)
        {
            return Ok("Update Sucessfully!");
        }
        return BadRequest(responseDTO);
    }

    [HttpPost("Movie")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateMovie([FromForm] NewMovie newMovie)
    {
        if(!ModelState.IsValid)
        {
            BadRequest("Invalid the movie");
        }
        ResponseDTO responseDTO = await _movieRepository.CreateMovie(newMovie);
        if(responseDTO.Status == HttpStatusCode.Created)
        {
            return Ok(responseDTO.Message);
        }

        return BadRequest(responseDTO);
    }

    [HttpDelete("Movie/{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteMovie(Guid id)
    {
        if(!ModelState.IsValid)
        {
            BadRequest("Invalid the movie");
        }
        ResponseDTO responseDTO = await _movieRepository.DeleteMovie(id);
        if (responseDTO.Status == HttpStatusCode.OK)
        {
            return Ok("Delete Sucessfully!");
        }
        return BadRequest(responseDTO);
    }
}