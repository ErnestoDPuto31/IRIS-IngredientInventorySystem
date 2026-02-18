using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
using IRIS.Presentation.DependencyInjection;
using IRIS.Presentation.Interfaces;
using IRIS.Presentation.Presenters;
using IRIS.Presentation.UserControls;
using IRIS.Presentation.Window_Forms;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace IRIS.Presentation.Forms
{
    public partial class InventoryControl : UserControl, IInventoryView
    {
        private FlowLayoutPanel _ingredientsGrid;
        private InventoryPresenter _presenter;

        private const int CARD_MARGIN = 10;
        private const int SCROLLBAR_OFFSET = 25;

        public InventoryControl()
        {
            InitializeComponent();
            InitializeCustomLayout();

            // Handle resizing to keep cards responsive
            this.Resize += (s, e) => ResizeCards();
        }

        private void InitializeCustomLayout()
        {
            this.DoubleBuffered = true;

            // --- 1. SETUP CATEGORY COMBOBOX ---
            if (cmbCategory != null)
            {
                cmbCategory.Items.Clear();
                cmbCategory.Items.Add("All Categories");

                // Get display names from Enum
                var categoryStrings = Enum.GetValues(typeof(Categories))
                                          .Cast<Categories>()
                                          .Select(c => GetEnumDisplayName(c))
                                          .ToArray();

                cmbCategory.Items.AddRange(categoryStrings);
                cmbCategory.SelectedIndex = 0;
            }

            // --- 2. SETUP SORT COMBOBOX ---
            if (cmbSortIngredients != null)
            {
                cmbSortIngredients.Items.Clear();
                cmbSortIngredients.Items.AddRange(new object[] {
                    "Newest First",
                    "Oldest First",
                    "Name (A-Z)",
                    "Name (Z-A)",
                    "Stock (Low to High)",
                    "Stock (High to Low)",
                    "Category" // Added for your new requirement
                });
                cmbSortIngredients.SelectedIndex = 0;
            }

            // --- 3. SETUP GRID ---
            _ingredientsGrid = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                AutoScroll = true,
                BackColor = Color.White,
                Padding = new Padding(10, 10, 0, 10)
            };

            // Enable Double Buffering on FlowLayoutPanel to prevent flickering
            typeof(FlowLayoutPanel).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, _ingredientsGrid, new object[] { true });

            pnlIngredients.Controls.Clear();
            pnlIngredients.Controls.Add(_ingredientsGrid);
        }

        // Helper to get [Display(Name="...")] from Enums
        private string GetEnumDisplayName(Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DisplayAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DisplayAttribute)) as DisplayAttribute;
            return attribute == null ? value.ToString() : attribute.Name;
        }

        private void Inventory_Load(object sender, EventArgs e)
        {
            if (pnlMainContent != null)
            {
                pnlMainContent.Dock = DockStyle.Fill;
                pnlMainContent.SendToBack();
            }

            // --- ROLE SECURITY CHECK ---
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

        // --- THIS IS THE UPDATED METHOD WITH THE MAPPING LOGIC ---
        private void TriggerSearch()
        {
            if (_presenter == null) return;

            string search = txtSearchIngredient.Text;
            string category = cmbCategory.SelectedItem?.ToString() ?? "All Categories";

            // 1. Get the raw string from UI
            string sortString = cmbSortIngredients.SelectedItem?.ToString() ?? "Newest First";

            // 2. Map String -> Enum (Presentation Layer Logic)
            IngredientSortBy sortEnum = sortString switch
            {
                "Oldest First" => IngredientSortBy.OldestFirst,
                "Name (A-Z)" => IngredientSortBy.NameAscending,
                "Name (Z-A)" => IngredientSortBy.NameDescending,
                "Stock (Low to High)" => IngredientSortBy.StockLowToHigh,
                "Stock (High to Low)" => IngredientSortBy.StockHighToLow,
                "Category" => IngredientSortBy.Category,
                _ => IngredientSortBy.NewestFirst // Default
            };

            // 3. Pass the clean Enum to the Presenter
            _presenter.LoadFilteredIngredients(search, category, sortEnum.ToString());
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
                            // Refresh logic handled by TriggerSearch usually, but direct update works too
                            TriggerSearch();
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
            int leftPadding = _ingredientsGrid.Padding.Left;
            // Accounting for scrollbar usually ~20px, giving it some buffer
            int usableWidth = totalWidth - SCROLLBAR_OFFSET - leftPadding;

            int gapBetweenCards = 15;
            // Assume we want 4 cards per row
            int totalGapSpace = 4 * gapBetweenCards;

            // Calculate width ensuring they fit
            int cardWidth = (usableWidth - totalGapSpace) / 4;

            // Safety check so cards don't get too small
            if (cardWidth < 200) cardWidth = 200;

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

        // Event Handlers that just call TriggerSearch
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