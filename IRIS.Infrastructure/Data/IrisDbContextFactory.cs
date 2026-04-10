using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Configuration;
using static System.Net.WebRequestMethods;

namespace IRIS.Infrastructure.Data
{
    public class IrisDbContextFactory : IDesignTimeDbContextFactory<IrisDbContext>
    {
        public IrisDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IrisDbContext>();

           string connectionString = ConfigurationManager.ConnectionStrings["IrisConnection"].ConnectionString;
          //change test
          //changeste2

            optionsBuilder.UseSqlServer(connectionString);

            return new IrisDbContext(optionsBuilder.Options);
        }
    }
}