using Microsoft.AspNetCore.Mvc;
using Movies.Business.globals;
using Movies.Business.seasons;
using Movies.Interface;
using System.Net;

namespace Movies.Controllers;

[ApiController]
public class EpisodeController : Controller
{

    private readonly IEpisodeRepository _episodeRepository;

    public EpisodeController(IEpisodeRepository episodeRepository)
    {
        _episodeRepository = episodeRepository;
    }

    [HttpPost("episode")]
    [ProducesResponseType(typeof(IEnumerable<ResponseDTO>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateEpisode(IEnumerable<NewEpisode> newEpisodes)
    {
        var response = await _episodeRepository.CreateEpisodes(newEpisodes);
        if(response.Status == HttpStatusCode.Created)
        {
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

    [HttpPut("episode/{episodeId}")]
    [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateEpisode(NewEpisode newEpisode, Guid episodeId)
    {
        var response = await _episodeRepository.UpdateEpisode(newEpisode, episodeId);
        if (response.Status == HttpStatusCode.OK)
        {
            return Ok(response.Message);
        }
        return BadRequest(response);
    }


}
