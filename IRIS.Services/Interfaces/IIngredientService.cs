using System.Collections.Generic;
using IRIS.Domain.Entities; // Make sure this matches where your 'Ingredient' class is

namespace IRIS.Services.Interfaces
{
    public interface IIngredientService
    {
   
        IEnumerable<Ingredient> GetAllIngredients();
    }
}