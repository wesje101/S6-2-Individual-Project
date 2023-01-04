namespace ChatService.Models.Interfaces;

public interface IUserRepository
{
    public User? GetUser(int id);
    public User? GetUser(string name);
    public IEnumerable<User> GetUsers();
    public User? AddUser(User user);
    public User? UpdateUser(User user);
    public User? DeleteUser(int id);
    
    
}