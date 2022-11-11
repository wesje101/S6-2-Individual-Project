using AccountService.Models;
using AccountService.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountLogic _logic;
    
    public AccountController(IAccountLogic logic)
    {
        _logic = logic;
    }
    
    [HttpPost("~/AddFriend",Name = "AddFriend")]
    public IActionResult UserAddFriend(string username, string friendname)
    {
        _logic.AddFriend(username, friendname);
        return Ok();
    }

    [HttpPost("~/RemoveFriend",Name = "RemoveFriend")]
    public IActionResult UserRemoveFriend(string username, string friendname)
    {
        _logic.RemoveFriend(username, friendname);
        return Ok();
    }

    [HttpPost("~/AddUser",Name = "AddUser")]
    public IActionResult AddUser(string username)
    {
        _logic.AddAccount(new Account { name = username });
        return Ok();
    }

    [HttpGet("~/GetUsers", Name = "GetUsers")]
    public IActionResult GetUsers()
    {
        return Ok(_logic.GetAccounts());
    }
    
    
}