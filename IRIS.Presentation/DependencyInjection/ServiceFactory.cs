using IRIS.Services.Interfaces;
using IRIS.Services.Implementations;
using IRIS.Infrastructure.Data; // To see DbContextFactory

namespace IRIS.Presentation.DependencyInjection
{
    public static class ServiceFactory
    {
        public static IRestockService GetRestockService()
        {
            // 1. Get the context from Infrastructure
            var context = DbContextFactory.Create();

            // 2. Pass it into the Service and return it
            return new RestockService(context);
        }

        // --- NEW METHOD ADDED BELOW ---
        public static IIngredientService GetIngredientService()
        {
            // 1. Get the context (same as above)
            var context = DbContextFactory.Create();

            // 2. Pass it into the IngredientService
            // Note: Ensure your IngredientService.cs constructor accepts 'context' 
            return new IngredientService(context);
        }
    }
}