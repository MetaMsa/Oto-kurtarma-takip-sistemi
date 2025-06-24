using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Services.Helper;

public class AppDbContext : DbContext
{
    public DbSet<UsersModel> Users { get; set; }
    public DbSet<RolesModel> Roles { get; set; }
    public DbSet<CompaniesModel> Companies { get; set; }
    public DbSet<StaffModel> Staff { get; set; }
    public DbSet<VehiclesModel> Vehicles { get; set; }

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

        modelBuilder.Entity<CompaniesModel>()
        .HasData(
            new CompaniesModel() { ID = 1, Company = "Moran" }
        );
        
        modelBuilder.Entity<StaffModel>()
        .HasData(
            new StaffModel() { ID = 1, Name = "Mehmet Serhat ASLAN", RolesModelId = 3, CompaniesModelId = 1}
        );

        modelBuilder.Entity<UsersModel>()
        .HasData(
            new UsersModel() { ID = 1, fullname = "Mehmet Serhat Aslan", username = "metamsa", Email = "mehmetserhataslan955@gmail.com", password = "XfAxxAYG1bnA0Ak7hoc/+gQ04FbqHHG7XR/7QVAOLWY=", RolesModelId = 1, CompaniesModelId = 1},
            new UsersModel() { ID = 2, fullname = "Mehmet Serhat Aslan", username = "meta", Email = "mserhataslan@hotmail.com", password = "XfAxxAYG1bnA0Ak7hoc/+gQ04FbqHHG7XR/7QVAOLWY=", RolesModelId = 2, CompaniesModelId = 1}
        );

        modelBuilder.Entity<VehiclesModel>()
        .HasData(
            new VehiclesModel() { ID = 1, plate = "03AYS111", type = "Otomobil", price = 10000, CompaniesModelId = 1}
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

        modelBuilder.Entity<CompaniesModel>().HasMany(c => c.Vehicles)
        .WithOne(v => v.CompaniesModel)
        .HasForeignKey(v => v.CompaniesModelId)
        .OnDelete(DeleteBehavior.Cascade);
    }
}