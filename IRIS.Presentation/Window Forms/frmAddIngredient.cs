using System;
using System.Windows.Forms;
using IRIS.Domain.Entities;
using IRIS.Domain.Enums; 

namespace IRIS.Presentation.Window_Forms
{
    public partial class frmAddIngredient : Form
    {
        public Ingredient NewIngredient { get; private set; }
        private int _ingredientId = 0;

        public frmAddIngredient()
        {
            InitializeComponent();

            lblIngredientTitle.Text = "Add Ingredient";
            btnAddIngredient.Text = "Add Ingredient";

            // Populate Category ComboBox with Enum values
            cmbCategory.DataSource = Enum.GetValues(typeof(Categories));

            SetupFormDefaults();
        }

        public frmAddIngredient(Ingredient ingredientToEdit) : this()
        {
            lblIngredientTitle.Text = "Update Ingredient";
            btnAddIngredient.Text = "Save Changes";

            // 1. Capture the ID from the ingredient we want to edit
            _ingredientId = ingredientToEdit.IngredientId;

            // 2. Populate fields
            txtIngredientName.Text = ingredientToEdit.Name;
            numCurrentStock.Value = ingredientToEdit.CurrentStock;
            numMinimumThreshold.Value = ingredientToEdit.MinimumStock;

            // Set the Category (ComboBox is bound to Enum, so we set the Item directly)
            cmbCategory.SelectedItem = ingredientToEdit.Category;

            // Set the Unit
            if (cmbUnit.Items.Contains(ingredientToEdit.Unit))
                cmbUnit.SelectedItem = ingredientToEdit.Unit;
            else
                cmbUnit.Text = ingredientToEdit.Unit;
        }

        private void SetupFormDefaults()
        {
            if (cmbUnit.SelectedIndex == -1 && cmbUnit.Items.Count > 0)
                cmbUnit.SelectedIndex = 0;

            // Wire up buttons
            btnExitForm.Click += (s, e) => this.Close();
            btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
        }

        private void btnAddIngredient_Click(object sender, EventArgs e)
        {
            // 1. Validation
            if (string.IsNullOrWhiteSpace(txtIngredientName.Text))
            {
                MessageBox.Show("Please enter an ingredient name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Parse Category safely
            Categories selectedCategory;
            if (cmbCategory.SelectedItem is Categories cat)
            {
                selectedCategory = cat;
            }
            else
            {
                // Fallback if user typed text instead of selecting (if DropDownStyle is not DropDownList)
                Enum.TryParse(cmbCategory.Text, true, out selectedCategory);
            }

            // 3. Create/Update Object
            NewIngredient = new Ingredient
            {
                IngredientId = _ingredientId,
                Name = txtIngredientName.Text.Trim(),
                Category = selectedCategory, // Assigned the Enum value
                Unit = cmbUnit.SelectedItem?.ToString() ?? cmbUnit.Text,
                CurrentStock = numCurrentStock.Value,
                MinimumStock = numMinimumThreshold.Value,

                // Ensure default dates are handled if this is new
                CreatedAt = _ingredientId == 0 ? DateTime.Now : DateTime.MinValue,
                UpdatedAt = DateTime.Now
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}