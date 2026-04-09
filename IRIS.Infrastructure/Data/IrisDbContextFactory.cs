using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IRIS.Infrastructure.Data
{
    public class IrisDbContextFactory : IDesignTimeDbContextFactory<IrisDbContext>
    {
        public IrisDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IrisDbContext>();

            string connectionString = ConfigurationManager.ConnectionStrings["IrisConnection"].ConnectionString;

            optionsBuilder.UseSqlServer(connectionString);

            return new IrisDbContext(optionsBuilder.Options);
        }
    }
}