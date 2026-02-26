using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
using IRIS.Presentation.DependencyInjection;
using IRIS.Presentation.Presenters;
using IRIS.Presentation.UserControls.Components;
using IRIS.Presentation.Window_Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace IRIS.Presentation.UserControls.PagesUC
{
    public partial class RestockPage : UserControl
    {
        private RestockPresenter _presenter;

        public RestockPage()
        {
            InitializeComponent();

            
            LowStockItems.TypeOfCard = CardType.LowStockItems;
            EmptyItems.TypeOfCard = CardType.EmptyItems;
            WellStockedItems.TypeOfCard = CardType.WellStockedItems;

            var service = ServiceFactory.GetRestockService();
            _presenter = new RestockPresenter(this, service);
            _presenter.RefreshData();

            restockTableuc1.RestockRequested += RestockTableuc1_RestockRequested;
        }

        private void RestockTableuc1_RestockRequested(object sender, Restock restockItem)
        {
            // ---> NEW: Pass the suggested amount as the second parameter <---
            using (var popup = new frmRestockIngredient(restockItem.Ingredient, restockItem.SuggestedRestockQuantity))
            {
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    _presenter.ProcessRestock(restockItem.IngredientId, popup.RestockQuantity);

                    MessageBox.Show($"{restockItem.Ingredient.Name} stock has been updated!",
                                    "Restock Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void RestockPage_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                _presenter.OnViewLoad();
            }
        }
        public void DisplayRestockList(List<Restock> data)
        {
            restockTableuc1.SetData(data);
        }

        public void RefreshStatusCards()
        {
            LowStockItems.LoadStatistics();
            EmptyItems.LoadStatistics();
            WellStockedItems.LoadStatistics();
        }

        public void BuildCategoryMenu(IEnumerable<string> categories)
        {
            cmbFilter.SelectedIndexChanged -= cmbFilter_SelectedIndexChanged;
            string currentSelection = cmbFilter.SelectedItem?.ToString();

            cmbFilter.Items.Clear();
            cmbFilter.Items.Add("All Categories");

            // FIX: Iterate over the actual categories passed from the database/service!
            foreach (var rawCategoryName in categories)
            {
                if (Enum.TryParse(typeof(Categories), rawCategoryName, out object parsedEnum))
                {
                    string displayName = GetEnumDisplayName((Enum)parsedEnum);
                    if (!cmbFilter.Items.Contains(displayName))
                    {
                        cmbFilter.Items.Add(displayName);
                    }
                }
            }

            if (!string.IsNullOrEmpty(currentSelection) && cmbFilter.Items.Contains(currentSelection))
            {
                cmbFilter.SelectedItem = currentSelection;
            }
            else if (cmbFilter.Items.Count > 0)
            {
                cmbFilter.SelectedIndex = 0;
            }

            cmbFilter.SelectedIndexChanged += cmbFilter_SelectedIndexChanged;
        }

        public void UpdateCategoryButtonText(string text)
        {
            cmbFilter.SelectedIndexChanged -= cmbFilter_SelectedIndexChanged;

            if (text == "All") text = "All Categories";
            if (Enum.TryParse(typeof(Categories), text, out object parsedEnum))
            {
                text = GetEnumDisplayName((Enum)parsedEnum);
            }

            if (cmbFilter.Items.Contains(text))
            {
                cmbFilter.SelectedItem = text;
            }


            cmbFilter.SelectedIndexChanged += cmbFilter_SelectedIndexChanged;
        }

        private string GetEnumDisplayName(Enum enumValue)
        {
            FieldInfo field = enumValue.GetType().GetField(enumValue.ToString());
            if (field != null)
            {
                var displayAttribute = (DisplayAttribute)Attribute.GetCustomAttribute(field, typeof(DisplayAttribute));
                if (displayAttribute != null)
                {
                    return displayAttribute.Name;
                }
            }
            return enumValue.ToString();
        }

        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilter.SelectedItem == null) return;

            string selectedDisplay = cmbFilter.SelectedItem.ToString();

            if (selectedDisplay == "All Categories")
            {
                _presenter.FilterByCategory("All");
                return;
            }

            string rawCategoryName = selectedDisplay;
            foreach (Categories cat in Enum.GetValues(typeof(Categories)))
            {
                if (GetEnumDisplayName(cat) == selectedDisplay)
                {
                    rawCategoryName = cat.ToString(); 
                    break;
                }
            }

            _presenter.FilterByCategory(rawCategoryName);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            _presenter.RefreshData();
        }
    }
}