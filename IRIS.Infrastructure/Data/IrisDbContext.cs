using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Request = IRIS.Domain.Entities.Request;

namespace IRIS.Infrastructure.Data
{
    public class IrisDbContext : DbContext
    {
        public IrisDbContext(DbContextOptions<IrisDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestDetails> RequestItems { get; set; }
        public DbSet<Approval> Approvals { get; set; }
        public DbSet<InventoryLog> InventoryLogs { get; set; }

        public DbSet<Restock> Restocks { get; set; }
        public DbSet<SystemNotification> SystemNotifications { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
            modelBuilder.Entity<Request>()
                .HasOne(r => r.EncodedBy)
                .WithMany()
                .HasForeignKey(r => r.EncodedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Approval>()
                .HasOne(a => a.Approver)
                .WithMany()
                .HasForeignKey(a => a.ApproverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>(
                    v => v.ToString(),
                    v => (UserRole)Enum.Parse(typeof(UserRole), v)
                );

            modelBuilder.Entity<Restock>()
                .Property(r => r.Status)
                .HasConversion<int>();
        }
    }
}