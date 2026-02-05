using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using IRIS.Infrastructure.Data;
using System;
using System.Windows.Forms;
using IRIS.Presentation.Forms;

namespace IRIS.Presentation
{
    internal static class Program
    {
        public static ServiceProvider? Services;

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Setup DI
            var services = new ServiceCollection();
            services.AddDbContext<IrisDbContext>(options =>
                options.UseSqlServer(
                    System.Configuration.ConfigurationManager
                        .ConnectionStrings["IrisConnection"].ConnectionString,
                    sqlOptions => sqlOptions.EnableRetryOnFailure() // handles transient SQL errors
                )
                .ConfigureWarnings(warnings =>
                    warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning) // ignore pending model changes
                )
            );

            Services = services.BuildServiceProvider();

            // Seed database safely (won't crash if DB not ready)
            try
            {
                using var scope = Services.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<IrisDbContext>();
                SeedData.Initialize(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database seeding skipped: {ex.Message}");
            }

            // Run the UI
            Application.Run(new Restock());
        }
    }
}
