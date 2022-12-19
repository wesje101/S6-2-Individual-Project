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
        if (Environment.GetEnvironmentVariable("HOSTED_ENVIRONMENT") == "docker")
        {
            context.Database.Migrate();
        }
    }

    public ChatRoom? GetChatroom(int id)
    {
        return _context.ChatRooms.Find(id);
    }

    public IEnumerable<ChatRoom?> GetChatrooms()
    {
        return _context.ChatRooms.ToList();
    }

    public ChatRoom? AddChatroom(ChatRoom chatRoom)
    {
        _context.Add(chatRoom);
        _context.SaveChanges();
        return GetChatroom(chatRoom._id);
    }

    public ChatRoom? UpdateChatroom(ChatRoom? chatRoom)
    {
        _context.ChatRooms.Update(chatRoom);
        _context.SaveChanges();
        return GetChatroom(chatRoom._id);
    }

    public bool DeleteChatroom(int id)
    {
        var room = GetChatroom(id);
        if (room == null) return false;
        _context.ChatRooms.Remove(room);
        _context.SaveChanges();
        return true;
    }
}