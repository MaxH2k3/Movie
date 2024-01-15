using Microsoft.AspNetCore.Mvc;
using Movies.Service;

namespace Movies.Controllers;

[ApiController]
public class CrawlController : Controller
{

    private readonly SeleniumService _seleniumService;

    public CrawlController(SeleniumService seleniumService)
    {
        _seleniumService = seleniumService;
    }

    [HttpPost("/crawl")]
    public IActionResult Crawl([FromBody] IEnumerable<Business.anothers.Cookie> cookies)
    {
        var result = _seleniumService.Login(cookies);
        _seleniumService.GetData();
        return Ok(result);
    }

}
