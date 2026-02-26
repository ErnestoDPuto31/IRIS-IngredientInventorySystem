namespace IRIS.Services.Interfaces
{
    public interface IInventoryLogService
    {
        void LogTransaction(int ingredientId, string action, decimal quantityChanged, decimal prevStock, decimal newStock);
    }
}