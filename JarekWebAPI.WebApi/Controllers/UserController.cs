//using Microsoft.AspNetCore.Mvc;
//using JarekWebAPI.Repositories;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using JarekWebAPI.WebApi;

//[ApiController]
//[Route("ÁccountUser/[controller]")]

//public class UserController : ControllerBase
//{
//    private List<AccountUser> userAccounts = new List<AccountUser>();

//    private readonly IAccountUserRepository _userAccount;
//    private readonly ILogger<UserController> _logger;
//    public UserController(IAccountUserRepository userRepository, ILogger<UserController> logger)
//    {
//        _userAccount = userRepository;
//        _logger = logger;
//    }
//    //public IUserAccount Get_userAccount()
//    //{
//    //    return _userAccount;
//    //}

//    //[HttpGet(Name = "ReadUserAccounts")]
//    //    public async Task<ActionResult<IEnumerable<UserAccount>>> Get() //userAccounts = _userAccount.ReadAsync();//IUserAccount _userAccount//Task<IEnumerable<UserAccount>>ReadAsync();//return BadRequest("dit is een test waarde");//(IEnumerable<UserAccount>)List<UserAccount>
//    //    {
//    //        IEnumerable<UserAccount> userAccountsEnumerable = await _userAccount.ReadAsync();  
//    //        userAccounts = userAccountsEnumerable.ToList();
//    //        if (userAccounts == null || !userAccounts.Any())
//    //        {
//    //            _logger.LogWarning("No user accounts found.");
//    //            return NotFound("No user accounts found.");
//    //        }
//    //        return Ok(userAccounts);
//    //    }


//    [HttpGet(Name = "ReadUserAccountsLogin")]
//    public async Task<ActionResult<AccountUser>> Get(AccountUser userAccount)//userAccounts = _userAccount.ReadAsync();//IUserAccount _userAccount//Task<IEnumerable<UserAccount>>ReadAsync();//return BadRequest("dit is een test waarde");//(IEnumerable<UserAccount>)List<UserAccount>
//    {

//        //_logger.LogWarning(Convert.ToString(userAccount.ID));
//        IEnumerable<AccountUser> userAccountsEnumerable = await _userAccount.ReadAsync();
//        userAccounts = userAccountsEnumerable.ToList();
//        if (userAccounts == null || !userAccounts.Any())
//        {
//            _logger.LogWarning("No user accounts found.");
//            return NotFound("No user accounts found.");
//        }
//        foreach (AccountUser user in userAccounts)
//        {
//            if (user.UserName == userAccount.UserName && user.Password == userAccount.Password)
//            {
//                userAccount.Id = user.Id;
//                return Ok(user);
//            }
//            if (user.UserName == userAccount.UserName && user.Password != userAccount.Password)
//            {
//                return BadRequest("Username already exists or password is wrong!");
//            }
//        }
//        return NotFound("No user accounts found.");
//    }

//    [HttpGet("{username}", Name = "ReadUserAccountByUserName")] //{date:datetime} //('3F2504E0-4F89-11D3-9A0C-0305E82C3301','Harry','Ditzijn50tDitzijn50tDitzijn50tDitzijn50tDitzijn50t')
//    public async Task<ActionResult<AccountUser>> Get(string username) // date aanpassen naar juiste waarde in dit geval ID
//    {
//        AccountUser userAccount = await _userAccount.ReadAsync(username);
//        //UserAccount userAccount = GetUserAccount(id);
//        if (userAccount == null)
//            //return NotFound("No user accounts found.");
//            return null;

//        return Ok(userAccount);
//    }



//    [HttpPost(Name = "CreateUserAccount")]
//    public async Task<ActionResult> Add(AccountUser userAccount)
//    {
//        //if (userAccount.ID == Guid.Empty)
//        //{
//        //    userAccount.ID = Guid.NewGuid();
//        //}
//        userAccount.Id = Guid.NewGuid();
//        if (await GetUserAccount(userAccount.UserName) != null)
//        {
//            return BadRequest("Account with the Username " + userAccount.UserName + " already exists.");
//        }

