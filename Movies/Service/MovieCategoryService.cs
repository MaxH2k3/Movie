using AutoMapper;
using Movies.Business.globals;
using Movies.Models;
using Movies.Repository;
using System.Net;

namespace Movies.Service;

public class MovieCategoryService : IMovieCategoryRepository
{
    private readonly MOVIESContext _context;
    private readonly IMapper _mapper;

    public MovieCategoryService(MOVIESContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public MovieCategoryService(IMapper mapper)
    {
        _mapper = mapper;
        _context = new MOVIESContext();
    }

    public async Task<ResponseDTO> CreateMovieCategory(Guid movieId, IEnumerable<int> movieCategories)
    {
        MovieCategory movieCategory = new MovieCategory();

        foreach (var categoryId in movieCategories)
        {
            movieCategory.MovieId = movieId;
            movieCategory.CategoryId = categoryId;
            _context.MovieCategories.Add(movieCategory);
            await _context.SaveChangesAsync();
        }
        return new ResponseDTO(HttpStatusCode.OK, "Saving moviecategories succesfully!");
        
    }

    public IEnumerable<MovieCategory> GetMovieCategories(Guid movieId)
    {
        return _context.MovieCategories.Where(x => x.MovieId == movieId).ToList();
    }

    public async Task<ResponseDTO> UpdateMovieCategory(Guid movieId, IEnumerable<int> MovieCategories)
    {
        //get categories of movie
        IEnumerable<MovieCategory> movieCategories = GetMovieCategories(movieId);
        //Refress data

        if(movieCategories.Count() > 0 && !await DeleteMovieCategory(movieCategories))
        {
            return new ResponseDTO(HttpStatusCode.NotModified, "Update Failed");
        }
        IEnumerable<ResponseDTO> responseDTOs = new LinkedList<ResponseDTO>();
        foreach (var categoryId in MovieCategories)
        {
            MovieCategory movieCategory = new MovieCategory();
            movieCategory.MovieId = movieId;
            movieCategory.CategoryId = categoryId;
            if (!movieCategories.Contains(movieCategory))
            {
                _context.MovieCategories.Add(movieCategory);
                if (await _context.SaveChangesAsync() == 0)
                {
                    responseDTOs.Append(new ResponseDTO(HttpStatusCode.NotModified, "Update Failed", movieCategory.CategoryId));
                }
            }
        }

        if (responseDTOs.Count() > 0)
        {
            return new ResponseDTO(HttpStatusCode.NotModified, "One or more error!", responseDTOs);
        }

        return new ResponseDTO(HttpStatusCode.OK, "Update Successfully");

    }

    public async Task<bool> DeleteMovieCategory(IEnumerable<MovieCategory> categories)
    {
        _context.MovieCategories.RemoveRange(categories);
        if(await _context.SaveChangesAsync() > 0)
        {
            return true;
        }
        return false;
    }
}
