using Movies.Business;
using Movies.Models;

namespace Movies.Interface
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetPersons();
        Person? GetPerson(int id);
        Person? GetPersonByName(string name);
        IEnumerable<Person> SearchByName(string name);
        Task<ResponseDTO> CreatePerson(PersonDetail actorDetail);
        Task<ResponseDTO> UpdatePerson(PersonDetail actorDetail);
        Task<ResponseDTO> DeletePerson(int id);
    }
}
