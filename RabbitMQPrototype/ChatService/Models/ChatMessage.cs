using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatService.Models;

public class ChatMessage
{
    public ChatMessage()
    {
        this._timeSent = DateTime.Now;
    }
    
    [Key]
    
    public int _id { get; set; }
    
    [Required]
    
    public string _message { get; set; }
    
    [Required]
    
    public User _sender { get; set; }
    
    [Required]
    
    public ChatRoom _ChatRoom { get; set; }

    public DateTime _timeSent { get; }
}