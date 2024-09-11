using BetaCustomers.API.IServices;
using BetaCustomers.API.Models;

namespace BetaCustomers.API.Services;

public class PersonsService : IPersonsService
{
    public PersonsService()
    {
    }

    private readonly List<Person> _persons = new()
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
        return _persons;
    }

    public Person CreatePerson(Person request)
    {
        var person = new Person()
        {
            Id = _persons.Max(per => per.Id) + 1,
            FirstName = request.FirstName,
            LastName = request.LastName
        };
        _persons.Add(person);
        return person;
    }

    public Person? GetPersonById(int id)
    {
        var result = _persons.FirstOrDefault(person => person.Id == id);
        return result;
    }

    public Person? UpdatePerson(Person person)
    {
        var result = _persons.FirstOrDefault(x => x.Id == person.Id);
        if (string.IsNullOrEmpty(result.FirstName)) return null;
        result.Id = person.Id;
        result.FirstName = person.FirstName;
        result.LastName = person.LastName;
        return result;
    }
}