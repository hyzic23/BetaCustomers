using BetaCustomers.API.Models;

namespace BetaCustomers.UnitTests.Fixtures;

public class PersonsFixture
{
    public readonly List<Person> _persons = new()
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
}