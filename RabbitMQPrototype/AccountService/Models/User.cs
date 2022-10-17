namespace AccountService.Models;

public class User
{
    public User(string name, string password)
    {
        this._name = name;
        this._password = password;
    }
    
    private readonly string _name;
    //password might not be required in AccountService, authentication will be handled in AuthService
    private readonly string _password;
    
    //TODO replace with database
    private readonly List<User> _placeholderFriendList = new List<User>();

    public string GetName()
    {
        return _name;
    }

    public string GetPassword()
    {
        return _password;
    }

    public void AddFriend(User friend)
    {
        if (!_placeholderFriendList.Contains(friend))
        {
            _placeholderFriendList.Add(friend);
        }
    }

    public void RemoveFriend(User friend)
    {
        if (_placeholderFriendList.Contains(friend))
        {
            _placeholderFriendList.Remove(friend);
        }
    }
    
}