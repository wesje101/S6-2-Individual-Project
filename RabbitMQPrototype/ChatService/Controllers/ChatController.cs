using ChatService.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatService.Controllers;

[ApiController]
[Route("[controller]")]
public class ChatController : ControllerBase
{
    private readonly ChatLogic _logic = new ChatLogic();
    
    [HttpPost(Name = "JoinChatRoomByRoomName")]
    public IActionResult JoinChatRoomByName(User user, string roomName)
    {
        if (_logic.JoinChatRoomByName(user, roomName))
        {
            return Ok();
        }

        return NotFound();
    }
    
    [HttpPost(Name = "LeaveChatRoomByRoomName")]
    public IActionResult LeaveChatRoomByName(User user, string roomName)
    {
        if (_logic.LeaveChatRoomByName(user, roomName))
        {
            return Ok();
        }
        
        return NotFound();
    }

    [HttpPost(Name = "SendChatMessage")]
    public IActionResult SendChatMessage(User user, string roomName, ChatMessage message)
    {
        if (_logic.SendChatMessage(user, roomName, message))
        {
            return Ok();
        }

        return NotFound();
    }
}