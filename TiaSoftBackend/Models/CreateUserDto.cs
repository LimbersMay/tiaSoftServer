using System.ComponentModel.DataAnnotations;

namespace TiaSoftBackend.Models;

public class CreateUserDto
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