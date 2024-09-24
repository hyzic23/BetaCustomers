using BetaCustomers.API.Models;

namespace BetaCustomers.API.IServices;

public interface IPersonsService
{
    ///  <summary>
    ///  Method is used to get all persons
    ///  </summary>
    ///  <returns>Person</returns>
    List<Person> LoadAll();
    
    
    ///  <summary>
    ///  Method is used to create person
    ///  </summary>
    ///  <param name="person"></param>
    ///  <returns>Person</returns>
    Person CreatePerson(Person person);
    
    ///  <summary>
    ///  Method is used to get person using id
    ///  </summary>
    ///  <param name="id"></param>
    ///  <returns>Person</returns>
    Person? GetPersonById(int id);
    
    ///  <summary>
    ///  Method is used to update person
    ///  </summary>
    ///  <param name="person"></param>
    ///  <returns>Person</returns>
    Person? UpdatePerson(Person person);
}