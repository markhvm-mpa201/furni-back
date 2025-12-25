using furni.Models;
using Microsoft.EntityFrameworkCore;

namespace furni.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<Blog> Blogs { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
}