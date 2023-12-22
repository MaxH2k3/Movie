using Movies.Business.globals;
using Movies.Business.persons;
using Movies.Models;

namespace Movies.Interface
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetPersons();
        Person? GetPerson(Guid id);
        Person? GetPersonByName(string name);
        IEnumerable<Person> GetActos();
        IEnumerable<Person> GetProducers();
        IEnumerable<Person> SearchByName(string name, string role);
        Task<ResponseDTO> CreatePerson(NewPerson newPerson);
        Task<ResponseDTO> UpdatePerson(PersonDetail actorDetail);
        Task<ResponseDTO> DeletePerson(Guid id);
    }
}
