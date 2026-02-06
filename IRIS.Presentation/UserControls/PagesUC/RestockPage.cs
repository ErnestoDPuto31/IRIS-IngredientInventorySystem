using System;
using System.Collections.Generic;
using System.Windows.Forms;
using IRIS.Domain.Entities;
using IRIS.Presentation.DependencyInjection;
using IRIS.Presentation.Presenters;
using IRIS.Presentation.UserControls.Components;

namespace IRIS.Presentation.UserControls.PagesUC
{
    public partial class RestockPage : UserControl
    {
        private RestockPresenter _presenter;

        public RestockPage()
        {
            InitializeComponent();

            // Setup Info Cards
            LowStockItems.TypeOfCard = CardType.LowStockItems;
            EmptyItems.TypeOfCard = CardType.EmptyItems;
            WellStockedItems.TypeOfCard = CardType.WellStockedItems;

            var service = ServiceFactory.GetRestockService();
            _presenter = new RestockPresenter(this, service);
        }

        private void RestockPage_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                _presenter.OnViewLoad();
            }
        }

        // ---------------------------------------------------------
        // DATA DISPLAY
        // ---------------------------------------------------------
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
            cmbFilter.Items.Clear();

            // 1. Add Default "All" option
            cmbFilter.Items.Add("All Categories");

            // 2. Add your specific fixed filters
            var fixedFilters = new List<string>
            {
                "Produce",
                "Protein",
                "Dairy & Eggs",
                "Pantry Staples",
                "Spices & Seasonings.",
                "Condiments & Oils",
                "Grains & Legumes",
                "Bakery & Sweets",
                "Beverages",
                "Frozen & Prepared"
            };

            foreach (var filter in fixedFilters)
            {
                cmbFilter.Items.Add(filter);
            }

            // 3. Add any dynamic categories from the DB that aren't already in the fixed list
            // (This prevents duplicates if the DB also returns "Produce")
            if (categories != null)
            {
                foreach (var cat in categories)
                {
                    if (!cmbFilter.Items.Contains(cat))
                    {
                        cmbFilter.Items.Add(cat);
                    }
                }
            }

            // 4. Set default selection
            if (cmbFilter.Items.Count > 0)
                cmbFilter.SelectedIndex = 0;
        }

        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilter.SelectedItem == null) return;

            string selected = cmbFilter.SelectedItem.ToString();

            // Handle the "All" logic
            if (selected == "All Categories") selected = "All";

            _presenter.FilterByCategory(selected);
        }

        public void UpdateCategoryButtonText(string text)
        {
            if (text == "All") text = "All Categories";

            if (cmbFilter.Items.Contains(text))
            {
                cmbFilter.SelectedItem = text;
            }
        }

        private void UpdateButtonSelection(object sender)
        {
            var clickedButton = sender as PillButton;
            if (clickedButton == null) return;

            btnFilterAll.IsSelected = false;
            btnFilterLow.IsSelected = false;
            btnFilterEmpty.IsSelected = false;
            btnFilterWell.IsSelected = false;

            clickedButton.IsSelected = true;
        }

        private void btnFilterAll_Click(object sender, EventArgs e)
        {
            _presenter.FilterByStatus("All");
            UpdateButtonSelection(sender);
        }

        private void btnFilterLow_Click(object sender, EventArgs e)
        {
            _presenter.FilterByStatus("Low");
            UpdateButtonSelection(sender);
        }

        private void btnFilterEmpty_Click(object sender, EventArgs e)
        {
            _presenter.FilterByStatus("Empty");
            UpdateButtonSelection(sender);
        }

        private void btnFilterWell_Click(object sender, EventArgs e)
        {
            _presenter.FilterByStatus("Well");
            UpdateButtonSelection(sender);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            _presenter.RefreshData();
        }
    }
}