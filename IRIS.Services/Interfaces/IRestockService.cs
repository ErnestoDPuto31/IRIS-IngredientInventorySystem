using IRIS.Domain.Entities;
using System.Collections.Generic;
using System;

namespace IRIS.Services.Interfaces
{
    public interface IRestockService
    {
        // Get all items (existing)
        IEnumerable<Restock> GetRestockList();

        // --- NEW: Clean Architecture Filters ---

        // 1. Used by the Grid to filter by Category ("All", "Spices", etc.) and Status ("Low", "Empty", "Well")
        IEnumerable<Restock> GetFilteredRestockList(string category, string status);

        // 2. Used by the Status Cards to get the count directly (e.g. returns 5 for "Low")
        int GetCountByStatus(string statusType);

        // ---------------------------------------

        IEnumerable<Restock> SearchRestockList(string searchTerm);

        void RefreshRestockData();

        void ProcessRestock(int id, decimal amount);

        IEnumerable<string> GetCategories();

        event Action OnInventoryUpdated;
    }
}