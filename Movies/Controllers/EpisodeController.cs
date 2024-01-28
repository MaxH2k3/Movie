using Microsoft.AspNetCore.Mvc;
using Movies.Business.globals;
using Movies.Business.seasons;
using Movies.Interface;
using Movies.Utilities;
using System.Net;

namespace Movies.Controllers;

[ApiController]
public class EpisodeController : Controller
{

    private readonly IEpisodeRepository _episodeRepository;
    private readonly IMovieRepository _movieService;

    public EpisodeController(IEpisodeRepository episodeRepository, IMovieRepository movieRepository)
    {
        _episodeRepository = episodeRepository;
        _movieService = movieRepository;
    }

    [HttpPost("episode")]
    [ProducesResponseType(typeof(IEnumerable<ResponseDTO>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateEpisode(IEnumerable<NewEpisode> newEpisodes, Guid seasonId)
    {
        if (newEpisodes.Count() == 0)
        {
            return BadRequest("No episode to create!");
        }
        var response = await _episodeRepository.CreateEpisodes(newEpisodes, seasonId);
        if(response.Status == HttpStatusCode.Created)
        {
            await _movieService.UpdateStatusMovie((Guid)response.Data, Constraint.StatusMovie.PENDING);
            return Created("Created episode successfully!", response.Data);
        }
        return BadRequest(response);
    }

    [HttpDelete("episode/{episodeId}")]
    [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteEpisode(Guid episodeId)
    {
        var response = await _episodeRepository.DeleteEpisode(episodeId);
        if (response.Status == HttpStatusCode.OK)
        {
            return Ok(response.Message);
        }
        return BadRequest(response);
    }

    [HttpPut("episode/{seasonId}")]
    [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateEpisode(IEnumerable<EpisodeDTO> newEpisodes, Guid seasonId)
    {
        if(newEpisodes.Count() == 0)
        {
            return BadRequest("No episode to update!");
        }
        var response = await _episodeRepository.UpdateEpisodes(seasonId, newEpisodes);
        if (response.Status == HttpStatusCode.OK)
        {
            return Ok(response.Message);
        }
        return BadRequest(response);
    }

}
