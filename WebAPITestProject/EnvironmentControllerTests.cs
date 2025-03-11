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

