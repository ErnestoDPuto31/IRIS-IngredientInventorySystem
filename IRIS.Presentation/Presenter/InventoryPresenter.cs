using IRIS.Domain.Entities;
using IRIS.Services.Interfaces;
using IRIS.Presentation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IRIS.Presentation.Presenters
{
    public class InventoryPresenter
    {
        private readonly IInventoryView _view;
        private readonly IIngredientService _service;

       
        public InventoryPresenter(IInventoryView view, IIngredientService service)
        {
            _view = view;
            _service = service;
        }

        public void LoadIngredients()
        {
            try
            {
             
                var ingredients = _service.GetAllIngredients();
                _view.DisplayIngredients(ingredients);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Failed to load inventory: {ex.Message}");
            }
        }

        public void AddNewIngredient(Ingredient newIngredient)
        {
            try
            {
                int newId = _service.AddIngredient(newIngredient);
                newIngredient.IngredientId = newId;
                _view.AddIngredientCard(newIngredient);
                _view.ShowMessage("Ingredient added successfully!");
            }
            catch (Exception ex)
            {
                _view.ShowError($"Failed to add ingredient: {ex.Message}");
            }
        }

        public void UpdateIngredient(Ingredient ingredient)
        {
            try
            {
                _service.UpdateIngredient(ingredient);

                _view.ShowMessage("Ingredient updated successfully!");
            }
            catch (Exception ex)
            {
                _view.ShowError($"Failed to update: {ex.Message}");
            }
        }

        public void DeleteIngredient(int id)
        {
            try
            {
                _service.DeleteIngredient(id);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Failed to delete: {ex.Message}");
            }
        }
        public void LoadFilteredIngredients(string search, string category, string sort)
        {
            try
            {
                var results = _service.GetFilteredIngredients(search, category, sort);

                _view.DisplayIngredients(results);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Filter error: {ex.Message}");
            }
        }
    }
}