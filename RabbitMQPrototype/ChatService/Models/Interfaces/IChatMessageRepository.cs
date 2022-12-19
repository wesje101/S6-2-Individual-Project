namespace ChatService.Models.Interfaces;

public interface IChatMessageRepository
{
    public ChatMessage? GetChatMessage(int id);
    public IEnumerable<ChatMessage> GetChatMessages();
    public ChatMessage? AddChatMessage(ChatMessage message);
    public ChatMessage? UpdateChatMessage(ChatMessage message);
    public bool DeleteChatMessage(int id);
}