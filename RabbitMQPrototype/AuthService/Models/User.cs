using AuthService.Messaging.MessagingDTOs;

namespace AuthService.Models;

using System.Text.Json;
using System.Text.Json.Serialization;
public class User
{
    public User(string name, string password)
    {
        this._name = name;
        this._password = password;
    }
    
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

    public override string ToString()
    {
        return JsonSerializer.Serialize(new UserDTO(DTOIdentifier.User,GetName(), GetPassword()));
    }
}