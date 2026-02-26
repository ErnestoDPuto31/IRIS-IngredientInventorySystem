using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
using IRIS.Infrastructure.Data;
using IRIS.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IRIS.Services.Implementations
{
    public class IngredientService : IIngredientService
    {
        private readonly IrisDbContext _context;
        private readonly IInventoryLogService _logService; 

        public IngredientService(IrisDbContext context, IInventoryLogService logService)
        {
            _context = context;
            _logService = logService;
        }
        public IEnumerable<Ingredient> GetAllIngredients() => _context.Ingredients.AsNoTracking().ToList();

        public int AddIngredient(Ingredient ingredient)
        {
            _context.Ingredients.Add(ingredient);
            _context.SaveChanges();
            _logService.LogTransaction(ingredient.IngredientId, "Added", ingredient.CurrentStock, 0, ingredient.CurrentStock);

            return ingredient.IngredientId;
        }

        public void UpdateIngredient(Ingredient ingredient)
        {
            var oldIngredient = _context.Ingredients.AsNoTracking().FirstOrDefault(i => i.IngredientId == ingredient.IngredientId);
            decimal oldStock = oldIngredient != null ? oldIngredient.CurrentStock : 0;

            var local = _context.Ingredients.Local.FirstOrDefault(entry => entry.IngredientId == ingredient.IngredientId);
            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }

            _context.Ingredients.Update(ingredient);
            _context.SaveChanges();

            decimal quantityChanged = ingredient.CurrentStock - oldStock;
            if (quantityChanged != 0)
            {
                _logService.LogTransaction(ingredient.IngredientId, "Updated", quantityChanged, oldStock, ingredient.CurrentStock);
            }
        }

        public void DeleteIngredient(int ingredientId)
        {
            var item = _context.Ingredients.Find(ingredientId);
            if (item != null)
            {
                decimal stockBeforeDelete = item.CurrentStock;

                _context.Ingredients.Remove(item);
                _context.SaveChanges();
                _logService.LogTransaction(ingredientId, "Removed", -stockBeforeDelete, stockBeforeDelete, 0);
            }
        }


        public IEnumerable<Ingredient> GetFilteredIngredients(string searchTerm, string category, IngredientSortBy sortBy)
        {
            var query = _context.Ingredients.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(i => i.Name.Contains(searchTerm));
            }

            if (!string.IsNullOrEmpty(category) && category != "All Categories")
            {
                if (Enum.TryParse<Categories>(category, out var catEnum))
                {
                    query = query.Where(i => i.Category == catEnum);
                }
            }

            switch (sortBy)
            {
                case IngredientSortBy.NewestFirst:
                    query = query.OrderByDescending(i => i.IngredientId);
                    break;
                case IngredientSortBy.OldestFirst:
                    query = query.OrderBy(i => i.IngredientId);
                    break;
                case IngredientSortBy.NameAscending:
                    query = query.OrderBy(i => i.Name);
                    break;
                case IngredientSortBy.NameDescending:
                    query = query.OrderByDescending(i => i.Name);
                    break;
                case IngredientSortBy.StockLowToHigh:
                    query = query.OrderBy(i => i.CurrentStock);
                    break;
                case IngredientSortBy.StockHighToLow:
                    query = query.OrderByDescending(i => i.CurrentStock);
                    break;
                case IngredientSortBy.Category:
                    query = query.OrderBy(i => i.Category);
                    break;
                default:
                    query = query.OrderByDescending(i => i.IngredientId);
                    break;
            }

            return query.ToList();
        }
    }
}