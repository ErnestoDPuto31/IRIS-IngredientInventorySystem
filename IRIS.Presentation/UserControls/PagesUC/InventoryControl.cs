using IRIS.Domain.Entities;
using IRIS.Domain.Enums; 
using IRIS.Presentation.DependencyInjection;
using IRIS.Presentation.Interfaces;
using IRIS.Presentation.Presenters;
using IRIS.Presentation.UserControls;
using IRIS.Presentation.Window_Forms;


namespace IRIS.Presentation.Forms
{
    public partial class InventoryControl : UserControl, IInventoryView
    {
        private FlowLayoutPanel _ingredientsGrid;
        private InventoryPresenter _presenter;

        private const int CARD_MARGIN = 10;
        private const int CARDS_PER_ROW = 4;
        private const int SCROLLBAR_OFFSET = 25;

        public InventoryControl()
        {
            InitializeComponent();
            InitializeCustomLayout();

            this.Resize += (s, e) => ResizeCards();
        }

        private void InitializeCustomLayout()
        {
            this.DoubleBuffered = true;
            if (cmbCategory != null)
            {
                cmbCategory.Items.Clear();
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
                cmbSortIngredients.Items.AddRange(new object[] {
                    "Newest First", "Oldest First",
                    "Name (A-Z)", "Name (Z-A)",
                    "Stock (Low to High)", "Stock (High to Low)"
                });
                cmbSortIngredients.SelectedIndex = 0;
            }

            _ingredientsGrid = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                AutoScroll = true,
                BackColor = Color.White,
                Padding = new Padding(10, 10, 0, 10)
            };

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

            // --- ROLE SECURITY CHECK ---
            // Only Office Staff can see the "Add Ingredient" button.
            if (UserSession.CurrentUser.Role != UserRole.OfficeStaff)
            {
                btnAddIngredient.Visible = false;
            }

            if (!DesignMode)
            {
                var service = ServiceFactory.GetIngredientService();
                _presenter = new InventoryPresenter(this, service);
                TriggerSearch();
            }
        }

        private void TriggerSearch()
        {
            if (_presenter == null) return;

            string search = txtSearchIngredient.Text;
            string category = cmbCategory.SelectedItem?.ToString() ?? "All Categories";
            string sort = cmbSortIngredients.SelectedItem?.ToString() ?? "Newest First";

            _presenter.LoadFilteredIngredients(search, category, sort);
        }

        public void DisplayIngredients(IEnumerable<Ingredient> ingredients)
        {
            _ingredientsGrid.SuspendLayout();
            _ingredientsGrid.Controls.Clear();

            foreach (var item in ingredients)
            {
                AddCardToGrid(item);
            }

            ResizeCards();
            _ingredientsGrid.ResumeLayout();
        }

        public void AddIngredientCard(Ingredient ingredient)
        {
            TriggerSearch();
        }

        private void AddCardToGrid(Ingredient item)
        {
            var card = new IngredientCard(item);
            card.Margin = new Padding(CARD_MARGIN);

            if (UserSession.CurrentUser.Role != UserRole.OfficeStaff)
            {
                card.HideActionButtons();
            }
            else
            {
                card.DeleteClicked += (sender, id) =>
                {
                    if (MessageBox.Show("Are you sure you want to delete this?", "Confirm",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        _presenter.DeleteIngredient(id);
                        TriggerSearch();
                    }
                };

                card.EditClicked += (sender, data) =>
                {
                    using (frmAddIngredient form = new frmAddIngredient(data))
                    {
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            form.NewIngredient.UpdatedAt = DateTime.Now;
                            _presenter.UpdateIngredient(form.NewIngredient);
                            if (sender is IngredientCard clickedCard)
                            {
                                clickedCard.UpdateData(form.NewIngredient);
                            }
                        }
                    }
                };
            }
            _ingredientsGrid.Controls.Add(card);
        }

        private void ResizeCards()
        {
            if (_ingredientsGrid.Controls.Count == 0) return;

            _ingredientsGrid.SuspendLayout();

            int totalWidth = _ingredientsGrid.ClientSize.Width;
            int scrollbarGap = 25;
            int leftPadding = _ingredientsGrid.Padding.Left;
            int usableWidth = totalWidth - scrollbarGap - leftPadding;
            int gapBetweenCards = 15;
            int totalGapSpace = 4 * gapBetweenCards;
            int cardWidth = (usableWidth - totalGapSpace) / 4;
            foreach (Control ctrl in _ingredientsGrid.Controls)
            {
                if (ctrl is IngredientCard card)
                {
                    card.MinimumSize = new Size(0, 0);
                    card.Size = new Size(cardWidth, 280);
                    card.Margin = new Padding(0, 0, gapBetweenCards, gapBetweenCards);
                }
            }

            _ingredientsGrid.ResumeLayout(true);
        }

        public void ShowMessage(string message) => MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        public void ShowError(string message) => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        private void txtSearchIngredient_TextChanged(object sender, EventArgs e) => TriggerSearch();
        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e) => TriggerSearch();
        private void cmbSortIngredients_SelectedIndexChanged(object sender, EventArgs e) => TriggerSearch();

        private void btnAddIngredient_Click(object sender, EventArgs e)
        {
            using (frmAddIngredient form = new frmAddIngredient())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _presenter.AddNewIngredient(form.NewIngredient);
                }
            }
        }
    }
}