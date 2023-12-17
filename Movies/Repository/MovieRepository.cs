using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movies.Business;
using Movies.Interface;
using Movies.Models;
using System.Diagnostics;
using System.Net;

namespace Movies.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MOVIESContext _context;
        private readonly IMapper _mapper;

        public MovieRepository(MOVIESContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public MovieRepository(IMapper mapper)
        {
            _context = new MOVIESContext();
            _mapper = mapper;
        }

        public IEnumerable<Movie> GetMovies()
        {
            return _context.Movies
                .Include(m => m.Nation)
                .Include(m => m.Feature)
                .Include(m => m.Casts).ThenInclude(c => c.Actor)
                .Include(m => m.MovieCategories).ThenInclude(mc => mc.Category);
        }

        public Movie? GetMovieById(int id)
        {
            return GetMovies().FirstOrDefault(m => m.MovieId == id);
        }

        public IEnumerable<Movie> GetMovieByName(string name)
        {
            return GetMovies().Where(
                m => m.VietnamName.ToLower().Contains(name) || m.EnglishName.ToLower().Contains(name))
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

        public async Task<ResponseDTO> CreateMovie(MovieDetail movieDetail)
        {
            Movie movie = new Movie();
            movie = _mapper.Map<Movie>(movieDetail);

            ResponseDTO responseDTO = validateData(movieDetail);
            if(responseDTO.Status != null)
            {
                return responseDTO;
            }

            _context.Movies.Add(movie);
            if (await _context.SaveChangesAsync() > 0)
            {
                return new ResponseDTO(HttpStatusCode.Created, "Create movie successfully!");
            }
            return new ResponseDTO(HttpStatusCode.ServiceUnavailable, "Server error!");
        }

        public async Task<ResponseDTO> UpdateMovie(MovieDetail movieDetail)
        {
            Movie? movie = GetMovieById(movieDetail.MovieId);
            if(movie == null)
            {
                return new ResponseDTO(HttpStatusCode.NotFound, "Movie not found!");
            }

            movie = _mapper.Map<Movie>(movieDetail);

            ResponseDTO responseDTO = validateData(movieDetail);
            if (responseDTO.Status != null)
            {
                return responseDTO;
            }

            _context.Movies.Update(movie);
            if (await _context.SaveChangesAsync() > 0)
            {
                return new ResponseDTO(HttpStatusCode.Created, "Update movie successfully!");
            }
            return new ResponseDTO(HttpStatusCode.ServiceUnavailable, "Server error!");
        }

        public async Task<ResponseDTO> DeleteMovie(int id)
        {
            Movie? movie = GetMovieById(id);
            if (movie == null)
            {
                return new ResponseDTO(HttpStatusCode.NotFound, "Movie not found!");
            }

            _context.Movies.Remove(movie);
            if (await _context.SaveChangesAsync() > 0)
            {
                return new ResponseDTO(HttpStatusCode.Created, "Remove movie successfully!");
            }
            return new ResponseDTO(HttpStatusCode.ServiceUnavailable, "Server error!");
        }

        private ResponseDTO validateData(MovieDetail movieDetail)
        {
            Nation? nation = _context.Nations.Find(movieDetail.NationId);
            if (nation == null)
            {
                return new ResponseDTO(HttpStatusCode.NotFound, "Nation not found");
            }

            FeatureFilm? feature = _context.FeatureFilms.Find(movieDetail.Feature?.FeatureId);
            if (feature == null)
            {
                return new ResponseDTO(HttpStatusCode.NotFound, "Feature Film not found");
            }
            return new ResponseDTO();
        }
    }
}
