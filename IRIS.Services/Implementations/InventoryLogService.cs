using IRIS.Domain.Entities;
using IRIS.Infrastructure.Data;
using IRIS.Services.Interfaces;
using System;

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

            var log = new InventoryLog
            {
                IngredientId = ingredientId,
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
    }
}