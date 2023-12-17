using Movies.Business;
using Movies.Models;

namespace Movies.Interface
{
    public interface IActorRepository
    {
        IEnumerable<Actor> GetActors();
        Actor? GetActor(int id);
        Actor? GetActorByName(string name);
        IEnumerable<Actor> SearchByName(string name);
        Task<ResponseDTO> CreateActor(ActorDetail actorDetail);
        Task<ResponseDTO> UpdateActor(ActorDetail actorDetail);
        Task<ResponseDTO> DeleteActor(int id);
    }
}
