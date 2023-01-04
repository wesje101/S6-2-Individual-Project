using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatService.Models;

public class ChatMessage
{

    [Key]
    
    public int _id { get; set; }
    
    [Required]
    
    public string _message { get; set; }
    
    [Required]
    
    public User _sender { get; set; }

    public long _timeSent { get; set; }
}