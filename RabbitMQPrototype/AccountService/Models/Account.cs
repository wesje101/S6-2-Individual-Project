using System.ComponentModel.DataAnnotations;

namespace AccountService.Models;

public class Account
{
    [Key] 
    public int id { get; set; }

    [Required, StringLength(60, MinimumLength = 3)]
    public string name { get; set; }
    
    public List<Account> FriendList { get; set; }


}