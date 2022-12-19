using System.ComponentModel.DataAnnotations;

namespace ChatService.Models;

public class ChatRoom
{
    [Key]
    public int _id;
    
    [Required, MinLength(3), MaxLength(60)]
    public string _roomName;
    
    public List<User> _participants;
    
    public List<ChatMessage> _messages;

}