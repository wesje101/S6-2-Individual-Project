using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    [HttpPost("~/AddFriend",Name = "AddFriend")]
    public IActionResult UserAddFriend(string username, string friendname)
    {
        AccountLogic.logic.AddFriend(username, friendname);
        return Ok();
    }

    [HttpPost("~/RemoveFriend",Name = "RemoveFriend")]
    public IActionResult UserRemoveFriend(string username, string friendname)
    {
        AccountLogic.logic.RemoveFriend(username, friendname);
        return Ok();
    }
    
    
}