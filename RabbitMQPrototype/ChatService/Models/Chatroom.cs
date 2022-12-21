using System.ComponentModel.DataAnnotations;

namespace ChatService.Models;

public class ChatRoom
{
    public ChatRoom()
    {
        if (_participants == null)
        {
            _participants = new List<User>();
        }

        if (_messages == null)
        {
            _messages = new List<ChatMessage>();
        }
    }
    
    [Key]
    public int _id { get; set; }
    
    [Required, MinLength(3), MaxLength(60)]
    public string _roomName { get; set; }

    public List<User> _participants { get; set; }
    public List<ChatMessage> _messages { get; set; }

}