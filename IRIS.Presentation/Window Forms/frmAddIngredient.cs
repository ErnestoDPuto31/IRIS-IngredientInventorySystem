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
            LoadUnitDropdown(); 
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

     
            cmbUnit.SelectedValue = ingredientToEdit.Unit;
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

        private void LoadUnitDropdown()
        {
            var unitList = Enum.GetValues(typeof(Units))
                .Cast<Units>()
                .Select(u => new
                {
                    Value = u,
                    Display = GetEnumDisplayName(u)
                })
                .ToList();

            cmbUnit.DataSource = unitList;
            cmbUnit.DisplayMember = "Display";
            cmbUnit.ValueMember = "Value";
        }

        private void SetupFormDefaults()
        {
            numCurrentStock.DecimalPlaces = 2;
            numCurrentStock.Increment = 0.5M;
            numCurrentStock.Minimum = 0;
            numCurrentStock.Maximum = 99999;

            numMinimumThreshold.DecimalPlaces = 2;
            numMinimumThreshold.Increment = 0.5M;
            numMinimumThreshold.Minimum = 0;
            numMinimumThreshold.Maximum = 99999;

            // Dropdown defaults
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

            Units selectedUnit = (cmbUnit.SelectedValue is Units u)
                ? u : default;

            NewIngredient = new Ingredient
            {
                IngredientId = _ingredientId,
                Name = cleanName,
                Category = selectedCategory,
                Unit = selectedUnit, 
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