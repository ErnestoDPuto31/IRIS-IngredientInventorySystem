using IRIS.Domain.Entities;

namespace IRIS.Services.Interfaces
{
    public interface IInventoryLogService
    {
        void LogTransaction(int ingredientId, string action, decimal quantityChanged, decimal prevStock, decimal newStock);
        IEnumerable<InventoryLog> GetPaginatedLogs(int pageNumber, int pageSize = 10, string searchTerm = "");
        int GetTotalLogCount(string searchTerm = "");
    }
}