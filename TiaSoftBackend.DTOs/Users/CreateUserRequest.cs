using System.ComponentModel.DataAnnotations;

namespace TiaSoftBackend.DTOs.Users;

public class CreateUserRequest : SignInUserRequest
{
    [Required]
    public required List<string> Roles { get; set; }
}