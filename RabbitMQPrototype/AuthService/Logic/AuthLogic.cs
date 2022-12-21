using AuthService.Messaging;
using AuthService.Models;
using AuthService.Models.Interfaces;

namespace AuthService.Logic;

public class AuthLogic : IAuthLogic
{
    private MessageBusSender _messageSender = new MessageBusSender();

    private readonly IAuthRepository _repository;

    private readonly ProfanityFilter.ProfanityFilter _filter;
    
    public AuthLogic(IAuthRepository authRepository)
    {
        _repository = authRepository;
        _filter = new ProfanityFilter.ProfanityFilter();
    }
    
    public bool SignIn(string username, string password)
    {
        var foundUser = _repository.GetUser(username);

        return foundUser != null && foundUser._password == password;
    }
    
    public bool SignOut()
    {
        return false;
    }
    
    public bool Register(string username, string password)
    {
        var foundUser = _repository.GetUser(username);
        
        if (foundUser != null) return false;

        if (_filter.ContainsProfanity(username))
        {
            return false;
        }
        
        var newUser = new User(){        
            _name = username,
            _password = password};

        _repository.AddUser(newUser);
        _messageSender.SendMessage(newUser.ToString());
        SignIn(username, password);
        return true;

    }

    public IEnumerable<User> GetUsers()
    {
        return _repository.GetUsers();
    }
}