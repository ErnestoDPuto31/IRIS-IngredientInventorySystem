using IRIS.Domain.Entities;
using IRIS.Presentation.Window_Forms;
using IRIS.Presentation.Presenters;
using IRIS.Presentation.Interfaces;
using IRIS.Presentation.DependencyInjection;
using IRIS.Presentation.UserControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace IRIS.Presentation.Forms
{
    public partial class InventoryControl : UserControl, IInventoryView
    {
        private FlowLayoutPanel _ingredientsGrid;
        private InventoryPresenter _presenter;

        public InventoryControl()
        {
            InitializeComponent();
            InitializeCustomLayout();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED (Prevents flickering)
                return cp;
            }
        }

        private void InitializeCustomLayout()
        {
            this.DoubleBuffered = true;

            // --- 1. SETUP DROPDOWNS ---
            if (cmbCategory != null)
            {
                cmbCategory.Items.Clear();
                // Ensure these strings match exactly what your Service expects
                cmbCategory.Items.AddRange(new object[] {
                    "All Categories", "Produce", "Protein", "Dairy & Eggs",
                    "Pantry Staples", "Spices & Seasonings", "Condiments & Oils",
                    "Grains & Legumes", "Bakery & Sweets", "Beverages", "Frozen & Prepared"
                });
                cmbCategory.SelectedIndex = 0;
            }

            if (cmbSortIngredients != null)
            {
                cmbSortIngredients.Items.Clear();
                // Ensure these strings match exactly what your Service expects
                cmbSortIngredients.Items.AddRange(new object[] {
                    "Newest First", "Oldest First",
                    "Name (A-Z)", "Name (Z-A)",
                    "Stock (Low to High)", "Stock (High to Low)"
                });
                cmbSortIngredients.SelectedIndex = 0;
            }

            // --- 2. SETUP GRID ---
            _ingredientsGrid = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                AutoScroll = true,
                BackColor = Color.White,
            };

            // Reflection Hack for smooth scrolling
            typeof(FlowLayoutPanel).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic,
                null, _ingredientsGrid, new object[] { true });

            pnlIngredients.Controls.Clear();
            pnlIngredients.Controls.Add(_ingredientsGrid);
        }

        private void Inventory_Load(object sender, EventArgs e)
        {
            if (pnlMainContent != null)
            {
                pnlMainContent.Dock = DockStyle.Fill;
                pnlMainContent.SendToBack();
            }

            if (!DesignMode)
            {
                var service = ServiceFactory.GetIngredientService();
                _presenter = new InventoryPresenter(this, service);

                // Instead of LoadIngredients(), we call TriggerSearch()
                // This loads the initial list respecting the default "All" and "Newest" filters.
                TriggerSearch();
            }
        }

        // --- 3. HELPER: The "Trigger" that asks the Service for data ---
        private void TriggerSearch()
        {
            if (_presenter == null) return;

            string search = txtSearchIngredient.Text;
            string category = cmbCategory.SelectedItem?.ToString() ?? "All Categories";
            string sort = cmbSortIngredients.SelectedItem?.ToString() ?? "Newest First";

            // Call the method you added to InventoryPresenter in the previous step
            _presenter.LoadFilteredIngredients(search, category, sort);
        }

        // --- IInventoryView Implementation ---

        public void DisplayIngredients(IEnumerable<Ingredient> ingredients)
        {
            _ingredientsGrid.SuspendLayout();
            _ingredientsGrid.Controls.Clear();

            foreach (var item in ingredients)
            {
                AddCardToGrid(item);
            }

            _ingredientsGrid.ResumeLayout();
        }

        public void AddIngredientCard(Ingredient ingredient)
        {
            // Instead of adding the card manually, we reload the search.
            // This ensures the new item is placed in the correct sorted position (e.g. A-Z).
            TriggerSearch();
        }

        private void AddCardToGrid(Ingredient item)
        {
            var card = new IngredientCard(item);
            card.Margin = new Padding(7);

            // --- DELETE LOGIC ---
            card.DeleteClicked += (sender, id) =>
            {
                if (MessageBox.Show("Are you sure you want to delete this?", "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    _presenter.DeleteIngredient(id);
                    TriggerSearch(); // Refresh the list from DB to confirm deletion
                }
            };

            // --- EDIT LOGIC ---
            card.EditClicked += (sender, data) =>
            {
                using (frmAddIngredient form = new frmAddIngredient(data))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        _presenter.UpdateIngredient(form.NewIngredient);
                        TriggerSearch(); // Refresh list to show updates & re-sort
                    }
                }
            };

            _ingredientsGrid.Controls.Add(card);
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // --- 4. EVENT HANDLERS (All map to TriggerSearch) ---

        private void txtSearchIngredient_TextChanged(object sender, EventArgs e)
        {
            TriggerSearch();
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            TriggerSearch();
        }

        private void cmbSortIngredients_SelectedIndexChanged(object sender, EventArgs e)
        {
            TriggerSearch();
        }

        private void btnAddIngredient_Click(object sender, EventArgs e)
        {
            using (frmAddIngredient form = new frmAddIngredient())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _presenter.AddNewIngredient(form.NewIngredient);
                    // AddNewIngredient -> calls AddIngredientCard -> calls TriggerSearch
                }
            }
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            // Future logic...
        }
    }
}