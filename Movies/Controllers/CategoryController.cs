using Microsoft.AspNetCore.Mvc;
using Movies.Business.persons;
using Movies.Interface;
using Movies.Repository;

namespace Movies.Controllers;

[ApiController]
public class CategoryController : Controller
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILogger<CategoryController> _logger;

    public CategoryController(ICategoryRepository categoryRepository, ILogger<CategoryController> logger)
    {
        _categoryRepository = categoryRepository;
        _logger = logger;
    }

    [HttpGet("Categories")]
    public IActionResult Categories()
    {
        var categories = _categoryRepository.GetCategories();
        return Ok(categories);
    }

}
