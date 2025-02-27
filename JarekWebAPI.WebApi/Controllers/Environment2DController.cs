//using Microsoft.AspNetCore.Mvc;
//using JarekWebAPI.WebApi;
//using Microsoft.Extensions.Hosting;
//using System.Collections.Generic;
//using System.Linq;
//using JarekWebAPI.Repositories;
//using Microsoft.AspNetCore.Authentication;

//[ApiController]
//[Route("Environment2D")]
//public class Environment2DController : ControllerBase
//{
//	private static List<Environment2D> environments = new List<Environment2D>()
//	{
//		new Environment2D()
//		{
//			Id = 1,
//			Name = "Vierkant",
//			MaxHeight = 4,
//			MaxLength = 4
//		},
//		new Environment2D()
//		{
//            Id = 2,
//            Name = "Rondje",
//            MaxHeight = 3,
//            MaxLength = 3
//        },
//		new Environment2D()
//		{
//            Id = 3,
//            Name = "Rechthoek",
//            MaxHeight = 5,
//            MaxLength = 8
//        }
//	};

//    private readonly IEnvironment2DRepository _environment2DRepository;

//    public Environment2DController(IEnvironment2DRepository environment2DRepository)
//    {
//        _environment2DRepository = environment2DRepository;
//    }

//    [HttpGet(Name = "GetAllEnvironments")]
//    public ActionResult<IEnumerable<Environment2D>> GetAll()
//    {
//        return Ok(environments);
//    }

//    [HttpGet("{id}", Name = "GetEnvironmentById")]
//    public ActionResult<Environment2D> GetById(int id)
//    {
//        var environment = environments.FirstOrDefault(e => e.Id == id);
//        if (environment == null)
//            return NotFound();
//        return environment;
//    }

//    [HttpPost(Name = "CreateEnvironment")]
//    public ActionResult Create(Environment2D environment)
//    {
//        if (environments.Any(e => e.Id == environment.Id))
//            return BadRequest("Environment with this ID already exists.");

//        environments.Add(environment);
//        return CreatedAtRoute("GetEnvironmentById", new { id = environment.Id }, environment);
//    }

//    [HttpPut("{id}", Name = "UpdateEnvironment")]
//    public IActionResult Update(int id, Environment2D updatedEnvironment)
//    {
//        var existingEnvironment = environments.FirstOrDefault(e => e.Id == id);
//        if (existingEnvironment == null)
//            return NotFound();

//        existingEnvironment.Name = updatedEnvironment.Name;
//        existingEnvironment.MaxHeight = updatedEnvironment.MaxHeight;
//        existingEnvironment.MaxLength = updatedEnvironment.MaxLength;

//        return Ok();
//    }

//    [HttpDelete("{id}", Name = "DeleteEnvironment")]
//    public IActionResult Delete(int id)
//    {
//        var environment = environments.FirstOrDefault(e => e.Id == id);
//        if (environment == null)
//            return NotFound();

//        environments.Remove(environment);
//        return Ok();
//    }
//}



using Microsoft.AspNetCore.Mvc;
using JarekWebAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using JarekWebAPI.WebApi;

[ApiController]
[Route("Environment2D")]
public class Environment2DController : ControllerBase
{
    private readonly IEnvironment2DRepository _environment2DRepository;

    public Environment2DController(IEnvironment2DRepository environment2DRepository)
    {
        _environment2DRepository = environment2DRepository;
    }

    [HttpGet(Name = "GetAllEnvironments")]
    public async Task<ActionResult<IEnumerable<Environment2D>>> GetAll()
    {
        var environments = await _environment2DRepository.ReadAllAsync();
        return Ok(environments);
    }

    [HttpGet("{id}", Name = "GetEnvironmentById")]
    public async Task<ActionResult<Environment2D>> GetById(int id)
    {
        var environment = await _environment2DRepository.ReadAsync(id);
        if (environment == null)
            return NotFound();
        return Ok(environment);
    }

    [HttpPost(Name = "CreateEnvironment")]
    public async Task<ActionResult> Create(Environment2D environment)
    {
        var createdEnvironment = await _environment2DRepository.InsertAsync(environment);
        return CreatedAtRoute("GetEnvironmentById", new { id = createdEnvironment.Id }, createdEnvironment);
    }

    [HttpPut("{id}", Name = "UpdateEnvironment")]
    public async Task<IActionResult> Update(int id, Environment2D updatedEnvironment)
    {
        var existingEnvironment = await _environment2DRepository.ReadAsync(id);
        if (existingEnvironment == null)
            return NotFound();

        updatedEnvironment.Id = id; // Zorg ervoor dat het ID hetzelfde blijft
        await _environment2DRepository.UpdateAsync(updatedEnvironment);
        return Ok(updatedEnvironment);
    }

    [HttpDelete("{id}", Name = "DeleteEnvironment")]
    public async Task<IActionResult> Delete(int id)
    {
        var existingEnvironment = await _environment2DRepository.ReadAsync(id);
        if (existingEnvironment == null)
            return NotFound();

        await _environment2DRepository.DeleteAsync(id);
        return Ok();
    }
}
