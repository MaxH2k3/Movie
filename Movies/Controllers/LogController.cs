using Microsoft.AspNetCore.Mvc;
using Movies.Utilities;
using Serilog;

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
        Log.Information("GetLogs");
        Log.CloseAndFlush();
        var logFile = System.IO.File.ReadAllText("logs/log20240121.txt");
        return Ok(logFile);
    }

}
