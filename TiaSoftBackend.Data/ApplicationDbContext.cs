using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TiaSoftBackend.Data.Entities;

namespace TiaSoftBackend.Data;

public class ApplicationDbContext: IdentityDbContext<User>
{
    
    private readonly string _connectionString;
    
    public ApplicationDbContext(DbContextOptions options, IConfiguration configuration): base(options)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL(_connectionString).EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Identity models updated maxlength 
        builder.Entity<IdentityUser>(entity => entity.Property(m => m.NormalizedEmail).HasMaxLength(85));
        builder.Entity<IdentityUser>(entity => entity.Property(m => m.NormalizedUserName).HasMaxLength(85));

        builder.Entity<IdentityRole>(entity => entity.Property(m => m.NormalizedName).HasMaxLength(85));

        builder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(85));
        builder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.ProviderKey).HasMaxLength(85));
        
        builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(85));
        builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.Name).HasMaxLength(85));

        builder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(85));
        builder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(85));
        
        // Many-to-many relationship between Order and Product
        builder.Entity<OrderProduct>()
            .HasKey(op => new { op.OrderId, op.ProductId });
        
        // Set the default value for the CreatedAt and UpdatedAt fields
        builder.Entity<OrderProduct>()
            .Property(c => c.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        builder.Entity<OrderProduct>()
            .Property(c => c.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP");
        
        // Set the default value for the CreatedAt and UpdatedAt fields on Order
        builder.Entity<Order>()
            .Property(c => c.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        builder.Entity<Order>()
            .Property(c => c.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP");
    }

    public DbSet<Category> Categories { get; set; }
    
    public DbSet<Product> Products { get; set; }
    
    public DbSet<Area> Areas { get; set; }
    
    public DbSet<TableStatus> TableStatuses { get; set; }
    
    public DbSet<TableEntity> Tables { get; set; }
    
    public DbSet<OrderStatus> OrderStatuses { get; set; }
    
    public DbSet<Order> Orders { get; set; }
    
    public DbSet<OrderProduct> OrderProducts { get; set; }
    
    public DbSet<Bill> Bills { get; set; }
    
    public DbSet<DailyOrderCounter> DailyOrderCounters { get; set; }
}