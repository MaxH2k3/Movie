using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MongoDB.Driver.Linq;
using Movies.Business.globals;
using Movies.Business.movies;
using Movies.Interface;
using Movies.Models;
using Movies.Repository;
using Movies.Utilities;
using Org.BouncyCastle.Asn1.Ocsp;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Movies.Controllers;

[ApiController]
public class MovieController : Controller
{
    private readonly IMovieService _movieRepository;
    private readonly IMapper _mapper;
    private readonly ISeasonService _seasonService;
    private readonly IMovieCategoryService _movieCategoryService;
    private readonly IEpisodeService _episodeService;

    public MovieController(IMovieService movieRepository,
        IMapper mapper, ISeasonService seasonRepository, 
        IMovieCategoryService movieCategoryRepository, IEpisodeService episodeService)
    {
        _movieRepository = movieRepository;
        _mapper = mapper;
        _seasonService = seasonRepository;
        _movieCategoryService = movieCategoryRepository;
        _episodeService = episodeService;
    }

    /// <summary>
    /// Get movies by filter
    /// </summary>
    /// <param name="filterBy">The filter option. Possible values:
    ///    <para>- category: take all movies by category</para>
    ///    <para>- feature: take all movies by feature</para>
    ///    <para>- actor: take all movie cast by actor </para>
    ///    <para>- producer: take all movie published by producer </para>
    ///    <para>- nation: take all movie by nation </para>
    ///    <para>- recommend: take all movie related to categories </para>
    ///    <para>- Get all movies if filterBy is empty </para>
    /// </param>
    /// <param name="key">The value option. Possible values:
    ///    <para>- recentupdate: featureId (int)</para>
    ///    <para>- category: categoryId (int)</para>
    ///    <para>- empty filterBy: search by englishName and vietnamName (string) </para>
    /// </param>
    /// <param name="status">The value option. Possible values: Upcoming, Pending, Release, All, Deleted
    ///     <para>- Deleted: just take deleted</para>
    ///     <para>- All: take without deleted</para>
    /// </param>
    /// <param name="sortBy">The value option. Possible values: ProducedDate, CreatedDate, DeletedDate
    ///     <para>- ProducedDate: sort by produced date</para>
    ///     <para>- CreatedDate: sort by created date</para>
    ///     <para>- DeletedDate: sort by deleted date</para>
    ///     <para>- Empty sortBy: not sort, default value when get from data</para>
    /// </param>
    /// <returns></returns>

