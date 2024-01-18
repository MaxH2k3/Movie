using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting.Internal;
using Movies.Configuration;
using Movies.ExceptionHandler;
using Movies.Interface;
using Movies.Models;
using Movies.Repository;
using Movies.Security;
using Movies.Service;
using Serilog;

namespace Movies;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddScoped<GlobalException>();

        builder.Services.AddScoped<IMovieRepository, MovieService>();
        builder.Services.AddScoped<IPersonRepository, PersonService>();
        builder.Services.AddScoped<ICategoryRepository, CategoryService>();
        builder.Services.AddScoped<IFeatureRepository, FeatureService>();
        builder.Services.AddScoped<ISeasonRepository, SeasonService>();
        builder.Services.AddScoped<IEpisodeRepository, EpisodeService>();
        builder.Services.AddScoped<IStorageRepository, StorageService>();
        builder.Services.AddScoped<IUserRepository, UserService>();
        builder.Services.AddScoped<IMovieCategoryRepository, MovieCategoryService>();
        builder.Services.AddScoped<JWTGenerator, JWTConfig>();
        builder.Services.AddScoped<IAuthentication, Authentication>();
        builder.Services.AddScoped<IMailRepository, MailService>();
        builder.Services.AddScoped<INationRepository, NationService>();
        builder.Services.AddScoped<ICastRepository, CastService>();
        builder.Services.AddScoped<GeminiService>();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(
            swagger =>
            {
                swagger.SwaggerDoc("v1", new() { Title = "Movies", Version = "v1" });
                swagger.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Movies.xml"));
            });
        builder.Services.AddCors();

        //Set size limit for request
        builder.Services.Configure<KestrelServerOptions>(options =>
        {
            options.Limits.MaxRequestBodySize = 1073741824; // 1GB
        });

        builder.Services.Configure<FormOptions>(options =>
        {
            options.MultipartBodyLengthLimit = 1073741824; // 1GB
        });

        //Set log file
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration).CreateLogger();

        builder.Host.UseSerilog();

        var app = builder.Build();

        // Configure cors
        app.UseCors(builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseSerilogRequestLogging();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.ConfigureExceptionHandler();

        app.Run();
    }
}