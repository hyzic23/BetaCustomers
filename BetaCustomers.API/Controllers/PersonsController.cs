using BetaCustomers.API.Config;
using BetaCustomers.API.IServices;
using BetaCustomers.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BetaCustomers.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonsController : ControllerBase
{
    private readonly IPersonsService _personsService;
    private readonly IMemoryCache _memoryCache;
    private readonly UsersApiConfig _usersApiConfig;

    public PersonsController(IPersonsService personsService, 
                             IMemoryCache memoryCache, 
                             UsersApiConfig usersApiConfig)
    {
        _personsService = personsService;
        _memoryCache = memoryCache;
        _usersApiConfig = usersApiConfig;
    }

    [HttpGet]
    [Route("GetAllPersons")]
    public async Task<IActionResult> Get()
    {
        var cachePersonData = _memoryCache.Get<IEnumerable<Person>>("persons");
        if (cachePersonData != null)
        {
            return Ok(cachePersonData);
        }

        var cacheExpiryTime = double.Parse(_usersApiConfig.CachingExpiryTimeInMinutes);
        
        var expirationTime = DateTimeOffset.Now.AddMinutes(cacheExpiryTime);
        cachePersonData = await Task.FromResult(_personsService.LoadAll());
        
        //Set cache for persons
        _memoryCache.Set("persons", cachePersonData, expirationTime);
        if (cachePersonData.Any())
        {
            return Ok(cachePersonData);
        }
        return NoContent();
    }

    [HttpPost]
    [Route("AddPerson")]
    public async Task<IActionResult> AddPerson(Person request)
    {
        if (string.IsNullOrEmpty(request.FirstName)) return BadRequest("First Name is required");
        var person = new Person
        {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName
        };
        _personsService.CreatePerson(person);
        return Ok(200);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetPersonById(int id)
    {
        var person = _personsService.GetPersonById(id);
        if (person == null)
        {
            return NotFound();
        }
        return Ok(person);
    }

    [HttpPut]
    [Route("UpdatePerson")]
    public async Task<IActionResult> UpdatePerson(Person request)
    {
        var person = _personsService.UpdatePerson(request);
        if (person == null) return NotFound();
        return Ok(person);
    }

}