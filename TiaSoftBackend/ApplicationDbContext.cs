using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TiaSoftBackend;

public class ApplicationDbContext: IdentityDbContext
{
    
    private readonly string _connectionString;
    
    public ApplicationDbContext(DbContextOptions options, IConfiguration configuration): base(options)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL(_connectionString);
    }
}