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
            lblAllowedQtyDisplay.Text = $"Total Budget: {_totalFinancialBudget:C2}";
        }

        private void btnAdd_Click(object sender, EventArgs e) => OpenIngredientSelector();
        private void btnViewIngredients_Click(object sender, EventArgs e) => OpenIngredientSelector();

        private void OpenIngredientSelector()
        {
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