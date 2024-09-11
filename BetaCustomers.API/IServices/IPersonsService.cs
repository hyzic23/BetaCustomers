using BetaCustomers.API.Models;

namespace BetaCustomers.API.IServices;

public interface IPersonsService
{
    List<Person> LoadAll();
    Person CreatePerson(Person person);
    Person? GetPersonById(int id);
    Person? UpdatePerson(Person person);
}