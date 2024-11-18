using System.ComponentModel.DataAnnotations;

namespace TiaSoftBackend.DTOs.Users;

public class CreateUserRequest
{
    [Required]
    public List<string> Roles { get; set; }
}