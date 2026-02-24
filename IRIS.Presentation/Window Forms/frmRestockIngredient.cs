using System;
using System.Windows.Forms;
using IRIS.Domain.Entities;

namespace IRIS.Presentation.Window_Forms
{
    public partial class frmRestockIngredient : Form
    {
        public decimal RestockQuantity { get; private set; }
        public string Remarks { get; private set; }

        private readonly Ingredient _ingredient;
        private readonly string _unit;

        // ---> 1. Create a variable to hold the number we passed in <---
        private readonly decimal _suggestedQty;

        public frmRestockIngredient(Ingredient ingredient, decimal suggestedQty)
        {
            InitializeComponent();

            _ingredient = ingredient;
            _unit = string.IsNullOrWhiteSpace(ingredient.Unit) ? "units" : ingredient.Unit.Trim();

            // ---> 2. Save it here! <---
            _suggestedQty = suggestedQty;

            SetupFormLogic();
        }

        private void SetupFormLogic()
        {
            lblTitle.Text = $"Restock {_ingredient.Name}";

            lblCurrentValue.Text = $"{_ingredient.CurrentStock:0.##} {_unit}";
            lblMinValue.Text = $"{_ingredient.MinimumStock:0.##} {_unit}";
            label4.Text = $"Restock Quantity ({_unit})";
            lblSuggestedValue.Text = $"{_suggestedQty:0.##} {_unit}";

            if (_suggestedQty > 0 && _suggestedQty <= numQuantity.Maximum)
            {
                numQuantity.Value = _suggestedQty;
            }
            else if (_suggestedQty > numQuantity.Maximum)
            {
                // Safety catch just in case it's huge
                numQuantity.Value = numQuantity.Maximum;
            }
            else
            {
                numQuantity.Value = 0;
            }

            NumQuantity_ValueChanged(this, EventArgs.Empty);
        }

        private void txtRestockAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void NumQuantity_ValueChanged(object sender, EventArgs e)
        {
            // Real-time calculation of what the new stock level will be
            decimal newStock = _ingredient.CurrentStock + numQuantity.Value;
            lblNewStockText.Text = $"{newStock:0.##} {_unit}";
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (numQuantity.Value <= 0)
            {
                lblError.Visible = true;
                lblError.Text = "Please enter a quantity greater than zero.";
                return;
            }

            RestockQuantity = numQuantity.Value;
            Remarks = txtRemarks.Text.Trim();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}