using ChatService.Models;
using ChatService.Models.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace ChatService;

public class RoomNotFoundException : Exception
{
    public RoomNotFoundException(string message) : base(message)
    {
        
    }
    
}

public class UserNotFoundException : Exception
{
    public UserNotFoundException(string message) : base(message)
    {
        
    }
}

public class RoomAlreadyExistsException : Exception
{
    public RoomAlreadyExistsException(string message) : base(message)
    {
        
    }
}

public class UserNotInChatroomException : Exception
{
    public UserNotInChatroomException(string message) : base(message)
    {
        
    }
}

public class RoomEmptyException : Exception
{
    public RoomEmptyException(string message) : base(message)
    {
        
    }
}
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

    public ChatRoom JoinChatRoomByName(User user, string roomName)
    {
        var foundRoom = FindChatRoom(roomName);

        if (foundRoom == null)
        {
            throw new RoomNotFoundException($"No room with name: {roomName} found");
        }
        
        User? foundUser = _userRepository.GetUser(user._id);

        if (foundUser == null)
        {
            throw new UserNotFoundException($"Given user could not be found");
        }


        var newParticipants = new List<User>();
        if (!foundRoom._participants.IsNullOrEmpty())
        {
            newParticipants = new List<User>(foundRoom._participants);
        }
        
        newParticipants.Add(foundUser);
        ChatRoom oldRoom = foundRoom;
        foundRoom._participants = newParticipants;
        _roomRepository.UpdateChatRoom(oldRoom._id, foundRoom);
        return foundRoom;
    }

    public ChatRoom LeaveChatRoomByName(User user, string roomName)
    {
        var foundRoom = FindChatRoom(roomName);
        
        if (foundRoom == null)
        {
            throw new RoomNotFoundException($"No room with name: {roomName} found");
        }
        
        User? foundUser = _userRepository.GetUser(user._id);

        if (foundUser == null)
        {
            throw new UserNotFoundException($"Given user could not be found");
        }
        
        if (foundRoom._participants.IsNullOrEmpty())
        {
            throw new RoomEmptyException($"{roomName} is empty");
        }
        var newParticipants = new List<User>(foundRoom._participants);
        newParticipants.Remove(foundUser);
        foundRoom._participants = newParticipants;
        _roomRepository.UpdateChatRoom(foundRoom._id, foundRoom);
        return foundRoom;
    }

    public ChatRoom CreateChatRoom(string roomName)
    {
        var foundRoom = FindChatRoom(roomName);

        if (foundRoom != null)
        {
            throw new RoomAlreadyExistsException($"room: {roomName} already exists");
        }

        ChatRoom createdRoom = new ChatRoom() { _roomName = roomName };
        _roomRepository.AddChatRoom(createdRoom);
        return createdRoom;
    }
    
    public ChatRoom CreateChatRoom(string roomName, User user)
    {
        var foundRoom = FindChatRoom(roomName);

        if (foundRoom != null)
        {
            throw new RoomAlreadyExistsException($"room: {roomName} already exists");
        }

        User? foundUser = _userRepository.GetUser(user._id);

        if (foundUser == null)
        {
            throw new UserNotFoundException($"Given user could not be found");
        }
        
        ChatRoom createdRoom = new ChatRoom() { _roomName = roomName, _participants = new List<User>(){foundUser}};

        
        return _roomRepository.AddChatRoom(createdRoom);
    }

    public ChatMessage SendChatMessage(User user, string roomName, string message)
    {
        var foundRoom = FindChatRoom(roomName);

        if (foundRoom == null)
        {
            throw new RoomNotFoundException($"No room with name: {roomName} found");
        }
        
        User? foundUser = _userRepository.GetUser(user._id);

        if (foundUser == null)
        {
            throw new UserNotFoundException($"Given user could not be found");
        }

        if (foundRoom._participants.IsNullOrEmpty() || !foundRoom._participants.Contains(foundUser))
        {
            throw new UserNotInChatroomException($"Given user is not part of {roomName}");
        }
        
        ChatMessage chatMessage = _messageFactory.CreateNewChatMessage(foundUser, message);

        ChatRoom updateRoom = foundRoom;
        
        var messages = new List<ChatMessage>();
        if (foundRoom._messages.IsNullOrEmpty())
        {
            foundRoom._messages = new List<ChatMessage>();
        }
        messages.Add(chatMessage);
        updateRoom._messages = messages;
        
        _roomRepository.UpdateChatRoom(foundRoom._id, updateRoom);

        return _messageRepository.GetChatMessage(chatMessage._id);
    }

    public IEnumerable<ChatRoom?> GetAllChatRooms()
    {
        return _roomRepository.GetChatRooms();
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _userRepository.GetUsers();
    }

    public ChatRoom? GetChatRoom(int id)
    {
        return _roomRepository.GetChatRoom(id);
    }
    
    public IEnumerable<ChatMessage> GetAllChatMessages()
    {
        return _messageRepository.GetChatMessages();
    }

    public IEnumerable<ChatMessage> GetAllChatMessagesFromRoom(string roomName)
    {
        IEnumerable<ChatRoom?> chatRooms = _roomRepository.GetChatRooms();
        if (chatRooms.Any())
        {
            return chatRooms
                .FirstOrDefault(i => i?._roomName == roomName, null)
                ._messages;
        }
        throw new RoomNotFoundException($"No room with name: {roomName} found");
    }

    public IEnumerable<User> GetAllParticipantsFromRoom(string roomName)
    {
        IEnumerable<ChatRoom?> chatrooms = _roomRepository.GetChatRooms();
        if (chatrooms.Any())
        {
            return chatrooms
                .FirstOrDefault(i => i?._roomName == roomName, null)
                ._participants;
        }

        throw new RoomNotFoundException($"No room with name: {roomName} found");
    }

    public User CreateUser(User user)
    {
        return _userRepository.AddUser(user);
    }

    public User GetUser(int id)
    {
        return _userRepository.GetUser(id);
    }

    public User DeleteUser(int id)
    {
        return _userRepository.DeleteUser(id);
    }

    private ChatRoom? FindChatRoom(string roomName)
    {
        return _roomRepository.GetChatRooms().FirstOrDefault(i => i?._roomName == roomName, null);
    }
}