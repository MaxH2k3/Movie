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

    public async Task<ResponseDTO> CreateMovieCategory(Guid movieId, IEnumerable<int> MovieCategories)
    {
        
        MovieCategory movieCategory = new MovieCategory();

        foreach (var categoryId in MovieCategories)
        {
            movieCategory.MovieId = movieId;
            movieCategory.CategoryId = categoryId;
            _context.MovieCategories.Add(movieCategory);
            await _context.SaveChangesAsync();
        }

        return new ResponseDTO(HttpStatusCode.Created, "Create Movie Successfully!", $"MovieId: {movieId}");
    }
}
