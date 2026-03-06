using Guna.UI2.WinForms;
using IRIS.Domain.Entities;
using IRIS.Presentation.UserControls;
using IRIS.Presentation.UserControls.Components;
using IRIS.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
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

        // ✅ 1. DECLARE THE NAVIGATION PANEL HERE
        private NavigationPanel _navigationPanel;

        private Panel _macTrafficHost;
        private Label _macTitleLabel;
        private MacTrafficLightButton _btnMacClose;
        private MacTrafficLightButton _btnMacMinimize;

        private readonly Color _topBarColor = Color.FromArgb(246, 246, 247);

        public MainForm()
        {
            InitializeComponent();
            SetupUserDisplay();
            SetupClock();
            SetupMacTopBar();

            // Setup navigation is called here
            SetupNavigation();
            SetupNotifications();

            LoadPage(new DashboardControl());
        }

        private void SetupNavigation()
        {
            // ✅ 2. CREATE AND DOCK THE PANEL PROGRAMMATICALLY
            _navigationPanel = new NavigationPanel();
            _navigationPanel.Dock = DockStyle.Left;

            // Add it to the form
            this.Controls.Add(_navigationPanel);

            _navigationPanel.BringToFront();

            var requestService = Program.Services.GetService<IRequestService>();

            if (requestService != null)
            {
                _navigationPanel.InitializeService(requestService);
            }

            _badgeTimer = new Timer
            {
                Interval = 3000
            };
            _badgeTimer.Tick += (s, e) =>
            {
                _navigationPanel?.RefreshBadgeCount();
            };
            _badgeTimer.Start();
        }

            _notificationService = (INotificationService)Program.Services.GetService(typeof(INotificationService));

            if (_notificationService != null)
            {
                _notificationService.CheckAllStockLevels();
            }

            _dropdownPanel = new NotificationDropdown();
            _dropdownPanel.Visible = false;

            _dropdownPanel.NotificationClicked += DropdownPanel_NotificationClicked;

            this.Controls.Add(_dropdownPanel);
            _dropdownPanel.BringToFront();

            _notificationTimer = new System.Windows.Forms.Timer();
            _notificationTimer.Interval = 5000; // Checks every 5 seconds
            _notificationTimer.Tick += NotificationTimer_Tick;
            _notificationTimer.Start();

            LoadPage(new DashboardControl());
        }

        private void DropdownPanel_NotificationClicked(object sender, EventArgs e)
        {

        }

        private void NotificationTimer_Tick(object sender, EventArgs e)
        {
            if (UserSession.CurrentUser == null || _notificationService == null) return;

            try
            {
                // 1. Force the database to check for any newly saved low stock!
                _notificationService.CheckAllStockLevels();

                // 2. Fetch the newly generated notifications
                var notifications = _notificationService.GetNotificationsForUser(UserSession.CurrentUser);

                // 3. Update the red bell badge count
                int unreadCount = notifications.Count(n => n.IsRead == false);
                notificationBadge1.Count = unreadCount;

                // 4. Live update: If the dropdown menu is open, refresh it instantly
                if (_dropdownPanel.Visible)
                {
                    _dropdownPanel.LoadNotifications(notifications);
                }
            }
            catch
            {
                // Silently ignore errors to prevent UI crashes during polling
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
                Domain.Enums.UserRole.QA => "QUALITY ASSURANCE",
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

        public void LoadPage(UserControl newPage)
        {
            if (pnlMainContent.Controls.Count > 0 &&
                pnlMainContent.Controls[0].GetType() == newPage.GetType())
            {
                return; // Already showing this page
            }

            // Clear existing controls
            while (pnlMainContent.Controls.Count > 0)
            {
                var oldControl = pnlMainContent.Controls[0];
                pnlMainContent.Controls.Remove(oldControl);

                oldControl?.Dispose();
            }

            newPage.Dock = DockStyle.Fill;
            pnlMainContent.Controls.Add(newPage);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void notificationBadge1_Click(object sender, EventArgs e)
        {
            if (UserSession.CurrentUser == null || _notificationService == null) return;

            // 1. Get current notifications
            var notifications = _notificationService.GetNotificationsForUser(UserSession.CurrentUser);

            // 2. Tell the DATABASE we have read them!
            var idsToMarkRead = notifications.Select(n => n.NotificationId).ToList();
            if (idsToMarkRead.Any())
            {
                _notificationService.MarkNotificationsAsRead(idsToMarkRead);
            }

            // 3. Kill the red number instantly
            notificationBadge1.Count = 0;

            // 4. Handle the Dropdown UI
            if (_dropdownPanel.Visible)
            {
                _dropdownPanel.Visible = false;
                return;
            }

            _dropdownPanel.Location = new Point(
                notificationBadge1.Location.X - _dropdownPanel.Width + notificationBadge1.Width,
                notificationBadge1.Location.Y + notificationBadge1.Height + 5
            );

            _dropdownPanel.LoadNotifications(notifications);
            _dropdownPanel.Visible = true;
            _dropdownPanel.BringToFront();
        }
    }
}