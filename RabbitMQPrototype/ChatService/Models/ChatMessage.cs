namespace ChatService.Models;

public class ChatMessage
{
    private string _message;
    private Chatroom _chatroom;
    private DateTime _timeSent;

    public ChatMessage(string message, Chatroom chatroom, DateTime timeSent)
    {
        _message = message;
        _chatroom = chatroom;
        _timeSent = timeSent;
    }
}