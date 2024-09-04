using BetaCustomers.API.Models;

namespace BetaCustomers.UnitTests.Fixtures;

public static class UsersFixture
{
    public static List<User> GetTestUsers() => new()
    {
        new User()
        {
            Id = 1,
            Name = "Josh Tosh",
            Address = new Address()
            {
                Street = "123 Main Str",
                City = "WaterDown",
                ZipCode = "9098K1"
            },
            Email = "josh.tosh@gmail.com"
        },
        new User()
        {
            Id = 2,
            Name = "Posh Dash",
            Address = new Address()
            {
                Street = "321 Side Str",
                City = "WaterSide",
                ZipCode = "9098K2"
            },
            Email = "posh.dash@gmail.com"
        },
        new User()
        {
            Id = 3,
            Name = "Mash Zash",
            Address = new Address()
            {
                Street = "231 Up Str",
                City = "WaterUp",
                ZipCode = "9098K3"
            },
            Email = "mash.zash@gmail.com"
        }
    };
}