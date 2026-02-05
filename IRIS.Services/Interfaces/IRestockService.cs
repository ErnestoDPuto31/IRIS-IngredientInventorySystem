using IRIS.Domain.Entities;
using System.Collections.Generic;

namespace IRIS.Services.Interfaces
{
    public interface IRestockService
    {
        // Get all items currently in the restock table
        List<Restock> SearchRestockList(string searchTerm);
        List<Restock> GetRestockList();

        // Logic to scan Ingredients and update the Restock table
        void RefreshRestockData();

        // Logic to handle the actual restock action
        void ProcessRestock(int restockId, decimal quantityReceived);

    }
}