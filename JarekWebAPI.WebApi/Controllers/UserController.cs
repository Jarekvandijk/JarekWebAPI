using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JarekWebAPI.Repositories;
using JarekWebAPI.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JarekWebAPI.WebApi.Repository;

[ApiController]
[Route("UserAccounts")]
public class UserController : ControllerBase
{
    private readonly IAccountUserRepository _userAccountRepository;
    private readonly ILogger<UserController> _logger;
    private static List<AccountUser> _accountUsers = new List<AccountUser>();

    public UserController(IAccountUserRepository userAccountRepository, ILogger<UserController> logger)
    {
        _userAccountRepository = userAccountRepository;
        _logger = logger;
    }

    [HttpGet(Name = "GetAllUserAccounts")]
    public async Task<ActionResult<IEnumerable<AccountUser>>> GetAll()
    {
        var userAccounts = await _userAccountRepository.ReadAsync();
        return Ok(userAccounts);
    }

    [HttpGet("{id:guid}", Name = "GetUserAccountById")]
    public async Task<ActionResult<AccountUser>> GetById(Guid id)
    {
        var userAccount = await _userAccountRepository.ReadAsync();
        var account = userAccount.FirstOrDefault(u => u.ID == id);
        if (account == null)
            return NotFound("User account not found.");

        return Ok(account);
    }

    [HttpPost(Name = "CreateUserAccount")]
    public async Task<ActionResult> Create(AccountUser userAccount)
    {
        if (_accountUsers.Any(u => u.UserName == userAccount.UserName))
            return BadRequest("A user with this username already exists.");

        userAccount.Id = Guid.NewGuid();
        var createdAccount = await _userAccountRepository.InsertAsync(userAccount);
        _accountUsers.Add(createdAccount);

        return CreatedAtRoute("GetUserAccountById", new { id = createdAccount.ID }, createdAccount);
    }

    [HttpPut("{id:guid}", Name = "UpdateUserAccount")]
    public async Task<IActionResult> Update(Guid id, AccountUser updatedUserAccount)
    {
        var existingUser = await _userAccountRepository.ReadAsync();
        var userToUpdate = existingUser.FirstOrDefault(u => u.Id == id);

        if (userToUpdate == null)
            return NotFound("User account not found.");

        updatedUserAccount.Id = id;
        await _userAccountRepository.UpdateAsync(updatedUserAccount);

        return Ok(updatedUserAccount);
    }

    [HttpDelete("{id:guid}", Name = "DeleteUserAccount")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var existingUser = await _userAccountRepository.ReadAsync();
        var userToDelete = existingUser.FirstOrDefault(u => u.Id == id);

        if (userToDelete == null)
            return NotFound("User account not found.");

        await _userAccountRepository.DeleteAsync(id);
        _accountUsers.RemoveAll(u => u.Id == id);

        return Ok();
    }
}
