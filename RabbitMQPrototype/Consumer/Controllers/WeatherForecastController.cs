using Microsoft.AspNetCore.Mvc;

namespace Consumer.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    [HttpGet(Name = "ConsumeMessage")]
    public IActionResult Get()
    {
        Consume.ConsumeMessage();
        return Ok();
    }
}