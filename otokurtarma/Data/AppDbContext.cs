using Microsoft.EntityFrameworkCore;
using otokurtarma.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    public DbSet<UsersModel> Users {get; set;}
}