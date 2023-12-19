using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.Business;
using Movies.Interface;
using Movies.Models;
using Movies.Repository;
using Movies.Utilities;
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
    /// <param name="filterBy">The filter option. Possible values:
    ///    <para>- actor: take all person which is actor</para>
    ///    <para>- producer: take all person which is producer</para>
    ///    <pra> Get all person if filterBy is empty </pra>
    /// </param>
    /// <param name="key">
    ///     <para>Existed value: search by name person</para>
    ///     <para>Empty value: get all persons</para>
    /// </param>
    /// <returns></returns>

    [HttpGet("Persons")]
    [ProducesResponseType(typeof(IEnumerable<PersonDetail>), StatusCodes.Status200OK)]
    public IActionResult GetPersons(string? filterBy, string? key, int page = 1, int eachPage = 6)
    {
        IEnumerable<PersonDTO>? persons;
        if(Constraint.FilterName.ACTOR.Equals(filterBy?.Trim().ToLower()))
        {
            persons = String.IsNullOrEmpty(key) ? 
                _mapper.Map<IEnumerable<PersonDTO>>(_personRepository.GetActos()) : 
                _mapper.Map<IEnumerable<PersonDTO>>(_personRepository.SearchByName(key.Trim().ToLower(), Constraint.RolePerson.ACTOR));
        } else if(Constraint.FilterName.PRODUCER.Equals(filterBy?.Trim().ToLower()))
        {
            persons = String.IsNullOrEmpty(key) ?
                _mapper.Map<IEnumerable<PersonDTO>>(_personRepository.GetProducers()) :
                _mapper.Map<IEnumerable<PersonDTO>>(_personRepository.SearchByName(key.Trim().ToLower(), Constraint.RolePerson.PRODUCER));
        }
        else if (String.IsNullOrEmpty(filterBy) && (!String.IsNullOrEmpty(key)))
        {
            persons = _mapper.Map<IEnumerable<PersonDTO>>(_personRepository.GetPersonByName(key.Trim().ToLower()));
        } else if (String.IsNullOrEmpty(filterBy))
        {
            persons = _mapper.Map<IEnumerable<PersonDTO>>(_personRepository.GetPersons());
        } else
        {
            return NotFound("Your filter did not existed!");
        }
        
        persons = persons.Skip((page - 1) * eachPage).Take(eachPage);
        return Ok(persons);
    }

    [HttpGet("Person/{PersonId}")]
    [ProducesResponseType(typeof(PersonDetail), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public IActionResult GetPerson(Guid PersonId)
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
    public async Task<IActionResult> DeletePerson(Guid id)
    {
        ResponseDTO response = await _personRepository.DeletePerson(id);
        if (response.Status == HttpStatusCode.OK)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

}
