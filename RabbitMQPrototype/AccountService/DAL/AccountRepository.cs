using AccountService.Models;
using AccountService.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AccountService.DAL;

public class AccountRepository : IAccountRepository
{
    private readonly AccountContext _context;
    private readonly ILogger<AccountRepository> _logger;

    string? runningEnvironment = Environment.GetEnvironmentVariable("HOSTED_ENVIRONMENT");

    
    public AccountRepository(AccountContext context, ILogger<AccountRepository> logger)
    {
        _context = context;
        _logger = logger;
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
        switch (runningEnvironment)
        {
            case("docker"):
                _context.Accounts.Add(account);
                _context.SaveChanges();
                break;
            case("kubernetes"):
                var transaction = _context.Database.BeginTransaction();
        
                _context.Database.ExecuteSql($"SET IDENTITY_INSERT [dbo].[Accounts] ON");
                _logger.LogInformation("SET IDENTITY_INSERT to ON");
                _context.Accounts.Add(account);
                _logger.LogInformation("Adding account");
                _context.SaveChanges();
                _logger.LogInformation("Saving changes");
                _context.Database.ExecuteSql($"SET IDENTITY_INSERT [dbo].[Accounts] OFF");
                _logger.LogInformation("SET IDENTITY_INSERT to OFF");
        
                transaction.Commit();
                break;
            default:
                _context.Accounts.Add(account);
                _context.SaveChanges();
                break;
        }
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
        Account deletedAccount = new Account() { id = id, name = "[Deleted]" };
        var account = GetAccount(id);
        if (account == null) return false;
        UpdateAccount(deletedAccount);
        return true;

    }
}