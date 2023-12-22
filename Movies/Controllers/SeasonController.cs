using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.Business.seasons;
using Movies.Interface;

namespace Movies.Controllers;

[ApiController]
public class SeasonController : Controller
{
    private readonly ISeasonRepository _seasonRepository;
    private readonly IMapper _mapper;

    public SeasonController(ISeasonRepository seasonRepository,
                          IMapper mapper)
    {
        _seasonRepository = seasonRepository;
        _mapper = mapper;
    }

    [HttpGet("Seasons")]
    public IActionResult GetSeasons(Guid movieId)
    {
        var seasons = _mapper.Map<IEnumerable<SeasonDTO>>(_seasonRepository.GetSeasonsByMovie(movieId));

        return Ok(seasons);
    }

}
