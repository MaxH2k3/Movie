using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movies.Business;
using Movies.Interface;
using Movies.Models;
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

        public async Task<ResponseDTO> CreatePerson(PersonDetail personDetail)
        {
            Person person = new Person();
            person = _mapper.Map<Person>(personDetail);

            Nation? nation = _context.Nations.Find(personDetail.NationId);
            if (nation == null)
            {
                return new ResponseDTO(HttpStatusCode.NotFound, "Nation not found");
            }
            
            _context.Persons.Add(person);
            if (await _context.SaveChangesAsync() > 0)
            {
                return new ResponseDTO(HttpStatusCode.Created, "Create person successfully");
            }
            return new ResponseDTO(HttpStatusCode.ServiceUnavailable, "Server error!");
        }

        public Person? GetPerson(int id)
        {
            return GetPersons().FirstOrDefault(a => a.PersonId == id);
        }

        public IEnumerable<Person> SearchByName(string name)
        {
            return GetPersons().Where(a => a.NamePerson.ToLower().Contains(name)).ToList();
        }

        public Person? GetPersonByName(string name)
        {
            return GetPersons().FirstOrDefault(a => a.NamePerson.ToLower().Equals(name));
        }

        public IEnumerable<Person> GetPersons()
        {
            return _context.Persons.Include(a => a.Nation);
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

            _context.Persons.Update(person);
            if (await _context.SaveChangesAsync() > 0)
            {
                return new ResponseDTO(HttpStatusCode.OK, "Update person successfully!");
            }

            return new ResponseDTO(HttpStatusCode.ServiceUnavailable, "Server error!");
        }

        public async Task<ResponseDTO> DeletePerson(int id)
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
