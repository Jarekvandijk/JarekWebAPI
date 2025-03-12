//using JarekWebAPI.WebApi;
//using JarekWebAPI.Repositories;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using Moq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using JarekWebAPI.WebApi.Repository;

//[TestClass]
//public class UserControllerTests
//{
//    private Mock<IAccountUserRepository> _userAccountRepository;
//    private Mock<ILogger<UserController>> _logger;
//    private UserController _controller;

//    [TestInitialize]
//    public void Setup()
//    {
//        _userAccountRepository = new Mock<IAccountUserRepository>();
//        _logger = new Mock<ILogger<UserController>>();
//        _controller = new UserController(_userAccountRepository.Object, _logger.Object);
//    }

//    [TestMethod]
//    public async Task Create_ValidAccount_ReturnsCreated()
//    {
//        var newAccount = new AccountUser { Id = Guid.NewGuid(), UserName = "user3", Password = "pass3" };

//        _userAccountRepository.Setup(repo => repo.ReadAsync("user3")).ReturnsAsync((AccountUser)null);
//        _userAccountRepository.Setup(repo => repo.InsertAsync(It.IsAny<AccountUser>())).ReturnsAsync(newAccount);

//        var result = await _controller.Create(newAccount);

//        Assert.IsInstanceOfType(result, typeof(CreatedResult));

//        var createdResult = result as CreatedResult;
//        Assert.IsNotNull(createdResult);

//        Assert.AreEqual(201, createdResult.StatusCode);
//    }

//    [TestMethod]
//    public async Task Create_AccountWithExistingUsername_ReturnsBadRequest()
//    {
//        var existingAccount = new AccountUser { Id = Guid.NewGuid(), UserName = "user3", Password = "pass3" };

//        _userAccountRepository.Setup(repo => repo.ReadAsync("user3")).ReturnsAsync(existingAccount);

//        var newAccount = new AccountUser { Id = Guid.NewGuid(), UserName = "user3", Password = "newpass" };

//        var result = await _controller.Create(newAccount);

//        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));

//        var badRequestResult = result as BadRequestObjectResult;
//        Assert.IsNotNull(badRequestResult);
//        Assert.AreEqual("Account with the Username user3 already exists.", badRequestResult.Value);
//    }
//}
