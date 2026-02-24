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

        public IngredientService(IrisDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Ingredient> GetAllIngredients() => _context.Ingredients.AsNoTracking().ToList();

        public int AddIngredient(Ingredient ingredient)
        {
            _context.Ingredients.Add(ingredient);
            _context.SaveChanges();
            return ingredient.IngredientId;
        }

        public void UpdateIngredient(Ingredient ingredient)
        {
            var local = _context.Ingredients
                .Local
                .FirstOrDefault(entry => entry.IngredientId == ingredient.IngredientId);

            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }

            _context.Ingredients.Update(ingredient);
            _context.SaveChanges();
        }

        public void DeleteIngredient(int ingredientId)
        {
            var item = _context.Ingredients.Find(ingredientId);
            if (item != null)
            {
                _context.Ingredients.Remove(item);
                _context.SaveChanges();
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