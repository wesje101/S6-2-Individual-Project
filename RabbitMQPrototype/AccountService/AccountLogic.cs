using AccountService.Models;

namespace AccountService;

using System.Linq;
public class AccountLogic
{
    public static readonly AccountLogic logic = new AccountLogic();
    
    //TODO Replace with database
    private readonly List<User> _placeholderUserList = new List<User>();
    
    public bool AddUser(string username)
    {
        if (_placeholderUserList.FirstOrDefault(i => i.GetName() == username) != null)
        {
            return false;
        }
        _placeholderUserList.Add(new User(username));
        Console.WriteLine($"Added user: {username}");
        return true;
    }
    
    public bool AddFriend(string username, string friendname)
    {
        var foundUser = _placeholderUserList.FirstOrDefault(i => i.GetName() == username);
        var foundFriend = _placeholderUserList.FirstOrDefault(i => i.GetName() == friendname);

        if (foundUser == null || foundFriend == null) return false;
        foundUser.AddFriend(foundFriend);
        return true;

    }
    
    public bool RemoveFriend(string username, string friendname)
    {
        var foundUser = _placeholderUserList.FirstOrDefault(i => i.GetName() == username);
        var foundFriend = _placeholderUserList.FirstOrDefault(i => i.GetName() == friendname);

        if (foundUser == null || foundFriend == null) return false;
        foundUser.RemoveFriend(foundFriend);
        return true;
        
    }
}