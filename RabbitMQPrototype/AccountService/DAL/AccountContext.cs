using AccountService.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountService.DAL;

public class AccountContext : DbContext
{
    public AccountContext(DbContextOptions<AccountContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    public DbSet<Account> Accounts { get; set; }
}