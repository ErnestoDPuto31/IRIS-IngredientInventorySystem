using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
using IRIS.Domain.Helpers;
using IRIS.Infrastructure.Data;
using IRIS.Services.DTOs;
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
            var rawStats = _context.Ingredients
                .GroupBy(i => i.Category)
                .Select(g => new { Cat = g.Key, Count = g.Count() })
                .ToList();

            return rawStats.ToDictionary(
                x => x.Cat.GetDisplayName(),
                x => (double)x.Count
            );
        }

        public List<Request> GetRecentTransactions()
        {
            return _context.Requests
                .AsNoTracking()
                .Include(r => r.EncodedBy)
                .OrderByDescending(r => r.CreatedAt)
                .Take(10)
                .ToList();
        }

        public List<LowStockItem> GetLowStockIngredients()
        {
            return _context.Ingredients
                .AsNoTracking()
                .Where(i => i.CurrentStock < i.MinimumStock)
                .Select(i => new
                {
                    i.Name,
                    i.Category,
                    i.CurrentStock,
                    i.MinimumStock,
                    i.Unit
                })
                .AsEnumerable()
                .Select(i => new LowStockItem(
                    i.Name ?? "Unknown",
                    i.Category.GetDisplayName(),
                    (float)i.CurrentStock,
                    (float)i.MinimumStock,
                    i.Unit.GetDisplayName()
                ))
                .ToList();
        }

        public List<TopIngredientItem> GetTopUsedIngredients(int count = 5)
        {
            var topUsed = _context.Set<RequestDetails>()
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
                      (req, ingredient) => new { req, ingredient })
                .AsEnumerable()
                .Select(x => new TopIngredientItem
                {
                    Name = x.ingredient.Name ?? "Unknown",
                    Category = x.ingredient.Category.GetDisplayName(),
                    TotalUsed = x.req.TotalUsedAmount,
                    Unit = x.ingredient.Unit.GetDisplayName()
                })
                .ToList();

            return topUsed;
        }

        public async Task<ReportsDashboardDto> GetDashboardDataAsync(int count = 5)
        {
            var dto = new ReportsDashboardDto();

            dto.TotalIngredients = await _context.Ingredients.CountAsync();

            dto.TotalRequests = await _context.Requests.CountAsync();

            dto.TotalTransactions = await _context.Requests
                .CountAsync(r => r.Status == RequestStatus.Released);

            var approvedCount = await _context.Requests
                .CountAsync(r =>
                    r.Status == RequestStatus.Approved ||
                    r.Status == RequestStatus.Released);

            dto.ApprovalRate = dto.TotalRequests == 0
                ? 0.0
                : Math.Round((double)approvedCount / dto.TotalRequests * 100, 1);

            var emptyCount = await _context.Ingredients.CountAsync(i => i.CurrentStock <= 0);
            var lowCount = await _context.Ingredients.CountAsync(i => i.CurrentStock > 0 && i.CurrentStock < i.MinimumStock);
            var fullCount = await _context.Ingredients.CountAsync(i => i.CurrentStock >= i.MinimumStock);

            dto.InventoryStats = new Dictionary<string, double>
            {
                { "Empty", emptyCount },
                { "Low Stock", lowCount },
                { "Full Stock", fullCount }
            };

            dto.RequestStats = await _context.Requests
                .AsNoTracking()
                .GroupBy(r => r.Status)
                .Select(g => new
                {
                    Status = g.Key.ToString(),
                    Count = (double)g.Count()
                })
                .ToDictionaryAsync(x => x.Status, x => x.Count);

            var rawCategoryStats = await _context.Ingredients
                .AsNoTracking()
                .GroupBy(i => i.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            dto.CategoryStats = rawCategoryStats.ToDictionary(
                x => x.Category.GetDisplayName(),
                x => (double)x.Count
            );

            var lowStockRaw = await _context.Ingredients
                .AsNoTracking()
                .Where(i => i.CurrentStock < i.MinimumStock)
                .Select(i => new
                {
                    i.Name,
                    i.Category,
                    i.CurrentStock,
                    i.MinimumStock,
                    i.Unit
                })
                .ToListAsync();

            dto.LowStockIngredients = lowStockRaw
                .Select(i => new LowStockItem(
                    i.Name ?? "Unknown",
                    i.Category.GetDisplayName(),
                    (float)i.CurrentStock,
                    (float)i.MinimumStock,
                    i.Unit.GetDisplayName()
                ))
                .ToList();

            var topUsedRaw = await _context.Set<RequestDetails>()
                .AsNoTracking()
                .GroupBy(req => req.IngredientId)
                .Select(group => new
                {
                    IngredientId = group.Key,
                    TotalUsedAmount = group.Sum(r => r.RequestedQty)
                })
                .OrderByDescending(x => x.TotalUsedAmount)
                .Take(count)
                .Join(_context.Ingredients.AsNoTracking(),
                      req => req.IngredientId,
                      ingredient => ingredient.IngredientId,
                      (req, ingredient) => new
                      {
                          req.TotalUsedAmount,
                          ingredient.Name,
                          ingredient.Category,
                          ingredient.Unit
                      })
                .ToListAsync();

            dto.TopUsedIngredients = topUsedRaw
                .Select(x => new TopIngredientItem
                {
                    Name = x.Name ?? "Unknown",
                    Category = x.Category.GetDisplayName(),
                    TotalUsed = x.TotalUsedAmount,
                    Unit = x.Unit.GetDisplayName()
                })
                .ToList();

            return dto;
        }
    }
}