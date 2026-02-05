using IRIS.Domain.Entities;
using IRIS.Presentation.DependencyInjection;
using IRIS.Presentation.Presenters;
using IRIS.Presentation.UserControls.Components;
using IRIS.Presentation.UserControls.Table;
using System;
using System.Collections.Generic;
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
        private void RestockPage_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                _presenter.OnViewLoad();
            }
        }

        public void DisplayRestockList(List<Restock> data)
        {
            restockTable1.SetData(data);
        }

        public void UpdateCategoryButtonText(string text)
        {
            btnFilterCategory.Text = text;
        }

        public void BuildCategoryMenu(IEnumerable<string> categories)
        {
            ContextMenuStrip menu = new ContextMenuStrip();
            menu.Items.Add("All Categories ▼", null, (s, e) => _presenter.FilterByCategory("All"));
            menu.Items.Add(new ToolStripSeparator());

            foreach (var cat in categories)
            {
                menu.Items.Add(cat, null, (s, e) => _presenter.FilterByCategory(cat));
            }
            btnFilterCategory.ContextMenuStrip = menu;
            btnFilterCategory.Click += (s, e) => menu.Show(btnFilterCategory, 0, btnFilterCategory.Height);
        }

        public void RefreshStatusCards()
        {
            LowStockItems.LoadStatistics();
            EmptyItems.LoadStatistics();
            WellStockedItems.LoadStatistics();
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

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}