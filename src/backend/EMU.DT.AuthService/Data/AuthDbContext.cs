using Microsoft.EntityFrameworkCore;
using EMU.DT.AuthService.Models;

namespace EMU.DT.AuthService.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 配置关系
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            // 初始化默认角色
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin", Description = "系统管理员" },
                new Role { Id = 2, Name = "Manager", Description = "段厂管理员" },
                new Role { Id = 3, Name = "Engineer", Description = "检修工程师" },
                new Role { Id = 4, Name = "Worker", Description = "检修工人" },
                new Role { Id = 5, Name = "DeviceEngineer", Description = "设备工程师" },
                new Role { Id = 6, Name = "Trainer", Description = "培训讲师" },
                new Role { Id = 7, Name = "DecisionMaker", Description = "决策层" },
                new Role { Id = 8, Name = "Guest", Description = "访客" }
            );

            // 初始化默认管理员用户
            modelBuilder.Entity<User>().HasData(
                new User 
                {
                    Id = 1, 
                    Username = "admin", 
                    PasswordHash = "$2a$11$4390S/q17uGz7uz2bAW9L.DfRQ6A5lWKO7YwH12Z6R4z41F1u/69u", // password: admin123
                    Email = "admin@emu-dt-sim.com",
                    Name = "系统管理员",
                    IsActive = true,
                    CreatedAt = DateTime.Now
                }
            );

            // 为管理员用户分配Admin角色
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { UserId = 1, RoleId = 1 }
            );
        }
    }
}