using Microsoft.EntityFrameworkCore;
using Entities.Models;

public class AppDbContext : DbContext
{
    public DbSet<UsersModel> Users { get; set; }
    public DbSet<RolesModel> Roles { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<RolesModel>()
        .HasData(
            new RolesModel() {ID = 1, Role = "Admin"},
            new RolesModel() {ID = 2, Role = "User"}
        );
    }
}