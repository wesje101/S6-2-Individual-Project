using ChatService.Models.Interfaces;
using ProfanityFilter;
using ProfanityFilter.Interfaces;

namespace ChatService.Models.Factories;

public class ChatMessageFactory : IChatMessageFactory
{
    private IProfanityFilter filter;

    public ChatMessageFactory()
    {
        filter = new ProfanityFilter.ProfanityFilter();
    }
    
    public ChatMessage CreateNewChatMessage(User sender, string message, ChatRoom room)
    {
        string censoredMessage = filter.CensorString(message);
        return new ChatMessage(){_sender = sender, _message = censoredMessage, _ChatRoom = room};
    }
}