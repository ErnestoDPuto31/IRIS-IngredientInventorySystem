using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
using IRIS.Services.Implementations;
using IRIS.Services.Interfaces;
using System.ComponentModel;
using System.Data;

namespace IRIS.Presentation.Window_Forms
{
    public partial class frmRequestEntry : Form
    {
        private readonly RequestService _requestService;
        private readonly IngredientService _ingredientService;
        private BindingList<RequestDetails> _tempItems;
        private readonly INotificationService _notificationService;

        private decimal _totalFinancialBudget = 0m;

        public frmRequestEntry(RequestService reqService, IngredientService ingService)
        {
            InitializeComponent();

            _requestService = reqService;
            _ingredientService = ingService;
            _tempItems = new BindingList<RequestDetails>();

            dtpDateOfUse.Value = DateTime.Today;
            dtpDateOfUse.MinDate = DateTime.Today;

            _notificationService = (INotificationService)Program.Services.GetService(typeof(INotificationService));

            numStudentCount.ValueChanged += (s, e) => CalculateBudget();
            numRecipeCosting.ValueChanged += (s, e) => CalculateBudget();

            CalculateBudget();
        }

        private void CalculateBudget()
        {
            // Total Budget = Budget Per Student * Number of Students
            _totalFinancialBudget = numStudentCount.Value * numRecipeCosting.Value;
            lblAllowedQtyDisplay.Text = $"₱ {_totalFinancialBudget:N2}";
        }

        private void btnAdd_Click(object sender, EventArgs e) => OpenIngredientSelector();

        private void OpenIngredientSelector()
        {
            if (numStudentCount.Value <= 0)
            {
                MessageBox.Show("Student count must be greater than zero.", "Invalid Student Count", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (numRecipeCosting.Value <= 0)
            {
                MessageBox.Show("Price Per Student must be greater than zero.", "Invalid Price", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var selector = new frmIngredientSelector(_ingredientService, (int)numStudentCount.Value, _totalFinancialBudget, _tempItems))
            {
                if (selector.ShowDialog() == DialogResult.OK)
                {
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

            // Calculate the total actual cost of items chosen
            decimal actualTotalPrice = _tempItems.Sum(item => item.RequestedQty * item.UnitPrice);

            var newRequest = new Request
            {
                Subject = txtSubject.Text.Trim(),
                FacultyName = txtFaculty.Text.Trim(),
                DateOfUse = dtpDateOfUse.Value,

                // StudentCount remains an int (no half students!), but the rest are now clean decimals.
                StudentCount = (int)numStudentCount.Value,
                TotalBudget = _totalFinancialBudget,
                PricePerStudent = numRecipeCosting.Value,
                TotalPrice = actualTotalPrice,

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
                    AllowedQty = item.AllowedQty,
                    UnitPrice = item.UnitPrice
                }).ToList();

                _requestService.CreateRequest(newRequest, itemsToSave);

                _notificationService.CreateNewRequestNotification(
                    newRequest.RequestId,
                    newRequest.FacultyName,
                    newRequest.Subject);

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
    }
}