using IRIS.Domain.Entities;
using IRIS.Presentation.UserControls;
using IRIS.Presentation.UserControls.Components;
using IRIS.Presentation.UserControls.PagesUC;
using IRIS.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.InteropServices;
using Timer = System.Windows.Forms.Timer;

namespace IRIS.Presentation.Forms
{
    public partial class MainForm : Form
    {
        #region Fields
        private Timer _clockTimer;
        private Timer _notificationTimer;
        private Timer _badgeTimer;

        private NotificationDropdown _dropdownPanel;
        private INotificationService _notificationService;
        private NavigationPanel _navigationPanel;

        private Panel _macTrafficHost;
        private Label _macTitleLabel;
        private MacTrafficLightButton _btnMacClose;
        private MacTrafficLightButton _btnMacMinimize;

        private readonly Color _topBarColor = Color.FromArgb(246, 246, 247);
        #endregion

        public MainForm()
        {
            InitializeComponent();
            DoubleBuffered = true;

            // 1. Core Setups
            SetupClock();
            SetupMacTopBar();
            SetupNavigation();
            SetupNotifications();
            SetupUserDisplay();

            // 2. Initial Page
            LoadPage(new DashboardControl());
        }

        #region Setup Methods
        private void SetupClock()
        {
            _clockTimer = new Timer { Interval = 30000 };
            _clockTimer.Tick += (s, e) => UpdateDateTimeLabel();
            _clockTimer.Start();
            UpdateDateTimeLabel();
        }

        private void UpdateDateTimeLabel()
        {
            if (lblDate != null)
                lblDate.Text = DateTime.Now.ToString("dddd, MMMM dd, yyyy - hh:mm tt");
        }

        private void SetupNavigation()
        {
            _navigationPanel = new NavigationPanel { Dock = DockStyle.Left };
            this.Controls.Add(_navigationPanel);
            _navigationPanel.BringToFront();

            var requestService = Program.Services.GetService<IRequestService>();
            if (requestService != null) _navigationPanel.InitializeService(requestService);

            _badgeTimer = new Timer { Interval = 3000 };
            _badgeTimer.Tick += (s, e) => _navigationPanel?.RefreshBadgeCount();
            _badgeTimer.Start();
        }

        private void SetupNotifications()
        {
            _notificationService = Program.Services.GetService<INotificationService>();

            _dropdownPanel = new NotificationDropdown { Visible = false, Size = new Size(390, 430) };
            this.Controls.Add(_dropdownPanel);
            _dropdownPanel.BringToFront();

            _notificationTimer = new Timer { Interval = 5000 };
            _notificationTimer.Tick += NotificationTimer_Tick;
            _notificationTimer.Start();
        }

        private void SetupUserDisplay()
        {
            if (UserSession.CurrentUser != null && txtRole != null)
            {
                txtRole.Text = FormatRoleName(UserSession.CurrentUser.Role);
                txtRole.Enabled = false;
                txtRole.DisabledState.FillColor = Color.White;
                txtRole.DisabledState.ForeColor = Color.Indigo;
            }
        }
        #endregion

