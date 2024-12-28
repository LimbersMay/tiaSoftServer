using System.ComponentModel.DataAnnotations;

namespace TiaSoftBackend.DTOs.Users;

public class UpdateUserRequest
{
    [Required]
    [DataType(DataType.Text)]
    public required string Username { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }
    
    public string? Password { get; set; }
    
    [Required]
    public required IEnumerable<string> Roles { get; set; }
}