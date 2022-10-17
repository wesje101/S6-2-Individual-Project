namespace ModerationService.Models;

public class ChatMessage
{
    private string _message;
    private User _sender;
    private DateTime _timeSent;

    public ChatMessage(string message, User sender, DateTime timeSent)
    {
        _message = message;
        _sender = sender;
        _timeSent = timeSent;
    }
}