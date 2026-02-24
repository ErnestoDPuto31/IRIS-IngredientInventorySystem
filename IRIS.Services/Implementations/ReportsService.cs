using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
using IRIS.Infrastructure.Data;
using IRIS.Services.Interfaces;
using IRIS.Domain.Helpers;
using Microsoft.EntityFrameworkCore;

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
        public int GetTotalTransactions() => _context.Requests.Count(r => r.Status == RequestStatus.Released);

        public double GetApprovalRate()
        {
            var total = _context.Requests.Count();
            if (total == 0) return 0.0;
            var approved = _context.Requests.Count(r => r.Status == RequestStatus.Approved || r.Status == RequestStatus.Released);
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

        public List<IRIS.Domain.Entities.LowStockItem> GetLowStockIngredients()
        {
            return _context.Ingredients
                .AsNoTracking()
                .Where(i => i.CurrentStock < i.MinimumStock)
                .Select(i => new { i.Name, i.Category, i.CurrentStock, i.MinimumStock, i.Unit })
                .AsEnumerable()
                .Select(i => new IRIS.Domain.Entities.LowStockItem(
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
    }
}