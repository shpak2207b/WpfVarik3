using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfVarik3.Models
{
    class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Role> Roles { get; set; }

        //User _user1 = new User { Id = 1, Login = "a", Password = "1", RegistrationDate = new DateOnly(2014, 04, 04), Name = "aaa", Surname = "bbb", PhoneNumber = "111", RoleId = 2 };

        public AppDbContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=db.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Login = "admin", Password = "1", RegistrationDate = new DateOnly(2014, 04, 04), Name = "aaa", Surname = "bbb", PhoneNumber = "111", RoleId = 2 }
                );
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "User", AccessRight="partial"},
                new Role { Id = 2, Name = "Admin", AccessRight="full"}
                );
            modelBuilder.Entity<Inventory>().HasData(
                new Inventory { Id = 1, InventoryNumber = 111, Name = "ball", Type = "balls", Description = "sfafdasf", PublicationDate = new DateOnly(2025, 06, 23), Status = Status.InStock },
                new Inventory { Id = 2, InventoryNumber = 123, Name = "stick", Type = "sticks", Description = "sfafdasf", PublicationDate = new DateOnly(2025, 06, 23), Status = Status.InStock }

                );
        }
    }
}
