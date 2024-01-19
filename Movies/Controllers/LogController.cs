using Microsoft.AspNetCore.Mvc;
using Movies.Utilities;

namespace Movies.Controllers;

[ApiController]
public class LogController : Controller
{
    private readonly ILogger<LogController> _logger;

    public LogController(ILogger<LogController> logger)
    {
        _logger = logger;
    }
}
