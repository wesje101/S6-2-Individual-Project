using AuthService.Attributes;
using AuthService.Models;
using AuthService.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthLogic _logic;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthLogic logic, ILogger<AuthController> logger)
    {
        _logic = logic;
        _logger = logger;
    }
    
    [HttpPost("~/SignIn",Name = "SignIn")]
    public IActionResult UserSignIn(string username, string password)
    {
        if (_logic.SignIn(username, password))
        {
            return Ok("Signed in successfully");
        }

        return BadRequest("Something went wrong during sign-in");
    }

    [HttpGet("~/SignOut",Name = "SignOut")]
    public IActionResult UserSignOut()
    {
        if (_logic.SignOut())
        {
            return Ok("Signed out successfully");
        }

        return BadRequest("Something went wrong during sign-out");
    }

    [ServiceFilter(typeof(AuthorizeGoogleTokenAttribute))]
    [HttpPost("~/Register",Name = "Register")]
    public IActionResult UserRegister()
    // {
    //     if (_logic.Register(username, password))
    //     {
    //         return Ok("Account created successfully");
    //     }
    //     return BadRequest("This username is unavailable/inappropriate");
    // }
    {
        (int _, string googleId, string username) = GetTokenInfo();
        if(googleId == "" || username == "")
        {
            _logger.Log(LogLevel.Error, "Google id or username not found in token");
            return BadRequest();
        }
        User user = new() { GoogleId = googleId, _name = username };
        bool result = _logic.Register(user);
        if (!result)
        {
            _logger.Log(LogLevel.Information, "Account with name {Name} attempted to be created, but already exists", user._name);
            return BadRequest("This username is unavailable/inappropriate");
        }
        return Ok("Account created successfully");
    }
    
    [HttpGet("~/GetUsers",Name = "GetUsers")]
    public IActionResult GetUsers()
    {
        return Ok(_logic.GetUsers());
    }
    
    private (int, string, string) GetTokenInfo()
    {
        string googleId = HttpContext.Items["GoogleId"] as string ?? "";
        string username = HttpContext.Items["Username"] as string ?? "";
        _logger.LogInformation("GoogleId: {GoogleId}, Username: {Username}", googleId, username);
        int id = _logic.GetAccountIdFromGoogleId(googleId);
        return (id, googleId, username);
    }
}