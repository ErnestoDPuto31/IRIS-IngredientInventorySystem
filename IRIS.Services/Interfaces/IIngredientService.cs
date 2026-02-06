using System.Collections.Generic;
using IRIS.Domain.Entities;

namespace IRIS.Services.Interfaces
{
    public interface IIngredientService
    {
        IEnumerable<Ingredient> GetAllIngredients();
        int AddIngredient(Ingredient ingredient);
        void UpdateIngredient(Ingredient ingredient);
        void DeleteIngredient(int ingredientId);
        IEnumerable<Ingredient> GetFilteredIngredients(string searchTerm, string category, string sortBy);
    }
}