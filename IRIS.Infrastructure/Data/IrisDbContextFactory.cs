using IRIS.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace IRIS.Infrastructure.Data
{
    public class IrisDbContextFactory : IDesignTimeDbContextFactory<IrisDbContext>
    {
        public IrisDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IrisDbContext>();

            string connectionString = "Data Source=172.17.9.204,1433;Initial Catalog = IRIS_DB;User ID = sa;Password = cheadmin;Connect Timeout = 30;Encrypt = True; Trust Server Certificate = True;Application Intent = ReadWrite;Multi Subnet Failover = False;Command Timeout = 30";


            optionsBuilder.UseSqlServer(connectionString);

            return new IrisDbContext(optionsBuilder.Options);
        }
    }
}