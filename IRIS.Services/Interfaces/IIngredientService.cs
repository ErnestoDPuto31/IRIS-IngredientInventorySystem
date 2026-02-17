using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
using System.Collections.Generic;

namespace IRIS.Services.Interfaces
{
    public interface IIngredientService
    {
        IEnumerable<Ingredient> GetAllIngredients();
        int AddIngredient(Ingredient ingredient);
        void UpdateIngredient(Ingredient ingredient);
        void DeleteIngredient(int ingredientId);
        IEnumerable<Ingredient> GetFilteredIngredients(string searchTerm, string category, IngredientSortBy sortBy);
    }
}