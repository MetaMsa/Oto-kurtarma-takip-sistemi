using Microsoft.EntityFrameworkCore;
using Entities.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    public DbSet<UsersModel> Users { get; set; }
}