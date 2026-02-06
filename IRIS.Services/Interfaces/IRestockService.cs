using IRIS.Domain.Entities;
using System.Collections.Generic;
using System;

namespace IRIS.Services.Interfaces
{
    public interface IRestockService
    {
        IEnumerable<Restock> GetRestockList();
        IEnumerable<Restock> GetFilteredRestockList(string category, string status);
        int GetCountByStatus(string statusType);
        IEnumerable<Restock> SearchRestockList(string searchTerm);

        void RefreshRestockData();

        void ProcessRestock(int id, decimal amount);

        IEnumerable<string> GetCategories();

        event Action OnInventoryUpdated;
    }
}