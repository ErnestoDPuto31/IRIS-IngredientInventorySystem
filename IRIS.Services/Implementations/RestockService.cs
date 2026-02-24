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
                    query = query.Where(r => r.Ingredient.CurrentStock > 0 && r.Ingredient.CurrentStock <= r.Ingredient.MinimumStock);
                    break;
                case "Empty":
                    query = query.Where(r => r.Ingredient.CurrentStock <= 0);
                    break;
                case "Well":
                    query = query.Where(r => r.Ingredient.CurrentStock > r.Ingredient.MinimumStock);
                    break;
            }
            return query.ToList();
        }

        public int GetCountByStatus(string statusType)
        {
            switch (statusType)
            {
                case "Empty":
                    return _context.Ingredients.Count(i => i.CurrentStock <= 0);
                case "Low":
                    return _context.Ingredients.Count(i => i.CurrentStock > 0 && i.CurrentStock <= i.MinimumStock);
                case "Well":
                    return _context.Ingredients.Count(i => i.CurrentStock > i.MinimumStock);
                default:
                    return 0;
            }
        }
        public IEnumerable<Restock> SearchRestockList(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return GetRestockList();

            // FIX: Search inside the linked Ingredient entity
            return _context.Restocks
                .Include(r => r.Ingredient)
                .Where(r => r.Ingredient.Name.Contains(searchTerm))
                .ToList();
        }

        public void RefreshRestockData()
        {
            var lowStockIngredients = _context.Ingredients
                .Where(i => i.CurrentStock <= i.MinimumStock)
                .ToList();

            foreach (var ing in lowStockIngredients)
            {
                var existingRestock = _context.Restocks
                    .FirstOrDefault(r => r.IngredientId == ing.IngredientId);

                var currentStatus = ing.CurrentStock <= 0 ? StockStatus.Empty : StockStatus.LowStock;

                decimal deficit = ing.MinimumStock - ing.CurrentStock;
                decimal suggestedQty = deficit > 0 ? Math.Ceiling(deficit * 1.2m) : 0;

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
            var resolvedRestocks = _context.Restocks
                .Include(r => r.Ingredient)
                .AsEnumerable()
                .Where(r => r.Ingredient != null && r.Ingredient.CurrentStock > r.Ingredient.MinimumStock)
                .ToList();

            if (resolvedRestocks.Any())
            {
                _context.Restocks.RemoveRange(resolvedRestocks);
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
                    if (ingredient.CurrentStock > ingredient.MinimumStock)
                    {
                        _context.Restocks.Remove(pendingRestock);
                    }
                    else
                    {
                        pendingRestock.Status = ingredient.CurrentStock <= 0 ? StockStatus.Empty : StockStatus.LowStock;
                        decimal deficit = ingredient.MinimumStock - ingredient.CurrentStock;
                        pendingRestock.SuggestedRestockQuantity = deficit > 0 ? Math.Ceiling(deficit * 1.2m) : 0;
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