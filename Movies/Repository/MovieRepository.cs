using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movies.Business.globals;
using Movies.Business.movies;
using Movies.Business.persons;
using Movies.Interface;
using Movies.Models;
using Movies.Utilities;
using System.Diagnostics;
using System.Net;

namespace Movies.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MOVIESContext _context;
        private readonly IMapper _mapper;
        private readonly IStorageRepository _storageRepository;

        public MovieRepository(MOVIESContext context, IMapper mapper, IStorageRepository storageRepository)
        {
            _context = context;
            _mapper = mapper;
            _storageRepository = storageRepository;
        }

        public MovieRepository(IMapper mapper)
        {
            _context = new MOVIESContext();
            _mapper = mapper;
            _storageRepository = new StorageRepository();
        }

        public IEnumerable<Movie> GetMovies()
        {
            return _context.Movies
                .Include(m => m.Nation)
                .Include(m => m.Feature)
                .Include(m => m.Producer)
                .Include(m => m.Casts).ThenInclude(c => c.Actor)
                .Include(m => m.MovieCategories).ThenInclude(mc => mc.Category);
        }

        public Movie? GetMovieById(Guid id)
        {
            return GetMovies().FirstOrDefault(m => m.MovieId.Equals(id));
        }

        public IEnumerable<Movie>? GetMovieByName(string name)
        {
            return GetMovies().Where(
                m => m.VietnamName.ToLower().Contains(name) || m.EnglishName.ToLower().Contains(name))?
                .OrderByDescending(m => m.DateCreated)
                .ToList();
        }

        public IEnumerable<Movie> GetRecentUpdateMovies(int featureId)
        {
            if(featureId == 0)
            {
                return GetMovies().OrderByDescending(m => m.DateUpdated).Take(8).ToList();
            }
            return GetMovies().Where(m => m.FeatureId == featureId).OrderByDescending(m => m.DateUpdated).Take(8).ToList();
        }

        public IEnumerable<Movie> GetMovieByCategory(int categoryId)
        {
            return GetMovies().Where(m => m.MovieCategories.Any(mc => mc.CategoryId == categoryId)).OrderByDescending(m => m.DateCreated).ToList();
        }

        public IEnumerable<Movie> GetMovieByActor(string actorId)
        {
            return GetMovies().Where(m => m.Casts.Any(c => c.ActorId.Equals(new Guid(actorId)))).OrderByDescending(m => m.DateCreated).ToList();
        }

        public IEnumerable<Movie> GetMovieByProducer(string producerId)
        {
            return GetMovies().Where(m => m.ProducerId.Equals(new Guid(producerId))).OrderByDescending(m => m.DateCreated).ToList();
        }

        public IEnumerable<Movie> GetMovieByFeature(int featureId)
        {
            return GetMovies().Where(m => m.FeatureId == featureId).OrderByDescending(m => m.DateCreated).ToList();
        }

        public Movie? GetMovieNewest()
        {
            return GetMovies().OrderByDescending(m => m.DateCreated).FirstOrDefault();
        }

        public IEnumerable<Movie> GetMovieByNation(string nationId)
        {
            return GetMovies().Where(m => m.NationId.Equals(nationId.Trim().ToUpper())).OrderByDescending(m => m.DateCreated).ToList();
        }

        public async Task<ResponseDTO> CreateMovie(NewMovie newMovie)
        {
            newMovie.MovieId = Guid.NewGuid();
            ResponseDTO responseDTO = await validateData(newMovie);
            if(responseDTO.Status != HttpStatusCode.Continue)
            {
                return responseDTO;
            }

            Movie movie = new Movie();
            movie = _mapper.Map<Movie>(newMovie);   
            movie.Status = Constraint.StatusMovie.PENDING;
            movie.NationId = movie.NationId?.ToUpper();
            movie.Thumbnail = responseDTO.Data?.ToString();
            if(movie.DateCreated == null)
                movie.DateCreated = DateTime.Now;
            

            _context.Movies.Add(movie);
            if (await _context.SaveChangesAsync() > 0)
            {
                return new ResponseDTO(HttpStatusCode.Created, "Create movie successfully!");
            }
            return new ResponseDTO(HttpStatusCode.ServiceUnavailable, "Server error!");
        }

        public async Task<ResponseDTO> UpdateMovie(NewMovie newMovie)
        {
            Movie movie = GetMovieById((Guid)newMovie.MovieId);
            string? oldThumnail = movie?.Thumbnail;
            int? totalSeasons = movie?.TotalSeasons;
            int? totalEpisodes = movie?.TotalEpisodes;
            if(movie == null)
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
            movie.Thumbnail = (newMovie.Thumbnail != null) ? responseDTO.Data?.ToString() : oldThumnail;
            movie.TotalSeasons = totalSeasons;
            movie.TotalEpisodes = totalEpisodes;
            if (movie.DateUpdated == null)
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
            _storageRepository.DeleteFile(movie.Thumbnail);
            if (await _context.SaveChangesAsync() > 0)
            {
                return new ResponseDTO(HttpStatusCode.OK, "Remove movie successfully!");
            }
            return new ResponseDTO(HttpStatusCode.ServiceUnavailable, "Server error!");
        }

        private async Task<ResponseDTO> validateData(NewMovie newMovie)
        {
            Nation? nation = _context.Nations.Find(newMovie.NationId);
            if (nation == null)
            {
                return new ResponseDTO(HttpStatusCode.NotFound, "Nation not found");
            }

            FeatureFilm? feature = _context.FeatureFilms.Find(newMovie.FeatureId);
            if (feature == null)
            {
                return new ResponseDTO(HttpStatusCode.NotFound, "Feature Film not found");
            }

            //upload image
            string? filePath = null;
            if (newMovie.Thumbnail != null)
            {
                filePath = $"movie/{feature.Name}/{newMovie.MovieId}";
                await _storageRepository.UploadFile(newMovie.Thumbnail, filePath);
            }

            return new ResponseDTO(HttpStatusCode.Continue, "Validate successfully!", filePath);
        }

        
    }
}
