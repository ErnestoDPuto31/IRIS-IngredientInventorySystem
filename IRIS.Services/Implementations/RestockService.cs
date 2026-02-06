using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
using IRIS.Infrastructure.Data;
using IRIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IRIS.Services.Implementations
{
    public class RestockService : IRestockService
    {
        private readonly IrisDbContext _context;

        public event Action OnInventoryUpdated;

        public RestockService(IrisDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Restock> GetRestockList()
        {
            return _context.Restocks.ToList();
        }

        // --- NEW CLEAN ARCHITECTURE METHODS ---

        public IEnumerable<Restock> GetFilteredRestockList(string category, string status)
        {
            var query = _context.Ingredients.AsQueryable();

            if (!string.IsNullOrEmpty(category) && category != "All")
            {
                query = query.Where(i => i.Category == category);
            }

            switch (status)
            {
                case "Low":
                    query = query.Where(i => i.CurrentStock > 0 && i.CurrentStock <= i.MinimumStock);
                    break;
                case "Empty":
                    query = query.Where(i => i.CurrentStock <= 0);
                    break;
                case "Well":
                    query = query.Where(i => i.CurrentStock > i.MinimumStock);
                    break;
            }


            return query.Select(i => new Restock
            {
                IngredientId = i.IngredientId,
                IngredientName = i.Name ?? "Unknown",
                Category = i.Category ?? "Uncategorized",
                CurrentStock = i.CurrentStock,
                MinimumThreshold = i.MinimumStock,
                SuggestedRestockQuantity = (i.MinimumStock - i.CurrentStock) > 0 ? (i.MinimumStock - i.CurrentStock) : 0,
                Status = i.CurrentStock <= 0 ? StockStatus.Empty :
                         (i.CurrentStock <= i.MinimumStock ? StockStatus.LowStock : StockStatus.LowStock) 
            }).ToList();
        }

        public int GetCountByStatus(string statusType)
        {
            switch (statusType)
            {
                case "Empty":
                    return _context.Ingredients.Count(i => i.CurrentStock <= 0);
                case "Low":
                    return _context.Ingredients.Count(i => i.CurrentStock > 0 && i.CurrentStock <= i.MinimumStock);
                case "Well":
                    return _context.Ingredients.Count(i => i.CurrentStock > i.MinimumStock);
                default:
                    return 0;
            }
        }

        // --------------------------------------

        public IEnumerable<Restock> SearchRestockList(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return GetRestockList();

            return _context.Restocks
                .Where(r => r.IngredientName.Contains(searchTerm) ||
                            r.Category.Contains(searchTerm))
                .ToList();
        }

        public void RefreshRestockData()
        {
            var lowStockIngredients = _context.Ingredients
                .Where(i => i.CurrentStock <= i.MinimumStock)
                .ToList();

            foreach (var ing in lowStockIngredients)
            {
                var existingRestock = _context.Restocks
                    .FirstOrDefault(r => r.IngredientId == ing.IngredientId);

                var currentStatus = ing.CurrentStock <= 0 ? StockStatus.Empty : StockStatus.LowStock;

                if (existingRestock == null)
                {
                    var newRestock = new Restock
                    {
                        IngredientId = ing.IngredientId,
                        IngredientName = ing.Name ?? "Unknown",
                        Category = ing.Category ?? "Uncategorized",
                        CurrentStock = ing.CurrentStock,
                        MinimumThreshold = ing.MinimumStock,
                        SuggestedRestockQuantity = ing.MinimumStock - ing.CurrentStock,
                        Status = currentStatus,
                    };
                    _context.Restocks.Add(newRestock);
                }
                else
                {
                    existingRestock.CurrentStock = ing.CurrentStock;
                    existingRestock.SuggestedRestockQuantity = ing.MinimumStock - ing.CurrentStock;
                    existingRestock.Status = currentStatus;
                }
            }

            var resolvedRestocks = _context.Restocks
                .AsEnumerable()
                .Where(r => !_context.Ingredients.Any(i => i.IngredientId == r.IngredientId && i.CurrentStock <= i.MinimumStock))
                .ToList();

            if (resolvedRestocks.Any())
            {
                _context.Restocks.RemoveRange(resolvedRestocks);
            }

            _context.SaveChanges();
            OnInventoryUpdated?.Invoke();
        }

        public void ProcessRestock(int id, decimal amount)
        {
            var restockItem = _context.Restocks.Find(id);
            if (restockItem != null)
            {
                var ingredient = _context.Ingredients.Find(restockItem.IngredientId);
                if (ingredient != null)
                {
                    ingredient.CurrentStock += amount;
                    ingredient.UpdatedAt = DateTime.Now;

                    if (ingredient.CurrentStock > ingredient.MinimumStock)
                    {
                        _context.Restocks.Remove(restockItem);
                    }
                    else
                    {
                        restockItem.CurrentStock = ingredient.CurrentStock;
                        restockItem.SuggestedRestockQuantity = ingredient.MinimumStock - ingredient.CurrentStock;
                    }
                }
                _context.SaveChanges();
                OnInventoryUpdated?.Invoke();
            }
        }

        public IEnumerable<string> GetCategories()
        {
            return _context.Ingredients
                            .Where(i => !string.IsNullOrEmpty(i.Category))
                            .Select(i => i.Category)
                            .Distinct()
                            .OrderBy(c => c)
                            .ToList();
        }
    }
}