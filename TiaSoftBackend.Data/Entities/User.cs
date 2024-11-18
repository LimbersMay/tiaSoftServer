using Microsoft.AspNetCore.Identity;

namespace TiaSoftBackend.Data.Entities;

public class User: IdentityUser
{
    public string FullName { get; set; }
}