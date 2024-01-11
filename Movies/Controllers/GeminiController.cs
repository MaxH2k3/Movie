using AutoMapper;
using GenerativeAI.Models;
using Microsoft.AspNetCore.Mvc;
using Movies.Business.anothers;
using Movies.Business.persons;
using Movies.Interface;
using Movies.Models;
using Movies.Repository;
using NuGet.Protocol;

namespace Movies.Controllers;

[ApiController]
public class GeminiController : Controller
{

    private readonly string apiKey = "AIzaSyCZCmjVY-awrPmxPDsfiE9Mi40CKs1HCnc";
    private readonly IPersonRepository _personService;
    private readonly IMapper _mapper;
    private readonly IFeatureRepository _featureService;
    private readonly ICategoryRepository _categoryService;

    public GeminiController(IPersonRepository personRepository, IMapper mapper, 
        IFeatureRepository featureService, ICategoryRepository categoryService)
    {
        _personService = personRepository;
        _mapper = mapper;
        _featureService = featureService;
        _categoryService = categoryService;
    }

    [HttpPost("Chat")]
    public async Task<IActionResult> Chat(string content)
    {
        var model = new GenerativeModel(apiKey);
        var persons = _mapper.Map<IEnumerable<PersonAI>>(_personService.GetActos()).ToJson();
        var features = _featureService.GetFeatures().ToJson();
        var categories = _categoryService.GetCategories().ToJson();

        var text = $"\"CREATE TABLE [dbo].[Movies](\r\n\r\n\t[MovieID] [uniqueidentifier] PRIMARY KEY,\r\n\r\n\t[FeatureId] [int] REFERENCES [dbo].[featurefilm]([FeatureId]),\r\n\r\n\t[NationID] [varchar](255) REFERENCES [dbo].[Nation]([NationID]),\r\n\r\n\t[Mark] [float] NULL,\r\n\r\n\t[Time] [int] NULL,\r\n\r\n\t[Viewer] [int] NULL,\r\n\r\n\t[Description] [nvarchar](max) NULL,\r\n\r\n);\r\n\r\nCREATE TABLE [dbo].[MovieCategory](\r\n\t[CategoryID] [int] REFERENCES [dbo].[Category]([CategoryID]),\r\n\t[MovieID] [uniqueidentifier] REFERENCES [dbo].[Movies]([MovieID]),\r\n);\r\n\r\n{categories}\r\n{features};\r\n\r\nGive me only one json about \"{content}\" for table Movies and MovieCategory using example from FeatureFilm and category.";

        var res = await model.GenerateContentAsync(text);
        
        
        return Ok(res);
    }
}
