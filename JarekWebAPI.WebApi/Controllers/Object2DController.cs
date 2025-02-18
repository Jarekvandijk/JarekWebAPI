using Microsoft.AspNetCore.Mvc;
using JarekWebAPI.WebApi;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

[ApiController]
[Route("Object2D")]
public class Object2DController : ControllerBase
{
    private static List<Object2D> objects = new List<Object2D>()
    {
        new Object2D()
        {
            Id = 1,         
            PrefabId = 1,
            PositionX = 1.5f,
            PositionY = 3f,  
            ScaleX = 2f,
            ScaleY = 2f,
            SortingLayer = 3
        },
        new Object2D()
        {
            Id = 2,
            PrefabId = 2,
            PositionX = 5f,
            PositionY = 2f,
            ScaleX = 1f,
            ScaleY = 1f,
            SortingLayer = 2
        },
        new Object2D()
        {
            Id = 3,
            PrefabId = 3,
            PositionX = 0f,
            PositionY = 0f,
            ScaleX = 1f,
            ScaleY = 1f,
            SortingLayer = 1
        }
    };


    private readonly ILogger<Object2DController> _logger;

    public Object2DController(ILogger<Object2DController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetAllObjects")]
    public ActionResult<IEnumerable<Object2D>> GetAll()
    {
        return objects;
    }

    [HttpGet("{id}", Name = "GetObjectById")]
    public ActionResult<Object2D> GetById(int id)
    {
        var obj = objects.FirstOrDefault(o => o.Id == id);
        if (obj == null)
            return NotFound();
        return obj;
    }

    [HttpPost(Name = "CreateObject")]
    public ActionResult Create(Object2D obj)
    {
        if (objects.Any(o => o.Id == obj.Id))
            return BadRequest("Object with this ID already exists.");

        objects.Add(obj);
        return CreatedAtRoute("GetObjectById", new { id = obj.Id }, obj);
    }

    [HttpPut("{id}", Name = "UpdateObject")]
    public IActionResult Update(int id, Object2D updatedObject)
    {
        var existingObject = objects.FirstOrDefault(o => o.Id == id);
        if (existingObject == null)
            return NotFound();

        existingObject.PrefabId = updatedObject.PrefabId;
        existingObject.PositionX = updatedObject.PositionX;
        existingObject.PositionY = updatedObject.PositionY;
        existingObject.ScaleX = updatedObject.ScaleX;
        existingObject.ScaleY = updatedObject.ScaleY;
        existingObject.SortingLayer = updatedObject.SortingLayer;

        return Ok();
    }

    [HttpDelete("{id}", Name = "DeleteObject")]
    public IActionResult Delete(int id)
    {
        var obj = objects.FirstOrDefault(o => o.Id == id);
        if (obj == null)
            return NotFound();

        objects.Remove(obj);
        return Ok();
    }
}
