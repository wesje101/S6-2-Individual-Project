using AccountService.Models;
using AccountService.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AccountService.DAL;

public class AccountRepository : IAccountRepository
{
    private readonly AccountContext _context;

    public AccountRepository(AccountContext context)
    {
        _context = context;
        //TODO figure out migration
        if (Environment.GetEnvironmentVariable("HOSTED_ENVIRONMENT") == "docker")
        {
            context.Database.Migrate();
        }
    }
    
    public Account? GetAccount(int id)
    {
        return _context.Accounts.Find(id);
    }

    public Account? GetAccount(string name)
    {
        return _context.Accounts.Find(name);
    }

    public IEnumerable<Account> GetAccounts()
    {
        return _context.Accounts.ToList();
    }

    public Account? AddAccount(Account account)
    {
        _context.Accounts.Add(account);
        _context.SaveChanges();
        return GetAccount(account.id);
    }

    public Account? UpdateAccount(Account account)
    {
        _context.Accounts.Update(account);
        _context.SaveChanges();
        return GetAccount(account.id);
    }

    public bool DeleteAccount(int id)
    {
        var account = GetAccount(id);
        if (account == null) return false;
        _context.Accounts.Remove(account);
        _context.SaveChanges();
        return true;

    }
}