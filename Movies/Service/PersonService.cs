﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movies.Business.globals;
using Movies.Business.persons;
using Movies.Interface;
using Movies.Models;
using Movies.Utilities;
using System.Net;

namespace Movies.Repository
{
    public class PersonService : IPersonRepository
    {
        private readonly MOVIESContext _context;
        private readonly IMapper _mapper;
        private readonly IStorageRepository _storageRepository;

        public PersonService(MOVIESContext context, IMapper mapper, IStorageRepository storageRepository)
        {
            _context = context;
            _mapper = mapper;
            _storageRepository = storageRepository;
        }

        public PersonService(IMapper mapper)
        {
            _context = new MOVIESContext();
            _mapper = mapper;
            _storageRepository = new StorageService();
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
            person.NationId = person.NationId?.ToUpper();
            person.Thumbnail = person.Thumbnail ?? responseDTO.Data?.ToString();
            

            _context.Persons.Add(person);
            if (await _context.SaveChangesAsync() > 0)
            {
                return new ResponseDTO(HttpStatusCode.Created, "Create person successfully");
            }
            return new ResponseDTO(HttpStatusCode.ServiceUnavailable, "Server error!");
        }

        public async Task<ResponseDTO> UpdatePerson(NewPerson newPerson)
        {
            Person? person = GetPerson((Guid) newPerson.PersonId);
            string? oldImage = person?.Thumbnail;
            if (person == null)
            {
                return new ResponseDTO(HttpStatusCode.NotFound, "Person not found!");
            }

            ResponseDTO responseDTO = await ValidateData(newPerson);
            if (responseDTO.Status != HttpStatusCode.Continue)
            {
                return responseDTO;
            }

            person = _mapper.Map<Person>(newPerson);
            person.Role = person.Role?.ToUpper();
            person.NationId = person.NationId?.ToUpper();
            person.Thumbnail = (newPerson.Thumbnail != null) ? responseDTO.Data?.ToString() : oldImage;
  

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
            
            if (!newPerson.Role.ToUpper().Equals(Constraint.RolePerson.ACTOR) &&
                !newPerson.Role.ToUpper().Equals(Constraint.RolePerson.PRODUCER))
            {
                return new ResponseDTO(HttpStatusCode.NotFound, "Role must be actor (AC) or producer (PR)");
            }

            //upload image
            string? filePath = null;
            if (newPerson.Thumbnail != null)
            {
                var role = newPerson.Role.ToUpper().Equals(Constraint.RolePerson.ACTOR) ? "actor" : "producer";
                filePath = $"person/{role}/{newPerson.PersonId}";
                await _storageRepository.UploadFile(newPerson.Thumbnail, filePath);
            }

            return new ResponseDTO(HttpStatusCode.Continue, "Validate Successfully!", filePath);
        }

        public async Task<ResponseDTO> DeletePerson(Guid id)
        {
            Person? person = _context.Persons.Find(id);
            if (person == null)
            {
                return new ResponseDTO(HttpStatusCode.NotFound, "Person not found!");
            }

            _context.Persons.Remove(person);
            await _storageRepository.DeleteFile(person.Thumbnail);
            if (await _context.SaveChangesAsync() > 0)
            {
                return new ResponseDTO(HttpStatusCode.OK, "Person delete successfully!");
            }

            return new ResponseDTO(HttpStatusCode.ServiceUnavailable, "Server error!");
        }

    }
}
