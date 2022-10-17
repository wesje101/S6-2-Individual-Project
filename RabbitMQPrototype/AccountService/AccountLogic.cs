using AccountService.Models;

namespace AccountService;

using System.Linq;
public class AccountLogic
{
    //TODO Replace with database
    private readonly List<User> _placeholderUserList = new List<User>();

    public AccountLogic()
    {
        
    }
    
    //TODO Authentication methods should move to AuthService
    public bool SignIn(string username, string password)
    {
        var foundUser = _placeholderUserList.FirstOrDefault(i => i.GetName() == username);

        return foundUser != null && foundUser.GetPassword() == password;
    }
    
    public bool SignOut()
    {
        return false;
    }
    
    public bool Register(string username, string password)
    {
        var foundUser = _placeholderUserList.FirstOrDefault(i => i.GetName() == username);

        if (foundUser == null)
        {
            _placeholderUserList.Add(new User(username, password));
            SignIn(username, password);
            return true;
        }
        return false;

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