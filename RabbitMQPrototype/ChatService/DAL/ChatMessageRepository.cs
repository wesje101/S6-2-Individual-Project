using ChatService.Models;
using ChatService.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatService.DAL;

public class ChatMessageRepository : IChatMessageRepository
{
    private readonly ChatContext _context;
    
    public ChatMessageRepository(ChatContext context)
    {
        _context = context;
        if (Environment.GetEnvironmentVariable("HOSTED_ENVIRONMENT") == "docker")
        {
            context.Database.Migrate();
        }
    }

    public ChatMessage? GetChatMessage(int id)
    {
        return _context.ChatMessages.Find(id);
    }

    public IEnumerable<ChatMessage> GetChatMessages()
    {
        return _context.ChatMessages.ToList();
    }

    public ChatMessage? AddChatMessage(ChatMessage message)
    {
        _context.ChatMessages.Add(message);
        _context.SaveChanges();
        return GetChatMessage(message._id);
    }

    public ChatMessage? UpdateChatMessage(ChatMessage message)
    {
        _context.ChatMessages.Update(message);
        _context.SaveChanges();
        return GetChatMessage(message._id);
    }

    public bool DeleteChatMessage(int id)
    {
        var message = GetChatMessage(id);
        if (message == null) return false;
        _context.ChatMessages.Remove(message);
        _context.SaveChanges();
        return true;
    }
}