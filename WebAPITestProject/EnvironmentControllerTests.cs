//using Castle.Core.Logging;
//using JarekWebAPI.Repositories;
//using JarekWebAPI.WebApi;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using Moq;

//[TestClass]

//public class EnvironmentControllerTests
//{
//    [TestMethod]
//    public void MyTestMethod()
//    {
//        var logger = new Mock<ILogger<Environment2DController>>();
//        //var environmentController = new Environment2DController(logger.Object);
//        var objectRepository = new Mock<IObject2DRepository>();
//        var environmentRepository = new Mock<IEnvironment2DRepository>();
//        var authenticationService = new Mock<IAuthenticationService>();

//        //var currenentUserId = Guid.NewGuid().ToString();
//        var id = Guid.NewGuid();
//        authenticationService.Setup(t => t.GetCurrentAuthenticatedUserId()).Returns(Guid.NewGuid().ToString());

//        environmentRepository.Setup(t => t.ReadAsync(id)).ReturnsAsync(new Environment2D());

//        var controller = new Environment2DController(environmentRepository.Object);


//        // Act 
//        var response = controller.GetAll();

//        // Assert 
//        Assert.IsInstanceOfType<OkObjectResult>(response.Result);


//    }
//}

using JarekWebAPI.WebApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JarekWebAPI.WebApi.Repository;

[TestClass]
public class EnvironmentControllerTests
{
    private Mock<IEnvironment2DRepository> _environmentRepository;
    private Mock<ILogger<Environment2DController>> _logger;
    private Environment2DController _controller;

    [TestInitialize]
    public void Setup()
    {
        _environmentRepository = new Mock<IEnvironment2DRepository>();
        _logger = new Mock<ILogger<Environment2DController>>();
        _controller = new Environment2DController(_environmentRepository.Object);
    }

    [TestMethod]
    public async Task GetAll_ReturnsOk_WithListOfEnvironments()
    {
        var environments = new List<Environment2D>
        {
            new Environment2D { Id = Guid.NewGuid(), Name = "Environment1", MaxHeight = 100, MaxLength = 200 },
            new Environment2D { Id = Guid.NewGuid(), Name = "Environment2", MaxHeight = 150, MaxLength = 250 }
        };

        _environmentRepository.Setup(repo => repo.ReadAllAsync()).ReturnsAsync(environments);

        var result = await _controller.GetAll();

        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.IsInstanceOfType(okResult.Value, typeof(List<Environment2D>));
    }

    [TestMethod]
    public async Task GetById_ExistingId_ReturnsOk()
    {
        var id = Guid.NewGuid();
        var environment = new Environment2D { Id = id, Name = "TestEnvironment", MaxHeight = 100, MaxLength = 200 };

        _environmentRepository.Setup(repo => repo.ReadAsync(id)).ReturnsAsync(environment);

        var result = await _controller.GetById(id);

        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(environment, okResult.Value);
    }

    [TestMethod]
    public async Task GetById_NonExistingId_ReturnsNotFound()
    {
        var id = Guid.NewGuid();
        _environmentRepository.Setup(repo => repo.ReadAsync(id)).ReturnsAsync((Environment2D)null);

        var result = await _controller.GetById(id);

        Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task Create_ValidEnvironment_ReturnsCreatedAtRoute()
    {
        var newEnvironment = new Environment2D
        {
            Id = Guid.NewGuid(),
            Name = "NewEnv",
            MaxHeight = 100,
            MaxLength = 200
        };

        _environmentRepository.Setup(repo => repo.InsertAsync(It.IsAny<Environment2D>())).ReturnsAsync(newEnvironment);

        var result = await _controller.Create(newEnvironment);

        Assert.IsInstanceOfType(result, typeof(CreatedAtRouteResult));
        var createdResult = result as CreatedAtRouteResult;
        Assert.IsNotNull(createdResult);
        Assert.AreEqual("GetEnvironmentById", createdResult.RouteName);
        Assert.AreEqual(newEnvironment.Id, createdResult.RouteValues["id"]);
    }

    [TestMethod]
    public async Task Create_InvalidEnvironment_ReturnsBadRequest()
    {
        var invalidEnvironment = new Environment2D
        {
            Id = Guid.NewGuid(),
            Name = null, // Missing required Name
            MaxHeight = 100,
            MaxLength = 200
        };

        var result = await _controller.Create(invalidEnvironment);

        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task Update_ExistingEnvironment_ReturnsOk()
    {
        var id = Guid.NewGuid();
        var existingEnvironment = new Environment2D
        {
            Id = id,
            Name = "ExistingEnv",
            MaxHeight = 100,
            MaxLength = 200
        };

        var updatedEnvironment = new Environment2D
        {
            Id = id,
            Name = "UpdatedEnv",
            MaxHeight = 150,
            MaxLength = 250
        };

        _environmentRepository.Setup(repo => repo.ReadAsync(id)).ReturnsAsync(existingEnvironment);
        _environmentRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Environment2D>())).Returns(Task.CompletedTask);

        var result = await _controller.Update(id, updatedEnvironment);

        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    }

    [TestMethod]
    public async Task Update_NonExistingEnvironment_ReturnsNotFound()
    {
        var id = Guid.NewGuid();
        _environmentRepository.Setup(repo => repo.ReadAsync(id)).ReturnsAsync((Environment2D)null);

        var result = await _controller.Update(id, new Environment2D());

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task Delete_ExistingEnvironment_ReturnsOk()
    {
        var id = Guid.NewGuid();
        var existingEnvironment = new Environment2D
        {
            Id = id,
            Name = "ToDelete",
            MaxHeight = 100,
            MaxLength = 200
        };

        _environmentRepository.Setup(repo => repo.ReadAsync(id)).ReturnsAsync(existingEnvironment);
        _environmentRepository.Setup(repo => repo.DeleteAsync(id)).Returns(Task.CompletedTask);

        var result = await _controller.Delete(id);

        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public async Task Delete_NonExistingEnvironment_ReturnsNotFound()
    {
        var id = Guid.NewGuid();
        _environmentRepository.Setup(repo => repo.ReadAsync(id)).ReturnsAsync((Environment2D)null);

        var result = await _controller.Delete(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
}
