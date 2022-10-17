using Microsoft.AspNetCore.Mvc;
using ModerationService.Models;

namespace ModerationService.Controllers;

[ApiController]
[Route("[controller]")]
public class ModerationController : ControllerBase
{

    [HttpGet(Name = "GetReports")]
    public IActionResult GetReports()
    {
        return BadRequest();
    }

    [HttpGet(Name = "GetReportsByUser")]
    public IActionResult GetReportsByUser(User user)
    {
        return BadRequest();
    }

    [HttpPost(Name = "CloseReport")]
    public IActionResult CloseReport(Report report)
    {
        return BadRequest();
    }

    [HttpPost(Name = "HideMessage")]
    public IActionResult HideMessage(ChatMessage message)
    {
        return BadRequest();
    }

    [HttpPost(Name = "BanUser")]
    public IActionResult BanUser(User user)
    {
        return BadRequest();
    }
}