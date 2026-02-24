using IRIS.Services.Interfaces;
using IRIS.Services.Implementations;
using IRIS.Infrastructure.Data;

namespace IRIS.Presentation.DependencyInjection
{
    public static class ServiceFactory
    {
        public static IInventoryLogService GetInventoryLogService()
        {
            var context = DbContextFactory.Create();
            return new InventoryLogService(context);
        }

        public static IRestockService GetRestockService()
        {
            var context = DbContextFactory.Create();
            var logService = new InventoryLogService(context);
            return new RestockService(context, logService);
        }

        public static IIngredientService GetIngredientService()
        {
            var context = DbContextFactory.Create();
            var logService = new InventoryLogService(context);
            return new IngredientService(context, logService);
        }

        public static IRequestService GetRequestService()
        {
            var context = DbContextFactory.Create();
            return new RequestService(context);
        }

        public static IReportsService GetReportsService()
        {
            var context = DbContextFactory.Create();
            return new ReportsService(context);
        }
    }
}