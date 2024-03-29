using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Movies.Configuration;
using Movies.ExceptionHandler;
using Movies.Interface;
using Movies.Models;
using Movies.Repository;
using Movies.Security;
using Movies.Service;
using Movies.Utilities;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System.Text;
using WatchDog;
using WatchDog.src.Enums;

namespace Movies;

public class Program
{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddScoped<GlobalException>();

        builder.Services.AddScoped<IMovieService, MovieService>();
        builder.Services.AddScoped<IPersonService, PersonService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<IFeatureService, FeatureService>();
        builder.Services.AddScoped<ISeasonService, SeasonService>();
        builder.Services.AddScoped<IEpisodeService, EpisodeService>();
        builder.Services.AddScoped<IStorageService, StorageService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IMovieCategoryService, MovieCategoryService>();
        builder.Services.AddScoped<JWTGenerator, JWTConfig>();
        builder.Services.AddScoped<IAuthentication, Authentication>();
        builder.Services.AddScoped<IMailService, MailService>();
        builder.Services.AddScoped<INationService, NationService>();
        builder.Services.AddScoped<ICastService, CastService>();
        builder.Services.AddScoped<IAnalystService, AnalystService>();
        builder.Services.AddScoped<IIPService, IPService>();
        builder.Services.AddScoped<IGeminiService, GeminiService>();
        builder.Services.AddDbContext<MOVIESContext>();
        builder.Services.AddScoped<IQuartzService, QuartzService>();
        builder.Services.AddQuartzConfig();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddWatchDogServices(opt =>
        {
            opt.IsAutoClear = true;
            opt.ClearTimeSchedule = WatchDogAutoClearScheduleEnum.Quarterly;
            opt.SetExternalDbConnString = builder.Configuration.GetConnectionString("Hangfire");
            opt.DbDriverOption = WatchDogDbDriverEnum.Mongo;
        });

        //set up configuration JWT

        builder.Services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = builder.Configuration["JWTSetting:Issuer"],
                    ValidAudience = builder.Configuration["JWTSetting:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTSetting:Securitykey"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true
                };
            });

        builder.Services.AddAuthorization(opt =>
        {
            opt.AddPolicy(Constraint.RoleUser.USER, policy => policy.RequireClaim("Role", Constraint.RoleUser.USER));
            opt.AddPolicy(Constraint.RoleUser.ADMIN, policy => policy.RequireClaim("Role", Constraint.RoleUser.ADMIN));
        });

        //set up configuration for quartz
        //builder.Services.AddQuartzService();

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

        //inject middleware watch dog logs
        app.UseWatchDogExceptionLogger();
        app.UseWatchDog(opt =>
        {
            opt.WatchPageUsername = "admin";
            opt.WatchPagePassword = "123";
            opt.Blacklist = "Admin/Statistics, Movies, Admin/Statistics, Categories, Features, Chat, Movies/Newest, " +
                            "nations, Persons, Person/{PersonId}, Seasons, User, analyst/GetViewerMovie, Admin/Job, " +
                            "Admin/Categories, Admin/Features";
        });

        app.UseAuthentication();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.ConfigureExceptionHandler();

        app.Run();
    }

}