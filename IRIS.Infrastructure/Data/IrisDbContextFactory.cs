using IRIS.Domain.Entities;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IRIS.Infrastructure.Data
{
    public class IrisDbContextFactory : IDesignTimeDbContextFactory<IrisDbContext>
    {
        public IrisDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IrisDbContext>();

            optionsBuilder.UseSqlServer(@"Data Source=10.100.6.107,1433;
User ID=admin;Password=admin;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30");

            return new IrisDbContext(optionsBuilder.Options);
        }
    }
}