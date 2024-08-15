using System.ComponentModel.DataAnnotations;

namespace TiaSoftBackend.Models;

public class LoginUserDto
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}