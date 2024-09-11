using BetaCustomers.API.IServices;
using BetaCustomers.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace BetaCustomers.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonsController : ControllerBase
{
    private readonly IPersonsService _personsService;

    public PersonsController(IPersonsService personsService)
    {
        _personsService = personsService;
    }

    [HttpGet]
    [Route("GetAllPersons")]
    public async Task<IActionResult> Get()
    {
        var results = _personsService.LoadAll();
        if (results.Any())
        {
            return Ok(results);
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