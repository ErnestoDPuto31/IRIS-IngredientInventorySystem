using IRIS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks; 

namespace IRIS.Services.Interfaces
{
    public interface IReportsService
    {
        Task<int> GetTotalIngredientsAsync();
        Task<int> GetTotalRequestsAsync();
        Task<int> GetTotalTransactionsAsync();
        Task<double> GetApprovalRateAsync();

        Task<Dictionary<string, double>> GetInventoryStatsAsync();
        Task<Dictionary<string, double>> GetRequestStatsAsync();
        Task<Dictionary<string, double>> GetCategoryStatsAsync();

        Task<List<Request>> GetRecentTransactionsAsync();
        Task<List<LowStockItem>> GetLowStockIngredientsAsync();
        Task<List<TopIngredientItem>> GetTopUsedIngredientsAsync(int count = 5);
    }
}