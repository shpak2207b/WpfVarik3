using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfVarik3.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }


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

            // Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Login = "admin",
                    Password = "1",
                    CreatedDate = DateTime.Now,
                    Name = "tima",
                    Role = "admin"
                }
            );

            // --- Categories ---
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "VR Headsets" },
                new Category { CategoryId = 2, CategoryName = "VR Controllers" },
                new Category { CategoryId = 3, CategoryName = "VR Trackers" }
            );

            // --- Suppliers ---
            modelBuilder.Entity<Supplier>().HasData(
                new Supplier { SupplierId = 1, SupplierName = "VirtualTech Ltd", ContactPhone = "+1-555-0101" },
                new Supplier { SupplierId = 2, SupplierName = "Immersion Corp", ContactPhone = "+1-555-0202" },
                new Supplier { SupplierId = 3, SupplierName = "HoloSupply", ContactPhone = "+1-555-0303" }
            );

            modelBuilder.Entity<Product>().HasData(
                // Category 1: VR Headsets
                new Product
                {
                    ProductId = 1,
                    ProductName = "HoloVision X2",
                    CategoryId = 1,
                    SupplierId = 1,
                    Manufacturer = "VirtualTech",
                    Price = 1299.99m,
                    Discount = 0.10m,
                    StockQuantity = 15,
                    CreatedDate = DateTime.Now
                },
                new Product
                {
                    ProductId = 2,
                    ProductName = "VR Spectra Pro",
                    CategoryId = 1,
                    SupplierId = 2,
                    Manufacturer = "Immersion",
                    Price = 1499.00m,
                    Discount = null,
                    StockQuantity = 20,
                    CreatedDate = DateTime.Now
                },
                new Product
                {
                    ProductId = 3,
                    ProductName = "NeoView Lite",
                    CategoryId = 1,
                    SupplierId = 3,
                    Manufacturer = "HoloSupply",
                    Price = 899.50m,
                    Discount = 0.05m,
                    StockQuantity = 35,
                    CreatedDate = DateTime.Now
                },

                // Category 2: VR Controllers
                new Product
                {
                    ProductId = 4,
                    ProductName = "HoloGrip Alpha",
                    CategoryId = 2,
                    SupplierId = 1,
                    Manufacturer = "VirtualTech",
                    Price = 299.99m,
                    Discount = 0.15m,
                    StockQuantity = 40,
                    CreatedDate = DateTime.Now
                },
                new Product
                {
                    ProductId = 5,
                    ProductName = "VR MotionStick S",
                    CategoryId = 2,
                    SupplierId = 2,
                    Manufacturer = "Immersion",
                    Price = 249.00m,
                    Discount = null,
                    StockQuantity = 50,
                    CreatedDate = DateTime.Now
                },
                new Product
                {
                    ProductId = 6,
                    ProductName = "ImmersionPad X",
                    CategoryId = 2,
                    SupplierId = 3,
                    Manufacturer = "HoloSupply",
                    Price = 279.99m,
                    Discount = 0.08m,
                    StockQuantity = 28,
                    CreatedDate = DateTime.Now
                },

                // Category 3: VR Trackers
                new Product
                {
                    ProductId = 7,
                    ProductName = "TrackSphere Mini",
                    CategoryId = 3,
                    SupplierId = 1,
                    Manufacturer = "VirtualTech",
                    Price = 159.99m,
                    Discount = null,
                    StockQuantity = 60,
                    CreatedDate = DateTime.Now
                },
                new Product
                {
                    ProductId = 8,
                    ProductName = "VR StepTracker Pro",
                    CategoryId = 3,
                    SupplierId = 2,
                    Manufacturer = "Immersion",
                    Price = 219.00m,
                    Discount = 0.12m,
                    StockQuantity = 25,
                    CreatedDate = DateTime.Now
                },
                new Product
                {
                    ProductId = 9,
                    ProductName = "MotionDot X1",
                    CategoryId = 3,
                    SupplierId = 3,
                    Manufacturer = "HoloSupply",
                    Price = 189.99m,
                    Discount = 0.05m,
                    StockQuantity = 33,
                    CreatedDate = DateTime.Now
                }
            );
        }

    }
}
