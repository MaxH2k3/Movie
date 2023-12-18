using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.Business;
using Movies.Interface;
using Movies.Models;
using Movies.Repository;
using System.Diagnostics;
using System.Net;
using System.Xml.Linq;

namespace Movies.Controllers;

[ApiController]
public class PersonController : Controller
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public PersonController(IPersonRepository personRepository,
                   IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Get persons by filter
    /// </summary>
    /// <param name="key">
    ///     <para>Existed value: search by name person</para>
    ///     <para>Empty value: get all persons</para>
    /// </param>
    /// <returns></returns>

    [HttpGet("Persons")]
    [ProducesResponseType(typeof(IEnumerable<PersonDetail>), StatusCodes.Status200OK)]
    public IActionResult GetPersons(string? key, int page = 1, int eachPage = 6)
    {
        IEnumerable<PersonDetail>? persons;
        if (!String.IsNullOrEmpty(key))
        {
            persons = _mapper.Map<IEnumerable<PersonDetail>>(_personRepository.SearchByName(key.Trim().ToLower()));
        } else
        {
            persons = _mapper.Map<IEnumerable<PersonDetail>>(_personRepository.GetPersons());
        }
        
        persons = persons.Skip((page - 1) * eachPage).Take(eachPage);
        return Ok(persons);
    }

    [HttpGet("Person/{PersonId}")]
    [ProducesResponseType(typeof(PersonDetail), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public IActionResult GetPerson(int PersonId)
    {
        var person = _mapper.Map<PersonDetail>(_personRepository.GetPerson(PersonId));
        if (person == null)
        {
            return NotFound("Person not found!");
        }
        
        return Ok(person);
    }

    [HttpPost("Person")]
    public async Task<IActionResult> CreatePerson([FromBody] PersonDetail personDetail)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest("Invalid data!");
        }
        ResponseDTO response = await _personRepository.CreatePerson(personDetail);
        if(response.Status == HttpStatusCode.Created)  
        {
            return Ok(response.Message);
        }
        return BadRequest(response.Message);
    }

    [HttpPut("Person")]
    public async Task<IActionResult> UpdatePerson([FromBody] PersonDetail personDetail)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid data!");
        }
        ResponseDTO response = await _personRepository.UpdatePerson(personDetail);
        if (response.Status == HttpStatusCode.OK)
        {
            return Ok(response.Message);
        }
        return BadRequest(response.Message);
    }

    [HttpDelete("Person/{id}")]
    public async Task<IActionResult> DeletePerson(int id)
    {
        ResponseDTO response = await _personRepository.DeletePerson(id);
        if (response.Status == HttpStatusCode.OK)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

}
