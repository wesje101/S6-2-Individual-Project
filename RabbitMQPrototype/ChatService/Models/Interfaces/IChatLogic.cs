namespace ChatService.Models.Interfaces;

public interface IChatLogic
{
    public ChatRoom? GetChatRoom(int id);
    public ChatRoom JoinChatRoomByName(User user, string name);
    public ChatRoom LeaveChatRoomByName(User user, string name);
    public ChatRoom CreateChatRoom(string name);
    public ChatRoom CreateChatRoom(string name, User user);
    public ChatMessage SendChatMessage(User user, string roomName, string message);
    public IEnumerable<ChatRoom?> GetAllChatRooms();
    public IEnumerable<User> GetAllUsers();
    public IEnumerable<ChatMessage> GetAllChatMessages();
    public IEnumerable<ChatMessage> GetAllChatMessagesFromRoom(string roomName);
    public IEnumerable<User> GetAllParticipantsFromRoom(string roomName);
    public User CreateUser(User user);
    public User GetUser(int id);
    public User DeleteUser(int id);
}