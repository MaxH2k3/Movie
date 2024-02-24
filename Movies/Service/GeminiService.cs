using GenerativeAI.Models;
using MongoDB.Bson;
using Movies.Business.movies;
using Movies.DTO.anothers;
using Movies.Interface;
using Movies.Models;
using Movies.Repository;
using NuGet.Protocol;
using System.Text.Json;
using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Driver;
using Movies.Utilities;

namespace Movies.Service;

public class GeminiService : IGeminiService
{
    private GenerativeModel _client;
    private readonly IFeatureService _featureService;
    private readonly ICategoryService _categoryService;
    private readonly INationService _nationService;
    private readonly GeminiMongoContext _context;

    public GeminiService(IFeatureService featureService, ICategoryService categoryService, 
        INationService nationRepository, GeminiMongoContext context)
    {
        _featureService = featureService;
        _categoryService = categoryService;
        _nationService = nationRepository;
        _context = context;
    }

    public GeminiService(IFeatureService featureService, ICategoryService categoryService,
        INationService nationRepository)
    {
        _featureService = featureService;
        _categoryService = categoryService;
        _nationService = nationRepository;
        _context = new GeminiMongoContext();
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

        string result = "";
        for(int i = 0; i < 3; i++)
        {
            var key = await GetGeminiKey();
            if(key == null)
            {
                return "Gemini Key not found";
            }

            _client = new GenerativeModel(key.APIKey);

            try
            {
                result = await _client.GenerateContentAsync(pattern);
                return CleanResult(result);
            } catch (Exception e)
            {
                DeleteGeminiKey(key.APIKey);
            }
        }
        return CleanResult(result);
    }

    public async Task<string> AddGeminiKey(string key)
    {
        GeminiKey geminiKey = new GeminiKey() { 
            APIKey = key,
            DateCreated = Utiles.GetNow()
        };
        await _context.GeminiKeys.InsertOneAsync(geminiKey);
        return "Saved successfully";
    }

    public async Task<string> DeleteGeminiKey(string key)
    {
        var geminiKey = await _context.GeminiKeys.FindOneAndDeleteAsync(gemini => gemini.APIKey.Equals(key));
        
        if(geminiKey == null)
        {
            return "Not found";
        }

        return "Deleted successfully";
    }

    public async Task<GeminiKey> GetGeminiKey()
    {
        var list = await (await _context.GeminiKeys.FindAsync(new BsonDocument())).ToListAsync();
        return list.FirstOrDefault();
    }

    public async Task<IEnumerable<GeminiKey>> GetGeminiKeys()
    {
        return await (await _context.GeminiKeys.FindAsync(new BsonDocument())).ToListAsync();
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
