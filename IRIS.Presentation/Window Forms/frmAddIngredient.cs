using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
using IRIS.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace IRIS.Presentation.Window_Forms
{
    public partial class frmAddIngredient : Form
    {
        public Ingredient NewIngredient { get; private set; }
        private readonly IIngredientService _service;
        private int _ingredientId = 0;

        public frmAddIngredient(IIngredientService service)
        {
            InitializeComponent();
            _service = service;

            lblIngredientTitle.Text = "Add Ingredient";
            btnAddIngredient.Text = "Add Ingredient";

            LoadCategoryDropdown();
            SetupFormDefaults();
        }

        public frmAddIngredient(IIngredientService service, Ingredient ingredientToEdit) : this(service)
        {
            _ingredientId = ingredientToEdit.IngredientId;

            lblIngredientTitle.Text = "Update Ingredient";
            btnAddIngredient.Text = "Save Changes";

            txtIngredientName.Text = ingredientToEdit.Name;
            numCurrentStock.Value = ingredientToEdit.CurrentStock;
            numMinimumThreshold.Value = ingredientToEdit.MinimumStock;
            cmbCategory.SelectedValue = ingredientToEdit.Category;

            if (cmbUnit.Items.Contains(ingredientToEdit.Unit))
                cmbUnit.SelectedItem = ingredientToEdit.Unit;
            else
                cmbUnit.Text = ingredientToEdit.Unit;
        }

        private void LoadCategoryDropdown()
        {
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
        }

        private void SetupFormDefaults()
        {
            if (cmbUnit.SelectedIndex == -1 && cmbUnit.Items.Count > 0)
                cmbUnit.SelectedIndex = 0;

            btnExitForm.Click += (s, e) => this.Close();
            btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
        }

        private (bool IsValid, string ErrorMessage) ValidateIngredient(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return (false, "Ingredient name cannot be empty.");

            if (char.IsLower(name[0]))
                return (false, "Please start with an uppercase letter (e.g., 'Tomato').");

            var letters = name.Where(char.IsLetter).ToList();
            if (letters.Count > 1 && letters.All(char.IsUpper))
                return (false, "Ingredient name cannot be ALL CAPS.");

            var existing = _service.GetAllIngredients();
            bool isDuplicate = existing.Any(i =>
                i.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                i.IngredientId != _ingredientId);

            if (isDuplicate)
                return (false, "An ingredient with this name already exists.");

            return (true, string.Empty);
        }

        private void btnAddIngredient_Click(object sender, EventArgs e)
        {
            string cleanName = txtIngredientName.Text.Trim();

            var validation = ValidateIngredient(cleanName);
            if (!validation.IsValid)
            {
                lblError.Text = validation.ErrorMessage;
                lblError.Visible = true;
                return; 
            }

            lblError.Visible = false;

            Categories selectedCategory = (cmbCategory.SelectedValue is Categories cat)
                ? cat : Categories.Produce;

            NewIngredient = new Ingredient
            {
                IngredientId = _ingredientId,
                Name = cleanName,
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