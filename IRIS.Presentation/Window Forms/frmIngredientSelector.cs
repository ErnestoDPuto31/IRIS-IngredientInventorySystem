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
        private readonly BindingList<RequestDetails> _existingItems;
        private Ingredient _selectedIngredient;

        private readonly Color IRIS_Indigo = Color.FromArgb(75, 0, 130);

        public frmIngredientSelector(IngredientService ingredientService, int studentCount, BindingList<RequestDetails> currentItems = null)
        {
            InitializeComponent();
            _ingredientService = ingredientService;
            _studentCount = studentCount;

            _existingItems = currentItems ?? new BindingList<RequestDetails>();


            this.btnAdd.Click += btnAdd_Click;
            this.btnRemove.Click += btnRemove_Click; 

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
                string displayString = $"{name} - {detail.RequestedQty:N2} {unit}";

                int xOffset = 10;
                Rectangle textBounds = new Rectangle(
                    e.Bounds.X + xOffset,
                    e.Bounds.Y,
                    e.Bounds.Width - xOffset,
                    e.Bounds.Height
                );

                TextRenderer.DrawText(
                    e.Graphics,
                    displayString,
                    lstSummary.Font,
                    textBounds,
                    foreColor,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Left
                );
            }
            e.DrawFocusRectangle();
        }

        private void ConfigureControls()
        {
            numRequestedQty.DecimalPlaces = 2;
            numRequestedQty.Increment = 0.5M;
        }

        private void frmIngredientSelector_Load(object sender, EventArgs e)
        {
            var ingredients = _ingredientService.GetAllIngredients().ToList();
            ucIngredientTable.SetData(ingredients);
        }

        private void UcIngredientTable_IngredientSelected(object sender, Ingredient selectedItem)
        {
            _selectedIngredient = selectedItem;
            var existing = _existingItems.FirstOrDefault(x => x.IngredientId == _selectedIngredient.IngredientId);

            if (existing != null)
            {
                numRequestedQty.Value = existing.RequestedQty;
                btnAdd.Text = "Update Qty";
            }
            else
            {
                numRequestedQty.Value = 0.00M;
                btnAdd.Text = "Add to List";
            }

            lblSelectedName.Text = $"{_selectedIngredient.Name} (Avail: {_selectedIngredient.CurrentStock:N2} {_selectedIngredient.Unit})";
            lblSelectedName.ForeColor = IRIS_Indigo;

            pnlInput.Enabled = true;
            numRequestedQty.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (_selectedIngredient == null) return;
            decimal requestedQty = numRequestedQty.Value;

            if (requestedQty <= 0)
            {
                MessageBox.Show("Please enter a quantity greater than zero.", "Validation");
                return;
            }

            var existing = _existingItems.FirstOrDefault(x => x.IngredientId == _selectedIngredient.IngredientId);

            if (existing != null)
            {
                existing.RequestedQty = requestedQty;
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
                    PortionPerStudent = 0,
                    AllowedQty = 0
                });
            }

            ResetInput();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstSummary.SelectedItem is RequestDetails selectedItem)
            {
                var result = MessageBox.Show($"Remove {selectedItem.Ingredient?.Name} from list?", "Confirm Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    _existingItems.Remove(selectedItem);
                }
            }
            else
            {
                MessageBox.Show("Please select an item from the summary to remove.", "No Selection");
            }
        }

        private void ResetInput()
        {
            pnlInput.Enabled = false;
            _selectedIngredient = null;
            lblSelectedName.Text = "Select another item...";
            numRequestedQty.Value = 0;
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