using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using Request = IRIS.Domain.Entities.Request;

namespace IRIS.Infrastructure.Data
{
    public class IrisDbContext : DbContext
    {
        public IrisDbContext(DbContextOptions<IrisDbContext> options)
            : base(options) { }

        // Existing Tables
        public DbSet<User> Users { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestItem> RequestItems { get; set; }
        public DbSet<Approval> Approvals { get; set; }
        public DbSet<InventoryLog> InventoryLogs { get; set; }

        // New Table for Restocking
        public DbSet<Restock> Restocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Existing Role Conversion (String based)
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>(
                    v => v.ToString(),
                    v => (UserRole)Enum.Parse(typeof(UserRole), v)
                );

            // New StockStatus Conversion (Integer based)
            // This stores 0, 1, or 2 in the database for efficiency
            modelBuilder.Entity<Restock>()
                .Property(r => r.Status)
                .HasConversion<int>();

            base.OnModelCreating(modelBuilder);
        }
    }
}