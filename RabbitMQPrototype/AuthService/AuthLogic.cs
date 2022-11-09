using AuthService.Messaging;
using AuthService.Models;

namespace AuthService;

public class AuthLogic
{
    //TODO Replace with database
    private readonly List<User> _placeholderUserList = new List<User>();

    private MessageBusSender _messageSender = new MessageBusSender();
    
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

        if (foundUser != null) return false;
        
        var newUser = new User(username, password);
        _placeholderUserList.Add(newUser);
        _messageSender.SendMessage(newUser.ToString());
        SignIn(username, password);
        return true;

    }
}