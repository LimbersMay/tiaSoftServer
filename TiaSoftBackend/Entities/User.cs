using Microsoft.AspNetCore.Identity;

namespace TiaSoftBackend.Entities;

public class User: IdentityUser
{
    public string FullName { get; set; }
}