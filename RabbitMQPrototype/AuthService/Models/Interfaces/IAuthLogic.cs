namespace AuthService.Models.Interfaces;

public interface IAuthLogic
{
    public bool SignIn(string username, string password);
    public bool SignOut();
    public bool Register(User user);
    public IEnumerable<User> GetUsers();
    int GetAccountIdFromGoogleId(string googleId);
}