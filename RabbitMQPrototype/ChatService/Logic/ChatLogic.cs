﻿using ChatService.Models;
using ChatService.Models.Interfaces;

namespace ChatService;

public class RoomNotFoundException : Exception
{
    public RoomNotFoundException(string message) : base(message)
    {
        
    }
}

public class RoomAlreadyExistsException : Exception
{
    public RoomAlreadyExistsException(string message) : base(message)
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

    public void JoinChatRoomByName(User user, string roomName)
    {
        var foundRoom = FindChatRoom(roomName);

        if (foundRoom == null)
        {
            throw new RoomNotFoundException($"No room with name: {roomName} found");
        }
        //TODO check if user exists
        foundRoom._participants.Add(user);
    }

    public void LeaveChatRoomByName(User user, string roomName)
    {
        var foundRoom = FindChatRoom(roomName);

        if (foundRoom == null)
        {
            throw new RoomNotFoundException($"No room with name: {roomName} found");
        }
        //TODO check if user exists
        foundRoom._participants.Remove(user);
    }

    public void CreateChatRoom(string roomName)
    {
        var foundRoom = FindChatRoom(roomName);

        if (foundRoom != null)
        {
            throw new RoomAlreadyExistsException($"room: {roomName} already exists");
        }

        _roomRepository.AddChatRoom(new ChatRoom() { _roomName = roomName });
    }
    
    public void CreateChatRoom(string roomName, User user)
    {
        var foundRoom = FindChatRoom(roomName);

        if (foundRoom != null)
        {
            throw new RoomAlreadyExistsException($"room: {roomName} already exists");
        }

        ChatRoom createdRoom = new ChatRoom() { _roomName = roomName };
        //TODO check if user exists
        createdRoom._participants.Add(user);
        
        _roomRepository.AddChatRoom(createdRoom);
    }

    public void SendChatMessage(User user, string roomName, string message)
    {
        var foundRoom = FindChatRoom(roomName);

        if (foundRoom == null)
        {
            throw new RoomNotFoundException($"No room with name: {roomName} found");
        }
        //TODO check if user exists
        ChatMessage chatMessage = _messageFactory.CreateNewChatMessage(user, message, foundRoom);

        ChatRoom updateRoom = foundRoom;
        updateRoom._messages.Add(chatMessage);
        
        _roomRepository.UpdateChatRoom(foundRoom, updateRoom);
        //TODO fix chatroom updates in database
        //TODO Continue here.
        _messageRepository.AddChatMessage(chatMessage);
    }

    public IEnumerable<ChatRoom?> GetAllChatRooms()
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

    private ChatRoom? FindChatRoom(string roomName)
    {
        return _roomRepository.GetChatRooms().FirstOrDefault(i => i?._roomName == roomName, null);
    }
}