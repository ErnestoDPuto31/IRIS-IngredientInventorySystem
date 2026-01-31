using IRIS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Request = IRIS.Domain.Entities.Request;
using IRIS.Domain.Enums;

namespace IRIS.Infrastructure.Data
{
    public class IrisDbContext : DbContext
    {
        public IrisDbContext(DbContextOptions<IrisDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestItem> RequestItems { get; set; }
        public DbSet<Approval> Approvals { get; set; }
        public DbSet<InventoryLog> InventoryLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>(
                    v => v.ToString(),
                    v => (UserRole)Enum.Parse(typeof(UserRole), v)
                );
        }
    }
}
