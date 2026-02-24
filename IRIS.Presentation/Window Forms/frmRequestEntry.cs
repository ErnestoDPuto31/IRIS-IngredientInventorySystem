using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
using IRIS.Services.Implementations;
using System.ComponentModel;
using System.Data;

namespace IRIS.Presentation.Window_Forms
{
    public partial class frmRequestEntry : Form
    {
        private readonly RequestService _requestService;
        private readonly IngredientService _ingredientService;
        private BindingList<RequestDetails> _tempItems;

        public frmRequestEntry(RequestService reqService, IngredientService ingService)
        {
            InitializeComponent();

            _requestService = reqService;
            _ingredientService = ingService;
            _tempItems = new BindingList<RequestDetails>();

            dtpDateOfUse.Value = DateTime.Today;
            dtpDateOfUse.MinDate = DateTime.Today;

            CalculateAllowedQuantity();

            btnSubmit.Enabled = false;
        }

        private void CalculateAllowedQuantity()
        {
            decimal totalAllowed = numStudentCount.Value * numRecipeCosting.Value;
            lblAllowedQtyDisplay.Text = $"{totalAllowed:N2} (Limit per Item)";

            foreach (var item in _tempItems)
            {
                item.PortionPerStudent = numRecipeCosting.Value;
                item.AllowedQty = totalAllowed;
            }
        }


        private void btnAdd_Click(object sender, EventArgs e) => OpenIngredientSelector();
        private void btnViewIngredients_Click(object sender, EventArgs e) => OpenIngredientSelector();

        private void OpenIngredientSelector()
        {
            using (var selector = new frmIngredientSelector(_ingredientService, (int)numStudentCount.Value, _tempItems))
            {
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    CalculateAllowedQuantity();

                    bool hasItems = _tempItems.Count > 0;
                    btnSubmit.Enabled = hasItems;
                }
            }
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
                Status = RequestStatus.Pending,
                EncodedById = UserSession.CurrentUser.UserId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            try
            {
                var itemsToSave = _tempItems.Select(item => new RequestDetails
                {
                    IngredientId = item.IngredientId,
                    PortionPerStudent = item.PortionPerStudent,
                    RequestedQty = item.RequestedQty,
                    AllowedQty = item.AllowedQty
                }).ToList();

                _requestService.CreateRequest(newRequest, itemsToSave);

                MessageBox.Show("Request submitted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += "\n\nDATABASE ERROR:\n" + ex.InnerException.Message;
                }
                MessageBox.Show(errorMessage, "Error Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExitForm_Click(object sender, EventArgs e) => this.Close();
        private void btnCancel_Click(object sender, EventArgs e) => this.Close();

        private void numStudentCount_ValueChanged(object sender, EventArgs e) => CalculateAllowedQuantity();
        private void numRecipeCosting_ValueChanged(object sender, EventArgs e) => CalculateAllowedQuantity();
    }
}