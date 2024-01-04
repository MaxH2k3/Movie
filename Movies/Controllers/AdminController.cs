using Microsoft.AspNetCore.Mvc;
using Movies.Interface;

namespace Movies.Controllers;

[ApiController]
public class AdminController : Controller
{
    private readonly IMovieRepository _movieService;
    private readonly IUserRepository _userService;

    public AdminController(IMovieRepository movieRepository,
                    IUserRepository userRepository)
    {
        _movieService = movieRepository;
        _userService = userRepository;
    }

    [HttpGet("Admin/Statistics")]
    [ProducesResponseType(typeof(Dictionary<string, int>), StatusCodes.Status200OK)]
    public IActionResult Statistics()
    {
        var result = _movieService.GetStatistic();
        result.Add("Account", _userService.GetUsers().Count());
        return Ok(result);
    }
}
