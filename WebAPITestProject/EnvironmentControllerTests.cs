using Castle.Core.Logging;
using JarekWebAPI.Repositories;
using JarekWebAPI.WebApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

[TestClass]

public class EnvironmentControllerTests
{
    [TestMethod]
    public void MyTestMethod()
    {
        var logger = new Mock<ILogger<Environment2DController>>();
        //var environmentController = new Environment2DController(logger.Object);
        var objectRepository = new Mock<IObject2DRepository>();
        var environmentRepository = new Mock<IEnvironment2DRepository>();
        var authenticationService = new Mock<IAuthenticationService>();

        //var currenentUserId = Guid.NewGuid().ToString();
        var id = Guid.NewGuid();
        authenticationService.Setup(t => t.GetCurrentAuthenticatedUserId()).Returns(Guid.NewGuid().ToString());

        environmentRepository.Setup(t => t.ReadAsync(id)).ReturnsAsync(new Environment2D());

        var controller = new Environment2DController(environmentRepository.Object);


        // Act 
        var response = controller.GetAll();

        // Assert 
        Assert.IsInstanceOfType<OkObjectResult>(response.Result);


    }
}

//using Azure;
//using Microsoft.AspNetCore.Http.HttpResults;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using JarekWebAPI.Repositories;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using JarekWebAPI.WebApi;

//namespace JarekWebAPI.Tests
//{
//    [TestClass]
//    public sealed class Environment2DControllerTests
//    {
//        [TestMethod]
//        public async Task Add_AddEnvironmentToUserWithNoEnvironments_ReturnsCreatedEnvironment()
//        {
//            // ARRANGE
//            var objectRepository = new Mock<IObject2DRepository>();
//            var environmentRepository = new Mock<IEnvironment2DRepository>();
//            var authenticationService = new Mock<IAuthenticationService>();

//            var id = 10;
//            var newEnvironment = GenerateRandomEnvironment("new environment");
//            var existingUserEnvironments = GenerateRandomEnvironments(0);

//            authenticationService.Setup(t => t.GetCurrentAuthenticatedUserId()).Returns(Guid.NewGuid().ToString());

//            environmentRepository.Setup(t => t.ReadAsync(id)).ReturnsAsync(new Environment2D());


//            var environmentController = new Environment2DController(environmentRepository.Object);

//            // ACT
//            var response = environmentController.GetAll(); // await??

//            // ASSERT
//            Assert.IsInstanceOfType<OkObjectResult>(response.Result);
//        }

//        private List<Environment2D> GenerateRandomEnvironments(int numberOfEnvironments)
//        {
//            List<Environment2D> list = [];

//            for (int i = 0; i < numberOfEnvironments; i++)
//            {
//                list.Add(GenerateRandomEnvironment($"Random Environment {i}"));
//            }

//            return list;
//        }

//        private Environment2D GenerateRandomEnvironment(string name)
//        {
//            var random = new Random();
//            return new Environment2D
//            {
//                Id = random.Next(1,1000),
//                MaxHeight = random.Next(1, 100),
//                MaxLength = random.Next(1, 100),
//                Name = name ?? "Random environment"
//            };
//        }
//    }
//}