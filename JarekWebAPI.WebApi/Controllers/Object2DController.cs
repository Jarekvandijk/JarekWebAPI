//using Microsoft.AspNetCore.Mvc;
//using JarekWebAPI.Repositories;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using JarekWebAPI.WebApi;

//[ApiController]
//[Route("Object2D")]
//public class Object2DController : ControllerBase
//{
//    private readonly IObject2DRepository _object2DRepository;

//    public Object2DController(IObject2DRepository object2DRepository)
//    {
//        _object2DRepository = object2DRepository;
//    }

//    [HttpGet(Name = "GetAllObjects")]
//    public async Task<ActionResult<IEnumerable<Object2D>>> GetAll()
//    {
//        var objects = await _object2DRepository.ReadAllAsync();
//        return Ok(objects);
//    }

//    [HttpGet("{id}", Name = "GetObjectById")]
//    public async Task<ActionResult<Object2D>> GetById(Guid id)
//    {
//        var obj = await _object2DRepository.ReadAsync(id);
//        if (obj == null)
//            return NotFound();
//        return Ok(obj);
//    }

//    [HttpPost(Name = "CreateObject")]
//    public async Task<ActionResult> Create(Object2D obj)
//    {
//        var createdObject = await _object2DRepository.InsertAsync(obj);
//        return CreatedAtRoute("GetObjectById", new { id = createdObject.Id }, createdObject);
//    }

//    [HttpPut("{id}", Name = "UpdateObject")]
//    public async Task<IActionResult> Update(Guid id, Object2D updatedObject)
//    {
//        var existingObject = await _object2DRepository.ReadAsync(id);
//        if (existingObject == null)
//            return NotFound();

//        updatedObject.Id = id;
//        await _object2DRepository.UpdateAsync(updatedObject);
//        return Ok(updatedObject);
//    }

//    [HttpDelete("{id}", Name = "DeleteObject")]
//    public async Task<IActionResult> Delete(Guid id)
//    {
//        var existingObject = await _object2DRepository.ReadAsync(id);
//        if (existingObject == null)
//            return NotFound();

//        await _object2DRepository.DeleteAsync(id);
//        return Ok();
//    }
//}
using Microsoft.AspNetCore.Mvc;
using JarekWebAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using JarekWebAPI.WebApi;

[ApiController]
[Route("Object2D")]
public class Object2DController : ControllerBase
{
    private readonly IObject2DRepository _object2DRepository;
    private static List<Object2D> _objects = new List<Object2D>();

    public Object2DController(IObject2DRepository object2DRepository)
    {
        _object2DRepository = object2DRepository;
    }

    [HttpGet(Name = "GetAllObjects")]
    public async Task<ActionResult<IEnumerable<Object2D>>> GetAll()
    {
        var objects = await _object2DRepository.ReadAllAsync();
        return Ok(objects);
    }

    [HttpGet("{id}", Name = "GetObjectById")]
    public async Task<ActionResult<Object2D>> GetById(Guid id)
    {
        var obj = await _object2DRepository.ReadAsync(id);
        if (obj == null)
            return NotFound();
        return Ok(obj);
    }

    [HttpPost(Name = "CreateObject")]
    public async Task<ActionResult> Create(Object2D obj)
    {
        var createdObject = await _object2DRepository.InsertAsync(obj);
        _objects.Add(createdObject);
        return CreatedAtRoute("GetObjectById", new { id = createdObject.Id }, createdObject);
    }

    [HttpPut("{id}", Name = "UpdateObject")]
    public async Task<IActionResult> Update(Guid id, Object2D updatedObject)
    {
        var existingObject = await _object2DRepository.ReadAsync(id);
        if (existingObject == null)
            return NotFound();

        updatedObject.Id = id;
        await _object2DRepository.UpdateAsync(updatedObject);
        return Ok(updatedObject);
    }

    [HttpDelete("{id}", Name = "DeleteObject")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var existingObject = await _object2DRepository.ReadAsync(id);
        if (existingObject == null)
            return NotFound();

        await _object2DRepository.DeleteAsync(id);
        _objects.RemoveAll(o => o.Id == id);
        return Ok();
    }
}
