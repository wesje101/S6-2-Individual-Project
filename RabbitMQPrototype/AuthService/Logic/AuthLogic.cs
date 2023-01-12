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

        return foundUser != null;
    }
    
    public bool SignOut()
    {
        return false;
    }
    
    public bool Register(User user)
    {
        var foundUser = _repository.GetUser(user._name);
        
        if (foundUser != null) return false;

        if (_filter.ContainsProfanity(user._name))
        {
            return false;
        }

        _repository.AddUser(user);
        _messageSender.SendMessage(user.ToString());
        SignIn(user._name, "");
        return true;

    }

    public bool ForgetUser(int id)
    {
        var foundUser = _repository.GetUser(id);

        if (foundUser == null) return false;

        _repository.DeleteUser(id);
        return true;
    }

    public IEnumerable<User> GetUsers()
    {
        return _repository.GetUsers();
    }

    public int GetAccountIdFromGoogleId(string googleId)
    {
        return _repository.GetAccountIdFromGoogleId(googleId);
    }
}