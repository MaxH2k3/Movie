using Microsoft.AspNetCore.Mvc;
using Movies.Interface;

namespace Movies.Controllers;

[ApiController]
public class FeatureController : Controller
{
    private readonly IFeatureRepository _featureRepository;

    public FeatureController(IFeatureRepository featureRepository)
    {
        _featureRepository = featureRepository;
    }

    [HttpGet("Features")]
    public IActionResult GetFeatures()
    {
        var features = _featureRepository.GetFeatures();
        return Ok(features);
    }
}
