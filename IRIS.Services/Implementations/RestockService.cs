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

        public RestockService(IrisDbContext context)
        {
            _context = context;
        }

        public List<Restock> GetRestockList()
        {
            return _context.Restocks.ToList();
        }

        public void RefreshRestockData()
        {
            // 1. Get all ingredients that are low on stock
            var lowStockIngredients = _context.Ingredients
                .Where(i => i.CurrentStock <= i.MinimumStock)
                .ToList();

            foreach (var ing in lowStockIngredients)
            {
                // 2. Check if it's already in the Restock table
                var existingRestock = _context.Restocks
                    .FirstOrDefault(r => r.IngredientId == ing.IngredientId);

                if (existingRestock == null)
                {
                    // 3. Add new entry if not exists
                    var newRestock = new Restock
                    {
                        IngredientId = ing.IngredientId,
                        IngredientName = ing.Name ?? "Unknown",
                        Category = ing.Category ?? "Uncategorized",
                        CurrentStock = ing.CurrentStock,
                        MinimumThreshold = ing.MinimumStock,
                        SuggestedRestockQuantity = ing.MinimumStock - ing.CurrentStock,
                        Status = ing.CurrentStock == 0 ? StockStatus.Empty : StockStatus.LowStock
                    };
                    _context.Restocks.Add(newRestock);
                }
                else
                {
                    // 4. Update existing entry with latest numbers
                    existingRestock.CurrentStock = ing.CurrentStock;
                    existingRestock.SuggestedRestockQuantity = ing.MinimumStock - ing.CurrentStock;
                    existingRestock.Status = ing.CurrentStock == 0 ? StockStatus.Empty : StockStatus.LowStock;
                }
            }

            _context.SaveChanges();
        }
        public List<Restock> SearchRestockList(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return GetRestockList();

            return _context.Restocks
                .Where(r => r.IngredientName.Contains(searchTerm) ||
                            r.Category.Contains(searchTerm))
                .ToList();
        }
        public void ProcessRestock(int restockId, decimal quantityReceived)
        {
            var restockItem = _context.Restocks.Find(restockId);
            if (restockItem != null)
            {
                var ingredient = _context.Ingredients.Find(restockItem.IngredientId);
                if (ingredient != null)
                {
                    // Update the master Ingredient stock
                    ingredient.CurrentStock += quantityReceived;
                    ingredient.UpdatedAt = DateTime.Now;

                    // Remove from restock list if it's now above threshold
                    if (ingredient.CurrentStock > ingredient.MinimumStock)
                    {
                        _context.Restocks.Remove(restockItem);
                    }
                }
                _context.SaveChanges();
            }
        }
    }
}