
using MimeKit;
using Movies.Business.globals;
using Movies.Business.users;
using Movies.Models;
using Movies.Repository;
using Movies.Utilities;
using NuGet.Common;
using Serilog;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace Movies.ExceptionHandler;

public class GlobalException : IMiddleware
{
    class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? Location { get; set; }
        public string? Detail { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }

    private readonly ILogger<GlobalException> _logger;
    private readonly IMailRepository _mailService;

    public GlobalException(ILogger<GlobalException> logger, IMailRepository mailService)
    {
        _logger = logger;
        _mailService = mailService;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            Log.Information($"Client ID: {context.Connection.Id}");
            Log.Information($"Sent at {context.Request.Headers.UserAgent}");
            //Log.Information($"Request starting {context.Request.Protocol} {context.Request.Method} {context.Request.Host}{context.Request.Path}");
            //Log.Information($"Excuting action '{context.GetEndpoint()}'");

            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            await HandleException(context, ex);
        } finally
        {
            //Log.Information($"Request finished {context.Request.Protocol} {context.Request.Method} https://{context.Request.Host}{context.Request.Path} - ContentType={context.Response.ContentType} {context.Response.Headers.AcceptCharset}");
        }
       
    }

    private static Task HandleException(HttpContext context, Exception ex)
    {
        int statusCode = (int)HttpStatusCode.InternalServerError;
        var methodError = ex.TargetSite?.DeclaringType?.FullName;
        var errorResponse = new ErrorResponse()
        {
            StatusCode = statusCode,
            Message = ex.GetType().ToString(),
            Location = (methodError != null ? ("Class: " + methodError + ", ") : "") + "Method: " + ex.TargetSite?.Name,
            Detail = ex.Message,
        };
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        
        return context.Response.WriteAsync(errorResponse.ToString());
    }
}

public static class ExceptionExtention
{
    public static async void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<GlobalException>();
    }
}
