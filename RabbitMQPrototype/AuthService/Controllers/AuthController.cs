using AuthService.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthLogic _logic;

    public AuthController(IAuthLogic logic)
    {
        _logic = logic;
    }
    
    [HttpPost("~/SignIn",Name = "SignIn")]
    public IActionResult UserSignIn(string username, string password)
    {
        if (_logic.SignIn(username, password))
        {
            return Ok("Signed in successfully");
        }

        return Ok("Something went wrong during sign-in");
    }

    [HttpGet("~/SignOut",Name = "SignOut")]
    public IActionResult UserSignOut()
    {
        if (_logic.SignOut())
        {
            return Ok("Signed out successfully");
        }

        return Ok("Something went wrong during sign-out");
    }

    [HttpPost("~/Register",Name = "Register")]
    public IActionResult UserRegister(string username, string password)
    {
        if (_logic.Register(username, password))
        {
            return Ok("Account created successfully");
        }
        return Ok("This username is unavailable/inappropriate");
    }
    
    [HttpGet("~/GetUsers",Name = "GetUsers")]
    public IActionResult GetUsers()
    {
        return Ok(_logic.GetUsers());
    }
}