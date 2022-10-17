using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly AccountLogic _logic = new AccountLogic();
    
    //TODO Authentication methods should move to AuthService
    [HttpPost(Name = "SignIn")]
    public IActionResult UserSignIn(string username, string password)
    {
        _logic.SignIn(username, password);
        return Ok();
    }

    [HttpGet(Name = "SignOut")]
    public IActionResult UserSignOut()
    {
        _logic.SignOut();
        return Ok();
    }

    [HttpPost(Name = "Register")]
    public IActionResult UserRegister(string username, string password)
    {
        _logic.Register(username, password);
        return Ok();
    }

    [HttpPost(Name = "AddFriend")]
    public IActionResult UserAddFriend(string username, string friendname)
    {
        _logic.AddFriend(username, friendname);
        return Ok();
    }

    [HttpPost(Name = "RemoveFriend")]
    public IActionResult UserRemoveFriend(string username, string friendname)
    {
        _logic.RemoveFriend(username, friendname);
        return Ok();
    }
    
    
}