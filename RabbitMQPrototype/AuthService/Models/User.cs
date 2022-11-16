using AuthService.Messaging.MessagingDTOs;

namespace AuthService.Models;

using System.Text.Json;
using System.Text.Json.Serialization;
public class User
{
    public User(string name, string password)
    {
        _name = name;
        _password = password;
    }

    private readonly int _id;
    private readonly string _name;
    private readonly string _password;
    
    public string GetName()
    {
        return _name;
    }

    public string GetPassword()
    {
        return _password;
    }

    public int GetId()
    {
        return _id;
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(new UserDTO(DTOIdentifier.User, GetId(),GetName(), GetPassword()));
    }
}