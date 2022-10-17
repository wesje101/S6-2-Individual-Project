namespace ChatService.Models;

public class Chatroom
{
    private string _roomName;
    private List<User> _participants;
    private List<ChatMessage> _messages;

    public Chatroom(List<User> participants, string roomName)
    {
        _participants = participants;
        _roomName = roomName;
        _messages = InitiateMessageList();
    }

    private List<ChatMessage> InitiateMessageList()
    {
        return new List<ChatMessage>();
    }
    
    public string GetRoomName()
    {
        return _roomName;
    }

    public IReadOnlyList<User> GetParticipants()
    {
        return _participants.AsReadOnly();
    }

    public bool JoinRoom(User user)
    {
        if (_participants.Contains(user)) return false;
        _participants.Add(user);
        return true;
    }

    public bool LeaveRoom(User user)
    {
        if (!_participants.Contains(user)) return false;
        _participants.Remove(user);
        return true;
    }

    public bool SendMessage(User user, ChatMessage message)
    {
        _messages.Add(message);
        return true;
    }
}