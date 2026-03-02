using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
using IRIS.Infrastructure.Data;
using IRIS.Services.Interfaces;
using IRIS.Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; // Added this!

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
        // Notice the return type is now Task<int>
        public async Task<int> GetTotalIngredientsAsync() => await _context.Ingredients.CountAsync();

        public async Task<int> GetTotalRequestsAsync() => await _context.Requests.CountAsync();

        public async Task<int> GetTotalTransactionsAsync() =>
            await _context.Requests.CountAsync(r => r.Status == RequestStatus.Released);

        public async Task<double> GetApprovalRateAsync()
        {
            var total = await _context.Requests.CountAsync();
            if (total == 0) return 0.0;

            var approved = await _context.Requests.CountAsync(r =>
                r.Status == RequestStatus.Approved || r.Status == RequestStatus.Released);

            return Math.Round((double)approved / total * 100, 1);
        }

        // --- CHARTS (NEW LOGIC) ---

        public async Task<Dictionary<string, double>> GetInventoryStatsAsync()
        {
            // By awaiting these, they happen much faster without locking the thread
            var empty = await _context.Ingredients.CountAsync(i => i.CurrentStock <= 0);
            var low = await _context.Ingredients.CountAsync(i => i.CurrentStock > 0 && i.CurrentStock < i.MinimumStock);
            var full = await _context.Ingredients.CountAsync(i => i.CurrentStock >= i.MinimumStock);

            return new Dictionary<string, double>
            {
                { "Empty", empty },
                { "Low Stock", low },
                { "Full Stock", full }
            };
        }

        public async Task<Dictionary<string, double>> GetRequestStatsAsync()
        {
            return await _context.Requests
                .GroupBy(r => r.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Status.ToString(), x => (double)x.Count);
        }

        public async Task<Dictionary<string, double>> GetCategoryStatsAsync()
        {
            var rawStats = await _context.Ingredients
                .GroupBy(i => i.Category)
                .Select(g => new { Cat = g.Key, Count = g.Count() })
                .ToListAsync();

            return rawStats.ToDictionary(
                x => x.Cat.GetDisplayName(),
                x => (double)x.Count
            );
        }

        // --- NEW: Table Implementation ---
        public async Task<List<Request>> GetRecentTransactionsAsync()
        {
            return await _context.Requests
                .AsNoTracking()
                .Include(r => r.EncodedBy)
                .OrderByDescending(r => r.CreatedAt)
                .Take(10)
                .ToListAsync();
        }

        public async Task<List<LowStockItem>> GetLowStockIngredientsAsync()
        {
            var lowStockDb = await _context.Ingredients
                .AsNoTracking()
                .Where(i => i.CurrentStock < i.MinimumStock)
                .Select(i => new { i.Name, i.Category, i.CurrentStock, i.MinimumStock, i.Unit })
                .ToListAsync();

            return lowStockDb
                .Select(i => new LowStockItem(
                    i.Name ?? "Unknown",
                    i.Category.ToString(),
                    (float)i.CurrentStock,
                    (float)i.MinimumStock,
                    i.Unit ?? "pcs"
                ))
                .ToList();
        }

        public async Task<List<TopIngredientItem>> GetTopUsedIngredientsAsync(int count = 5)
        {
            return await _context.Set<RequestDetails>()
                .AsNoTracking()
                .GroupBy(req => req.IngredientId)
                .Select(group => new
                {
                    IngredientId = group.Key,
                    TotalUsedAmount = group.Sum(r => r.RequestedQty)
                })
                .OrderByDescending(x => x.TotalUsedAmount)
                .Take(count)
                .Join(_context.Ingredients,
                      req => req.IngredientId,
                      ingredient => ingredient.IngredientId,
                      (req, ingredient) => new TopIngredientItem
                      {
                          Name = ingredient.Name ?? "Unknown",
                          Category = ingredient.Category.ToString(),
                          TotalUsed = req.TotalUsedAmount,
                          Unit = ingredient.Unit ?? "pcs"
                      })
                .ToListAsync();
        }
    }
}