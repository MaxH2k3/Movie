using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movies.Business.globals;
using Movies.Business.persons;
using Movies.Interface;
using Movies.Models;
using Movies.Utilities;
using System.Diagnostics;
using System.Net;

namespace Movies.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly MOVIESContext _context;
        private readonly IMapper _mapper;

        public PersonRepository(MOVIESContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public PersonRepository(IMapper mapper)
        {
            _context = new MOVIESContext();
            _mapper = mapper;
        }

        public Person? GetPerson(Guid id)
        {
            return GetPersons().FirstOrDefault(a => a.PersonId.Equals(id));
        }

        public IEnumerable<Person> SearchByName(string name, string role)
        {
            if(role.Equals(Constraint.RolePerson.ACTOR))
            {
                return GetActos().Where(a => a.NamePerson.ToLower().Contains(name)).ToList();
            } else
            {
                return GetProducers().Where(a => a.NamePerson.ToLower().Contains(name)).ToList();
            }
            
        }

        public Person? GetPersonByName(string name)
        {
            return GetPersons().FirstOrDefault(a => a.NamePerson.ToLower().Equals(name));
        }

        public IEnumerable<Person> GetPersons()
        {
            return _context.Persons.Include(a => a.Nation);
        }
        public IEnumerable<Person> GetActos()
        {
            return GetPersons().Where(a => a.Role.ToUpper().Equals(Constraint.RolePerson.ACTOR)).ToList();
        }

        public IEnumerable<Person> GetProducers()
        {
            return GetPersons().Where(a => a.Role.ToUpper().Equals(Constraint.RolePerson.PRODUCER)).ToList();
        }

        public async Task<ResponseDTO> CreatePerson(NewPerson newPerson)
        {
            newPerson.PersonId = Guid.NewGuid();
            ResponseDTO responseDTO = await ValidateData(newPerson);

            if (responseDTO.Status != HttpStatusCode.Continue)
            {
                return responseDTO;
            }

            Person person = new Person();
            person = _mapper.Map<Person>(newPerson);

            person.Role = person.Role?.ToUpper();
            

            _context.Persons.Add(person);
            if (await _context.SaveChangesAsync() > 0)
            {
                return new ResponseDTO(HttpStatusCode.Created, "Create person successfully");
            }
            return new ResponseDTO(HttpStatusCode.ServiceUnavailable, "Server error!");
        }

        public async Task<ResponseDTO> UpdatePerson(PersonDetail personDetail)
        {
            Person? person = GetPerson(personDetail.PersonId);
            if (person == null)
            {
                return new ResponseDTO(HttpStatusCode.NotFound, "Person not found!");
            }

            person = _mapper.Map<Person>(personDetail);

            Nation? nation = _context.Nations.Find(personDetail.NationId);
            if (nation == null)
            {
                return new ResponseDTO(HttpStatusCode.NotFound, "Nation not found!");
            }

            //if (!person.Role.ToUpper().(Constraint.RolePerson.ACTOR, Constraint.RolePerson.PRODUCER))
            //{
            //    return new ResponseDTO(HttpStatusCode.BadRequest, "Role must be actor (AC) or producer (PR)");
            //}

            person.Role = person.Role.ToUpper();

            _context.Persons.Update(person);
            if (await _context.SaveChangesAsync() > 0)
            {
                return new ResponseDTO(HttpStatusCode.OK, "Update person successfully!");
            }

            return new ResponseDTO(HttpStatusCode.ServiceUnavailable, "Server error!");
        }

        public async Task<ResponseDTO> ValidateData(NewPerson newPerson)
        {
            Nation? nation = _context.Nations.Find(newPerson.NationId);
            if (nation == null)
            {
                return new ResponseDTO(HttpStatusCode.NotFound, "Nation not found");
            }

            //if (!newPerson.Role.ToUpper().StringIn(Constraint.RolePerson.ACTOR, Constraint.RolePerson.PRODUCER))
            //{
            //    return new ResponseDTO(HttpStatusCode.NotFound, "Role must be actor (AC) or producer (PR)");
            //}

            //upload umage
            //string? result = null;
            //if (newPerson.Thumbnail != null)
            //{   
            //    var role = newPerson.Role.Equals(Constraint.RolePerson.ACTOR) ? "actor" : "producer";
            //    var filePath = $"thumbnail-person/{role}/{newPerson.PersonId}";
            //    if(String.IsNullOrEmpty(result))
            //    {
            //        return new ResponseDTO(HttpStatusCode.Conflict, $"{ newPerson.Thumbnail.FileName } is existed!, Please change your thumbnail.");
            //    }
            //}

            return new ResponseDTO(HttpStatusCode.Continue, "Validate Successfully!", "a");
        }

        public async Task<ResponseDTO> DeletePerson(Guid id)
        {
            Person? person = GetPerson(id);
            if (person == null)
            {
                return new ResponseDTO(HttpStatusCode.NotFound, "Person not found!");
            }

            _context.Persons.Remove(person);
            if (await _context.SaveChangesAsync() > 0)
            {
                return new ResponseDTO(HttpStatusCode.OK, "Person delete successfully!");
            }

            return new ResponseDTO(HttpStatusCode.ServiceUnavailable, "Server error!");
        }
    }
}
