using IRIS.Domain.Entities;

namespace IRIS.Presentation.Forms
{
    public partial class MainForm : Form
    {
        private System.Windows.Forms.Timer _clockTimer;

        public MainForm()
        {
            InitializeComponent();
            SetupUserDisplay();
            SetupClock();

        }

        private void SetupUserDisplay()
        {
            if (UserSession.CurrentUser != null)
            {
                txtRole.Text = FormatRoleName(UserSession.CurrentUser.Role);

                txtRole.Enabled = false;

                txtRole.DisabledState.FillColor = Color.White;
                txtRole.DisabledState.ForeColor = Color.Indigo;
                txtRole.DisabledState.BorderColor = Color.Indigo;

                txtRole.DisabledState.PlaceholderForeColor = Color.Indigo;
            }
        }

        private string FormatRoleName(Domain.Enums.UserRole role)
        {
            return role switch
            {
                Domain.Enums.UserRole.OfficeStaff => "OFFICE STAFF",
                Domain.Enums.UserRole.AssistantDean => "ASSISTANT DEAN",
                Domain.Enums.UserRole.Dean => "DEAN",
                Domain.Enums.UserRole.QA => "QA",
                _ => role.ToString().ToUpper()
            };
        }

        private void SetupClock()
        {
            _clockTimer = new System.Windows.Forms.Timer();
            _clockTimer.Interval = 30000;
            _clockTimer.Tick += (s, e) => UpdateDateTimeLabel();
            _clockTimer.Start();

            UpdateDateTimeLabel();
        }

        private void UpdateDateTimeLabel()
        {
            lblDate.Text = DateTime.Now.ToString("dddd, MMMM dd, yyyy - hh:mm tt");
        }

        public void LoadPage(UserControl page)
        {
            if (pnlMainContent.Controls.Count > 0)
            {
                pnlMainContent.Controls.Clear();
            }

            page.Dock = DockStyle.Fill;
            pnlMainContent.Controls.Add(page);
            page.BringToFront();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
