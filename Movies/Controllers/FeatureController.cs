using Microsoft.AspNetCore.Mvc;
using Movies.Interface;

namespace Movies.Controllers;

[ApiController]
public class FeatureController : Controller
{
    private readonly IFeatureService _featureRepository;

    public FeatureController(IFeatureService featureRepository)
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
