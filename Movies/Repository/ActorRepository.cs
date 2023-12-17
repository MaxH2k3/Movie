using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movies.Business;
using Movies.Interface;
using Movies.Models;
using System.Diagnostics;
using System.Net;

namespace Movies.Repository
{
    public class ActorRepository : IActorRepository
    {
        private readonly MOVIESContext _context;
        private readonly IMapper _mapper;

        public ActorRepository(MOVIESContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ActorRepository(IMapper mapper)
        {
            _context = new MOVIESContext();
            _mapper = mapper;
        }

        public async Task<ResponseDTO> CreateActor(ActorDetail actorDetail)
        {
            Actor actor = new Actor();
            actor = _mapper.Map<Actor>(actorDetail);

            Nation? nation = _context.Nations.Find(actorDetail.NationId);
            if (nation == null)
            {
                return new ResponseDTO(HttpStatusCode.NotFound, "Nation not found");
            }
            
            _context.Actors.Add(actor);
            if (await _context.SaveChangesAsync() > 0)
            {
                return new ResponseDTO(HttpStatusCode.Created, "Create actor successfully");
            }
            return new ResponseDTO(HttpStatusCode.ServiceUnavailable, "Server error!");
        }

        public Actor? GetActor(int id)
        {
            return GetActors().FirstOrDefault(a => a.ActorId == id);
        }

        public IEnumerable<Actor> SearchByName(string name)
        {
            return GetActors().Where(a => a.NameActor.ToLower().Contains(name)).ToList();
        }

        public Actor? GetActorByName(string name)
        {
            return GetActors().FirstOrDefault(a => a.NameActor.ToLower().Equals(name));
        }

        public IEnumerable<Actor> GetActors()
        {
            return _context.Actors.Include(a => a.Nation);
        }

        public async Task<ResponseDTO> UpdateActor(ActorDetail actorDetail)
        {
            Actor? actor = GetActor(actorDetail.ActorId);
            if (actor == null)
            {
                return new ResponseDTO(HttpStatusCode.NotFound, "Actor not found!");
            }

            actor = _mapper.Map<Actor>(actorDetail);

            Nation? nation = _context.Nations.Find(actorDetail.NationId);
            if (nation == null)
            {
                return new ResponseDTO(HttpStatusCode.NotFound, "Nation not found!");
            }

            _context.Actors.Update(actor);
            if (await _context.SaveChangesAsync() > 0)
            {
                return new ResponseDTO(HttpStatusCode.OK, "Update actor successfully!");
            }

            return new ResponseDTO(HttpStatusCode.ServiceUnavailable, "Server error!");
        }

        public async Task<ResponseDTO> DeleteActor(int id)
        {
            Actor? actor = GetActor(id);
            if (actor == null)
            {
                return new ResponseDTO(HttpStatusCode.NotFound, "Actor not found!");
            }

            _context.Actors.Remove(actor);
            if (await _context.SaveChangesAsync() > 0)
            {
                return new ResponseDTO(HttpStatusCode.OK, "Actor delete successfully!");
            }

            return new ResponseDTO(HttpStatusCode.ServiceUnavailable, "Server error!");
        }


    }
}
