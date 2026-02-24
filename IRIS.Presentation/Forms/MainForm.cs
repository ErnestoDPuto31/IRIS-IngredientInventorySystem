using IRIS.Domain.Entities;
using IRIS.Presentation.UserControls;
using IRIS.Services.Implementations;
using IRIS.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Timer = System.Windows.Forms.Timer;
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
            // 1. Ask your global Program to hand you the secure service
            // 1. Ask your global Program to hand you the secure service using the native method
            var requestService = (IRequestService)Program.Services.GetService(typeof(IRequestService));

            // 2. Pass it into the navigation panel!
            navigationPanel1.InitializeService(requestService);
            System.Windows.Forms.Timer badgeTimer = new Timer();
            badgeTimer.Interval = 3000;
            badgeTimer.Tick += (s, e) => navigationPanel1.RefreshBadgeCount();
            badgeTimer.Start();
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
