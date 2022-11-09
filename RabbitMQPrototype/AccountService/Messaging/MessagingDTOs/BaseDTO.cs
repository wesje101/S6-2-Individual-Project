namespace AccountService.Messaging.MessagingDTOs;

public class BaseDTO
{
    public BaseDTO(DTOIdentifier identifier)
    {
        Identifier = identifier;
    }

    public DTOIdentifier Identifier { get; set; }
}