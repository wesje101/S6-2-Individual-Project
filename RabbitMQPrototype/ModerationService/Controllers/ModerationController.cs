using Microsoft.AspNetCore.Mvc;
using ModerationService.Models;

namespace ModerationService.Controllers;

[ApiController]
[Route("[controller]")]
public class ModerationController : ControllerBase
{

    [HttpGet("~/GetReports",Name = "GetReports")]
    public IActionResult GetReports()
    {
        return BadRequest();
    }

    [HttpPost("~/GetReportsByUser",Name = "GetReportsByUser")]
    public IActionResult GetReportsByUser(User user)
    {
        return BadRequest();
    }

    [HttpPost("~/CloseReport",Name = "CloseReport")]
    public IActionResult CloseReport(Report report)
    {
        return BadRequest();
    }

    [HttpPost("~/HideMessage",Name = "HideMessage")]
    public IActionResult HideMessage(ChatMessage message)
    {
        return BadRequest();
    }

    [HttpPost("~/BanUser",Name = "BanUser")]
    public IActionResult BanUser(User user)
    {
        return BadRequest();
    }
}