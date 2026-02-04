using IRIS.Domain.Entities;
using System;
using System.Windows.Forms;

namespace IRIS.Presentation.Window_Forms
{
    public partial class frmAddIngredient : Form
    {
        public Ingredient NewIngredient { get; private set; }

        public frmAddIngredient()
        {
            InitializeComponent();

            lblIngredientTitle.Text = "Add Ingredient";
            btnAddIngredient.Text = "Add Ingredient";

            SetupFormDefaults();
        }

        public frmAddIngredient(Ingredient ingredientToEdit) : this()
        {
            lblIngredientTitle.Text = "Update Ingredient";
            btnAddIngredient.Text = "Save Changes";
            txtIngredientName.Text = ingredientToEdit.Name;

            numCurrentStock.Value = ingredientToEdit.CurrentStock;
            numMinimumThreshold.Value = ingredientToEdit.MinimumStock;

            if (cmbCategory.Items.Contains(ingredientToEdit.Category))
                cmbCategory.SelectedItem = ingredientToEdit.Category;
            else
                cmbCategory.Text = ingredientToEdit.Category;

            if (cmbUnit.Items.Contains(ingredientToEdit.Unit))
                cmbUnit.SelectedItem = ingredientToEdit.Unit;
            else
                cmbUnit.Text = ingredientToEdit.Unit;
        }

        private void SetupFormDefaults()
        {
            if (cmbCategory.SelectedIndex == -1 && cmbCategory.Items.Count > 0)
                cmbCategory.SelectedIndex = 0;

            if (cmbUnit.SelectedIndex == -1 && cmbUnit.Items.Count > 0)
                cmbUnit.SelectedIndex = 0;

            btnExitForm.Click += (s, e) => this.Close();
            btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
        }

        private void btnAddIngredient_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIngredientName.Text))
            {
                MessageBox.Show("Please enter an ingredient name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            NewIngredient = new Ingredient
            {
                Name = txtIngredientName.Text.Trim(),
                Category = cmbCategory.SelectedItem?.ToString() ?? cmbCategory.Text,
                Unit = cmbUnit.SelectedItem?.ToString() ?? cmbUnit.Text,
                CurrentStock = numCurrentStock.Value,
                MinimumStock = numMinimumThreshold.Value,
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}