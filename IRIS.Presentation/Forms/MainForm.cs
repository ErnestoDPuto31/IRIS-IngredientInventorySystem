using IRIS.Domain.Entities;
using IRIS.Presentation.UserControls;
using IRIS.Presentation.UserControls.Components;
using IRIS.Services.Implementations;
using IRIS.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace IRIS.Presentation.Forms
{
    public partial class MainForm : Form
    {
        private System.Windows.Forms.Timer _clockTimer;
        private System.Windows.Forms.Timer _notificationTimer; // 1. Added the notification timer
        private NotificationDropdown _dropdownPanel;
        private INotificationService _notificationService;

        public MainForm()
        {
            InitializeComponent();
            SetupUserDisplay();
            SetupClock();

            var requestService = (IRequestService)Program.Services.GetService(typeof(IRequestService));

            // Pass it into the navigation panel!
            navigationPanel1.InitializeService(requestService);
            System.Windows.Forms.Timer badgeTimer = new Timer();
            badgeTimer.Interval = 3000;
            badgeTimer.Tick += (s, e) => navigationPanel1.RefreshBadgeCount();
            badgeTimer.Start();

            _notificationService = (INotificationService)Program.Services.GetService(typeof(INotificationService));

            // Setup the Dropdown (but keep it hidden initially)
            _dropdownPanel = new NotificationDropdown();
            _dropdownPanel.Visible = false;
            this.Controls.Add(_dropdownPanel);
            _dropdownPanel.BringToFront(); // Ensure it floats on top of everything

            // 2. Setup the Notification "Heartbeat" Timer for the Bell Icon
            _notificationTimer = new System.Windows.Forms.Timer();
            _notificationTimer.Interval = 5000; // Checks every 5 seconds
            _notificationTimer.Tick += NotificationTimer_Tick;
            _notificationTimer.Start();
        }

        // 3. The method that updates the bell icon every 5 seconds
        private void NotificationTimer_Tick(object sender, EventArgs e)
        {
            // Safety check: Don't do anything if no one is logged in yet
            if (UserSession.CurrentUser == null || _notificationService == null) return;

            try
            {
                // Ask the database how many UNREAD alerts this specific user has
                int unreadCount = _notificationService.GetUnreadCountForUser(UserSession.CurrentUser);

                // Update the red badge number! 
                notificationBadge1.Count = unreadCount;
            }
            catch
            {
                // Silently ignore errors here so a random network blip doesn't crash your app
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
            // If it's already open, close it
            if (_dropdownPanel.Visible)
            {
                _dropdownPanel.Visible = false;
                return;
            }

            // Position the dropdown directly under the bell icon
            _dropdownPanel.Location = new Point(
                notificationBadge1.Location.X - _dropdownPanel.Width + notificationBadge1.Width,
                notificationBadge1.Location.Y + notificationBadge1.Height + 5
            );

            // Fetch the real data using your Clean Architecture Service!
            var notifications = _notificationService.GetNotificationsForUser(UserSession.CurrentUser);

            // Load data and show
            _dropdownPanel.LoadNotifications(notifications);
            _dropdownPanel.Visible = true;
            _dropdownPanel.BringToFront();
        }
    }
}