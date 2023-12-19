using Movies.Business;
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
        Task<ResponseDTO> CreatePerson(PersonDetail actorDetail);
        Task<ResponseDTO> UpdatePerson(PersonDetail actorDetail);
        Task<ResponseDTO> DeletePerson(Guid id);
    }
}
