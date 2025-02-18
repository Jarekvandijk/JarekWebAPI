using Microsoft.AspNetCore.Mvc;
using JarekWebAPI.WebApi;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("Environment2D")]
public class Environment2DController : ControllerBase
{
	private static List<Environment2D> environments = new List<Environment2D>()
	{
		new Environment2D()
		{
         
			Id = 1,
		
			Name = "Vierkant",
 
			MaxHeight = 4,
    
			MaxLength = 4
		},
		new Environment2D()
		{
            Id = 2,

            Name = "Rondje",

            MaxHeight = 3,

            MaxLength = 3
        },
		new Environment2D()
		{
            Id = 3,

            Name = "Rechthoek",

            MaxHeight = 5,

            MaxLength = 8
        }
	};


	private readonly ILogger<Object2DController> _logger;

	public Environment2DController(ILogger<Object2DController> logger)
	{
		_logger = logger;
	}

    [HttpGet(Name = "GetAllEnvironments")]
    public ActionResult<IEnumerable<Environment2D>> GetAll()
    {
        return environments;
    }

    [HttpGet("{id}", Name = "GetEnvironmentById")]
    public ActionResult<Environment2D> GetById(int id)
    {
        var environment = environments.FirstOrDefault(e => e.Id == id);
        if (environment == null)
            return NotFound();
        return environment;
    }

    [HttpPost(Name = "CreateEnvironment")]
    public ActionResult Create(Environment2D environment)
    {
        if (environments.Any(e => e.Id == environment.Id))
            return BadRequest("Environment with this ID already exists.");

        environments.Add(environment);
        return CreatedAtRoute("GetEnvironmentById", new { id = environment.Id }, environment);
    }

    [HttpPut("{id}", Name = "UpdateEnvironment")]
    public IActionResult Update(int id, Environment2D updatedEnvironment)
    {
        var existingEnvironment = environments.FirstOrDefault(e => e.Id == id);
        if (existingEnvironment == null)
            return NotFound();

        existingEnvironment.Name = updatedEnvironment.Name;
        existingEnvironment.MaxHeight = updatedEnvironment.MaxHeight;
        existingEnvironment.MaxLength = updatedEnvironment.MaxLength;

        return Ok();
    }

    [HttpDelete("{id}", Name = "DeleteEnvironment")]
    public IActionResult Delete(int id)
    {
        var environment = environments.FirstOrDefault(e => e.Id == id);
        if (environment == null)
            return NotFound();

        environments.Remove(environment);
        return Ok();
    }


    //[TestMethod]
    //public async Task Add_AddEnvironmentToUserWithMaxNumberOfEnvironments_ReturnsBadRequest()
    //{
    //    // Arrange
    //    var currentUserId
    //    var newEnvironment = new Environment2D();
    //    List<Environment2D> existingUserEnvironments = newList<Environment2D>[new Environment2D(), new Environment2D(), new Environment2D(), new Environment2D(), new Environment2D()];
    //    Mock<IEnvironmentRepository> EnvironmentRepository = new Mock<IEnvironmentRepository>();
    //    Mock<IObjectRepository> objectRepository = new Mock<IObjectRepository>();
    //    Mock<IUserRepository> userRepository = new Mock<IUserRepository>();

       
    //    // Create user
    //    userRepository.Setup(x => x.GetCurrentUserId()).Returns(currentUserId);
    //    // Create number of random environments
    //    // environmentRepository.Setup(x => x.ReadByOwnerId(currentUserId)).ReturnsAsync()
    //    var environmentController = new EnvironmentController();

    //    // Act 
    //}
}