        #region Logic Methods
        public void LoadPage(UserControl newPage)
        {
            if (newPage == null || pnlMainContent == null) return;
            pnlMainContent.Controls.Clear();
            newPage.Dock = DockStyle.Fill;
            pnlMainContent.Controls.Add(newPage);
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

        private void PositionDropdownUnderBadge()
        {
            if (notificationBadge == null || notificationBadge.Parent == null) return;

            Control anchor = notificationBadge.Parent;
            Point screenLoc = anchor.PointToScreen(new Point(anchor.Width - _dropdownPanel.Width, anchor.Height + 5));
            Point clientLoc = this.PointToClient(screenLoc);

            _dropdownPanel.Location = clientLoc;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            PositionMacButtons();
            if (_dropdownPanel != null && _dropdownPanel.Visible)
            {
                PositionDropdownUnderBadge();
            }
        }
        #endregion

        #region Event Handlers
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void NotificationTimer_Tick(object sender, EventArgs e)
        {
            if (UserSession.CurrentUser == null || _notificationService == null) return;
            try
            {
                _notificationService.CheckAllStockLevels();
                var list = _notificationService.GetNotificationsForUser(UserSession.CurrentUser);
                int unread = list.Count(n => !n.IsRead);

                if (notificationBadge != null) notificationBadge.Count = unread;
                if (_dropdownPanel != null && _dropdownPanel.Visible)
                {
                    _dropdownPanel.LoadNotifications(list);
                    PositionDropdownUnderBadge();
                }
            }
            catch { /* Polling safety */ }
        }

        private void notificationBadge_Click(object sender, EventArgs e)
        {
            if (UserSession.CurrentUser == null || _notificationService == null) return;

            if (_dropdownPanel.Visible)
            {
                // Hide the dropdown with animation when it's already visible
                _dropdownPanel.HideBubble();
                return; // Return to prevent reloading the notifications if it's being hidden
            }

            var list = _notificationService.GetNotificationsForUser(UserSession.CurrentUser);
            var ids = list.Select(n => n.NotificationId).ToList();
            if (ids.Any()) _notificationService.MarkNotificationsAsRead(ids);

            if (notificationBadge != null) notificationBadge.Count = 0;

            PositionDropdownUnderBadge();
            _dropdownPanel.LoadNotifications(list);
            _dropdownPanel.Visible = true;
            _dropdownPanel.BringToFront();

            // Show the dropdown with animation
            _dropdownPanel.ShowBubble();
        }
        #endregion

        #region Mac Custom Title Bar & Dragging
        private void SetupMacTopBar()
        {
            if (guna2Panel1 == null) return;

            FormBorderStyle = FormBorderStyle.None;
            guna2Panel1.Dock = DockStyle.Top;
            guna2Panel1.Height = 42;
            guna2Panel1.FillColor = _topBarColor;
            guna2Panel1.BackColor = _topBarColor;

            if (btnExit != null) btnExit.Visible = false;

            if (_macTrafficHost == null)
            {
                _macTrafficHost = new Panel
                {
                    Name = "macTrafficHost",
                    BackColor = _topBarColor,
                    Size = new Size(46, 16),
                    Anchor = AnchorStyles.Top | AnchorStyles.Right
                };

                _btnMacMinimize = new MacTrafficLightButton(MacTrafficLightKind.Minimize, _topBarColor) { Location = new Point(0, 0) };
                _btnMacMinimize.Click += (s, e) => WindowState = FormWindowState.Minimized;

                _btnMacClose = new MacTrafficLightButton(MacTrafficLightKind.Close, _topBarColor) { Location = new Point(24, 0) };
                _btnMacClose.Click += (s, e) => Close();

                _macTrafficHost.Controls.Add(_btnMacMinimize);
                _macTrafficHost.Controls.Add(_btnMacClose);
                guna2Panel1.Controls.Add(_macTrafficHost);
                _macTrafficHost.BringToFront();
            }

            if (_macTitleLabel == null)
            {
                _macTitleLabel = new Label
                {
                    AutoSize = false,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = "IRIS",
                    Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                    ForeColor = Color.FromArgb(82, 82, 88),
                    BackColor = _topBarColor
                };
                guna2Panel1.Controls.Add(_macTitleLabel);
                _macTitleLabel.SendToBack();
            }

            PositionMacButtons();
            WireDragToControl(guna2Panel1);
            WireDragToControl(_macTitleLabel);
        }

        private void PositionMacButtons()
        {
            if (guna2Panel1 == null || _macTrafficHost == null) return;
            int rightPadding = 14;
            int y = Math.Max(8, (guna2Panel1.Height - _macTrafficHost.Height) / 2);
            int x = guna2Panel1.Width - rightPadding - _macTrafficHost.Width;
            _macTrafficHost.Location = new Point(x, y);
            _macTrafficHost.BringToFront();
        }

        private void WireDragToControl(Control control)
        {
            if (control == null) return;
            control.MouseDown -= DragSurface_MouseDown;
            control.MouseDown += DragSurface_MouseDown;

            foreach (Control child in control.Controls)
            {
                if (child is MacTrafficLightButton) continue;
                WireDragToControl(child);
            }
        }

        private void DragSurface_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            ReleaseCapture();
            SendMessage(Handle, 0xA1, 0x2, 0);
        }

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        #endregion
    }
}