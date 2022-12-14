namespace ChatService.Models.Interfaces;

public interface IChatRoomRepository
{
    public ChatRoom? GetChatRoom(int id);
    public IEnumerable<ChatRoom?> GetChatRooms();
    public ChatRoom? AddChatRoom(ChatRoom chatRoom);
    public ChatRoom? UpdateChatRoom(int roomId, ChatRoom? updatedRoom);
    public bool DeleteChatRoom(int id);
}