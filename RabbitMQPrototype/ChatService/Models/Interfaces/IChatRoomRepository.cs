namespace ChatService.Models.Interfaces;

public interface IChatRoomRepository
{
    public ChatRoom? GetChatroom(int id);
    public IEnumerable<ChatRoom?> GetChatrooms();
    public ChatRoom? AddChatroom(ChatRoom chatRoom);
    public ChatRoom? UpdateChatroom(ChatRoom? chatRoom);
    public bool DeleteChatroom(int id);
}