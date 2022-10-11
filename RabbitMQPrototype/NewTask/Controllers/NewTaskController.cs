using Microsoft.AspNetCore.Mvc;

namespace NewTask.Controllers;

[ApiController]
[Route("[controller]")]
public class NewTaskController : ControllerBase
{
    [HttpPost(Name = "PostTask")]
    public IActionResult Get(string[] args)
    {
        NewTask.SendMessage(args);
        return Ok();
    }
}