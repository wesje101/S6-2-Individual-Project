using Microsoft.AspNetCore.Mvc;

namespace Worker.Controllers;

[ApiController]
[Route("[controller]")]
public class WorkerController : ControllerBase
{
    [HttpGet(Name = "WorkTask")]
    public IActionResult Get()
    {
        Worker.ConsumeMessage();
        return Ok();
    }
}