using IRIS.Domain.Entities;
using IRIS.Domain.Entitites;
using IRIS.Domain.Enums;
using IRIS.Infrastructure.Data;
using IRIS.Services.Interfaces;
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
            // Group by Category String
            return _context.Ingredients
                .GroupBy(i => i.Category)
                .Select(g => new { Cat = g.Key, Count = g.Count() })
                .ToDictionary(x => x.Cat ?? "Uncategorized", x => (double)x.Count);
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
        public List<LowStockItem> GetLowStockIngredients()
        {
            return _context.Ingredients
                .AsNoTracking()
                .Where(i => i.CurrentStock < i.MinimumStock)
                .Select(i => new LowStockItem(
                    i.Name ?? "Unknown",  // Changed from IngredientName to Name
                    i.Category ?? "General",
                    (float)i.CurrentStock,
                    (float)i.MinimumStock,
                    i.Unit ?? "pcs"
                ))
                .ToList();
        }
    }
}