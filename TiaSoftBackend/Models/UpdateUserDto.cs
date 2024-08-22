using System.ComponentModel.DataAnnotations;

namespace TiaSoftBackend.Models;

public class UpdateUserDto
{
    [Required]
    [DataType(DataType.Text)]
    public string Username { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    
    public string? Password { get; set; }
    
    [Required]
    public IEnumerable<string> Roles { get; set; }
}