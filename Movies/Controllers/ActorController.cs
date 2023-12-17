using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.Business;
using Movies.Interface;
using Movies.Models;
using Movies.Repository;
using System.Diagnostics;
using System.Net;
using System.Xml.Linq;

namespace Movies.Controllers
{
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

        [HttpGet("Actors")]
        public IActionResult GetActors()
        {
            var actors = _mapper.Map<IEnumerable<ActorDetail>>(_actorRepository.GetActors());
            return Ok(actors);
        }

        [HttpGet("Actor/{key}")]
        public IActionResult GetActor(string key)
        {
            ActorDetail? actor;
            if(int.TryParse(key, out int id))
            {
                actor = _mapper.Map<ActorDetail>(_actorRepository.GetActor(id));
            } else
            {
                actor = _mapper.Map<ActorDetail>(_actorRepository.GetActorByName(key.Trim().ToLower()));
            }
            
            return Ok(actor);
        }

        [HttpGet("Actor/Search")]
        public IActionResult SearchByName(string name)
        {
            var actors = _mapper.Map<IEnumerable<ActorDetail>>(_actorRepository.SearchByName(name.Trim().ToLower()));
            return Ok(actors);
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
                return Ok(response);
            }
            return BadRequest(response);
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
                return Ok(response);
            }
            return BadRequest(response);
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
}
