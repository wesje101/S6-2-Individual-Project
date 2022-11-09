namespace AccountService.Messaging.MessagingDTOs;

public class UserDTO
{
    public UserDTO(DTOIdentifier identifier , string username, string password)
    {
        Identifier = identifier;
        Username = username;
        Password = password;
    }

    public DTOIdentifier Identifier { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}