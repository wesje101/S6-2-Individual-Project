using System.ComponentModel.DataAnnotations;

namespace ChatService.Models;

public class User
{
    [Key]
    
    public int _id { get; set; }
    
    [Required, StringLength(60, MinimumLength = 3)]
    public string _name { get; set; }
    
}