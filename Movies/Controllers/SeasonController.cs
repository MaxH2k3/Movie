using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.Business.globals;
using Movies.Business.seasons;
using Movies.Interface;
using System.Net;

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
    [ProducesResponseType(typeof(IEnumerable<SeasonDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IActionResult GetSeasons(Guid movieId, int? seasonNumber)
    {
        IEnumerable<SeasonDTO> seasons = null;

        if (seasonNumber != null)
        {
            seasons = _mapper.Map<IEnumerable<SeasonDTO>>(_seasonRepository.GetSeasonsByMovieAndNumber(movieId, seasonNumber));
        } else if (seasonNumber == null) {
            seasons = _mapper.Map<IEnumerable<SeasonDTO>>(_seasonRepository.GetSeasonsByMovie(movieId));
        } else
        {
            BadRequest("Error!");
        }

        
        return Ok(seasons);
    }

    [HttpPost("Seasons")]
    [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSeason([FromBody] NewSeason newSeason)
    {
        var response = await _seasonRepository.CreateSeason(newSeason);
        if (response.Status == HttpStatusCode.Created)
        {
            return Created(response.Message, response.Data);
        }
        return BadRequest(response);
    }

    [HttpDelete("Seasons/{seasonId}")]
    [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteSeason(Guid seasonId)
    {
        var response = await _seasonRepository.DeleteSeason(seasonId);
        if (response.Status == HttpStatusCode.OK)
        {
            return Ok(response);
        }
        return BadRequest(response.Message);
    }

    [HttpPut("Seasons/{seasonId}")]
    [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateSeason(Guid seasonId, [FromBody] string? name)
    {
        var response = await _seasonRepository.UpdateSeason(name, seasonId);
        if (response.Status == HttpStatusCode.OK)
        {
            return Ok(response);
        }
        return BadRequest(response.Message);
    }
}
