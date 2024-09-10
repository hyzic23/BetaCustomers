using BetaCustomers.API.Models;

namespace BetaCustomers.API.IServices;

public interface IPersonsService
{
    List<Person> LoadAll();
    void CreatePerson(Person person);
    Person GetPersonById(int id);
    void UpdatePerson();
}