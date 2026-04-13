using IRIS.Domain.Entities;
using IRIS.Services.Implementations;
using IRIS.Services.Interfaces;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace IRIS.Presentation.Window_Forms
{
    public partial class frmIngredientSelector : Form
    {
        private readonly IIngredientService _ingredientService;

        private readonly int _studentCount;
        private readonly decimal _totalBudget;
        private readonly BindingList<RequestDetails> _existingItems;
        private Ingredient _selectedIngredient;

        private readonly Color IRIS_Indigo = Color.FromArgb(75, 0, 130);

        private decimal _currentRequestedQty = 0m;
        private decimal _currentPortionPerStudent = 0m;
        private bool _isCalculating = false;

        public frmIngredientSelector(IIngredientService ingredientService, int studentCount, decimal totalBudget, BindingList<RequestDetails> currentItems = null)
        {
            InitializeComponent();
            _ingredientService = ingredientService;
            _studentCount = studentCount;
            _totalBudget = totalBudget;

            _existingItems = currentItems ?? new BindingList<RequestDetails>();

            this.numPricePerUnit.ValueChanged += CalculateTotalsAndValidations;
            this.numRequestedQty.ValueChanged += NumRequestedQty_ValueChanged;
            this.numPortionPerStudent.ValueChanged += NumPortionPerStudent_ValueChanged;
            this.lstSummary.SelectedIndexChanged += LstSummary_SelectedIndexChanged;

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

                decimal price = detail.UnitPrice;

                string displayString = $"{name} - {detail.RequestedQty:N2} {unit} @ ₱{price:N2}/{unit}";

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
            numPricePerUnit.DecimalPlaces = 2;
            numPricePerUnit.Enabled = true;
            numPricePerUnit.Increment = 0.5m;

            numRequestedQty.DecimalPlaces = 2;
            numRequestedQty.Enabled = true;
            numRequestedQty.Increment = 1.0m;

            numPortionPerStudent.DecimalPlaces = 4;
            numPortionPerStudent.Enabled = true;
            numPortionPerStudent.Increment = 0.005m;
        }

        private void NumRequestedQty_ValueChanged(object sender, EventArgs e)
        {
            if (_isCalculating) return;

            _isCalculating = true;
            if (_studentCount > 0)
            {
                numPortionPerStudent.Value = numRequestedQty.Value / _studentCount;
            }
            CalculateTotalsAndValidations(sender, e);
            _isCalculating = false;
        }

        private void NumPortionPerStudent_ValueChanged(object sender, EventArgs e)
        {
            if (_isCalculating) return;

            _isCalculating = true;
            numRequestedQty.Value = numPortionPerStudent.Value * _studentCount;
            CalculateTotalsAndValidations(sender, e);
            _isCalculating = false;
        }

        private void CalculateTotalsAndValidations(object sender, EventArgs e)
        {
            _currentRequestedQty = numRequestedQty.Value;
            _currentPortionPerStudent = numPortionPerStudent.Value;
            decimal currentPrice = numPricePerUnit.Value;

            if (_currentRequestedQty > 0)
            {
                numRequestedQty.ForeColor = (_selectedIngredient != null && _currentRequestedQty > _selectedIngredient.CurrentStock)
                    ? Color.Red
                    : Color.Black;
            }
            else
            {
                numRequestedQty.ForeColor = Color.Black;
            }

            decimal newItemCost = _currentRequestedQty * currentPrice;

            if (lblIngredientPrice != null)
            {
                lblIngredientPrice.Text = $"₱ {newItemCost:N2}";
            }

            decimal existingCost = GetExistingListCost();

            if ((existingCost + newItemCost) > _totalBudget)
            {
                numPricePerUnit.ForeColor = Color.Red;
                lblIngredientPrice.ForeColor = Color.Red;
            }
            else
            {
                numPricePerUnit.ForeColor = Color.Black;
                lblIngredientPrice.ForeColor = Color.Black;
            }
        }

        private decimal GetExistingListCost()
        {
            decimal total = 0m;
            foreach (var item in _existingItems)
            {
                if (_selectedIngredient == null || item.IngredientId != _selectedIngredient.IngredientId)
                {
                    total += (item.RequestedQty * item.UnitPrice);
                }
            }
            return total;
        }

        private void frmIngredientSelector_Load(object sender, EventArgs e)
        {
            lblStudentCount.Text = _studentCount.ToString();

            if (this.Controls.Find("lblTotalBudget", true).FirstOrDefault() is Label lblBudget)
            {
                lblBudget.Text = $"₱ {_totalBudget:N2}";
            }

            var ingredients = _ingredientService.GetAllIngredients().ToList();
            ucIngredientTable.SetData(ingredients);

            UpdateTotalPriceDisplay();
            lstSummary.ClearSelected();
        }

        private void UcIngredientTable_IngredientSelected(object sender, Ingredient selectedItem)
        {
            _selectedIngredient = selectedItem;
            lblPricePerUnit.Text = $"Price/{_selectedIngredient.Unit}";

            if (lblUnit != null) lblUnit.Text = _selectedIngredient.Unit.ToString();
            if (lblUnit1 != null) lblUnit1.Text = _selectedIngredient.Unit.ToString();

            var existing = _existingItems.FirstOrDefault(x => x.IngredientId == _selectedIngredient.IngredientId);

            _isCalculating = true;

            if (existing != null)
            {
                _currentRequestedQty = existing.RequestedQty;
                _currentPortionPerStudent = existing.PortionPerStudent;

                numRequestedQty.Value = _currentRequestedQty;
                numPortionPerStudent.Value = _currentPortionPerStudent;

                numPricePerUnit.Value = existing.UnitPrice;
                btnAdd.Text = "Update Qty";
            }
            else
            {
                _currentRequestedQty = 0m;
                _currentPortionPerStudent = 0m;

                numRequestedQty.Value = 0.00M;
                numPortionPerStudent.Value = 0.00M;
                numPricePerUnit.Value = 0.00M;
                btnAdd.Text = "Add to List";
            }

            _isCalculating = false;
            CalculateTotalsAndValidations(null, null);

            lblSelectedName.Text = $"{_selectedIngredient.Name} (Avail: {_selectedIngredient.CurrentStock:N2} {_selectedIngredient.Unit})";
            lblSelectedName.ForeColor = IRIS_Indigo;

            pnlInput.Enabled = true;
            pnlInput1.Enabled = true;
            numRequestedQty.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (_selectedIngredient == null) return;

            decimal price = numPricePerUnit.Value;
            decimal qty = numRequestedQty.Value;

            if (price <= 0 || qty <= 0)
            {
                MessageBox.Show("Please enter a valid price and requested quantity.", "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_currentRequestedQty > _selectedIngredient.CurrentStock)
            {
                MessageBox.Show($"Not enough stock! You requested {_currentRequestedQty:N2} {_selectedIngredient.Unit}, but only {_selectedIngredient.CurrentStock:N2} {_selectedIngredient.Unit} is available.", "Stock Limit Exceeded", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            decimal newItemCost = _currentRequestedQty * price;
            decimal existingListCost = GetExistingListCost();

            if ((existingListCost + newItemCost) > _totalBudget)
            {
                MessageBox.Show($"This item exceeds your budget!\n\nTotal Allowed: ₱{_totalBudget:N2}\nUsed by other items: ₱{existingListCost:N2}\nCost of this item: ₱{newItemCost:N2}", "Budget Limit Exceeded", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var existing = _existingItems.FirstOrDefault(x => x.IngredientId == _selectedIngredient.IngredientId);

            if (existing != null)
            {
                existing.RequestedQty = _currentRequestedQty;
                existing.PortionPerStudent = _currentPortionPerStudent;
                existing.AllowedQty = _currentRequestedQty;
                existing.UnitPrice = price; 

                int index = _existingItems.IndexOf(existing);
                _existingItems.ResetItem(index);
            }
            else
            {
                _existingItems.Add(new RequestDetails
                {
                    IngredientId = _selectedIngredient.IngredientId,
                    Ingredient = _selectedIngredient,
                    RequestedQty = _currentRequestedQty,
                    PortionPerStudent = _currentPortionPerStudent,
                    AllowedQty = _currentRequestedQty,
                    UnitPrice = price 
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

                    UpdateTotalPriceDisplay();
                    ResetInput();
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
                // UPDATED: Pull UnitPrice directly
                grandTotal += (item.RequestedQty * item.UnitPrice);
            }

            if (lblTotalPrice != null)
            {
                lblTotalPrice.Text = $"₱ {grandTotal:N2}";
                lblTotalPrice.ForeColor = grandTotal > _totalBudget ? Color.Red : Color.Indigo;
            }
        }

        private void LstSummary_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSummary.SelectedItem is RequestDetails selectedItem)
            {
                _selectedIngredient = selectedItem.Ingredient;
                lblPricePerUnit.Text = $"Price/{_selectedIngredient.Unit}";

                if (lblUnit != null) lblUnit.Text = _selectedIngredient.Unit.ToString();
                if (lblUnit1 != null) lblUnit1.Text = _selectedIngredient.Unit.ToString();

                _currentRequestedQty = selectedItem.RequestedQty;
                _currentPortionPerStudent = selectedItem.PortionPerStudent;

                _isCalculating = true;

                numRequestedQty.Value = _currentRequestedQty;
                numPortionPerStudent.Value = _currentPortionPerStudent;

                // UPDATED: Pull UnitPrice directly
                numPricePerUnit.Value = selectedItem.UnitPrice;

                _isCalculating = false;

                lblSelectedName.Text = $"{_selectedIngredient.Name} (Avail: {_selectedIngredient.CurrentStock:N2} {_selectedIngredient.Unit})";
                lblSelectedName.ForeColor = IRIS_Indigo;

                btnAdd.Text = "Update Qty";
                pnlInput.Enabled = true;
                pnlInput1.Enabled = true;

                CalculateTotalsAndValidations(null, null);
            }
        }

        private void ResetInput()
        {
            pnlInput.Enabled = false;
            pnlInput1.Enabled = false;
            _selectedIngredient = null;
            lblSelectedName.Text = "No Ingredient Selected";

            _isCalculating = true;

            numPricePerUnit.Value = 0;
            numRequestedQty.Value = 0;
            numPortionPerStudent.Value = 0;

            _isCalculating = false;

            numPricePerUnit.ForeColor = Color.Black;
            numRequestedQty.ForeColor = Color.Black;

            if (lblIngredientPrice != null) lblIngredientPrice.Text = "₱ 0.00";
            if (lblUnit != null) lblUnit.Text = "";
            if (lblUnit1 != null) lblUnit1.Text = "";

            _currentRequestedQty = 0m;
            _currentPortionPerStudent = 0m;

            btnAdd.Text = "Add to List";
            lstSummary.ClearSelected();

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