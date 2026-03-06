using IRIS.Domain.Contracts;
using IRIS.Domain.Entities;

namespace IRIS.Services.Interfaces
{
    public interface IReportsService
    {
        int GetTotalIngredients();
        int GetTotalRequests();
        int GetTotalTransactions();
        double GetApprovalRate();

        Dictionary<string, double> GetInventoryStats();
        Dictionary<string, double> GetRequestStats();
        Dictionary<string, double> GetCategoryStats();
        List<Request> GetRecentTransactions();
        List<LowStockItem> GetLowStockIngredients();
        List<TopIngredientItem> GetTopUsedIngredients(int count = 5);

        Task<ReportsDashboardSummary> GetDashboardDataAsync(int count = 5);
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