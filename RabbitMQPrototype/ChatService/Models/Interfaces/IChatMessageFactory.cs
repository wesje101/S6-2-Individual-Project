namespace ChatService.Models.Interfaces;

public interface IChatMessageFactory
{
    public ChatMessage CreateNewChatMessage(User sender, string message, ChatRoom room);
}