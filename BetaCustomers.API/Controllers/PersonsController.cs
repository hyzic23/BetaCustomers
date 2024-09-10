using BetaCustomers.API.IServices;
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

    [HttpGet(Name = "GetPersons")]
    public async Task<IActionResult> Get()
    {
        var results = _personsService.LoadAll();
        if (results.Any())
        {
            return Ok(results);
        }
        return NoContent();
    }
}