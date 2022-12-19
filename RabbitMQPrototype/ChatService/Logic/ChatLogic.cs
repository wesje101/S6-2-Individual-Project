using ChatService.Models;
using ChatService.Models.Interfaces;

namespace ChatService;

public class ChatLogic : IChatLogic
{
    private readonly IChatMessageRepository _messageRepository;
    private readonly IChatRoomRepository _roomRepository;
    private readonly IUserRepository _userRepository;
    private readonly IChatMessageFactory _messageFactory;

    public ChatLogic(IChatMessageRepository messageRepository, IChatRoomRepository roomRepository, IUserRepository userRepository, IChatMessageFactory messageFactory)
    {
        _messageRepository = messageRepository;
        _roomRepository = roomRepository;
        _userRepository = userRepository;
        _messageFactory = messageFactory;
    }

    public void JoinChatRoomByName(User user, string roomName)
    {
        var foundRoom = FindChatRoom(roomName);

        if (foundRoom == null)
        {
            throw new Exception($"No room with name: {roomName} found");
        }
        
        foundRoom._participants.Add(user);
    }

    public void LeaveChatRoomByName(User user, string roomName)
    {
        var foundRoom = FindChatRoom(roomName);

        if (foundRoom == null)
        {
            throw new Exception($"No room with name: {roomName} found");
        }
        
        foundRoom._participants.Remove(user);
    }

    public void CreateChatRoom(string roomName)
    {
        var foundRoom = FindChatRoom(roomName);

        if (foundRoom != null)
        {
            throw new Exception($"room: {roomName} already exists");
        }

        _roomRepository.AddChatRoom(new ChatRoom() { _roomName = roomName });
    }
    
    public void CreateChatRoom(string roomName, User user)
    {
        var foundRoom = FindChatRoom(roomName);

        if (foundRoom != null)
        {
            throw new Exception($"room: {roomName} already exists");
        }

        ChatRoom createdRoom = new ChatRoom() { _roomName = roomName };
        createdRoom._participants.Add(user);
        
        _roomRepository.AddChatRoom(createdRoom);
    }

    public void SendChatMessage(User user, string roomName, string message)
    {
        var foundRoom = FindChatRoom(roomName);

        if (foundRoom == null)
        {
            throw new Exception($"No room with name: {roomName} found");
        };
        
        ChatMessage chatMessage = _messageFactory.CreateNewChatMessage(user, message, foundRoom);
        
        _messageRepository.AddChatMessage(chatMessage);
    }

    public IEnumerable<ChatRoom> GetAllChatRooms()
    {
        return _roomRepository.GetChatRooms();
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _userRepository.GetUsers();
    }

    public IEnumerable<ChatMessage> GetAllChatMessages()
    {
        return _messageRepository.GetChatMessages();
    }

    public IEnumerable<ChatMessage> GetAllChatMessagesFromRoom(string roomName)
    {
        return _roomRepository.GetChatRooms()
            .FirstOrDefault(i => i._roomName == roomName)
            ._messages;
    }

    public IEnumerable<User> GetAllParticipantsFromRoom(string roomName)
    {
        return _roomRepository.GetChatRooms()
            .FirstOrDefault(i => i._roomName == roomName)
            ._participants;
    }

    private ChatRoom? FindChatRoom(string roomName)
    {
        return _roomRepository.GetChatRooms().FirstOrDefault(i => i._roomName == roomName);
    }
}