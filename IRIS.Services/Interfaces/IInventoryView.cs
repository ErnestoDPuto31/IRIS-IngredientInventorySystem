using IRIS.Domain.Entities;
using System.Collections.Generic;

namespace IRIS.Presentation.Interfaces
{
    public interface IInventoryView
    {
        void DisplayIngredients(IEnumerable<Ingredient> ingredients);
        void AddIngredientCard(Ingredient ingredient);
        void ShowMessage(string message);
        void ShowError(string message);
    }
}