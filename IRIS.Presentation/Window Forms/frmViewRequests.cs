using IRIS.Domain.Entities;
using IRIS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel; // Needed for preventing serialization errors

namespace IRIS.Presentation.Window_Forms
{
    public partial class frmViewRequests : Form
    {
        private readonly IrisDbContext _context;

        // --- FIX 1: Add the Property ---
        // We use [Browsable(false)] so the Designer doesn't try to save this property and cause errors
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int TargetRequestId { get; set; }

        // --- FIX 2: Parameterless Constructor (Like LoginForm) ---
        public frmViewRequests()
        {
            InitializeComponent();

            // Setup DB Context (Just like LoginForm)
            var options = new DbContextOptionsBuilder<IrisDbContext>()
                .UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;
                                Database=IRIS_DB;
                                Trusted_Connection=True;")
                .Options;

            _context = new IrisDbContext(options);
        }

        // --- FIX 3: Load Data in OnLoad (After Property is Set) ---
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (TargetRequestId > 0)
            {
                LoadRequestData(TargetRequestId);
            }
            else
            {
                MessageBox.Show("No Request ID provided.");
                this.Close();
            }
        }

        private void LoadRequestData(int id)
        {
            try
            {
                var request = _context.Requests
                    .Include(r => r.RequestItems)
                        .ThenInclude(ri => ri.Ingredient)
                    .FirstOrDefault(r => r.RequestId == id);

                if (request == null)
                {
                    MessageBox.Show("Request not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Get User Name
                var user = _context.Users.FirstOrDefault(u => u.UserId == request.EncodedBy);
                string studentName = user != null ? user.Username : "Unknown";

                SetupReqForm(request, studentName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
        }

        private void SetupReqForm(Request req, string studentName)
        {
            labelRequested.Text = $"Submitted by {studentName} on {req.CreatedAt:M/d/yyyy, h:mm:ss tt}";
            lblSubjectData.Text = req.Subject ?? "N/A";
            lblFacultyData.Text = req.FacultyName ?? "N/A";
            lblStudentData.Text = studentName;
            lblDateData.Text = req.CreatedAt.ToString("MMM dd, yyyy");

            bunifuDataGridView1.Rows.Clear();
            if (req.RequestItems != null)
            {
                foreach (var item in req.RequestItems)
                {
                    string ingredientName = item.Ingredient != null ? item.Ingredient.Name : "Unknown Item";
                    string qty = $"{item.RequestedQty} {item.Ingredient?.Unit ?? "units"}";
                    bunifuDataGridView1.Rows.Add(ingredientName, qty);
                }
            }

            lblStatusBadge.Text = $"● {req.Status}";
            ConfigureStatusUI(req.Status);
        }

        private void ConfigureStatusUI(string status)
        {
            switch (status)
            {
                case "Approved":
                    lblStatusBadge.ForeColor = Color.FromArgb(21, 128, 61);
                    lblStatusBadge.BackColor = Color.FromArgb(220, 252, 231);
                    btnApprove.Visible = false;
                    btnReject.Visible = false;
                    txtRemarks.ReadOnly = true;
                    break;
                case "Rejected":
                    lblStatusBadge.ForeColor = Color.FromArgb(185, 28, 28);
                    lblStatusBadge.BackColor = Color.FromArgb(254, 226, 226);
                    btnApprove.Visible = false;
                    btnReject.Visible = false;
                    txtRemarks.ReadOnly = true;
                    break;
                default:
                    lblStatusBadge.ForeColor = Color.FromArgb(161, 98, 7);
                    lblStatusBadge.BackColor = Color.FromArgb(254, 249, 195);
                    btnApprove.Visible = true;
                    btnReject.Visible = true;
                    txtRemarks.ReadOnly = false;
                    break;
            }
        }

        public event EventHandler RequestUpdated;

        private void UpdateRequestStatus(string newStatus)
        {
            try
            {
                var req = _context.Requests.FirstOrDefault(r => r.RequestId == TargetRequestId);
                if (req != null)
                {
                    req.Status = newStatus;
                    _context.SaveChanges();

                    MessageBox.Show($"Request {newStatus.ToLower()}.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    RequestUpdated?.Invoke(this, EventArgs.Empty);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void BtnApprove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Approve request?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                UpdateRequestStatus("Approved");
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRemarks.Text))
            {
                MessageBox.Show("Remarks required for rejection."); return;
            }
            if (MessageBox.Show("Reject request?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                UpdateRequestStatus("Rejected");
        }

        private void btnTopClose_Click(object sender, EventArgs e) => this.Close();

        // CLEAN UP CONTEXT ON CLOSE
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _context?.Dispose();
            base.OnFormClosed(e);
        }
    }
}