namespace AccountService.Models.Interfaces;

public interface IAccountLogic
{
    public Account? GetAccount(int id);
    public Account? GetAccount(string name);
    public Account? AddFriend(int id, int friendId);
    public Account? AddFriend(string name, string friendName);
    public Account? RemoveFriend(int id, int friendId);
    public Account? RemoveFriend(string name, string friendName);
    public IEnumerable<Account> GetAccounts();
    public Account? AddAccount(Account account);
    public Account? UpdateAccount(Account account);
    public bool DeleteAccount(int id);
}