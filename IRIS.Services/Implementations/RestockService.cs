using System;
using System.Collections.Generic;
using System.Linq;
using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
using IRIS.Infrastructure.Data;
using IRIS.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IRIS.Services.Implementations
{
    public class RestockService : IRestockService
    {
        private readonly IrisDbContext _context;
        private readonly IInventoryLogService _logService;

        public event Action OnInventoryUpdated;

        public RestockService(IrisDbContext context, IInventoryLogService logService)
        {
            _context = context;
            _logService = logService;
        }

        public IEnumerable<Restock> GetRestockList()
        {
            return _context.Restocks
                .Include(r => r.Ingredient)
                .AsEnumerable()
                // ---> FIXED: Sort explicitly by the Status property
                .OrderBy(r => r.Status == StockStatus.Empty ? 0 :
                              r.Status == StockStatus.LowStock ? 1 : 2)
                .ThenBy(r => r.Ingredient.CurrentStock) // Empty/Low sorted lowest to highest
                .ToList();
        }

        public IEnumerable<Restock> GetFilteredRestockList(string category, string status)
        {
            var query = _context.Restocks.Include(r => r.Ingredient).AsQueryable();

            if (!string.IsNullOrEmpty(category) && category != "All" && category != "All Categories")
            {
                if (Enum.TryParse<Categories>(category, true, out var catEnum))
                {
                    query = query.Where(r => r.Ingredient.Category == catEnum);
                }
            }

            switch (status)
            {
                case "Low":
                    query = query.Where(r => r.Ingredient.CurrentStock > 0 && r.Ingredient.CurrentStock < r.Ingredient.MinimumStock);
                    break;
                case "Empty":
                    query = query.Where(r => r.Ingredient.CurrentStock <= 0);
                    break;
                case "Well":
                    query = query.Where(r => r.Ingredient.CurrentStock >= r.Ingredient.MinimumStock);
                    break;
            }

            return query
                .AsEnumerable()
                // ---> FIXED: Sort explicitly by the Status property
                .OrderBy(r => r.Status == StockStatus.Empty ? 0 :
                              r.Status == StockStatus.LowStock ? 1 : 2)
                .ThenBy(r => r.Ingredient.CurrentStock)
                .ToList();
        }

        public int GetCountByStatus(string statusType)
        {
            switch (statusType)
            {
                case "Empty":
                    return _context.Ingredients.Count(i => i.CurrentStock <= 0);
                case "Low":
                    return _context.Ingredients.Count(i => i.CurrentStock > 0 && i.CurrentStock < i.MinimumStock);
                case "Well":
                    return _context.Ingredients.Count(i => i.CurrentStock >= i.MinimumStock);
                default:
                    return 0;
            }
        }

        public IEnumerable<Restock> SearchRestockList(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return GetRestockList();

            return _context.Restocks
                .Include(r => r.Ingredient)
                .Where(r => r.Ingredient.Name.Contains(searchTerm))
                .AsEnumerable()
                // ---> FIXED: Sort explicitly by the Status property
                .OrderBy(r => r.Status == StockStatus.Empty ? 0 :
                              r.Status == StockStatus.LowStock ? 1 : 2)
                .ThenBy(r => r.Ingredient.CurrentStock)
                .ToList();
        }

        public void RefreshRestockData()
        {
            var allIngredients = _context.Ingredients.ToList();

            foreach (var ing in allIngredients)
            {
                var existingRestock = _context.Restocks
                    .FirstOrDefault(r => r.IngredientId == ing.IngredientId);

                StockStatus currentStatus;
                if (ing.CurrentStock <= 0)
                    currentStatus = StockStatus.Empty;
                else if (ing.CurrentStock < ing.MinimumStock)
                    currentStatus = StockStatus.LowStock;
                else
                    currentStatus = StockStatus.WellStocked;

                decimal suggestedQty = ing.MinimumStock - ing.CurrentStock;
                if (suggestedQty < 0) suggestedQty = 0;

                if (existingRestock == null)
                {
                    var newRestock = new Restock
                    {
                        IngredientId = ing.IngredientId,
                        Status = currentStatus,
                        SuggestedRestockQuantity = suggestedQty
                    };
                    _context.Restocks.Add(newRestock);
                }
                else
                {
                    existingRestock.Status = currentStatus;
                    existingRestock.SuggestedRestockQuantity = suggestedQty;
                }
            }

            _context.SaveChanges();
            OnInventoryUpdated?.Invoke();
        }

        public void ProcessRestock(int ingredientId, decimal amount)
        {
            var ingredient = _context.Ingredients.Find(ingredientId);

            if (ingredient != null)
            {
                decimal oldStock = ingredient.CurrentStock;

                ingredient.CurrentStock += amount;
                ingredient.UpdatedAt = DateTime.Now;

                var pendingRestock = _context.Restocks.FirstOrDefault(r => r.IngredientId == ingredientId);

                if (pendingRestock != null)
                {
                    if (ingredient.CurrentStock >= ingredient.MinimumStock)
                    {
                        pendingRestock.Status = StockStatus.WellStocked;
                        pendingRestock.SuggestedRestockQuantity = 0;
                    }
                    else
                    {
                        pendingRestock.Status = ingredient.CurrentStock <= 0 ? StockStatus.Empty : StockStatus.LowStock;

                        decimal suggestedQty = ingredient.MinimumStock - ingredient.CurrentStock;
                        pendingRestock.SuggestedRestockQuantity = suggestedQty > 0 ? suggestedQty : 0;
                    }
                }

                _context.SaveChanges();

                _logService.LogTransaction(ingredientId, "Restocked", amount, oldStock, ingredient.CurrentStock);

                OnInventoryUpdated?.Invoke();
            }
        }

        public IEnumerable<string> GetCategories()
        {
            return _context.Ingredients
                .Select(i => i.Category)
                .Distinct()
                .ToList()
                .Select(c => c.ToString())
                .OrderBy(c => c)
                .ToList();
        }
    }
}