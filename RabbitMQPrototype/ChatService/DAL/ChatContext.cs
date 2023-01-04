using ChatService.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatService.DAL;

public class ChatContext: DbContext
{
    public ChatContext(DbContextOptions<ChatContext> options) : base(options)
    {
        //Database.EnsureCreated();
    }
    
    public DbSet<ChatMessage> ChatMessages { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<ChatRoom> ChatRooms { get; set; }
}