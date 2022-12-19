namespace ChatService.Models.Interfaces;

public interface IChatLogic
{
    public void JoinChatRoomByName(User user, string name);
    public void LeaveChatRoomByName(User user, string name);
    public void CreateChatRoom(string name);
    public void CreateChatRoom(string name, User user);
    public void SendChatMessage(User user, string roomName, string message);
}