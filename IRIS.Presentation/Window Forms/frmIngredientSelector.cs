using IRIS.Domain.Entities;
using IRIS.Services.Implementations;
using System.ComponentModel;
using System.Data;

namespace IRIS.Presentation.Window_Forms
{
    public partial class frmIngredientSelector : Form
    {
        private readonly IngredientService _ingredientService;
        private readonly int _studentCount;
        private readonly decimal _totalBudget;
        private readonly BindingList<RequestDetails> _existingItems;
        private Ingredient _selectedIngredient;

        private readonly Color IRIS_Indigo = Color.FromArgb(75, 0, 130);
        private readonly Dictionary<int, decimal> _tempPrices = new Dictionary<int, decimal>();

        public frmIngredientSelector(IngredientService ingredientService, int studentCount, decimal totalBudget, BindingList<RequestDetails> currentItems = null)
        {
            InitializeComponent();
            _ingredientService = ingredientService;
            _studentCount = studentCount;
            _totalBudget = totalBudget;

            _existingItems = currentItems ?? new BindingList<RequestDetails>();

            this.btnAdd.Click += btnAdd_Click;
            this.btnRemove.Click += btnRemove_Click;

            this.numPricePerUnit.ValueChanged += NumPricePerUnit_ValueChanged;

            SetupSummaryPanel();
            ConfigureControls();

            ucIngredientTable.IngredientSelected += UcIngredientTable_IngredientSelected;
        }

        private void SetupSummaryPanel()
        {
            lstSummary.DataSource = _existingItems;
            lstSummary.DrawMode = DrawMode.OwnerDrawFixed;
            lstSummary.Font = new Font("Poppins", 10F, FontStyle.Regular);
            lstSummary.ItemHeight = 30;
            lstSummary.DrawItem += LstSummary_DrawItem;
        }

        private void LstSummary_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            Color backColor = isSelected ? IRIS_Indigo : Color.White;
            Color foreColor = isSelected ? Color.White : Color.Black;

            e.Graphics.FillRectangle(new SolidBrush(backColor), e.Bounds);

            if (lstSummary.Items[e.Index] is RequestDetails detail)
            {
                string name = detail.Ingredient?.Name ?? "Unknown";
                string unit = detail.Ingredient?.Unit.ToString() ?? "";
                decimal price = _tempPrices.ContainsKey(detail.IngredientId) ? _tempPrices[detail.IngredientId] : 0m;

                string displayString = $"{name} - {detail.RequestedQty:N2} {unit} @ {price:C2}/{unit}";

                int xOffset = 10;
                Rectangle textBounds = new Rectangle(
                    e.Bounds.X + xOffset, e.Bounds.Y,
                    e.Bounds.Width - xOffset, e.Bounds.Height
                );

                TextRenderer.DrawText(e.Graphics, displayString, lstSummary.Font, textBounds, foreColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
            }
            e.DrawFocusRectangle();
        }

        private void ConfigureControls()
        {
            // FIX 2: Talk to the controls directly to lock/unlock them
            numRequestedQty.DecimalPlaces = 2;
            numRequestedQty.Enabled = false;

            numPortionPerStudent.DecimalPlaces = 2;
            numPortionPerStudent.Enabled = false;

            numPricePerUnit.DecimalPlaces = 2;
            numPricePerUnit.Enabled = true;
        }

        private void NumPricePerUnit_ValueChanged(object sender, EventArgs e)
        {
            decimal price = numPricePerUnit.Value;

            if (price > 0 && _totalBudget > 0)
            {
                // The Math Engine now runs!
                numRequestedQty.Value = _totalBudget / price;

                if (_studentCount > 0)
                {
                    numPortionPerStudent.Value = numRequestedQty.Value / _studentCount;
                }
            }
            else
            {
                numRequestedQty.Value = 0;
                numPortionPerStudent.Value = 0;
            }
        }

