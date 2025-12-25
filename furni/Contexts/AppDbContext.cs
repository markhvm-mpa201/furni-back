using furni.Models;
using Microsoft.EntityFrameworkCore;

namespace furni.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public AppDbContext(DbContextOptions options) : base(options)
    {        
    }
}
