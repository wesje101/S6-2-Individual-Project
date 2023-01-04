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

    [HttpGet("GetChatRoom", Name = "GetChatRoom")]
    public IActionResult GetChatRoom(int id)
    {
        return Ok(_logic.GetChatRoom(id));
    }
    
    [HttpGet("GetAllChatRooms", Name = "GetAllChatRooms")]
    public IActionResult GetAllChatRooms()
    {
        return Ok(_logic.GetAllChatRooms());
    }
    
    [HttpGet("GetAllChatMessages", Name = "GetAllChatMessages")]
    public IActionResult GetAllChatMessages()
    {
        return Ok(_logic.GetAllChatMessages());
    }
    
    [HttpGet("GetAllUsers", Name = "GetAllUsers")]
    public IActionResult GetAllUsers()
    {
        return Ok(_logic.GetAllUsers());
    }
    
    [HttpPost("JoinChatRoomByRoomName",Name = "JoinChatRoomByRoomName")]
    public IActionResult JoinChatRoomByName(User user, string roomName)
    {
        try
        {
            return Ok(_logic.JoinChatRoomByName(user, roomName));

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
            return Ok(_logic.LeaveChatRoomByName(user, roomName));
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
            
            return Ok(_logic.SendChatMessage(user, roomName, message));
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
            return Ok(_logic.CreateChatRoom(roomName));
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
            return Ok(_logic.CreateChatRoom(roomName, user));
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost("CreateUser", Name = "CreateUser")]
    public IActionResult CreateUser(string userName)
    {
        try
        {
            return Ok(_logic.CreateUser(new User(){_name = userName}));
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet("GetUser", Name = "GetUser")]
    public IActionResult GetUser(int id)
    {
        try
        {
            return Ok(_logic.GetUser(id));
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("DeleteUser", Name = "Deleteuser")]
    public IActionResult DeleteUser(int id)
    {
        try
        {
            return Ok(_logic.DeleteUser(id));
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
}