using System.ComponentModel.DataAnnotations;

namespace TiaSoftBackend.DTOs.Users;

public class SignInUserRequest
{
    [Required]
    [DataType(DataType.Text)]
    public string UserName { get; set; }
    
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}