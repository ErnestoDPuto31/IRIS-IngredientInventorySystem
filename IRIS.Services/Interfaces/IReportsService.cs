using IRIS.Domain.Entities;
using IRIS.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        Task<ReportsDashboardDto> GetDashboardDataAsync(int count = 5);
    }
}