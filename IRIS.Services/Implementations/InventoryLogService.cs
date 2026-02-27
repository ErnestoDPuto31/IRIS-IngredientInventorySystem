using IRIS.Domain.Entities;
using IRIS.Infrastructure.Data;
using IRIS.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IRIS.Services.Implementations
{
    public class InventoryLogService : IInventoryLogService
    {
        private readonly IrisDbContext _context;

        public InventoryLogService(IrisDbContext context)
        {
            _context = context;
        }

        public void LogTransaction(int ingredientId, string action, decimal quantityChanged, decimal prevStock, decimal newStock)
        {
            int userId = UserSession.CurrentUser?.UserId ?? 1;

            var ingredientName = _context.Ingredients
                .Where(i => i.IngredientId == ingredientId)
                .Select(i => i.Name)
                .FirstOrDefault() ?? "Unknown Item";

            var log = new InventoryLog
            {
                IngredientId = ingredientId,
                IngredientName = ingredientName,
                ActionType = action,
                QuantityChanged = quantityChanged,
                PreviousStock = prevStock,
                NewStock = newStock,
                PerformedByUserId = userId,
                Timestamp = DateTime.Now
            };

            _context.InventoryLogs.Add(log);
            _context.SaveChanges();
        }

        public IEnumerable<InventoryLog> GetPaginatedLogs(int pageNumber, int pageSize = 10, string searchTerm = "")
        {
            var query = _context.InventoryLogs.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(x =>
                    x.IngredientName.Contains(searchTerm) ||
                    x.ActionType.Contains(searchTerm)
                );
            }

            int rowsToSkip = (pageNumber - 1) * pageSize;
            return query
                .OrderByDescending(log => log.Timestamp)
                .Skip(rowsToSkip)
                .Take(pageSize)
                .ToList();
        }

        public int GetTotalLogCount(string searchTerm = "")
        {
            var query = _context.InventoryLogs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(x =>
                    x.IngredientName.Contains(searchTerm) ||
                    x.ActionType.Contains(searchTerm)
                );
            }

            return query.Count();
        }
    }
}