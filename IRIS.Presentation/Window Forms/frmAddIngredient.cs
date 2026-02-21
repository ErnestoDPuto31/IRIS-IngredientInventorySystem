using System;
using System.Linq;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
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

            var categoryList = Enum.GetValues(typeof(Categories))
                .Cast<Categories>()
                .Select(c => new
                {
                    Value = c,
                    Display = GetEnumDisplayName(c) 
                })
                .ToList();

            cmbCategory.DataSource = categoryList;
            cmbCategory.DisplayMember = "Display"; 
            cmbCategory.ValueMember = "Value";     

            SetupFormDefaults();
        }

        public frmAddIngredient(Ingredient ingredientToEdit) : this()
        {
            lblIngredientTitle.Text = "Update Ingredient";
            btnAddIngredient.Text = "Save Changes";

            _ingredientId = ingredientToEdit.IngredientId;

            txtIngredientName.Text = ingredientToEdit.Name;
            numCurrentStock.Value = ingredientToEdit.CurrentStock;
            numMinimumThreshold.Value = ingredientToEdit.MinimumStock;

            cmbCategory.SelectedValue = ingredientToEdit.Category;

            if (cmbUnit.Items.Contains(ingredientToEdit.Unit))
                cmbUnit.SelectedItem = ingredientToEdit.Unit;
            else
                cmbUnit.Text = ingredientToEdit.Unit;
        }

        private void SetupFormDefaults()
        {
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

            Categories selectedCategory = Categories.Produce; // Default fallback
            if (cmbCategory.SelectedValue is Categories cat)
            {
                selectedCategory = cat;
            }

            NewIngredient = new Ingredient
            {
                IngredientId = _ingredientId,
                Name = txtIngredientName.Text.Trim(),
                Category = selectedCategory, 
                Unit = cmbUnit.SelectedItem?.ToString() ?? cmbUnit.Text,
                CurrentStock = numCurrentStock.Value,
                MinimumStock = numMinimumThreshold.Value,

                CreatedAt = _ingredientId == 0 ? DateTime.Now : DateTime.MinValue, 
                UpdatedAt = DateTime.Now
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
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
    }
}