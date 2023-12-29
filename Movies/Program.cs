using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting.Internal;
using Movies.ExceptionHandler;
using Movies.Interface;
using Movies.Models;
using Movies.Repository;
using Movies.Security;

namespace Movies;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddTransient<GlobalException>();

        builder.Services.AddScoped<IMovieRepository, MovieRepository>();
        builder.Services.AddScoped<IPersonRepository, PersonRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IFeatureRepository, FeatureRepository>();
        builder.Services.AddScoped<ISeasonRepository, SeasonRepository>();
        builder.Services.AddScoped<IEpisodeRepository, EpisodeRepository>();
        builder.Services.AddScoped<IStorageRepository, StorageRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<JWTGenerator, JWTConfig>();
        builder.Services.AddScoped<IAuthentication, Authentication>();

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


        builder.Services.Configure<KestrelServerOptions>(options =>
        {
            options.Limits.MaxRequestBodySize = 1073741824; // 1GB
        });

        builder.Services.Configure<FormOptions>(options =>
        {
            options.MultipartBodyLengthLimit = 1073741824; // 1GB
        });

        var app = builder.Build();

        // configure cors
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

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.ConfigureExceptionHandler();

        app.Run();
    }
}