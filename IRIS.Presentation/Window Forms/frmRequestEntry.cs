    using IRIS.Domain.Entities;
    using IRIS.Domain.Enums;
    using IRIS.Services.Implementations;
    using System.ComponentModel;
    namespace IRIS.Presentation.Window_Forms
    {
    public partial class frmRequestEntry : Form
    {
        private readonly RequestService _requestService;
        private readonly IngredientService _ingredientService;

        private BindingList<RequestItem> _tempItems;

        public frmRequestEntry(RequestService reqService, IngredientService ingService)
        {
            InitializeComponent();

            _requestService = reqService;
            _ingredientService = ingService;
            _tempItems = new BindingList<RequestItem>();

            LoadIngredients();
            SetupGrid();

            CalculateAllowedQuantity();
        }


        private void LoadIngredients()
        {
            var ingredients = _ingredientService.GetAllIngredients();

            cboIngredients.DataSource = ingredients;
            cboIngredients.ValueMember = "IngredientId";

            cboIngredients.FormattingEnabled = true;

            cboIngredients.SelectedIndex = -1;
        }

        private void SetupGrid()
        {
            gridItems.AutoGenerateColumns = false;
            gridItems.DataSource = _tempItems;

            gridItems.Columns["Ingreident"].DataPropertyName = "Ingredient";
            gridItems.Columns["Quantity"].DataPropertyName = "RequestedQty";

            gridItems.CellFormatting += (s, e) =>
            {
                if (gridItems.Columns[e.ColumnIndex].Name == "Ingreident" && e.Value != null)
                {
                    e.Value = ((Ingredient)e.Value).Name;
                    e.FormattingApplied = true;
                }
                if (gridItems.Columns[e.ColumnIndex].Name == "Quantity" && e.Value != null)
                {
                    e.Value = $"{e.Value} g";
                    e.FormattingApplied = true;
                }
            };
        }

        private void CalculateAllowedQuantity()
        {
            // Logic: Student Count * Recipe Costing = Total Allowed Grams
            decimal totalAllowed = numStudentCount.Value * numRecipeCosting.Value;
            lblAllowedQtyDisplay.Text = $"{totalAllowed:N0}g";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var selectedIngredient = cboIngredients.SelectedItem as Ingredient;
            decimal qty = numAddQty.Value;

            // Validation
            if (selectedIngredient == null)
            {
                MessageBox.Show("Please select an ingredient.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (qty <= 0)
            {
                MessageBox.Show("Quantity must be greater than zero.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if already added
            if (_tempItems.Any(x => x.IngredientId == selectedIngredient.IngredientId))
            {
                MessageBox.Show("This ingredient is already in the list.", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Add to list
            _tempItems.Add(new RequestItem
            {
                IngredientId = selectedIngredient.IngredientId,
                Ingredient = selectedIngredient,
                RequestedQty = qty,
                AllowedQty = qty
            });

            // Reset inputs
            cboIngredients.SelectedIndex = -1;
            numAddQty.Value = 0;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSubject.Text) || string.IsNullOrWhiteSpace(txtFaculty.Text))
            {
                MessageBox.Show("Please fill in Subject and Faculty Name.", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (_tempItems.Count == 0)
            {
                MessageBox.Show("Please add at least one ingredient.", "Empty Request", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newRequest = new Request
            {
                Subject = txtSubject.Text.Trim(),
                FacultyName = txtFaculty.Text.Trim(),
                DateOfUse = dtpDateOfUse.Value,
                StudentCount = (int)numStudentCount.Value,
                RecipeCosting = numRecipeCosting.Value,
                Status = RequestStatus.Pending, // Default status
                EncodedById = UserSession.CurrentUser.UserId,
                CreatedAt = DateTime.Now
            };

            try
            {
                _requestService.CreateRequest(newRequest, _tempItems.ToList());

                MessageBox.Show("Request submitted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // Signal to parent to refresh
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error submitting request: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExitForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void numStudentCount_ValueChanged(object sender, EventArgs e)
        {
            CalculateAllowedQuantity();
        }

        private void numRecipeCosting_ValueChanged(object sender, EventArgs e)
        {
            CalculateAllowedQuantity();
        }

        private void cboIngredients_Format(object sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is Ingredient ingredient)
            {
                string qty = $"{ingredient.CurrentStock:N0} {ingredient.Unit}";
                e.Value = $"{ingredient.Name} - {qty} Avail.";
            }
        }
    }
}