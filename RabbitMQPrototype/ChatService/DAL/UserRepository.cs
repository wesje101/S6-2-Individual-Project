﻿using ChatService.Models;
using ChatService.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatService.DAL;

public class UserRepository : IUserRepository
{
    private readonly ChatContext _context;
    
    public UserRepository(ChatContext context)
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

    public User? DeleteUser(int id)
    {
        var account = GetUser(id);
        if (account == null) return null;
        _context.Users.Remove(account);
        _context.SaveChanges();
        return account;
    }
}