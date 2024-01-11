using AutoMapper;
using GenerativeAI.Models;
using GenerativeAI.Types;
using LLMSharp.Google.Palm;
using LLMSharp.Google.Palm.DiscussService;
using MongoDB.Bson;
using Movies.Business.movies;
using Movies.Interface;
using Movies.Repository;
using NuGet.Protocol;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Movies.Service;

public class GeminiService
{
    private readonly GenerativeModel _client;
    private readonly IFeatureRepository _featureService;
    private readonly ICategoryRepository _categoryService;
    private readonly INationRepository _nationService;

    public GeminiService(IFeatureRepository featureService, ICategoryRepository categoryService, INationRepository nationRepository)
    {
        _featureService = featureService;
        _categoryService = categoryService;
        _nationService = nationRepository;
        _client = new GenerativeModel(GetConnectionString());
    }

    private string GetConnectionString()
    {

        IConfiguration config = new ConfigurationBuilder()

        .SetBasePath(Directory.GetCurrentDirectory())

        .AddJsonFile("appsettings.json", true, true)

        .Build();

        var strConn = config["GeminiAI:APIKey"];

        return strConn;

    }

    public async Task<string> Chat(string content, string nation)
    {
        var features = _featureService.GetFeatures().ToJson();
        var categories = _categoryService.GetCategories().ToJson();
        var nations = _nationService.GetNations().ToJson();

        MovieGemini movieGemini = new MovieGemini();
        var pattern = $"{movieGemini.ToString()} \n\n Give me only one json with that format about film has named \"{content}\" ";
        pattern += (nation != null) ? "produced in " + nation : "";
        pattern += $" using example from FeatureFilm {features} and Category {categories} and Nation {nations}. You should search exactly name";

        Console.WriteLine(pattern);
        var result = await _client.GenerateContentAsync(pattern);

         return CleanResult(result);
    }

    public string CleanResult(string data)
    {
        data = data.Replace("`", "");
        data = data.Replace("json", "");
        return data;
    }

    public bool IsJson(string str)
    {
        try
        {
            JsonDocument.Parse(str);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool CheckNull(string data)
    {
        Regex regex = new Regex("null");
        MatchCollection matches = regex.Matches(data);
        if(matches.Count >= 2)
        {
            return true;
        }
        return false;
    }
}
