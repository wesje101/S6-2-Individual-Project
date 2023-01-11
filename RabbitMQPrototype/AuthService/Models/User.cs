using System.ComponentModel.DataAnnotations;
using AuthService.Messaging.MessagingDTOs;

namespace AuthService.Models;

using System.Text.Json;
using System.Text.Json.Serialization;
public class User
{
    [Key]
    public int _id { get; set; }

    [Required, StringLength(60, MinimumLength = 3)]
    public string _name { get; set; }

    public string GoogleId { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(new UserDTO(DTOIdentifier.User, _id,_name, GoogleId));
    }
}