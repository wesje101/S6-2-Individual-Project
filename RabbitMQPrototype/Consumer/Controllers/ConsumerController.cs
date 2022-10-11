using Microsoft.AspNetCore.Mvc;

namespace Consumer.Controllers;

[ApiController]
[Route("[controller]")]
public class ConsumerController : ControllerBase
{
    [HttpGet(Name = "ConsumeMessage")]
    public IActionResult Get()
    {
        Consume.ConsumeMessage();
        return Ok();
    }
}