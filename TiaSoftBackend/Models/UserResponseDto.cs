namespace TiaSoftBackend.Models;

public class UserResponseDto
{
    public string UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public List<string> Roles { get; set; }
}