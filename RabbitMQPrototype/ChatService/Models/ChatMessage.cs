using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatService.Models;

public class ChatMessage
{
    public ChatMessage()
    {
        //TODO Move out of constructor, time gets set to current time whenever retrieved from database
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