using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movies.Business.globals;
using Movies.Business.movies;
using Movies.Interface;
using Movies.Models;
using Movies.Utilities;
using System.Net;

namespace Movies.Repository;

public class MovieService : IMovieService
{
    private readonly MOVIESContext _context;
    private readonly IMapper _mapper;

    public MovieService(MOVIESContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public MovieService(IMapper mapper)
    {
        _context = new MOVIESContext();
        _mapper = mapper;
    }

    public IEnumerable<Movie> GetMovies(string? status = null, bool deleted = false)
    {
        IEnumerable<Movie> movies = _context.Movies
            .Include(m => m.Nation)
            .Include(m => m.Feature)
            .Include(m => m.Casts).ThenInclude(c => c.Actor)
            .Include(m => m.MovieCategories).ThenInclude(mc => mc.Category);
        if (deleted)
            movies = movies.Where(m => m.DateDeleted != null);
        else if (status == null)
            movies = movies.Where(m => !m.Status.ToLower().Equals(Constraint.StatusMovie.UPCOMING.ToLower()) && m.DateDeleted == null);
        else if (status.Trim().ToLower().Equals(Constraint.StatusMovie.ALL_STATUS.ToLower()))
            movies = movies.Where(m => m.DateDeleted == null);
        else if (status != null)
            movies = movies.Where(m => m.Status.ToLower().Equals(status.ToLower()) && m.DateDeleted == null);
        
        return movies;
    }

    public Movie? GetMovieById(Guid id, string? status = null)
    {
        return _context.Movies
            .Include(m => m.Nation)
            .Include(m => m.Feature)
            .Include(m => m.Casts).ThenInclude(c => c.Actor)
            .Include(m => m.MovieCategories).ThenInclude(mc => mc.Category)
            .FirstOrDefault(m => m.MovieId.Equals(id) && m.DateDeleted == null);
    }

    public IEnumerable<Movie>? GetMovieByName(string name, string status)
    {
        return GetMovies(status).Where(
            m => m.VietnamName.ToLower().Contains(name) || m.EnglishName.ToLower().Contains(name))?
            .OrderByDescending(m => m.DateCreated)
            .ToList();
    }

    public IEnumerable<Movie> GetRecentUpdateMovies(int featureId, string status)
    {
        if(featureId == 0)
        {
            return GetMovies(status).OrderByDescending(m => m.DateCreated).Take(8).ToList();
        }
        return GetMovies().Where(m => m.FeatureId == featureId).OrderByDescending(m => m.DateCreated).Take(8).ToList();
    }

    public IEnumerable<Movie> GetMovieByCategory(int categoryId, string status)
    {
        return GetMovies().Where(m => m.MovieCategories.Any(mc => mc.CategoryId == categoryId)).OrderByDescending(m => m.DateCreated).ToList();
    }

    public IEnumerable<Movie> GetMovieByActor(string actorId, string status)
    {
        return GetMovies(status).Where(m => 
                m.Casts.Any(c => c.ActorId.Equals(new Guid(actorId)) &&
                c.Actor.Role.ToLower().Equals(Constraint.RolePerson.ACTOR.ToLower())))
                .OrderByDescending(m => m.DateCreated).ToList();
    }

    public IEnumerable<Movie> GetMovieByProducer(string producerId, string status)
    {
        return GetMovies(status).Where(m => 
                m.Casts.Any(c => c.ActorId.Equals(new Guid(producerId)) && 
                c.Actor.Role.ToLower().Equals(Constraint.RolePerson.PRODUCER.ToLower())))
                .OrderByDescending(m => m.DateCreated).ToList();
    }

    public IEnumerable<Movie> GetMovieByFeature(int featureId, string status)
    {
        return GetMovies(status).Where(m => m.FeatureId == featureId).OrderByDescending(m => m.DateCreated).ToList();
    }

    public async Task<Movie?> GetMovieNewest()
    {
        return await _context.Movies
            .Include(m => m.MovieCategories).ThenInclude(c => c.Category)
            .Where(m => m.DateDeleted == null && !m.Status.Equals(Constraint.StatusMovie.UPCOMING))
            .OrderByDescending(m => m.ProducedDate)
            .FirstOrDefaultAsync();
    }

    public async Task<Movie?> GetMovieTopRating()
    {
        return await _context.Movies
            .Include(m => m.MovieCategories).ThenInclude(c => c.Category)
            .Where(m => m.DateDeleted == null && !m.Status.Equals(Constraint.StatusMovie.UPCOMING))
            .OrderByDescending(m => m.Mark)
            .FirstOrDefaultAsync();
    }

    public async Task<Movie?> GetMovieTopViewer()
    {
        return await _context.Movies
            .Include(m => m.MovieCategories).ThenInclude(c => c.Category)
            .Where(m => m.DateDeleted == null && !m.Status.Equals(Constraint.StatusMovie.UPCOMING))
            .OrderByDescending(m => m.Viewer)
            .FirstOrDefaultAsync();
    }

    public IEnumerable<Movie> GetMovieByNation(string nationId, string status)
    {
        return GetMovies(status).Where(m => m.NationId.Equals(nationId.Trim().ToUpper())).OrderByDescending(m => m.DateCreated).ToList();
    }

    public async Task<ResponseDTO> CreateMovie(NewMovie newMovie)
    {
        if(CheckExistMovie(newMovie.EnglishName.ToLower(), newMovie.VietnamName.ToLower(), null))
        {
            return new ResponseDTO(HttpStatusCode.BadRequest, "Movie already exists!");
        }

        newMovie.MovieId = Guid.NewGuid();
        ResponseDTO responseDTO = await validateData(newMovie);
        if(responseDTO.Status != HttpStatusCode.Continue)
        {
            return responseDTO;
        }

        Movie movie = new Movie();
        movie = _mapper.Map<Movie>(newMovie);   
        movie.Status = Constraint.StatusMovie.UPCOMING;
        movie.NationId = movie.NationId?.ToUpper();
        movie.DateCreated = DateTime.Now;

        ResponseDTO response;
        _context.Movies.Add(movie);
        if (await _context.SaveChangesAsync() > 0)
        {
            response = new ResponseDTO(HttpStatusCode.Created, "Create movie successfully!", newMovie.MovieId);
        } else
        {
            response = new ResponseDTO(HttpStatusCode.ServiceUnavailable, "Server error!");
        }

        return response;
    }

    public async Task<ResponseDTO> UpdateMovie(NewMovie newMovie)
    {
        if (CheckExistMovie(newMovie.EnglishName.ToLower(), newMovie.VietnamName.ToLower(), newMovie.MovieId))
        {
            return new ResponseDTO(HttpStatusCode.BadRequest, "Movie already exists!");
        }
        Movie movie = GetMovieById((Guid)newMovie.MovieId);
        DateTime? DateCreated = movie?.DateCreated;
        string? Status = movie?.Status;
        if (movie == null)
        {
            return new ResponseDTO(HttpStatusCode.NotFound, "Movie not found");
        }

        ResponseDTO responseDTO = await validateData(newMovie);
        if (responseDTO.Status != HttpStatusCode.Continue)
        {
            return responseDTO;
        }

        movie = _mapper.Map<Movie>(newMovie);
        movie.NationId = movie.NationId?.ToUpper();
        movie.DateCreated = DateCreated;
        movie.Status = Status;
        movie.DateUpdated = DateTime.Now;

        _context.Movies.Update(movie);
        if (await _context.SaveChangesAsync() > 0)
        {
            return new ResponseDTO(HttpStatusCode.OK, "Update movie successfully!");
        }
        return new ResponseDTO(HttpStatusCode.ServiceUnavailable, "Server error!");
    }

    public async Task<ResponseDTO> DeleteMovie(Guid id)
    {
        Movie? movie = GetMovieById(id);
        if (movie == null)
        {
            return new ResponseDTO(HttpStatusCode.NotFound, "Movie not found!");
        }

        _context.Movies.Remove(movie);

        if (await _context.SaveChangesAsync() > 0)
        {
            return new ResponseDTO(HttpStatusCode.OK, "Remove movie successfully!");
        }
        return new ResponseDTO(HttpStatusCode.ServiceUnavailable, "Server error!");
    }

    private async Task<ResponseDTO> validateData(NewMovie newMovie)
    {

        Nation? nation = await _context.Nations.FindAsync(newMovie.NationId);
        if (nation == null)
        {
            return new ResponseDTO(HttpStatusCode.NotFound, "Nation not found");
        }

        FeatureFilm? feature = await _context.FeatureFilms.FindAsync(newMovie.FeatureId);
        if (feature == null)
        {
            return new ResponseDTO(HttpStatusCode.NotFound, "Feature Film not found");
        }

        return new ResponseDTO(HttpStatusCode.Continue, "Validate successfully!", "");
    }

    public bool CheckExistMovie(string englishName, string vietNamName, Guid? id)
    {   
        if(id == null)
        {
            return _context.Movies.Any(m => m.EnglishName.ToLower().Equals(englishName) ||
                                m.VietnamName.ToLower().Equals(vietNamName));
        }
        else if(id != null)
        {
            return _context.Movies.Any(m => (m.EnglishName.ToLower().Equals(englishName) ||
                                m.VietnamName.ToLower().Equals(vietNamName)) && !m.MovieId.Equals(id));
        }
        return false;
    }

    public int? GetFeatureIdByMovieId(Guid movieId)
    {
        return GetMovieById(movieId)?.FeatureId;
    }

    public async Task<Dictionary<string, int>> GetStatistic()
    {
        Dictionary<string, int> statistics = new Dictionary<string, int>();
        var movies = _context.Movies;
        statistics.Add(Constraint.StatusMovie.UPCOMING, await movies.CountAsync(m => m.Status.ToLower().Equals(Constraint.StatusMovie.UPCOMING) && m.DateDeleted == null));
        statistics.Add(Constraint.StatusMovie.PENDING, await movies.CountAsync(m => m.Status.ToLower().Equals(Constraint.StatusMovie.PENDING) && m.DateDeleted == null));
        statistics.Add(Constraint.StatusMovie.RELEASE, await movies.CountAsync(m => m.Status.ToLower().Equals(Constraint.StatusMovie.RELEASE) && m.DateDeleted == null));
        statistics.Add(Constraint.StatusMovie.DELETED, await movies.CountAsync(m => m.DateDeleted != null));
        statistics.Add("Account", await _context.Users.CountAsync());
        
        return statistics;
    }

    public async Task<ResponseDTO> UpdateStatusMovie(Guid movieId, string status)
    {
        var movie = await _context.Movies.FirstOrDefaultAsync(m => m.MovieId.Equals(movieId));
        if(movie == null)
        {
            return new ResponseDTO(HttpStatusCode.NotFound, "Movie not found");
        }

        if(status.Trim().ToLower().Equals(Constraint.StatusMovie.DELETED))
        {
            movie.DateDeleted = DateTime.Now;
        } else if(status.Trim().ToLower().Equals(Constraint.StatusMovie.REVERT))
        {
            movie.DateDeleted = null;
        } else
        {
            movie.DateDeleted = null;
            movie.Status = status;
            movie.DateUpdated = DateTime.Now;
        }
        
        _context.Movies.Update(movie);
        if(await _context.SaveChangesAsync() > 0)
        {
            return new ResponseDTO(HttpStatusCode.OK, "Update Successfully!");
        }
        return new ResponseDTO(HttpStatusCode.ServiceUnavailable, "Server error!");
    }

    public IEnumerable<Movie> FilterMovie(string? name, string? status = null)
    {

        IEnumerable<Movie> movies;
        if (status.ToLower().Equals(Constraint.StatusMovie.DELETED.ToLower()))
            movies = GetMovies(null, true);
        else
            movies = GetMovies(status);

        if (name != null)
        {
            movies.Where(m => m.EnglishName.ToLower().Contains(name.ToLower()) ||
                        m.VietnamName.ToLower().Contains(name.ToLower()));
        }
        return movies.ToList();
    }

    public async Task<ResponseDTO> DeleteMovieByStatus(string status)
    {
        IEnumerable<Movie> movies;
        if(status.ToLower().Equals(Constraint.StatusMovie.DELETED.ToLower()))
            movies = _context.Movies.Where(m => m.DateDeleted != null);
        else 
            movies = _context.Movies.Where(m => m.Status.Trim().ToLower().Equals(status.ToLower()) && m.DateDeleted == null);

        if(movies.Count() <= 0)
        {
            return new ResponseDTO(HttpStatusCode.NotFound, "Not Found!");
        }
        
        _context.Movies.RemoveRange(movies);

        if(await _context.SaveChangesAsync() > 0)
        {
            return new ResponseDTO(HttpStatusCode.OK, "Delete successfully!");
        }
        return new ResponseDTO(HttpStatusCode.ServiceUnavailable, "Server error!");
    }

    /*public IEnumerable<Movie> GetMovieRelated(Guid movieId)
    {
        var movie = GetMovieById(movieId);
        var moviescategories = movie.MovieCategories.Select(mc => mc.CategoryId).ToList();
        var movies = GetMovies()
            .Where(m => m.MovieCategories.Any(mc => moviescategories.Contains(mc.CategoryId)))
            .OrderByDescending(m => m.MovieCategories.Count(mc => moviescategories.Contains(mc.CategoryId)));
        return movies;
    }*/

    public IEnumerable<Movie> GetMovieRelated(Guid movieId)
    {
       var movie = _context.Movies.Include(m => m.MovieCategories)
            .FirstOrDefault(m => m.MovieId.Equals(movieId));

        if(movie == null)
        {
            return new List<Movie>();
        }

        var moviescategories = movie.MovieCategories.Select(mc => mc.CategoryId).ToList();

        var query = $@"SELECT m.*
                FROM Movies m
                LEFT JOIN MovieCategory mc ON m.MovieID = mc.MovieID
                WHERE m.MovieID != '{movieId}'
                GROUP BY m.MovieID, m.FeatureId, m.NationID, m.Mark, m.Time, m.Viewer, m.Description, m.EnglishName, 
                m.VietnamName, m.Thumbnail, m.Trailer, m.Status, m.ProducedDate, m.DateCreated, m.DateUpdated, m.DateDeleted
                ORDER BY {(moviescategories.Count > 0 ? $"COUNT(CASE WHEN mc.CategoryID IN ({string.Join(',', moviescategories)}) THEN 1 ELSE NULL END) DESC," : "")}  m.ProducedDate DESC";
        
        var result = _context.Movies.FromSqlRaw(query).ToList();

        return result;
    }

    public async Task<Dictionary<string, int>> GetStatisticFeature()
    {
        Dictionary<string, int> statistics = new Dictionary<string, int>();

        var features = _context.FeatureFilms.ToList();
        var movies = _context.Movies;
        foreach (var item in features)
        {
            statistics.Add(item.Name.Replace(" ", ""), await movies.CountAsync(m => (m.FeatureId == item.FeatureId) && (m.DateDeleted == null)));
        }

        return statistics;
    }

    public async Task<Dictionary<string, int>> GetStatisticCategory()
    {
        Dictionary<string, int> statistics = new Dictionary<string, int>();

        var categories = _context.Categories.ToList();
        var movies = _context.MovieCategories;

        foreach (var item in categories)
        {
            statistics.Add(item.Name, await movies.CountAsync(c => c.CategoryId == item.CategoryId));
        }

        return statistics;
    }
}
