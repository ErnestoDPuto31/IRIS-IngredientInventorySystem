using IRIS.Infrastructure.Data;
using IRIS.Services;
using IRIS.Services.Implementations;
using IRIS.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Licensing;

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
                    sqlOptions => sqlOptions.EnableRetryOnFailure()
                )
                .ConfigureWarnings(warnings =>
                    warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning)
                )
            );
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IRequestService, RequestService>();
            Services = services.BuildServiceProvider();

            // Seed database safely
            try
            {
                using (IServiceScope scope = Services.CreateScope())
                {
                    var context = Microsoft.Extensions.DependencyInjection
                        .ServiceProviderServiceExtensions.GetRequiredService<IrisDbContext>(scope.ServiceProvider);

                    SeedData.Initialize(context);
                }
            }
            catch (Exception ex)
            {
                // Useful for debugging in your Software Design course
                System.Diagnostics.Debug.WriteLine($"IRIS Database seeding skipped: {ex.Message}");
            }

            // Run the UI
            Application.Run(new LoginForm());
        }
    }
}