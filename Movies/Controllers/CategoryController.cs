using Microsoft.AspNetCore.Mvc;
using Movies.Business.persons;
using Movies.Interface;
using Movies.Repository;
using Serilog;

namespace Movies.Controllers;

[ApiController]
public class CategoryController : Controller
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    [HttpGet("Categories")]
    public IActionResult Categories()
    {
        var categories = _categoryRepository.GetCategories();

        return Ok(categories);
    }

}
