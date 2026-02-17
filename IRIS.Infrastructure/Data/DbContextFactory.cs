using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace IRIS.Infrastructure.Data
{
    public static class DbContextFactory
    {
        public static IrisDbContext Create()
        {
            var settings = ConfigurationManager.ConnectionStrings["IrisConnection"];

            if (settings == null || string.IsNullOrEmpty(settings.ConnectionString))
            {
                throw new Exception("CRITICAL ERROR: Connection string 'IrisConnection' was not found in the App.config. " +
                                    "Please ensure your Startup Project (IRIS.Presentation) has an App.config with this name.");
            }

            var optionsBuilder = new DbContextOptionsBuilder<IrisDbContext>();

            optionsBuilder.UseSqlServer(settings.ConnectionString, sqlOptions =>
                sqlOptions.EnableRetryOnFailure());

            return new IrisDbContext(optionsBuilder.Options);
        }
    }
}