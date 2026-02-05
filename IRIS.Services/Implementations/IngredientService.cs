using IRIS.Domain.Entities;
using IRIS.Infrastructure.Data; // Ensure this is where your AppDbContext lives
using IRIS.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace IRIS.Services.Implementations
{
    public class IngredientService : IIngredientService
    {
        // 1. Define the field to hold the database connection
        private readonly IrisDbContext _context;

        // 2. THIS IS THE MISSING CONSTRUCTOR
        // It must accept 'AppDbContext' (or whatever your Context class is named)
        public IngredientService(IrisDbContext context)
        {
            _context = context;
        }

        // 3. Implement the interface method
        public IEnumerable<Ingredient> GetAllIngredients()
        {
            // This queries the database for all ingredients
            return _context.Ingredients.ToList();
        }
    }
}