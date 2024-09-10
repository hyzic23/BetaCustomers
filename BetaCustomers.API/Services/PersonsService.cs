using BetaCustomers.API.IServices;
using BetaCustomers.API.Models;

namespace BetaCustomers.API.Services;

public class PersonsService : IPersonsService
{
    // public PersonsService(List<Person> Persons)
    // {
    //     this.Persons = Persons;
    // }
    
    public PersonsService()
    {
    }

    private List<Person> Persons = new List<Person>
    {
        new Person()
        {
            Id = 1,
            FirstName = "John Doe",
            LastName = "Sarah Doe"
        },
        new Person()
        {
            Id = 2,
            FirstName = "Leo Stones",
            LastName = "Max Stones"
        },
        new Person()
        {
            Id = 3,
            FirstName = "Chi Mark",
            LastName = "Talia Mark"
        }
    };
    
    
    public List<Person> LoadAll()
    {
        return Persons;
    }

    public void CreatePerson(Person person)
    {
        var persons = Persons[0];
        Persons.Add(person);
    }

    public Person GetPersonById(int id)
    {
        throw new NotImplementedException();
    }

    public void UpdatePerson()
    {
        throw new NotImplementedException();
    }
}