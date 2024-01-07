using Microsoft.AspNetCore.Mvc;
using Movies.Models;
using Movies.Repository;

namespace Movies.Controllers;

[ApiController]
public class NationController : Controller
{
    private readonly INationRepository _nationService;

    public NationController(INationRepository nationRepository)
    {
        _nationService = nationRepository;
    }

    [HttpGet("nations")]
    [ProducesResponseType(typeof(IEnumerable<Nation>), StatusCodes.Status200OK)]
    public IEnumerable<Nation> GetNations(int page = 1, int eachPage = 6)
    {
        var nations = _nationService.GetNations();
        if(page != 0)
            nations = nations.Skip((page - 1) * eachPage).Take(eachPage);

        return nations;
    }
}
