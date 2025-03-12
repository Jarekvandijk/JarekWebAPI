//using JarekWebAPI.WebApi;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using JarekWebAPI.WebApi.Repository;

//[TestClass]
//public class Object2DControllerTests
//{
//    private Mock<IObject2DRepository> _objectRepository;
//    private Object2DController _controller;

//    [TestInitialize]
//    public void Setup()
//    {
//        _objectRepository = new Mock<IObject2DRepository>();
//        _controller = new Object2DController(_objectRepository.Object);
//    }

//    [TestMethod]
//    public async Task GetAll_ReturnsOk_WithListOfObjects()
//    {
//        var objects = new List<Object2D>
//        {
//            new Object2D
//            {
//                Id = Guid.NewGuid(),
//                EnvironmentId = Guid.NewGuid(),
//                PrefabId = "Prefab1",
//                PositionX = 10.0f,
//                PositionY = 20.0f,
//                ScaleX = 1.0f,
//                ScaleY = 1.0f,
//                SortingLayer = 0
//            },
//            new Object2D
//            {
//                Id = Guid.NewGuid(),
//                EnvironmentId = Guid.NewGuid(),
//                PrefabId = "Prefab2",
//                PositionX = 30.0f,
//                PositionY = 40.0f,
//                ScaleX = 1.5f,
//                ScaleY = 1.5f,
//                SortingLayer = 1
//            }
//        };

//        _objectRepository.Setup(repo => repo.ReadAllAsync()).ReturnsAsync(objects);

//        var result = await _controller.GetAll();

//        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
//        var okResult = result.Result as OkObjectResult;
//        Assert.IsNotNull(okResult);
//        Assert.IsInstanceOfType(okResult.Value, typeof(List<Object2D>));
//    }

//    [TestMethod]
//    public async Task GetById_ExistingId_ReturnsOk()
//    {
//        var id = Guid.NewGuid();
//        var obj = new Object2D
//        {
//            Id = id,
//            EnvironmentId = Guid.NewGuid(),
//            PrefabId = "TestObject",
//            PositionX = 10.0f,
//            PositionY = 20.0f,
//            ScaleX = 1.0f,
//            ScaleY = 1.0f,
//            SortingLayer = 0
//        };

//        _objectRepository.Setup(repo => repo.ReadAsync(id)).ReturnsAsync(obj);

//        var result = await _controller.GetById(id);

//        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
//        var okResult = result.Result as OkObjectResult;
//        Assert.IsNotNull(okResult);
//        Assert.AreEqual(obj, okResult.Value);
//    }

//    [TestMethod]
//    public async Task GetById_NonExistingId_ReturnsNotFound()
//    {
//        var id = Guid.NewGuid();
//        _objectRepository.Setup(repo => repo.ReadAsync(id)).ReturnsAsync((Object2D)null);

//        var result = await _controller.GetById(id);

//        Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
//    }

//    [TestMethod]
//    public async Task Create_ValidObject_ReturnsCreatedAtRoute()
//    {
//        var newObject = new Object2D
//        {
//            Id = Guid.NewGuid(),
//            EnvironmentId = Guid.NewGuid(),
//            PrefabId = "NewPrefab",
//            PositionX = 10.0f,
//            PositionY = 20.0f,
//            ScaleX = 1.0f,
//            ScaleY = 1.0f,
//            SortingLayer = 0
//        };

//        _objectRepository.Setup(repo => repo.InsertAsync(It.IsAny<Object2D>())).ReturnsAsync(newObject);

//        var result = await _controller.Create(newObject);

//        Assert.IsInstanceOfType(result, typeof(CreatedAtRouteResult));
//        var createdResult = result as CreatedAtRouteResult;
//        Assert.IsNotNull(createdResult);
//        Assert.AreEqual("GetObjectById", createdResult.RouteName);
//        Assert.AreEqual(newObject.Id, createdResult.RouteValues["id"]);
//    }

//    [TestMethod]
//    public async Task Create_InvalidObject_ReturnsBadRequest()
//    {
//        var invalidObject = new Object2D
//        {
//            Id = Guid.NewGuid(),
//            EnvironmentId = Guid.NewGuid(),
//            PrefabId = null,
//            PositionX = 10.0f,
//            PositionY = 20.0f,
//            ScaleX = 1.0f,
//            ScaleY = 1.0f,
//            SortingLayer = 0
//        };

//        var result = await _controller.Create(invalidObject);

//        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
//    }

//    [TestMethod]
//    public async Task Update_ExistingObject_ReturnsOk()
//    {
//        var id = Guid.NewGuid();
//        var existingObject = new Object2D
//        {
//            Id = id,
//            EnvironmentId = Guid.NewGuid(),
//            PrefabId = "ExistingObject",
//            PositionX = 10.0f,
//            PositionY = 20.0f,
//            ScaleX = 1.0f,
//            ScaleY = 1.0f,
//            SortingLayer = 0
//        };

//        var updatedObject = new Object2D
//        {
//            Id = id,
//            EnvironmentId = Guid.NewGuid(),
//            PrefabId = "UpdatedObject",
//            PositionX = 15.0f,
//            PositionY = 25.0f,
//            ScaleX = 1.2f,
//            ScaleY = 1.2f,
//            SortingLayer = 1
//        };

//        _objectRepository.Setup(repo => repo.ReadAsync(id)).ReturnsAsync(existingObject);
//        _objectRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Object2D>())).Returns(Task.CompletedTask);

//        var result = await _controller.Update(id, updatedObject);

//        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
//    }

//    [TestMethod]
//    public async Task Update_NonExistingObject_ReturnsNotFound()
//    {
//        var id = Guid.NewGuid();
//        _objectRepository.Setup(repo => repo.ReadAsync(id)).ReturnsAsync((Object2D)null);

//        var result = await _controller.Update(id, new Object2D());

//        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
//    }

//    [TestMethod]
//    public async Task Delete_ExistingObject_ReturnsOk()
//    {
//        var id = Guid.NewGuid();
//        var existingObject = new Object2D
//        {
//            Id = id,
//            EnvironmentId = Guid.NewGuid(),
//            PrefabId = "ToDelete",
//            PositionX = 10.0f,
//            PositionY = 20.0f,
//            ScaleX = 1.0f,
//            ScaleY = 1.0f,
//            SortingLayer = 0
//        };

//        _objectRepository.Setup(repo => repo.ReadAsync(id)).ReturnsAsync(existingObject);
//        _objectRepository.Setup(repo => repo.DeleteAsync(id)).Returns(Task.CompletedTask);

//        var result = await _controller.Delete(id);

//        Assert.IsInstanceOfType(result, typeof(OkResult));
//    }

//    [TestMethod]
//    public async Task Delete_NonExistingObject_ReturnsNotFound()
//    {
//        var id = Guid.NewGuid();
//        _objectRepository.Setup(repo => repo.ReadAsync(id)).ReturnsAsync((Object2D)null);

//        var result = await _controller.Delete(id);

//        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
//    }
//}
