using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{

    [HttpGet(Name = "GetWeatherForecast")]
    public IActionResult Get()
    {
        return Ok();
    }
}