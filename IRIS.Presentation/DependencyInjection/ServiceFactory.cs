using IRIS.Services.Interfaces;
using IRIS.Services.Implementations;
using IRIS.Infrastructure.Data; 

namespace IRIS.Presentation.DependencyInjection
{
    public static class ServiceFactory
    {
        public static IRestockService GetRestockService()
        {
            var context = DbContextFactory.Create();
            return new RestockService(context);
        }
        public static IIngredientService GetIngredientService()
        {
            var context = DbContextFactory.Create();
            return new IngredientService(context);
        }

    }
}