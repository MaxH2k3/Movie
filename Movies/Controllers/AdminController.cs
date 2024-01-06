using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.Business.movies;
using Movies.Interface;
using Movies.Utilities;
using System.Linq;

namespace Movies.Controllers;

[ApiController]
public class AdminController : Controller
{
    private readonly IMovieRepository _movieService;
    private readonly IUserRepository _userService;
    private readonly IMapper _mapper;

    public AdminController(IMovieRepository movieRepository, IMapper mapper,
                    IUserRepository userRepository)
    {
        _movieService = movieRepository;
        _userService = userRepository;
        _mapper = mapper;
    }

    [HttpGet("Admin/Statistics")]
    [ProducesResponseType(typeof(Dictionary<string, int>), StatusCodes.Status200OK)]
    public IActionResult Statistics()
    {
        var result = _movieService.GetStatistic();
        result.Add("Account", _userService.GetUsers().Count());
        return Ok(result);
    }

    [HttpGet("Admin/Movies")]
    [ProducesResponseType(typeof(IEnumerable<MoviePreview>), StatusCodes.Status200OK)]
    public IActionResult Movies(string? name, string? status = null)
    {
        if(status != null && !Constraint.StatusMovie.ALL.Contains(status))
        {
            return BadRequest("Invalid status!");
        }

        var movies = _mapper.Map<IEnumerable<MoviePreview>>(_movieService.FilterMovie(name, status));
        return Ok(movies);
    }

    
}
