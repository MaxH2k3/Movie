using AutoMapper;
using Movies.Business.globals;
using Movies.Interface;
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
        //refress categories of movie
        _context.MovieCategories.RemoveRange(movieCategories);
        //add new categories of movie
        MovieCategory movieCategory = new MovieCategory();
        foreach (var categoryId in MovieCategories)
        {
            movieCategory.MovieId = movieId;
            movieCategory.CategoryId = categoryId;
            movieCategories.Append(movieCategory);
        }

        _context.MovieCategories.UpdateRange(movieCategories);

        if(await _context.SaveChangesAsync() > 0)
        {
            return new ResponseDTO(HttpStatusCode.OK, "Update Successfully");
        }
        return new ResponseDTO(HttpStatusCode.NotModified, "Update Failed");

    }

}
