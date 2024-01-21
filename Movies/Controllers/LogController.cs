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

    [HttpGet("")]
    public IActionResult GetLogsAsync()
    {
        var logFile = System.IO.File.ReadAllText("logs/log20240121.txt");
        return Ok(logFile);
    }

}
