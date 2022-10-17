using ChatService.Models;

namespace ChatService;

public class ChatLogic
{
    private readonly List<Chatroom> _chatrooms = new List<Chatroom>();

    public bool JoinChatRoomByName(User user, string roomName)
    {
        var foundRoom = _chatrooms.FirstOrDefault(i => i.GetRoomName() == roomName);

        return foundRoom != null && foundRoom.JoinRoom(user);
    }

    public bool LeaveChatRoomByName(User user, string roomName)
    {
        var foundRoom = _chatrooms.FirstOrDefault(i => i.GetRoomName() == roomName);

        return foundRoom != null && foundRoom.LeaveRoom(user);
    }

    public bool SendChatMessage(User user, string roomName, ChatMessage message)
    {
        var foundRoom = _chatrooms.FirstOrDefault(i => i.GetRoomName() == roomName);

        return foundRoom != null && foundRoom.SendMessage(user, message);
    }
}