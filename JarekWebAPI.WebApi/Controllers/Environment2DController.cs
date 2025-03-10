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
    private static List<Environment2D> _environments = new List<Environment2D>();

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
    public async Task<ActionResult<Environment2D>> GetById(Guid id)
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
        _environments.Add(createdEnvironment);
        return CreatedAtRoute("GetEnvironmentById", new { id = createdEnvironment.Id }, createdEnvironment);
    }

    [HttpPut("{id}", Name = "UpdateEnvironment")]
    public async Task<IActionResult> Update(Guid id, Environment2D updatedEnvironment)
    {
        var existingEnvironment = await _environment2DRepository.ReadAsync(id);
        if (existingEnvironment == null)
            return NotFound();

        updatedEnvironment.Id = id;
        await _environment2DRepository.UpdateAsync(updatedEnvironment);
        return Ok(updatedEnvironment);
    }

    [HttpDelete("{id}", Name = "DeleteEnvironment")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var existingEnvironment = await _environment2DRepository.ReadAsync(id);
        if (existingEnvironment == null)
            return NotFound();

        await _environment2DRepository.DeleteAsync(id);
        _environments.RemoveAll(e => e.Id == id);
        return Ok();
    }
}
