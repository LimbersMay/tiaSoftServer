using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TiaSoftBackend.Entities;

namespace TiaSoftBackend;

public class ApplicationDbContext: IdentityDbContext<User>
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
    
    public DbSet<Category> Categories { get; set; }
}