using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthLogic _logic = new AuthLogic();
    
    [HttpPost("~/SignIn",Name = "SignIn")]
    public IActionResult UserSignIn(string username, string password)
    {
        if (_logic.SignIn(username, password))
        {
            return Ok();
        }

        return NotFound();
    }

    [HttpGet("~/SignOut",Name = "SignOut")]
    public IActionResult UserSignOut()
    {
        if (_logic.SignOut())
        {
            return Ok();
        }

        return NotFound();
    }

    [HttpPost("~/Register",Name = "Register")]
    public IActionResult UserRegister(string username, string password)
    {
        if (_logic.Register(username, password))
        {
            return Ok();
        }
        return NotFound();
    }
}