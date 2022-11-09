namespace AccountService.Models;

public class Account
{
    public Account(int id, string name)
    {
        this.id = id;
        this.name = name;
    }

    public int id;
    public string name;

    
    //TODO replace with database
    private readonly List<Account> _placeholderFriendList = new List<Account>();

    public void AddFriend(Account friend)
    {
        if (!_placeholderFriendList.Contains(friend))
        {
            _placeholderFriendList.Add(friend);
        }
    }

    public void RemoveFriend(Account friend)
    {
        if (_placeholderFriendList.Contains(friend))
        {
            _placeholderFriendList.Remove(friend);
        }
    }
    
}