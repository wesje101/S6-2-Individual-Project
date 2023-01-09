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
        return _context.ChatRooms.Include(
            chatroom => chatroom._participants).Include(
            chatroom => chatroom._messages).SingleOrDefault(chatroom => chatroom._id == id);
    }

    public IEnumerable<ChatRoom?> GetChatRooms()
    {
        return _context.ChatRooms.Include(
            chatroom => chatroom._participants).Include(
            chatroom => chatroom._messages).ToList();
    }

    public ChatRoom? AddChatRoom(ChatRoom chatRoom)
    {
        _context.Add(chatRoom);
        _context.SaveChanges();
        return GetChatRoom(chatRoom._id);
    }

    public ChatRoom? UpdateChatRoom(int roomId, ChatRoom updatedRoom)
    {
        ChatRoom? repoRoom = GetChatRoom(roomId);
        if (repoRoom == null)
        {
            return null;
        }
        repoRoom._messages = updatedRoom._messages;
        repoRoom._participants = updatedRoom._participants;
        repoRoom._roomName = updatedRoom._roomName;
        _context.SaveChanges();
        return GetChatRoom(repoRoom._id);
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