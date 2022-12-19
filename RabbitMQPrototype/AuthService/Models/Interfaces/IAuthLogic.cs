namespace AuthService.Models.Interfaces;

public interface IAuthLogic
{
    public bool SignIn(string username, string password);
    public bool SignOut();
    public bool Register(string username, string password);
    public IEnumerable<User> GetUsers();
}