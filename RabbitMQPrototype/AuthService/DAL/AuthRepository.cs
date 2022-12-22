using AuthService.Models;
using AuthService.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthService.DAL;

public class AuthRepository : IAuthRepository
{
    private readonly AuthContext _context;

    public AuthRepository(AuthContext context)
    {
        _context = context;
    }
    
    public User? GetUser(int id)
    {
        return _context.Users.Find(id);
    }

    public User? GetUser(string name)
    {
        return _context.Users.FirstOrDefault(u => u._name == name);
    }

    public IEnumerable<User> GetUsers()
    {
        return _context.Users.ToList();
    }

    public User? AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return GetUser(user._id);
    }

    public User? UpdateUser(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
        return GetUser(user._id);
    }

    public bool DeleteUser(int id)
    {
        var account = GetUser(id);
        if (account == null) return false;
        _context.Users.Remove(account);
        _context.SaveChanges();
        return true;

    }
}