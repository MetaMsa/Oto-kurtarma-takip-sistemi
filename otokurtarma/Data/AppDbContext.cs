using Microsoft.EntityFrameworkCore;
using Entities.Models;

public class AppDbContext : DbContext
{
    public DbSet<UsersModel> Users { get; set; }
    public DbSet<RolesModel> Roles { get; set; }
    public DbSet<CompaniesModel> Companies { get; set; }
    public DbSet<StaffModel> Staff { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<RolesModel>()
        .HasData(
            new RolesModel() { ID = 1, Role = "Admin" },
            new RolesModel() { ID = 2, Role = "User" },
            new RolesModel() { ID = 3, Role = "Staff" }
        );

        modelBuilder.Entity<RolesModel>().HasMany(c => c.Users)
        .WithOne(u => u.RolesModel)
        .HasForeignKey(u => u.RolesModelId)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<RolesModel>().HasMany(r => r.Staffs)
        .WithOne(s => s.RolesModel)
        .HasForeignKey(s => s.RolesModelId)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CompaniesModel>().HasMany(c => c.Users)
        .WithOne(u => u.CompaniesModel)
        .HasForeignKey(u => u.CompaniesModelId)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CompaniesModel>().HasMany(c => c.Staffs)
        .WithOne(s => s.CompaniesModel)
        .HasForeignKey(s => s.CompaniesModelId)
        .OnDelete(DeleteBehavior.Cascade);
    }
}