namespace AuthService.Messaging.MessagingDTOs;


public class UserDTO
{
    public UserDTO(DTOIdentifier identifier, int id, string username, string password)
    {
        Identifier = identifier;
        Id = id;
        Username = username;
        Password = password;
    }

    public DTOIdentifier Identifier { get; set; }
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}