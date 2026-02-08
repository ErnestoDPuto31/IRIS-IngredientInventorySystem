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
            // 1. Basic Modern Settings
            gridItems.AutoGenerateColumns = false;
            gridItems.DataSource = _tempItems;
            gridItems.AllowUserToAddRows = false;
            gridItems.AllowUserToResizeRows = false;
            gridItems.RowHeadersVisible = false; // Hide the ugly selector column on the left
            gridItems.BackgroundColor = Color.White;
            gridItems.BorderStyle = BorderStyle.None;
            gridItems.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal; // Only horizontal lines
            gridItems.GridColor = Color.FromArgb(240, 240, 240); // Very light grey lines
            gridItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridItems.MultiSelect = false;

            // 2. Data Binding
            if (gridItems.Columns["Ingreident"] != null) gridItems.Columns["Ingreident"].DataPropertyName = "Ingredient";
            if (gridItems.Columns["Quantity"] != null) gridItems.Columns["Quantity"].DataPropertyName = "RequestedQty";

            // 3. Header Styling
            gridItems.EnableHeadersVisualStyles = false; // REQUIRED to change header colors
            gridItems.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            gridItems.ColumnHeadersHeight = 50; // Taller modern header
            gridItems.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 246, 250); // Light gray modern background
            gridItems.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64); // Dark gray text
            gridItems.ColumnHeadersDefaultCellStyle.Font = new Font("Poppins", 9f, FontStyle.Bold); // Your font
            gridItems.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 246, 250); // Prevent blue highlight on header click

            // 4. Row Styling
            gridItems.DefaultCellStyle.BackColor = Color.White;
            gridItems.DefaultCellStyle.ForeColor = Color.FromArgb(45, 52, 54);
            gridItems.DefaultCellStyle.SelectionBackColor = Color.FromArgb(236, 240, 241); // Soft selection color (not deep blue)
            gridItems.DefaultCellStyle.SelectionForeColor = Color.Black;
            gridItems.DefaultCellStyle.Font = new Font("Poppins", 9f, FontStyle.Regular);
            gridItems.RowTemplate.Height = 45; // More breathing room (padding)

            // 5. Delete Button Configuration
            if (gridItems.Columns.Contains("Delete"))
            {
                var deleteCol = (DataGridViewButtonColumn)gridItems.Columns["Delete"];
                deleteCol.HeaderText = "";
                deleteCol.Text = "Delete";
                deleteCol.UseColumnTextForButtonValue = true;
                deleteCol.Width = 80;
                deleteCol.FlatStyle = FlatStyle.Flat;
            }

            // 6. EVENT HANDLERS

            // Handle Formatting (Text and Units)
            gridItems.CellFormatting += (s, e) =>
            {
                if (e.RowIndex < 0 || e.Value == null) return;

                // Format Ingredient Name
                if (gridItems.Columns[e.ColumnIndex].Name == "Ingreident")
                {
                    if (e.Value is Ingredient ing)
                    {
                        e.Value = ing.Name;
                        e.FormattingApplied = true;
                    }
                }

                // Format Quantity with CORRECT Unit
                if (gridItems.Columns[e.ColumnIndex].Name == "Quantity")
                {
                    // Get the actual data object for this row to find the specific unit
                    var item = gridItems.Rows[e.RowIndex].DataBoundItem as RequestItem;
                    if (item != null && item.Ingredient != null)
                    {
                        // Result: "500 g", "2 pcs", "1.5 kg" based on the ingredient
                        e.Value = $"{e.Value} {item.Ingredient.Unit}";
                        e.FormattingApplied = true;
                    }
                }
            };

            // Handle Custom Painting (To make the Delete button look like a rounded pill)
            gridItems.CellPainting += (s, e) =>
            {
                if (e.RowIndex >= 0 && gridItems.Columns[e.ColumnIndex].Name == "Delete")
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                    // Define the button area (make it smaller than the cell for padding)
                    var buttonRect = new Rectangle(e.CellBounds.X + 5, e.CellBounds.Y + 8, e.CellBounds.Width - 10, e.CellBounds.Height - 16);

                    // Choose color: Red normally, Darker Red if mouse is hovering
                    // Note: Use simple logic here, or track mouse state for hover effects
                    var btnColor = Color.FromArgb(255, 235, 238); // Very light red background
                    var textColor = Color.FromArgb(211, 47, 47);   // Dark red text

                    // Draw Rounded Rectangle (Simulated by drawing path or simple fill for simplicity)
                    using (var brush = new SolidBrush(btnColor))
                    {
                        e.Graphics.FillRectangle(brush, buttonRect); // Keeping it simple rectangle for performance, or use GraphicsPath for round
                    }

                    // Draw Text
                    TextRenderer.DrawText(e.Graphics, "Delete", e.CellStyle.Font, buttonRect, textColor,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                    e.Handled = true; // Tell Windows Forms we finished drawing
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

        private void gridItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && gridItems.Columns[e.ColumnIndex].Name == "Delete")
            {
                var result = MessageBox.Show("Are you sure you want to remove this ingredient?",
                                           "Confirm Delete",
                                           MessageBoxButtons.YesNo,
                                           MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    _tempItems.RemoveAt(e.RowIndex);
                }
            }
        }
    }
}