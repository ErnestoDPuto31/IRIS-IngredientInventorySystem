using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
using IRIS.Infrastructure.Data;
using IRIS.Services.Interfaces;
using Microsoft.EntityFrameworkCore; 
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
            return _context.Restocks
                .Include(r => r.Ingredient)
                .ToList();
        }

        public IEnumerable<Restock> GetFilteredRestockList(string category, string status)
        {
            var query = _context.Ingredients.AsQueryable();
            if (!string.IsNullOrEmpty(category) && category != "All" && category != "All Categories")
            {
                if (Enum.TryParse<Categories>(category, true, out var catEnum))
                {
                    query = query.Where(i => i.Category == catEnum);
                }
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
            return query.ToList().Select(i => new Restock
            {
                IngredientId = i.IngredientId,
                Ingredient = i,
            //    SuggestedRestockQuantity = (i.MinimumStock - i.CurrentStock) > 0 ? (i.MinimumStock - i.CurrentStock) : 0,
                Status = i.CurrentStock <= 0 ? StockStatus.Empty :
                         (i.CurrentStock <= i.MinimumStock ? StockStatus.LowStock : StockStatus.WellStocked)
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
        public IEnumerable<Restock> SearchRestockList(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return GetRestockList();

            // FIX: Search inside the linked Ingredient entity
            return _context.Restocks
                .Include(r => r.Ingredient)
                .Where(r => r.Ingredient.Name.Contains(searchTerm))
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
                // Your logic: Empty vs LowStock
                var currentStatus = ing.CurrentStock <= 0 ? StockStatus.Empty : StockStatus.LowStock;

                if (existingRestock == null)
                {
                    var newRestock = new Restock
                    {
                        IngredientId = ing.IngredientId,
                     //   SuggestedRestockQuantity = ing.MinimumStock - ing.CurrentStock,
                        Status = currentStatus
                    };
                    _context.Restocks.Add(newRestock);
                }
                else
                {
                 //   existingRestock.SuggestedRestockQuantity = ing.MinimumStock - ing.CurrentStock;
                    existingRestock.Status = currentStatus;
                }
            }
            var resolvedRestocks = _context.Restocks
                .Include(r => r.Ingredient)
                .AsEnumerable()
                .Where(r => r.Ingredient != null && r.Ingredient.CurrentStock > r.Ingredient.MinimumStock)
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
                  
                  //      restockItem.SuggestedRestockQuantity = ingredient.MinimumStock - ingredient.CurrentStock;
                        restockItem.Status = ingredient.CurrentStock <= 0 ? StockStatus.Empty : StockStatus.LowStock;
                    }
                }
                _context.SaveChanges();
                OnInventoryUpdated?.Invoke();
            }
        }

        public IEnumerable<string> GetCategories()
        {
            // FIX: Convert the Enums to Strings for your UI
            return _context.Ingredients
                .Select(i => i.Category)
                .Distinct()
                .ToList()               // Fetch Enums from DB
                .Select(c => c.ToString()) // Convert to String in memory
                .OrderBy(c => c)
                .ToList();
        }
    }
}