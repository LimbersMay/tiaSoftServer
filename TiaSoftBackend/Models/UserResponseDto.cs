namespace TiaSoftBackend.Models;

public class UserResponseDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public List<string> Roles { get; set; }
}