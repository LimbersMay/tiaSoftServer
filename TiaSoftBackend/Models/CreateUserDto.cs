using System.ComponentModel.DataAnnotations;

namespace TiaSoftBackend.Models;

public class CreateUserDto : SignInUserDto
{
    [Required]
    public List<string> Roles { get; set; }
}