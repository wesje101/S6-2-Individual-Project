using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatService.Models;

public class ChatRoom
{

    [Key]
    public int _id { get; set; }
    
    [Required, MinLength(3), MaxLength(60)]
    public string _roomName { get; set; }
    public IEnumerable<User> _participants { get; set; }
    public IEnumerable<ChatMessage> _messages { get; set; }

}