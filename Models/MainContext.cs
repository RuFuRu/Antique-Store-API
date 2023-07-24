using Microsoft.EntityFrameworkCore;

namespace Antique_Store_API.Models;
public class MainContext : DbContext
{
    private readonly IConfiguration _configuration;
    
    public MainContext(DbContextOptions<MainContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }
    
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(ConStr.GetConStr());
    }
}