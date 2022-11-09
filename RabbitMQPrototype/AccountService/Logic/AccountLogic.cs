using AccountService.Models;
using AccountService.Models.Interfaces;

namespace AccountService;

using System.Linq;
public class AccountLogic : IAccountLogic
{
   
    private readonly IAccountRepository _repository;

    public AccountLogic(IAccountRepository repository)
    {
        _repository = repository;
    }

    public Account? AddFriend(int id, int friendId)
    {
        var foundUser = GetAccount(id);
        var foundFriend = GetAccount(friendId);
        
        if (foundUser == null || foundFriend == null) return foundUser;
        foundUser.AddFriend(foundFriend);
        return foundUser;
    }

    public Account? RemoveFriend(int id, int friendId)
    {
        var foundUser = GetAccount(id);
        var foundFriend = GetAccount(friendId);
        
        if (foundUser == null || foundFriend == null) return foundUser;
        foundUser.RemoveFriend(foundFriend);
        return foundUser;
    }

    public Account? AddFriend(string name, string friendName)
    {
        var foundUser = GetAccount(name);
        var foundFriend = GetAccount(friendName);
        
        if (foundUser == null || foundFriend == null) return foundUser;
        foundUser.AddFriend(foundFriend);
        return foundUser;

    }
    
    public Account? RemoveFriend(string name, string friendName)
    {

        var foundUser = GetAccount(name);
        var foundFriend = GetAccount(friendName);
        
        if (foundUser == null || foundFriend == null) return foundUser;
        foundUser.RemoveFriend(foundFriend);
        return foundUser;
        
    }

    public Account? GetAccount(int id)
    {
        return _repository.GetAccount(id);
    }

    public Account? GetAccount(string name)
    {
        return _repository.GetAccount(name);
    }

    public IEnumerable<Account> GetAccounts()
    {
        return _repository.GetAccounts();
    }

    public Account? AddAccount(Account account)
    {
        return _repository.AddAccount(account);
    }

    public Account? UpdateAccount(Account account)
    {
        return _repository.UpdateAccount(account);
    }

    public bool DeleteAccount(int id)
    {
        return _repository.DeleteAccount(id);
    }
}