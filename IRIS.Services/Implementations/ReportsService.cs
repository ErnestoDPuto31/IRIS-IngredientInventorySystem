using IRIS.Domain.Contracts;
using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
using IRIS.Domain.Helpers;
using IRIS.Infrastructure.Data;
using IRIS.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IRIS.Services.Implementations
{
    public class ReportsService : IReportsService
    {
        private readonly IrisDbContext _context;

        public ReportsService(IrisDbContext context)
        {
            _context = context;
        }

        public int GetTotalIngredients() => _context.Ingredients.Count();

        public int GetTotalRequests() => _context.Requests.Count();

        public int GetTotalTransactions() =>
            _context.Requests.Count(r => r.Status == RequestStatus.Released);

        public double GetApprovalRate()
        {
            var total = _context.Requests.Count();
            if (total == 0) return 0.0;

            var approved = _context.Requests.Count(r =>
                r.Status == RequestStatus.Approved ||
                r.Status == RequestStatus.Released);

            return Math.Round((double)approved / total * 100, 1);
        }

        public Dictionary<string, double> GetInventoryStats()
        {
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
            return _context.Requests
                .GroupBy(r => r.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToDictionary(x => x.Status.ToString(), x => (double)x.Count);
        }

        public Dictionary<string, double> GetCategoryStats()
        {
            return _context.Ingredients
                .GroupBy(i => i.Category)
                .Select(g => new { Category = g.Key, Count = g.Count() })
                .ToDictionary(x => x.Category.ToString(), x => (double)x.Count);
        }

        public List<Request> GetRecentTransactions()
        {
            return _context.Requests
                .OrderByDescending(r => r.CreatedAt)
                .Take(10)
                .ToList();
        }

        public List<LowStockItem> GetLowStockIngredients()
        {
            return _context.Ingredients
                .Where(i => i.CurrentStock < i.MinimumStock)
                .Select(i => new LowStockItem(
                    i.Name,
                    i.Category.ToString(),
                    (float)i.CurrentStock,
                    (float)i.MinimumStock,
                    i.Unit.ToString()
                ))
                .ToList();
        }

        public List<TopIngredientItem> GetTopUsedIngredients(int count = 5)
        {
            var grouped = _context.Set<RequestDetails>()
                .AsNoTracking()
                .Where(rd =>
                    rd.Request != null &&
                    rd.Ingredient != null &&
                    (rd.Request.Status == RequestStatus.Approved ||
                     rd.Request.Status == RequestStatus.Released))
                .GroupBy(rd => new
                {
                    rd.IngredientId,
                    rd.Ingredient!.Name,
                    rd.Ingredient.Category,
                    rd.Ingredient.Unit
                })
                .Select(g => new
                {
                    Name = g.Key.Name,
                    Category = g.Key.Category,
                    Unit = g.Key.Unit,
                    TotalUsed = g.Sum(x => x.AllowedQty > 0 ? x.AllowedQty : x.RequestedQty)
                })
                .OrderByDescending(x => x.TotalUsed)
                .Take(count)
                .ToList();

            return grouped
                .Select(x => new TopIngredientItem
                {
                    Name = x.Name,
                    Category = x.Category.ToString(),
                    TotalUsed = x.TotalUsed,
                    Unit = x.Unit.ToString()
                })
                .ToList();
        }

        public async Task<ReportsDashboardSummary> GetDashboardDataAsync(int count = 5)
        {
            return new ReportsDashboardSummary
            {
                TotalIngredients = await GetTotalIngredientsAsync(),
                TotalRequests = await GetTotalRequestsAsync(),
                TotalTransactions = await GetTotalTransactionsAsync(),
                ApprovalRate = await GetApprovalRateAsync(),
                InventoryStats = await GetInventoryStatsAsync(),
                RequestStats = await GetRequestStatsAsync(),
                CategoryStats = await GetCategoryStatsAsync(),
                LowStockIngredients = await GetLowStockIngredientsAsync(),
                TopUsedIngredients = await GetTopUsedIngredientsAsync(count)
            };
        }

        public async Task<int> GetTotalIngredientsAsync() => await _context.Ingredients.CountAsync();

        public async Task<int> GetTotalRequestsAsync() => await _context.Requests.CountAsync();

        public async Task<int> GetTotalTransactionsAsync() =>
            await _context.Requests.CountAsync(r => r.Status == RequestStatus.Released);

        public async Task<double> GetApprovalRateAsync()
        {
            var total = await _context.Requests.CountAsync();
            if (total == 0) return 0.0;

            var approved = await _context.Requests.CountAsync(r =>
                r.Status == RequestStatus.Approved ||
                r.Status == RequestStatus.Released);

            return Math.Round((double)approved / total * 100, 1);
        }

        public async Task<Dictionary<string, double>> GetInventoryStatsAsync()
        {
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
            var grouped = await _context.Requests
                .GroupBy(r => r.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToListAsync();

            return grouped.ToDictionary(x => x.Status.ToString(), x => (double)x.Count);
        }

        public async Task<Dictionary<string, double>> GetCategoryStatsAsync()
        {
            var grouped = await _context.Ingredients
                .GroupBy(i => i.Category)
                .Select(g => new { Category = g.Key, Count = g.Count() })
                .ToListAsync();

            return grouped.ToDictionary(x => x.Category.ToString(), x => (double)x.Count);
        }

        public async Task<List<LowStockItem>> GetLowStockIngredientsAsync()
        {
            return await _context.Ingredients
                .Where(i => i.CurrentStock < i.MinimumStock)
                .Select(i => new LowStockItem(
                    i.Name,
                    i.Category.ToString(),
                    (float)i.CurrentStock,
                    (float)i.MinimumStock,
                    i.Unit.ToString()
                ))
                .ToListAsync();
        }

        public async Task<List<Request>> GetRecentTransactionsAsync()
        {
            return await _context.Requests
                .OrderByDescending(r => r.CreatedAt)
                .Take(10)
                .ToListAsync();
        }

        public async Task<List<TopIngredientItem>> GetTopUsedIngredientsAsync(int count = 5)
        {
            var grouped = await _context.Set<RequestDetails>()
                .AsNoTracking()
                .Where(rd =>
                    rd.Request != null &&
                    rd.Ingredient != null &&
                    (rd.Request.Status == RequestStatus.Approved ||
                     rd.Request.Status == RequestStatus.Released))
                .GroupBy(rd => new
                {
                    rd.IngredientId,
                    rd.Ingredient!.Name,
                    rd.Ingredient.Category,
                    rd.Ingredient.Unit
                })
                .Select(g => new
                {
                    Name = g.Key.Name,
                    Category = g.Key.Category,
                    Unit = g.Key.Unit,
                    TotalUsed = g.Sum(x => x.AllowedQty > 0 ? x.AllowedQty : x.RequestedQty)
                })
                .OrderByDescending(x => x.TotalUsed)
                .Take(count)
                .ToListAsync();

            return grouped
                .Select(x => new TopIngredientItem
                {
                    Name = x.Name,
                    Category = x.Category.ToString(),
                    TotalUsed = x.TotalUsed,
                    Unit = x.Unit.ToString()
                })
                .ToList();
        }
    }
}