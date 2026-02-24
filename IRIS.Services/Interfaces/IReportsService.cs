using IRIS.Domain.Entities;
using System.Collections.Generic;

namespace IRIS.Services.Interfaces
{
    public interface IReportsService
    {
        // Card Data
        int GetTotalIngredients();
        int GetTotalRequests();
        int GetTotalTransactions();
        double GetApprovalRate();

        // Chart Data (Add these!)
        Dictionary<string, double> GetInventoryStats();
        Dictionary<string, double> GetRequestStats();
        Dictionary<string, double> GetCategoryStats();
        List<Request> GetRecentTransactions();
        List<LowStockItem> GetLowStockIngredients();
        List<TopIngredientItem> GetTopUsedIngredients(int count = 5);
    }
}