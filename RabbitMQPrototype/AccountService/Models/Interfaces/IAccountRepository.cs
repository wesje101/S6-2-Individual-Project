namespace AccountService.Models.Interfaces;

public interface IAccountRepository
{
    public Account? GetAccount(int id);
    public Account? GetAccount(string name);
    public IEnumerable<Account> GetAccounts();
    public Account? AddAccount(Account account);
    public Account? UpdateAccount(Account account);
    public bool DeleteAccount(int id);
}