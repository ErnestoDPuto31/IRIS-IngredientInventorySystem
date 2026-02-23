using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
using IRIS.Infrastructure.Data;
using IRIS.Services.Interfaces;
using IRIS.Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IRIS.Services.Implementations
{
    public class ReportsService : IReportsService
    {
        private readonly IrisDbContext _context;

        public ReportsService(IrisDbContext context)
        {
            _context = context;
        }

        // --- CARDS ---
        public int GetTotalIngredients() => _context.Ingredients.Count();
        public int GetTotalRequests() => _context.Requests.Count();
        public int GetTotalTransactions() => _context.Requests.Count(r => r.Status == RequestStatus.Released);

        public double GetApprovalRate()
        {
            var total = _context.Requests.Count();
            if (total == 0) return 0.0;
            var approved = _context.Requests.Count(r => r.Status == RequestStatus.Approved || r.Status == RequestStatus.Released);
            return Math.Round((double)approved / total * 100, 1);
        }

        // --- CHARTS (NEW LOGIC) ---

        public Dictionary<string, double> GetInventoryStats()
        {
            // Logic: Compare CurrentStock vs MinimumStock
            var empty = _context.Ingredients.Count(i => i.CurrentStock <= 0);
            var low = _context.Ingredients.Count(i => i.CurrentStock > 0 && i.CurrentStock < i.MinimumStock);
            var full = _context.Ingredients.Count(i => i.CurrentStock >= i.MinimumStock);

            return new Dictionary<string, double>
            {
                { "Empty", empty },
                { "Low Stock", low },
                { "Full Stock", full }
            };
        }

        public Dictionary<string, double> GetRequestStats()
        {
            // Group by Status Enum
            return _context.Requests
                .GroupBy(r => r.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToDictionary(x => x.Status.ToString(), x => (double)x.Count);
        }

        public Dictionary<string, double> GetCategoryStats()
        {
            // STEP 1: Bring the grouped data into C# memory
            var rawStats = _context.Ingredients
                .GroupBy(i => i.Category)
                .Select(g => new { Cat = g.Key, Count = g.Count() })
                .ToList();

            // STEP 2: Since Cat is definitely an enum, just call your helper directly!
            return rawStats.ToDictionary(
                x => x.Cat.GetDisplayName(),
                x => (double)x.Count
            );
        }

        // --- NEW: Table Implementation ---
        public List<Request> GetRecentTransactions()
        {
            return _context.Requests
                .AsNoTracking()
                .Include(r => r.EncodedBy) // Corrected: Use 'EncodedBy' navigation property
                .OrderByDescending(r => r.CreatedAt) // Corrected: Use 'CreatedAt'
                .Take(10)
                .ToList();
        }

        public List<IRIS.Domain.Entities.LowStockItem> GetLowStockIngredients()
        {
            return _context.Ingredients
                .AsNoTracking()
                .Where(i => i.CurrentStock < i.MinimumStock)
                .Select(i => new { i.Name, i.Category, i.CurrentStock, i.MinimumStock, i.Unit })
                .AsEnumerable()
                .Select(i => new IRIS.Domain.Entities.LowStockItem(
                    i.Name ?? "Unknown",
                    i.Category.ToString(), 
                    (float)i.CurrentStock,
                    (float)i.MinimumStock,
                    i.Unit ?? "pcs"
                ))
                .ToList();
        }

        public List<TopIngredientItem> GetTopUsedIngredients(int count = 5)
        {         
            var topUsed = _context.InventoryLogs
                .AsNoTracking()
                .Where(log => log.QuantityChanged < 0) 
                .GroupBy(log => log.IngredientId)
                .Select(group => new
                {
                    IngredientId = group.Key,
                    TotalUsedAmount = Math.Abs(group.Sum(l => l.QuantityChanged))
                })
                .OrderByDescending(x => x.TotalUsedAmount)
                .Take(count)
                .Join(_context.Ingredients,
                      log => log.IngredientId,
                      ingredient => ingredient.IngredientId,
                      (log, ingredient) => new TopIngredientItem
                      {
                          Name = ingredient.Name ?? "Unknown",
                          Category = ingredient.Category.ToString(),
                          TotalUsed = log.TotalUsedAmount,
                          Unit = ingredient.Unit ?? "pcs"
                      })
                .ToList();

            return topUsed;
        }
    }
}