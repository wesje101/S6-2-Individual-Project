namespace AuthService.Models.Interfaces;

public interface IAuthRepository
{
    public User? GetUser(int id);
    public User? GetUser(string name);
    public IEnumerable<User> GetUsers();
    public User? AddUser(User user);
    public User? UpdateUser(User user);
    public bool DeleteUser(int id);
    int GetAccountIdFromGoogleId(string googleId);
}