using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountService.Models;

public class Account
{
    [Key] [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int id { get; set; }
    
    public string name { get; set; }
    
    public List<Account> FriendList { get; set; }


}