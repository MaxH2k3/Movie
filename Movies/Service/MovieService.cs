using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movies.Business.globals;
using Movies.Business.movies;
using Movies.Business.persons;
using Movies.Interface;
using Movies.Models;
using Movies.Service;
using Movies.Utilities;
using NuGet.Packaging;
using System.Diagnostics;
using System.Net;

namespace Movies.Repository
{
    public class MovieService : IMovieRepository
    {
        private readonly MOVIESContext _context;
        private readonly IMapper _mapper;
        private readonly IStorageRepository _storageRepository;

        public MovieService(MOVIESContext context, IMapper mapper, 
            IStorageRepository storageRepository)
        {
            _context = context;
            _mapper = mapper;
            _storageRepository = storageRepository;
        }

        public MovieService(IMapper mapper, IStorageRepository storageRepository)
        {
            _context = new MOVIESContext();
            _mapper = mapper;
            _storageRepository = storageRepository;
        }

        public IEnumerable<Movie> GetMovies(string? status = null)
        {
            IEnumerable<Movie> movies = _context.Movies
                .Include(m => m.Nation)
                .Include(m => m.Feature)
                .Include(m => m.Casts).ThenInclude(c => c.Actor)
                .Include(m => m.MovieCategories).ThenInclude(mc => mc.Category);
            if(status == null)
                movies = movies.Where(m => !m.Status.ToLower().Equals(Constraint.StatusMovie.DELETED.ToLower()));
            else if(status != null)
                movies = movies.Where(m => m.Status.ToLower().Equals(status.ToLower()));
            return movies;
        }

        public Movie? GetMovieById(Guid id)
        {
            return GetMovies().FirstOrDefault(m => m.MovieId.Equals(id));
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

        public Movie? GetMovieNewest()
        {
            return GetMovies().OrderByDescending(m => m.DateCreated).FirstOrDefault();
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
            movie.Thumbnail = responseDTO.Data?.ToString();
            movie.DateCreated = DateTime.Now;

            ResponseDTO response;
            _context.Movies.Add(movie);
            if (await _context.SaveChangesAsync() > 0)
            {
                response = new ResponseDTO(HttpStatusCode.Created, "Create movie successfully!", newMovie.MovieId);
            } else
            {
                return response = new ResponseDTO(HttpStatusCode.ServiceUnavailable, "Server error!");
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
            string? oldThumnail = movie?.Thumbnail;
            int? totalSeasons = movie?.TotalSeasons;
            int? totalEpisodes = movie?.TotalEpisodes;
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
            movie.Thumbnail = (newMovie.Thumbnail != null) ? responseDTO.Data?.ToString() : oldThumnail;
            movie.TotalSeasons = totalSeasons;
            movie.TotalEpisodes = totalEpisodes;
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
            await _storageRepository.DeleteFile(movie.Thumbnail.Replace("https://streamit-movie.azurewebsites.net/file?fileName=", ""));
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

            //upload image
            string? filePath = null;
            string url = "https://streamit-movie.azurewebsites.net/file?fileName=";
            if (newMovie.Thumbnail != null)
            {
                filePath = $"movie/{feature.Name}/{newMovie.MovieId}";
                await _storageRepository.DeleteFile(filePath);
                await _storageRepository.UploadFile(newMovie.Thumbnail, filePath);
            }

            return new ResponseDTO(HttpStatusCode.Continue, "Validate successfully!", url + filePath);
        }

        public bool CheckExistMovie(string englishName, string vietNamName, Guid? id)
        {   
            if(id == null)
            {
                return GetMovies().Any(m => m.EnglishName.ToLower().Equals(englishName) ||
                                    m.VietnamName.ToLower().Equals(vietNamName));
            }
            else if(id != null)
            {
                return GetMovies().Any(m => (m.EnglishName.ToLower().Equals(englishName) ||
                                    m.VietnamName.ToLower().Equals(vietNamName)) && !m.MovieId.Equals(id));
            }
            return false;
        }

        public int? GetFeatureIdByMovieId(Guid movieId)
        {
            return GetMovieById(movieId)?.FeatureId;
        }

        public Dictionary<string, int> GetStatistic()
        {
            Dictionary<string, int> statistics = new Dictionary<string, int>();
            statistics.Add(Constraint.StatusMovie.UPCOMING, GetMovies(Constraint.StatusMovie.UPCOMING).Count());
            statistics.Add(Constraint.StatusMovie.PENDING, GetMovies(Constraint.StatusMovie.PENDING).Count());
            statistics.Add(Constraint.StatusMovie.RELEASE, GetMovies(Constraint.StatusMovie.RELEASE).Count());
            statistics.Add(Constraint.StatusMovie.DELETED, GetMovies(Constraint.StatusMovie.DELETED).Count());
            
            return statistics;
        }

        public async Task<ResponseDTO> UpdateStatusMovie(Guid movieId, string status)
        {
            var movie = GetMovieById(movieId);
            if(movie == null)
            {
                return new ResponseDTO(HttpStatusCode.NotFound, "Movie not found");
            }
            movie.Status = status;
            movie.DateUpdated = DateTime.Now;
            _context.Movies.Update(movie);
            if(await _context.SaveChangesAsync() > 0)
            {
                return new ResponseDTO(HttpStatusCode.OK, "Update Successfully!");
            }
            return new ResponseDTO(HttpStatusCode.ServiceUnavailable, "Server error!");
        }

        public IEnumerable<Movie> FilterMovie(string? name, string? status = null)
        {
            IEnumerable<Movie> movies = GetMovies(status);
            if(name != null)
            {
                movies.Where(m => m.EnglishName.ToLower().Contains(name.ToLower()) ||
                            m.VietnamName.ToLower().Contains(name.ToLower()));
            }
            return movies;
        }
    }
}
