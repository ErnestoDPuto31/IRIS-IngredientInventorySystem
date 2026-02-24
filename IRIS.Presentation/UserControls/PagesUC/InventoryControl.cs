using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
using IRIS.Presentation.DependencyInjection;
using IRIS.Presentation.Interfaces;
using IRIS.Presentation.Presenters;
using IRIS.Presentation.UserControls;
using IRIS.Presentation.Window_Forms;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace IRIS.Presentation.Forms
{
    public partial class InventoryControl : UserControl, IInventoryView
    {
        private FlowLayoutPanel _ingredientsGrid;
        private InventoryPresenter _presenter;
        private System.Windows.Forms.Timer _searchTimer; 

        private const int CARD_MARGIN = 10;
        private const int SCROLLBAR_OFFSET = 25;

        public InventoryControl()
        {
            InitializeComponent();
            InitializeCustomLayout();

            _searchTimer = new System.Windows.Forms.Timer();
            _searchTimer.Interval = 400;
            _searchTimer.Tick += SearchTimer_Tick;

            if (txtSearchIngredient != null) txtSearchIngredient.TextChanged += txtSearchIngredient_TextChanged;
            if (cmbCategory != null) cmbCategory.SelectedIndexChanged += cmbCategory_SelectedIndexChanged;
            if (cmbSortIngredients != null) cmbSortIngredients.SelectedIndexChanged += cmbSortIngredients_SelectedIndexChanged;

            this.Resize += (s, e) => ResizeCards();
        }

        private void InitializeCustomLayout()
        {
            this.DoubleBuffered = true;

            if (cmbCategory != null)
            {
                var categoryList = new List<KeyValuePair<string, string>>();
                categoryList.Add(new KeyValuePair<string, string>("All Categories", "All Categories"));

                foreach (Categories cat in Enum.GetValues(typeof(Categories)))
                {
                    categoryList.Add(new KeyValuePair<string, string>(GetEnumDisplayName(cat), cat.ToString()));
                }

                cmbCategory.DataSource = new BindingSource(categoryList, null);
                cmbCategory.DisplayMember = "Key";   
                cmbCategory.ValueMember = "Value"; 
            }

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
                    "Category"
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
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, _ingredientsGrid, new object[] { true });

            pnlIngredients.Controls.Clear();
            pnlIngredients.Controls.Add(_ingredientsGrid);
        }

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

            // THE FIX: Grab the SelectedValue (which holds the exact Enum string like "DairyAndOils")
            string category = cmbCategory.SelectedValue?.ToString() ?? "All Categories";

            string sortString = cmbSortIngredients.SelectedItem?.ToString() ?? "Newest First";
            IngredientSortBy sortEnum = sortString switch
            {
                "Oldest First" => IngredientSortBy.OldestFirst,
                "Name (A-Z)" => IngredientSortBy.NameAscending,
                "Name (Z-A)" => IngredientSortBy.NameDescending,
                "Stock (Low to High)" => IngredientSortBy.StockLowToHigh,
                "Stock (High to Low)" => IngredientSortBy.StockHighToLow,
                "Category" => IngredientSortBy.Category,
                _ => IngredientSortBy.NewestFirst
            };

            _presenter.LoadFilteredIngredients(search, category, sortEnum.ToString());
        }

        public void DisplayIngredients(IEnumerable<Ingredient> ingredients)
        {
            _ingredientsGrid.SuspendLayout();
            foreach (Control ctrl in _ingredientsGrid.Controls)
            {
                ctrl.Dispose();
            }
            _ingredientsGrid.Controls.Clear();
            var cards = new List<IngredientCard>();

            foreach (var item in ingredients)
            {
                cards.Add(CreateIngredientCard(item));
            }
            if (cards.Any())
            {
                _ingredientsGrid.Controls.AddRange(cards.ToArray());
            }

            ResizeCards();
            _ingredientsGrid.ResumeLayout(true);
        }

        public void AddIngredientCard(Ingredient ingredient)
        {
            TriggerSearch();
        }

        private IngredientCard CreateIngredientCard(Ingredient item)
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
                    var service = ServiceFactory.GetIngredientService();
                    using (frmAddIngredient form = new frmAddIngredient(service, data))
                    {
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            _presenter.UpdateIngredient(form.NewIngredient);
                            TriggerSearch();
                        }
                    }
                };
            }

            return card;
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

        private void txtSearchIngredient_TextChanged(object sender, EventArgs e)
        {
            _searchTimer.Stop();
            _searchTimer.Start();
        }

        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            _searchTimer.Stop();
            TriggerSearch();
        }
        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e) => TriggerSearch();
        private void cmbSortIngredients_SelectedIndexChanged(object sender, EventArgs e) => TriggerSearch();

        private void btnAddIngredient_Click(object sender, EventArgs e)
        {
            var service = ServiceFactory.GetIngredientService();
            using (var frm = new frmAddIngredient(service))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    _presenter.AddNewIngredient(frm.NewIngredient);
                    TriggerSearch();
                }
            }
        }
    }
}