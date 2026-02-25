using IRIS.Domain.Entities;
using IRIS.Presentation.UserControls;
using IRIS.Presentation.UserControls.Components;
using IRIS.Services.Implementations;
using IRIS.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Drawing;
using System.Linq; // Added for LINQ counting
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace IRIS.Presentation.Forms
{
    public partial class MainForm : Form
    {
        private System.Windows.Forms.Timer _clockTimer;
        private System.Windows.Forms.Timer _notificationTimer;
        private NotificationDropdown _dropdownPanel;
        private INotificationService _notificationService;

        public MainForm()
        {
            InitializeComponent();
            SetupUserDisplay();
            SetupClock();

            var requestService = (IRequestService)Program.Services.GetService(typeof(IRequestService));

            navigationPanel1.InitializeService(requestService);
            System.Windows.Forms.Timer badgeTimer = new Timer();
            badgeTimer.Interval = 3000;
            badgeTimer.Tick += (s, e) => navigationPanel1.RefreshBadgeCount();
            badgeTimer.Start();

            _notificationService = (INotificationService)Program.Services.GetService(typeof(INotificationService));

            // --- NEW: Scan all stock levels when the dashboard loads! ---
            _notificationService.CheckAllStockLevels();
            // ------------------------------------------------------------

            _dropdownPanel = new NotificationDropdown();
            _dropdownPanel.Visible = false;

            _dropdownPanel.NotificationClicked += DropdownPanel_NotificationClicked;

            this.Controls.Add(_dropdownPanel);
            _dropdownPanel.BringToFront();

            _notificationTimer = new System.Windows.Forms.Timer();
            _notificationTimer.Interval = 3000;
            _notificationTimer.Tick += NotificationTimer_Tick;
            _notificationTimer.Start();
        }

        private void DropdownPanel_NotificationClicked(object sender, EventArgs e)
        {
            // We removed the forced "Count = 0" here. 
            // The badge number will now strictly rely on the database status!
        }

        private void NotificationTimer_Tick(object sender, EventArgs e)
        {
            if (UserSession.CurrentUser == null || _notificationService == null) return;

            try
            {
                // Fetch all notifications for the user
                var notifications = _notificationService.GetNotificationsForUser(UserSession.CurrentUser);

                // --- UPDATED: Now counts "Pending", "New", AND "stock" (for low/critical stock alerts) ---
                int pendingCount = notifications.Count(n =>
                    n.Message.Contains("Pending", StringComparison.OrdinalIgnoreCase) ||
                    n.Message.Contains("New", StringComparison.OrdinalIgnoreCase) ||
                    n.Message.Contains("stock", StringComparison.OrdinalIgnoreCase));

                // Set the badge count to the number of pending items!
                notificationBadge1.Count = pendingCount;
            }
            catch
            {
                // Silently ignore errors
            }
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

        private void notificationBadge1_Click(object sender, EventArgs e)
        {
            // --- NEW: Immediately clear the red number visually ---
            notificationBadge1.Count = 0;

            if (_dropdownPanel.Visible)
            {
                _dropdownPanel.Visible = false;
                return;
            }

            _dropdownPanel.Location = new Point(
                notificationBadge1.Location.X - _dropdownPanel.Width + notificationBadge1.Width,
                notificationBadge1.Location.Y + notificationBadge1.Height + 5
            );

            // Fetch and load notifications
            var notifications = _notificationService.GetNotificationsForUser(UserSession.CurrentUser);
            _dropdownPanel.LoadNotifications(notifications);
            _dropdownPanel.Visible = true;
            _dropdownPanel.BringToFront();
        }
    }
}