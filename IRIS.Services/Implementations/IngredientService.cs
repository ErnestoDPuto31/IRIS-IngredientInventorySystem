using IRIS.Domain.Entities;
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

        public IEnumerable<Ingredient> GetFilteredIngredients(string searchTerm, string category, string sortBy)
        {
            // FIX 3: Use AsNoTracking().AsQueryable() here
            var query = _context.Ingredients.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string term = searchTerm.ToLower().Trim();
                query = query.Where(i => i.Name.ToLower().Contains(term));
            }

            if (!string.IsNullOrEmpty(category) && category != "All Categories")
            {
                query = query.Where(i => i.Category == category);
            }

            switch (sortBy)
            {
                case "Newest First":
                    query = query.OrderByDescending(i => i.IngredientId);
                    break;
                case "Oldest First":
                    query = query.OrderBy(i => i.IngredientId);
                    break;
                case "Name (A-Z)":
                    query = query.OrderBy(i => i.Name);
                    break;
                case "Name (Z-A)":
                    query = query.OrderByDescending(i => i.Name);
                    break;
                case "Stock (Low to High)":
                    query = query.OrderBy(i => i.CurrentStock);
                    break;
                case "Stock (High to Low)":
                    query = query.OrderByDescending(i => i.CurrentStock);
                    break;
                default:
                    query = query.OrderByDescending(i => i.IngredientId);
                    break;
            }

            return query.ToList();
        }
    }
}