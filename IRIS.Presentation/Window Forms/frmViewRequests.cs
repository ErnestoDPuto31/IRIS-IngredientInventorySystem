using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
using IRIS.Services.Implementations;

namespace IRIS.Presentation.Window_Forms
{
    public partial class frmViewRequests : Form
    {
        private readonly int _requestId;
        private readonly RequestService _requestService;
        private readonly int _currentUserId;
        private Request _currentRequest;

        private readonly Color _cReleaseBlue = Color.FromArgb(33, 150, 243); // Blue
        private readonly Color _cApproveGreen = Color.FromArgb(56, 142, 60); // Green

        public frmViewRequests(int requestId, RequestService requestService, int currentUserId)
        {
            InitializeComponent();
            _requestId = requestId;
            _requestService = requestService;
            _currentUserId = currentUserId;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadRequestData();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            gridItems.ClearSelection();
            gridItems.CurrentCell = null;
            this.ActiveControl = null;
        }

        private void LoadRequestData()
        {
            try
            {
                _currentRequest = _requestService.GetRequestById(_requestId);

                if (_currentRequest == null) { MessageBox.Show("Request not found!"); this.Close(); return; }

                lblSubject.Text = _currentRequest.Subject;
                lblFaculty.Text = _currentRequest.FacultyName;
                lblDateOfUse.Text = _currentRequest.DateOfUse.ToString("MM/dd/yyyy");
                lblStudentCount.Text = _currentRequest.StudentCount.ToString();
                lblRecipeCosting.Text = _currentRequest.RecipeCosting.ToString("N2");
                lblAllowedQuantity.Text = _currentRequest.TotalAllowedQty.ToString("N2");

                string submitterName = _currentRequest.EncodedBy != null ? _currentRequest.EncodedBy.Username : "Unknown";
                string submitDate = _currentRequest.CreatedAt.ToString("MMM dd, yyyy");
                string submitTime = _currentRequest.CreatedAt.ToString("hh:mm tt");

                lblSubmittedBy.Text = $"Submitted by <b style='color:#1976D2'>{FormatName(submitterName)}</b> on {submitDate} at {submitTime}";

                // Populate Grid
                gridItems.Rows.Clear();
                foreach (var item in _currentRequest.RequestItems)
                {
                    string name = item.Ingredient?.Name ?? "Item";
                    string unit = item.Ingredient?.Unit ?? "";
                    gridItems.Rows.Add(name, $"{item.RequestedQty} {unit}");
                }

                gridItems.ClearSelection();

                var lastAction = _currentRequest.Approvals.OrderByDescending(a => a.ActionDate).FirstOrDefault();

                if (lastAction != null)
                {
                    txtRemarks.Text = lastAction.Remarks;

                    string actionBy = lastAction.Approver?.Username ?? "Admin";
                    string actionDate = lastAction.ActionDate.ToString("MMM dd, yyyy");
                    string actionTime = lastAction.ActionDate.ToString("hh:mm tt");
                    string actionVerb = lastAction.ActionType.ToString();

                    string colorHex = (actionVerb == "Rejected") ? "#D32F2F" : "#388E3C";

                    lblApprovedRejectedBy.Text = $"{actionVerb} by <b style='color:{colorHex}'>{FormatName(actionBy)}</b> on {actionDate} at {actionTime}";
                    lblApprovedRejectedBy.Visible = true;
                }
                else
                {
                    lblApprovedRejectedBy.Visible = false;
                }

                UpdateStatusUI(_currentRequest.Status);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
        }

        private string FormatName(string username)
        {
            if (string.IsNullOrEmpty(username)) return "Unknown";
            return char.ToUpper(username[0]) + username.Substring(1);
        }

        private void UpdateStatusUI(RequestStatus status)
        {
            // 1. Setup Badge Colors (Keep existing logic)
            Color bg, fg;
            string statusText = status.ToString().ToUpper();

            switch (status)
            {
                case RequestStatus.Approved:
                    bg = Color.FromArgb(232, 245, 233);
                    fg = Color.FromArgb(56, 142, 60);
                    break;
                case RequestStatus.Rejected:
                    bg = Color.FromArgb(255, 235, 238);
                    fg = Color.FromArgb(211, 47, 47);
                    break;
                case RequestStatus.Released:
                    bg = Color.FromArgb(227, 242, 253);
                    fg = Color.FromArgb(33, 150, 243);
                    break;
                default: // Pending
                    bg = Color.FromArgb(255, 248, 225);
                    fg = Color.FromArgb(255, 143, 0);
                    break;
            }

            // 2. Apply Badge Styles (Keep existing logic)
            lblStatusBadge.Text = statusText;
            lblStatusBadge.FillColor = bg;
            lblStatusBadge.ForeColor = fg;
            lblStatusBadge.BorderColor = bg;
            lblStatusBadge.DisabledState.FillColor = bg;
            lblStatusBadge.DisabledState.ForeColor = fg;
            lblStatusBadge.DisabledState.BorderColor = bg;

            // 3. Determine User Roles (Keep existing logic)
            UserRole currentUserRole = UserSession.CurrentUser.Role;

            if (currentUserRole == UserRole.OfficeStaff)
            {
                HideActionButtons();
                txtRemarks.Visible = false;
                lblRemarksTitle.Visible = false;
            }
            else if (status == RequestStatus.Pending)
            {
                ShowActionButtons("Approve", _cApproveGreen);
            }
            else if (status == RequestStatus.Approved && currentUserRole == UserRole.Dean)
            {
                ShowActionButtons("Release", _cReleaseBlue);
                btnReject.Visible = false;
            }
            else
            {
                HideActionButtons();
                txtRemarks.Visible = true;
                txtRemarks.ReadOnly = true;
                lblRemarksTitle.Text = "Remarks";
            }
        }

        private void ShowActionButtons(string approveBtnText, Color? baseColor = null)
        {
            btnApprove.Visible = true;
            btnReject.Visible = true;
            txtRemarks.Visible = true;
            txtRemarks.ReadOnly = false;
            lblRemarksTitle.Visible = true;

            btnApprove.Text = approveBtnText;

            if (baseColor.HasValue)
            {
                Color color = baseColor.Value;

                btnApprove.FillColor = color;
                btnApprove.FillColor2 = ControlPaint.Light(color, 0.1f);

                btnApprove.HoverState.FillColor = ControlPaint.Light(color, 0.3f);
                btnApprove.HoverState.FillColor2 = ControlPaint.Light(color, 0.4f);
            }
        }

        private void HideActionButtons()
        {
            btnApprove.Visible = false;
            btnReject.Visible = false;
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            bool isReleaseAction = btnApprove.Text == "Release";
            string actionName = isReleaseAction ? "Release" : "Approve";
            RequestStatus newStatus = isReleaseAction ? RequestStatus.Released : RequestStatus.Approved;

            if (MessageBox.Show($"Are you sure you want to {actionName} this request?", $"Confirm {actionName}", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _requestService.UpdateRequestStatus(_requestId, newStatus, txtRemarks.Text, _currentUserId);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRemarks.Text)) { MessageBox.Show("Remarks required."); return; }

            if (MessageBox.Show("Are you sure you want to Reject this request?", "Confirm Rejection", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    _requestService.UpdateRequestStatus(_requestId, RequestStatus.Rejected, txtRemarks.Text, _currentUserId);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
        private void btnExitForm_Click(object sender, EventArgs e) => this.Close();
    }
}