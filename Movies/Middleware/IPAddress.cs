using Movies.ExceptionHandler;

namespace Movies.Middleware
{
    public class IPAddress
    {
        private readonly ILogger<IPAddress> _logger;

        public IPAddress(ILogger<IPAddress> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var ipAddress = context.Connection.RemoteIpAddress;
            _logger.LogInformation($"Request from {ipAddress}");
            await next(context);
        }



    }
}
