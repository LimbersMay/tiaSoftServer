using System.ComponentModel.DataAnnotations;

namespace TiaSoftBackend.DTOs.Users;

public class LoginUserRequest
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}