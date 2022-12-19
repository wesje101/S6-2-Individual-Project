using ChatService.Models;
using ChatService.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChatService.Controllers;

[ApiController]
[Route("[controller]")]
public class ChatController : ControllerBase
{
    private readonly IChatLogic _logic;

    public ChatController(IChatLogic logic)
    {
        _logic = logic;
    }
    
    [HttpPost("JoinChatRoomByRoomName",Name = "JoinChatRoomByRoomName")]
    public IActionResult JoinChatRoomByName(User user, string roomName)
    {
        try
        {
            _logic.JoinChatRoomByName(user, roomName);
            return Ok($"Room: {roomName} joined");

        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }

    }
    
    [HttpPost("LeaveChatRoomByRoomName",Name = "LeaveChatRoomByRoomName")]
    public IActionResult LeaveChatRoomByName(User user, string roomName)
    {
        try
        {
            _logic.LeaveChatRoomByName(user, roomName);
            return Ok($"Room: {roomName} left");
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost("SendChatMessage",Name = "SendChatMessage")]
    public IActionResult SendChatMessage(User user, string roomName, string message)
    {
        try
        {
            _logic.SendChatMessage(user, roomName, message);
            return Ok($"Message sent to room: {roomName}");
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost("CreateChatRoom", Name = "CreateChatRoom")]
    public IActionResult CreateChatRoom(string roomName)
    {
        try
        {
            _logic.CreateChatRoom(roomName);
            return Ok($"Chatroom: {roomName} created");
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpPost("CreateAndJoinChatRoom", Name = "CreateAndJoinChatRoom")]
    public IActionResult CreateAndJoinChatRoom(string roomName, User user)
    {
        try
        {
            _logic.CreateChatRoom(roomName, user);
            return Ok($"Chatroom: {roomName} created and joined");
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
}