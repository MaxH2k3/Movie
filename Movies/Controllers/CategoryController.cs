using Microsoft.AspNetCore.Mvc;
using Movies.Business.persons;
using Movies.Interface;
using Movies.Repository;

namespace Movies.Controllers;

[ApiController]
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryRepository;

    public CategoryController(ICategoryService categoryRepository)
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