        private void frmIngredientSelector_Load(object sender, EventArgs e)
        {
            var ingredients = _ingredientService.GetAllIngredients().ToList();
            ucIngredientTable.SetData(ingredients);
        }

        private void UcIngredientTable_IngredientSelected(object sender, Ingredient selectedItem)
        {
            _selectedIngredient = selectedItem;

            // FIX 3: Update the label text directly!
            lblPricePerUnit.Text = $"Price/{_selectedIngredient.Unit}";

            var existing = _existingItems.FirstOrDefault(x => x.IngredientId == _selectedIngredient.IngredientId);

            if (existing != null)
            {
                numPricePerUnit.Value = _tempPrices.ContainsKey(existing.IngredientId) ? _tempPrices[existing.IngredientId] : 0m;
                btnAdd.Text = "Update Qty";
            }
            else
            {
                numPricePerUnit.Value = 0.00M;
                btnAdd.Text = "Add to List";
            }

            lblSelectedName.Text = $"{_selectedIngredient.Name} (Avail: {_selectedIngredient.CurrentStock:N2} {_selectedIngredient.Unit})";
            lblSelectedName.ForeColor = IRIS_Indigo;

            pnlInput.Enabled = true;
            numPricePerUnit.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (_selectedIngredient == null) return;

            decimal requestedQty = numRequestedQty.Value;
            decimal portion = numPortionPerStudent.Value;
            decimal price = numPricePerUnit.Value;

            if (price <= 0)
            {
                MessageBox.Show("Please enter a valid price to calculate the quantity.", "Validation");
                return;
            }

            _tempPrices[_selectedIngredient.IngredientId] = price;

            var existing = _existingItems.FirstOrDefault(x => x.IngredientId == _selectedIngredient.IngredientId);

            if (existing != null)
            {
                existing.RequestedQty = requestedQty;
                existing.PortionPerStudent = portion;
                existing.AllowedQty = requestedQty;
                int index = _existingItems.IndexOf(existing);
                _existingItems.ResetItem(index);
            }
            else
            {
                _existingItems.Add(new RequestDetails
                {
                    IngredientId = _selectedIngredient.IngredientId,
                    Ingredient = _selectedIngredient,
                    RequestedQty = requestedQty,
                    PortionPerStudent = portion,
                    AllowedQty = requestedQty
                });
            }

            ResetInput();
            UpdateTotalPriceDisplay();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstSummary.SelectedItem is RequestDetails selectedItem)
            {
                var result = MessageBox.Show($"Remove {selectedItem.Ingredient?.Name} from list?", "Confirm Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    _existingItems.Remove(selectedItem);
                    if (_tempPrices.ContainsKey(selectedItem.IngredientId)) _tempPrices.Remove(selectedItem.IngredientId);
                    UpdateTotalPriceDisplay();
                }
            }
            else
            {
                MessageBox.Show("Please select an item from the summary to remove.", "No Selection");
            }
        }

        private void UpdateTotalPriceDisplay()
        {
            decimal grandTotal = 0m;
            foreach (var item in _existingItems)
            {
                decimal price = _tempPrices.ContainsKey(item.IngredientId) ? _tempPrices[item.IngredientId] : 0m;
                grandTotal += (item.RequestedQty * price);
            }
            if (this.Controls.Find("lblTotalPrice", true).FirstOrDefault() is Label lbl)
            {
                lbl.Text = $"Total Estimated Cost: {grandTotal:C2}";
            }
        }

        private void ResetInput()
        {
            pnlInput.Enabled = false;
            _selectedIngredient = null;
            lblSelectedName.Text = "Select another item...";

            numPricePerUnit.Value = 0;
            numRequestedQty.Value = 0;
            numPortionPerStudent.Value = 0;

            ucIngredientTable.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_existingItems.Count == 0)
            {
                MessageBox.Show("Your request is empty.", "Empty List");
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}