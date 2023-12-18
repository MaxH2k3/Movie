using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.Business;
using Movies.Interface;
using Movies.Models;
using Movies.Repository;
using System.Diagnostics;
using System.Net;
using System.Xml.Linq;

namespace Movies.Controllers;

[ApiController]
public class ActorController : Controller
{
    private readonly IActorRepository _actorRepository;
    private readonly IMapper _mapper;

    public ActorController(IActorRepository actorRepository,
                   IMapper mapper)
    {
        _actorRepository = actorRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Get actors by filter
    /// </summary>
    /// <param name="key">
    ///     <para>Existed value: search by name actor</para>
    ///     <para>Empty value: get all actors</para>
    /// </param>
    /// <returns></returns>

    [HttpGet("Actors")]
    [ProducesResponseType(typeof(IEnumerable<ActorDetail>), StatusCodes.Status200OK)]
    public IActionResult GetActors(string? key, int page = 1, int eachPage = 6)
    {
        IEnumerable<ActorDetail>? actors;
        if (!String.IsNullOrEmpty(key))
        {
            actors = _mapper.Map<IEnumerable<ActorDetail>>(_actorRepository.SearchByName(key.Trim().ToLower()));
        } else
        {
            actors = _mapper.Map<IEnumerable<ActorDetail>>(_actorRepository.GetActors());
        }
        
        actors = actors.Skip((page - 1) * eachPage).Take(eachPage);
        return Ok(actors);
    }

    [HttpGet("Actor/{ActorId}")]
    [ProducesResponseType(typeof(ActorDetail), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public IActionResult GetActor(int ActorId)
    {
        var actor = _mapper.Map<ActorDetail>(_actorRepository.GetActor(ActorId));
        if (actor == null)
        {
            return NotFound("Actor not found!");
        }
        
        return Ok(actor);
    }

    [HttpPost("Actor")]
    public async Task<IActionResult> CreateActor([FromBody] ActorDetail actorDetail)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest("Invalid data!");
        }
        ResponseDTO response = await _actorRepository.CreateActor(actorDetail);
        if(response.Status == HttpStatusCode.Created)  
        {
            return Ok(response.Message);
        }
        return BadRequest(response.Message);
    }

    [HttpPut("Actor")]
    public async Task<IActionResult> UpdateActor([FromBody] ActorDetail actorDetail)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid data!");
        }
        ResponseDTO response = await _actorRepository.UpdateActor(actorDetail);
        if (response.Status == HttpStatusCode.OK)
        {
            return Ok(response.Message);
        }
        return BadRequest(response.Message);
    }

    [HttpDelete("Actor/{id}")]
    public async Task<IActionResult> DeleteActor(int id)
    {
        ResponseDTO response = await _actorRepository.DeleteActor(id);
        if (response.Status == HttpStatusCode.OK)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

}
