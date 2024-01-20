using Movies.Business.globals;
using Movies.Business.persons;
using Movies.Models;
using Movies.Repository;
using System.Net;

namespace Movies.Service;

public class CastService : ICastRepository
{
    private readonly MOVIESContext _context;

    public CastService(MOVIESContext context)
    {
        _context = context;
    }

    public CastService()
    {
        _context = new MOVIESContext();
    }

    public async Task<ResponseDTO> CreateCast(Guid movieId, IEnumerable<NewCast> newCasts)
    {
        LinkedList<Cast> casts = new LinkedList<Cast>();
        ResponseDTO responseDTO = CheckExist(movieId, newCasts);

        if(responseDTO.Status != HttpStatusCode.OK)
        {
            return responseDTO;
        }

        foreach (var cast in newCasts)
        {
            casts.AddLast(new Cast()
            {
                MovieId = movieId,
                ActorId = cast.PersonId,
                CharacterName = cast.CharacterName,
            });
        }
        
        _context.Casts.AddRange(casts);
        if(await _context.SaveChangesAsync() > 0)
        {
            return new ResponseDTO(HttpStatusCode.Created, "Cast Created Successfully");
        }

        return new ResponseDTO(HttpStatusCode.ServiceUnavailable, "Cast Created Failed");
    }

    public Task<ResponseDTO> DeleteCast(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseDTO> UpdateCast(Guid movieId, IEnumerable<NewCast> newCasts)
    {
        LinkedList<Cast> casts = new LinkedList<Cast>();

        foreach (var cast in newCasts)
        {
            casts.AddLast(new Cast()
            {
                MovieId = movieId,
                ActorId = cast.PersonId,
                CharacterName = cast.CharacterName,
            });
        }

        //get cast by movieId
        var existingCasts = _context.Casts.Where(c => c.MovieId == movieId).ToList();

        //delete cast not in newCasts
        var castsToDelete = existingCasts.Where(ec => !casts.Any(c => c.ActorId == ec.ActorId)).ToList();
        _context.Casts.RemoveRange(castsToDelete);

        //update cast in newCasts
        _context.Casts.UpdateRange(casts);

        if(await _context.SaveChangesAsync() > 0)
        {
            return new ResponseDTO(HttpStatusCode.OK, "Cast Updated Successfully");
        }

        return new ResponseDTO(HttpStatusCode.ServiceUnavailable, "Cast Updated Failed");
    }

    private ResponseDTO CheckExist(Guid movieId, IEnumerable<NewCast> newCasts)
    {
        //check exist movie
        var movie = _context.Movies.Where(c => c.MovieId == movieId);
        if(movie == null) {
            return new ResponseDTO(HttpStatusCode.NotFound, "Movie Not Exist", movieId); 
        }
        //check exist person in cast of movie
        var casts = _context.Casts.Where(c => c.MovieId == movieId).ToList();
        foreach (var item in newCasts)
        {
            if(casts.Any(c => c.ActorId.Equals(item.PersonId)))
            {
                return new ResponseDTO(HttpStatusCode.Conflict, "Cast Already Exist", item);
            }
        }
        return new ResponseDTO(HttpStatusCode.OK, "Cast Not Exist");
    }

}