    [HttpGet("Movies")]
    [ProducesResponseType(typeof(IEnumerable<MoviePreview>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IEnumerable<MovieRecommend>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IActionResult Movies(string? filterBy, string? key, string? status, string? sortBy, int page = 1, int eachPage = 6)
    {
        if(status != null && !Constraint.StatusMovie.ALL.Contains(status.Trim().ToLower()))
        {
            return BadRequest("Invalid status!");
        }

        if(sortBy != null && !Constraint.SortName.ALL.Contains(sortBy.Trim().ToLower()))
        {
            return BadRequest("Invalid sort by!");
        }

        IEnumerable<dynamic> movies;

        if (Constraint.FilterName.CATEGORY.Equals(filterBy?.Trim().ToLower()))
        {
            if (int.TryParse(key, out int id))
            {
                movies = _mapper.Map<IEnumerable<MoviePreview>>(_movieRepository.GetMovieByCategory(id, status));
            } else
            {
                return BadRequest("Invalid your key! Key is a categoryId (int)");
            }
        } else if (Constraint.FilterName.FEATURE.Equals(filterBy?.Trim().ToLower()))
        {
            if (int.TryParse(key, out int id))
            {
                movies = _mapper.Map<IEnumerable<MoviePreview>>(_movieRepository.GetMovieByFeature(id, status));
            } else
            {
                return BadRequest("Invalid your key! Key is a featureId (int)");
            }
        } else if (Constraint.FilterName.NATION.Equals(filterBy?.Trim().ToLower()))
        {
            movies = _mapper.Map<IEnumerable<MoviePreview>>(_movieRepository.GetMovieByNation(key, status));
        } else if (Constraint.FilterName.ACTOR.Equals(filterBy?.Trim().ToLower()))
        {
            if (Guid.TryParse(key, out Guid id))
            {
                movies = _mapper.Map<IEnumerable<MoviePreview>>(_movieRepository.GetMovieByActor(key, status));
            } else
            {
                return BadRequest("Invalid your key! Key is a actorId (Guid)");
            }
        } else if (Constraint.FilterName.PRODUCER.Equals(filterBy?.Trim().ToLower()))
        {
            if (Guid.TryParse(key, out Guid id))
            {
                movies = _mapper.Map<IEnumerable<MoviePreview>>(_movieRepository.GetMovieByProducer(key, status));
            } else
            {
                return BadRequest("Invalid your key! Key is a producerId (Guid)");
            }
        } else if (Constraint.FilterName.RECOMMEND.Equals(filterBy?.Trim().ToLower())) {
            if (Guid.TryParse(key, out Guid id))
            {
                movies = _mapper.Map<IEnumerable<MovieRecommend>>(_movieRepository.GetMovieRelated(id));
            } else
            {
                return BadRequest("Invalid your key! Key is a movieId (Guid)");
            }
        } else if (Constraint.FilterName.DELETED.Equals(filterBy?.Trim().ToLower())) {
            movies = _mapper.Map<IEnumerable<MoviePreview>>(_movieRepository.GetMovies(null, true));
        } else if (String.IsNullOrEmpty(filterBy) && !String.IsNullOrEmpty(key))
        {
            movies = _mapper.Map<IEnumerable<MoviePreview>>(_movieRepository.GetMovieByName(key.Trim().ToLower(), status));
        } else if (String.IsNullOrEmpty(filterBy))
        {
            movies = _mapper.Map<IEnumerable<MoviePreview>>(_movieRepository.GetMovies(status));
        } else
        {
            return NotFound("Your filter did not existed!");
        }

        Response.Headers.Add("X-Total-Element", movies.Count().ToString());
        Response.Headers.Add("X-Total-Page", Math.Ceiling((double)movies.Count() / eachPage).ToString());
        Response.Headers.Add("X-Current-Page", page.ToString());

        if (Constraint.SortName.PRODUCED_DATE.Equals(sortBy?.Trim().ToLower()))
        {
            movies = movies.OrderByDescending(m => m.ProducedDate).Skip((page - 1) * eachPage).Take(eachPage);
        } else if (Constraint.SortName.DELETED_DATE.Equals(sortBy?.Trim().ToLower())) {
            movies = movies.OrderByDescending(m => m.DateDeleted).Skip((page - 1) * eachPage).Take(eachPage);
        } else if(Constraint.SortName.CREATED_DATE.Equals(sortBy?.Trim().ToLower()))
        {
            movies = movies.OrderByDescending(m => m.DateCreated).Skip((page - 1) * eachPage).Take(eachPage);
        } else
        {
            movies = movies.Skip((page - 1) * eachPage).Take(eachPage);
        }
        
        return Ok(movies);
    }

    

    [HttpGet("Movies/Newest")]
    [ProducesResponseType(typeof(MovieNewest), StatusCodes.Status200OK)]
    public async Task<IActionResult> MoviesNewest()
    {
        int number = Utiles.RandomNumber(1, 4);
        MovieNewest movie;
        if (number == 1)
        {
            movie = _mapper.Map<MovieNewest>(await _movieRepository.GetMovieNewest());
            movie.Tag = "Newest Movie";
        } else if(number == 2)
        {
            movie = _mapper.Map<MovieNewest>(await _movieRepository.GetMovieTopRating());
            movie.Tag = "Top Rating";
        } else
        {
            movie = _mapper.Map<MovieNewest>(await _movieRepository.GetMovieTopViewer());
            movie.Tag = "Top Views";
        }
        
        return Ok(movie);
    }

    [HttpGet("Movie/{MovieId}")]
    [ProducesResponseType(typeof(MovieDetail), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public IActionResult Movie(Guid MovieId)
    {
        var movie = _mapper.Map<MovieDetail>(_movieRepository.GetMovieById(MovieId, Constraint.StatusMovie.ALL_STATUS));
        if (movie == null)
        {
            return NotFound("The movie did not existed");
        }

        movie.TotalSeasons = _seasonService.GetTotalSeason(MovieId);
        movie.TotalEpisodes = _episodeService.GetTotalEpisodes(MovieId);

        return Ok(movie);
    }

    [HttpPut("Movie")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateMovie([FromForm] NewMovie newMovie)
    {
        if(!ModelState.IsValid)
        {
            BadRequest("Invalid the movie");
        }
        ResponseDTO responseDTO = await _movieRepository.UpdateMovie(newMovie);
        if (responseDTO.Status == HttpStatusCode.OK)
        {
            if (newMovie.Categories.Count() > 0)
            {
                responseDTO = await _movieCategoryService.UpdateMovieCategory((Guid)newMovie.MovieId, newMovie.Categories);
            }
            if(responseDTO.Status == HttpStatusCode.OK)
            {
                return Ok("Update Sucessfully!");
            }
        }
        return BadRequest(responseDTO);
    }

    [HttpPost("Movie")]
    [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateMovie([FromForm] NewMovie newMovie)
    {
        if(!ModelState.IsValid)
        {
            BadRequest("Invalid the movie");
        }

        ResponseDTO responseDTO = await _movieRepository.CreateMovie(newMovie);

        if(responseDTO.Status == HttpStatusCode.Created)
        {
            if(newMovie.Categories.Count() > 0)
            {
                await _movieCategoryService.CreateMovieCategory((Guid)responseDTO.Data, newMovie.Categories);
                return Created(responseDTO.Message, responseDTO.Data);
            }
        }

        return BadRequest(responseDTO);
    }

    [HttpDelete("Movie/{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteMovie(Guid id, Boolean? permanently)
    {
        if(!ModelState.IsValid)
        {
            BadRequest("Invalid the movie");
        }
        ResponseDTO responseDTO;
        if (permanently == null || !(bool)permanently)
        {
            responseDTO = await _movieRepository.UpdateStatusMovie(id, Constraint.StatusMovie.DELETED);
        } else
        {
            responseDTO = await _seasonService.DeleteSeasonByMovie(id);
            if(responseDTO.Status == HttpStatusCode.OK || responseDTO.Status == HttpStatusCode.NotFound)
            {
                responseDTO = await _movieRepository.DeleteMovie(id);
            } else
            {
                return BadRequest(responseDTO);
            }
            
        }

        if (responseDTO.Status == HttpStatusCode.OK)
        {
            return Ok("Delete Sucessfully!");
        }
        return BadRequest(responseDTO);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="movieId"></param>
    /// <param name="status">The filter option. Possible values: Upcoming, Pending, Release, Revert
    /// <para>Revert: revert previous data</para>
    /// </param>
    /// <returns></returns>

    [HttpPatch("Movie/{movieId}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateStatusMovie(Guid movieId, string status)
    {
        if(!Constraint.StatusMovie.FILTER.Contains(status.Trim().ToLower()))
        {
            return BadRequest("Invalid status!");
        }

        ResponseDTO responseDTO = await _movieRepository.UpdateStatusMovie(movieId, status);
        if (responseDTO.Status == HttpStatusCode.OK)
        {
            return Ok("Update Sucessfully!");
        }
        return BadRequest(responseDTO);
    }

    /// <summary>
    /// Delete all movie have status. Movie will be deleted permanently include season and episode
    /// </summary>
    /// <param name="status">Upcoming, Pending, Release, Deleted</param>
    /// <returns></returns>

    [HttpDelete("Movies")]
    public async Task<IActionResult> DeleteMovieByStatus([Required] string status)
    {
        var movies = _movieRepository.FilterMovie(null, status);
        if(movies.Count() <= 0)
        {
            return NotFound("Not Found any movie have status " + status);
        }

        foreach (var movie in movies)
        {
            await _seasonService.DeleteSeasonByMovie(movie.MovieId);
        }

        foreach (var item in movies)
        {
            await _movieCategoryService.DeleteCategoryByMovie(item.MovieId);
        }

        var response = await _movieRepository.DeleteMovieByStatus(status);

        if(response.Status == HttpStatusCode.ServiceUnavailable)
        {
            return BadRequest(response);
        }

        return Ok("Delete Sucessfully!");
    }

}