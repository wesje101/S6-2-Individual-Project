namespace AccountService.Models;

public class User
{
    public User(string name)
    {
        this._name = name;
    }
    
    private readonly string _name;

    
    //TODO replace with database
    private readonly List<User> _placeholderFriendList = new List<User>();

    public string GetName()
    {
        return _name;
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