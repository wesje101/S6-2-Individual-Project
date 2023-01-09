using AuthService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.DAL;

public class AuthContext : DbContext
{
    public AuthContext(DbContextOptions<AuthContext> options) : base(options)
    {

    }
    
    public DbSet<User> Users { get; set; }
}