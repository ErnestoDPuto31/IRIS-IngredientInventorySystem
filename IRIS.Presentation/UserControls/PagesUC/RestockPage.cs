using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Windows.Forms;
using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
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

            LowStockItems.TypeOfCard = CardType.LowStockItems;
            EmptyItems.TypeOfCard = CardType.EmptyItems;
            WellStockedItems.TypeOfCard = CardType.WellStockedItems;

            var service = ServiceFactory.GetRestockService();
            _presenter = new RestockPresenter(this, service);

            SetupIndigoScrollBar();
        }

        private void SetupIndigoScrollBar()
        {
            pnlMainContent.AutoScroll = false;
            pnlMainContent.HorizontalScroll.Maximum = 0;
            pnlMainContent.HorizontalScroll.Visible = false;
            pnlMainContent.AutoScroll = true;

            var vScroll = new Guna.UI2.WinForms.Guna2VScrollBar
            {
                BindingContainer = pnlMainContent,
                ThumbColor = Color.Indigo,
                HoverState = { ThumbColor = Color.DarkViolet },
                BorderRadius = 4,
                Width = 10,
                FillColor = Color.Transparent,
                Margin = new Padding(0, 5, 2, 5)
            };
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

            foreach (Categories cat in Enum.GetValues(typeof(Categories)))
            {
                string displayName = GetEnumDisplayName(cat);
                if (!cmbFilter.Items.Contains(displayName))
                {
                    cmbFilter.Items.Add(displayName);
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