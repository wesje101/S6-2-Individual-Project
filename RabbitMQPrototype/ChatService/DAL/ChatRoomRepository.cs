using ChatService.Models;
using ChatService.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatService.DAL;

public class ChatRoomRepository : IChatRoomRepository
{
    private readonly ChatContext _context;
    
    public ChatRoomRepository(ChatContext context)
    {
        _context = context;
    }

    public ChatRoom? GetChatRoom(int id)
    {
        return _context.ChatRooms.Find(id);
    }

    public IEnumerable<ChatRoom?> GetChatRooms()
    {
        return _context.ChatRooms.ToList();
    }

    public ChatRoom? AddChatRoom(ChatRoom chatRoom)
    {
        _context.Add(chatRoom);
        _context.SaveChanges();
        return GetChatRoom(chatRoom._id);
    }

    public ChatRoom? UpdateChatRoom(ChatRoom oldRoom, ChatRoom updatedRoom)
    {
        //_context.ChatRooms.Update(oldRoom);
        oldRoom._messages = updatedRoom._messages;
        oldRoom._participants = updatedRoom._participants;
        oldRoom._roomName = updatedRoom._roomName;
        _context.SaveChanges();
        return GetChatRoom(oldRoom._id);
    }

    public bool DeleteChatRoom(int id)
    {
        var room = GetChatRoom(id);
        if (room == null) return false;
        _context.ChatRooms.Remove(room);
        _context.SaveChanges();
        return true;
    }
}