//        await _userAccount.InsertAsync(userAccount);
//        //userAccounts.Add(userAccount);
//        return Created();
//    }


//    [HttpPut("{id:Guid}", Name = "UpdateUserAccountByID")]
//    public async Task<IActionResult> Update(Guid id, AccountUser updatedUserAccount)
//    {
//        if (id != updatedUserAccount.Id)
//            return BadRequest("The id of the object did not match the id of the route");

//        AccountUser userAccountToUpdate = await GetUserAccountbyID(updatedUserAccount.Id);
//        if (userAccountToUpdate == null)
//            return NotFound();
//        await _userAccount.UpdateAsync(userAccountToUpdate);
//        //userAccounts.Remove(userAccountToUpdate);
//        //userAccounts.Add(newUserAccount);

//        return Ok(userAccounts);

//    }

//    [HttpDelete("{id:Guid}", Name = "DeleteUserAccountByID")]
//    public async Task<IActionResult> Delete(Guid id)
//    {
//        AccountUser userAccountToDelete = await GetUserAccountbyID(id);
//        if (userAccountToDelete == null)
//            return NotFound();
//        await _userAccount.DeleteAsync(id);

//        //userAccounts.Remove(userAccountToDelete);
//        return Ok(userAccounts);
//    }

//    //private async Task<UserAccount> GetUserAccount(string username)
//    //{
//    //    var user = await Get(username);

//    ////foreach (UserAccount userAccount in userAccounts)
//    ////{
//    ////    if (userAccount.ID == id || userAccount.UserName == username)
//    ////        return userAccount;
//    ////}

//    //return null;
//    //}
//    //private async Task<UserAccount> GetUserAccount(string username)
//    //{
//    //    var result = await Get(username);

//    //    if (result == null || result.Result is NotFoundResult) // Voorkom NullReferenceException
//    //        return null;

//    //    return result.Value; // Dit is nu veilig
//    //}

//    private async Task<AccountUser> GetUserAccount(string username)
//    {
//        var result = await _userAccount.ReadAsync(username); 
//        return result; 
//    }



//    private async Task<AccountUser> GetUserAccountbyID(Guid id)
//    {
//        var result = await _userAccount.ReadAsync();
//        foreach (AccountUser userAccount in result)
//        {
//            if (userAccount.Id == id)
//                return userAccount;
//        }

//        return null;
//    }
//}



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
    private List<AccountUser> accountUsers = new List<AccountUser>();

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
        var account = userAccount.FirstOrDefault(u => u.Id == id);
        if (account == null)
            return NotFound("User account not found.");

        return Ok(account);
    }

    [HttpPost(Name = "CreateUserAccount")]
    public async Task<ActionResult> Create(AccountUser userAccount)
    {
        userAccount.Id = Guid.NewGuid();
        if (await GetAccountUser(userAccount.UserName) != null)
        {
            return BadRequest("Account with the Username " + userAccount.UserName + " already exists.");
        }
        var createdAccount = await _userAccountRepository.InsertAsync(userAccount);
        accountUsers.Add(createdAccount);

        //return CreatedAtRoute("GetUserAccountById", new { id = createdAccount.Id }, createdAccount);
        return Created();
    }

    //[HttpPost(Name = "CreateUserAccount")]
    //public async Task<ActionResult> Add(UserAccount userAccount)
    //{
    //    //if (userAccount.ID == Guid.Empty)
    //    //{
    //    //    userAccount.ID = Guid.NewGuid();
    //    //}
    //    userAccount.ID = Guid.NewGuid();
    //    if (await GetUserAccount(userAccount.UserName) != null)
    //    {
    //        return BadRequest("Account with the Username " + userAccount.UserName + " already exists.");
    //    }

    //    await _userAccount.InsertAsync(userAccount);
    //    //userAccounts.Add(userAccount);
    //    return Created();
    //}


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
        accountUsers.RemoveAll(u => u.Id == id);

        return Ok();
    }

    private async Task<AccountUser> GetAccountUser(string username)
    {
        var result = await _userAccountRepository.ReadAsync(username); 
        return result; 
    }
}