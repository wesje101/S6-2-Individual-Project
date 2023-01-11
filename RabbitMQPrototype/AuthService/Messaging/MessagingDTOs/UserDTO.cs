namespace AuthService.Messaging.MessagingDTOs;


public class UserDTO
{
    public UserDTO(DTOIdentifier identifier, int id, string username, string googleId)
    {
        Identifier = identifier;
        Id = id;
        Username = username;
        GoogleId = googleId;
    }

    public DTOIdentifier Identifier { get; set; }
    public int Id { get; set; }
    public string Username { get; set; }
    public string GoogleId { get; set; }
}