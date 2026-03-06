using Guna.UI2.WinForms;
using IRIS.Domain.Entities;
using IRIS.Presentation.UserControls.Components;
using IRIS.Presentation.UserControls.PagesUC;
using IRIS.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using Timer = System.Windows.Forms.Timer;

namespace IRIS.Presentation.Forms
{
    public partial class MainForm : Form
    {
        private Timer _clockTimer;
        private Timer _notificationTimer;
        private Timer _badgeTimer;

        private NotificationDropdown _dropdownPanel;
        private INotificationService _notificationService;

        private Panel _macTrafficHost;
        private Label _macTitleLabel;
        private MacTrafficLightButton _btnMacClose;
        private MacTrafficLightButton _btnMacMinimize;

        private readonly Color _topBarColor = Color.FromArgb(246, 246, 247);

        public MainForm()
        {
            InitializeComponent();

            DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);

            SetupUserDisplay();
            SetupClock();
            SetupMacTopBar();
            SetupNavigation();
            SetupNotifications();

            LoadPage(new DashboardControl());
        }

        private void SetupNavigation()
        {
            var requestService = Program.Services.GetService<IRequestService>();

            if (navigationPanel != null && requestService != null)
            {
                navigationPanel.InitializeService(requestService);
            }

            _badgeTimer = new Timer
            {
                Interval = 3000
            };
            _badgeTimer.Tick += (s, e) =>
            {
                navigationPanel?.RefreshBadgeCount();
            };
            _badgeTimer.Start();
        }

        private void SetupNotifications()
        {
            _notificationService = Program.Services.GetService<INotificationService>();

            if (_notificationService != null)
            {
                _notificationService.CheckAllStockLevels();
            }

            _dropdownPanel = new NotificationDropdown
            {
                Visible = false,
                Size = new Size(390, 430)
            };

            _dropdownPanel.NotificationClicked += DropdownPanel_NotificationClicked;

            Controls.Add(_dropdownPanel);
            _dropdownPanel.BringToFront();

            _notificationTimer = new Timer
            {
                Interval = 5000
            };
            _notificationTimer.Tick += NotificationTimer_Tick;
            _notificationTimer.Start();
        }

        private void DropdownPanel_NotificationClicked(object sender, EventArgs e)
        {
            if (_dropdownPanel != null && _dropdownPanel.Visible)
            {
                _dropdownPanel.HideBubble();
            }

            RefreshNotificationBadge();
        }

        private void NotificationTimer_Tick(object sender, EventArgs e)
        {
            RefreshNotificationBadge();

            if (_dropdownPanel != null && _dropdownPanel.Visible && UserSession.CurrentUser != null && _notificationService != null)
            {
                var notifications = _notificationService.GetNotificationsForUser(UserSession.CurrentUser) ?? new List<NotificationDto>();
                _dropdownPanel.LoadNotifications(notifications);
                PositionDropdownUnderBadge();
            }
        }

        private void RefreshNotificationBadge()
        {
            if (UserSession.CurrentUser == null || _notificationService == null) return;

            try
            {
                var notifications = _notificationService.GetNotificationsForUser(UserSession.CurrentUser) ?? new List<NotificationDto>();
                int unreadCount = notifications.Count(n => n != null && !n.IsRead);

                if (notificationBadge1 != null)
                {
                    notificationBadge1.Count = unreadCount;
                    notificationBadge1.Visible = true;
                }
            }
            catch
            {
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
            _clockTimer = new Timer
            {
                Interval = 30000
            };
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
            if (newPage == null || pnlMainContent == null) return;

            if (pnlMainContent.Controls.Count > 0 &&
                ReferenceEquals(pnlMainContent.Controls[0], newPage))
            {
                return;
            }

            pnlMainContent.SuspendLayout();

            while (pnlMainContent.Controls.Count > 0)
            {
                var oldControl = pnlMainContent.Controls[0];
                pnlMainContent.Controls.Remove(oldControl);
                oldControl?.Dispose();
            }

            newPage.Dock = DockStyle.Fill;
            pnlMainContent.Controls.Add(newPage);
            newPage.BringToFront();

            pnlMainContent.ResumeLayout();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void notificationBadge1_Click(object sender, EventArgs e)
        {
            if (UserSession.CurrentUser == null || _notificationService == null || _dropdownPanel == null) return;

            var notifications = _notificationService.GetNotificationsForUser(UserSession.CurrentUser) ?? new List<NotificationDto>();

            var idsToMarkRead = notifications
                .Where(n => n != null)
                .Select(n => n.NotificationId)
                .ToList();

            if (idsToMarkRead.Any())
            {
                _notificationService.MarkNotificationsAsRead(idsToMarkRead);
            }

            if (_dropdownPanel.Visible)
            {
                _dropdownPanel.HideBubble();
                notificationBadge1.Count = 0;
                return;
            }

            notificationBadge1.Count = 0;
            notificationBadge1.Visible = true;

            _dropdownPanel.LoadNotifications(notifications);
            PositionDropdownUnderBadge();
            _dropdownPanel.BringToFront();
            _dropdownPanel.ShowBubble();
        }

        private void PositionDropdownUnderBadge()
        {
            if (_dropdownPanel == null || notificationBadge1 == null) return;

            Point badgeScreen = notificationBadge1.PointToScreen(Point.Empty);
            Point badgeClient = PointToClient(badgeScreen);

            int spacing = 10;
            int x = badgeClient.X + notificationBadge1.Width - _dropdownPanel.Width;
            int y = badgeClient.Y + notificationBadge1.Height + spacing;

            int maxX = ClientSize.Width - _dropdownPanel.Width - 12;
            int maxY = ClientSize.Height - _dropdownPanel.Height - 12;

            if (x < 12) x = 12;
            if (x > maxX) x = Math.Max(12, maxX);

            if (y < 12) y = 12;
            if (y > maxY) y = Math.Max(12, maxY);

            _dropdownPanel.Location = new Point(x, y);
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

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _clockTimer?.Stop();
            _notificationTimer?.Stop();
            _badgeTimer?.Stop();

            _clockTimer?.Dispose();
            _notificationTimer?.Dispose();
            _badgeTimer?.Dispose();

            base.OnFormClosed(e);
        }

        private void SetupMacTopBar()
        {
            if (guna2Panel1 == null) return;

            FormBorderStyle = FormBorderStyle.None;

            guna2Panel1.Dock = DockStyle.Top;
            guna2Panel1.Height = 42;
            guna2Panel1.FillColor = _topBarColor;
            guna2Panel1.BackColor = _topBarColor;
            guna2Panel1.BorderColor = Color.FromArgb(223, 224, 228);
            guna2Panel1.BorderThickness = 1;
            guna2Panel1.CustomBorderThickness = new Padding(0, 0, 0, 1);
            guna2Panel1.ShadowDecoration.Enabled = false;
            guna2Panel1.Padding = new Padding(14, 8, 14, 8);

            if (btnExit != null)
            {
                btnExit.Visible = false;
            }

            if (_macTrafficHost == null)
            {
                _macTrafficHost = new Panel
                {
                    Name = "macTrafficHost",
                    BackColor = _topBarColor,
                    Size = new Size(46, 16),
                    Anchor = AnchorStyles.Top | AnchorStyles.Right
                };

                _btnMacMinimize = new MacTrafficLightButton(MacTrafficLightKind.Minimize, _topBarColor)
                {
                    Location = new Point(0, 0)
                };
                _btnMacMinimize.Click += (s, e) => WindowState = FormWindowState.Minimized;

                _btnMacClose = new MacTrafficLightButton(MacTrafficLightKind.Close, _topBarColor)
                {
                    Location = new Point(24, 0)
                };
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
            SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
        }

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        private enum MacTrafficLightKind
        {
            Close,
            Minimize
        }

        private sealed class MacTrafficLightButton : Control
        {
            private readonly MacTrafficLightKind _kind;
            private readonly Timer _hoverTimer;
            private readonly Color _surfaceColor;

            private float _scale = 1f;
            private float _targetScale = 1f;
            private int _glyphAlpha = 0;
            private int _targetGlyphAlpha = 0;
            private bool _hovered;

            public MacTrafficLightButton(MacTrafficLightKind kind, Color surfaceColor)
            {
                _kind = kind;
                _surfaceColor = surfaceColor;

                Size = new Size(14, 14);
                Cursor = Cursors.Hand;
                BackColor = surfaceColor;
                Margin = Padding.Empty;

                SetStyle(ControlStyles.AllPaintingInWmPaint |
                         ControlStyles.OptimizedDoubleBuffer |
                         ControlStyles.ResizeRedraw |
                         ControlStyles.UserPaint, true);

                _hoverTimer = new Timer
                {
                    Interval = 15
                };
                _hoverTimer.Tick += HoverTimer_Tick;
            }

            protected override void OnMouseEnter(EventArgs e)
            {
                base.OnMouseEnter(e);
                _hovered = true;
                _targetScale = 1.12f;
                _targetGlyphAlpha = 230;
                _hoverTimer.Start();
                Invalidate();
            }

            protected override void OnMouseLeave(EventArgs e)
            {
                base.OnMouseLeave(e);
                _hovered = false;
                _targetScale = 1f;
                _targetGlyphAlpha = 0;
                _hoverTimer.Start();
                Invalidate();
            }

            protected override void OnMouseDown(MouseEventArgs e)
            {
                base.OnMouseDown(e);
                _targetScale = 0.94f;
                _targetGlyphAlpha = 255;
                _hoverTimer.Start();
                Invalidate();
            }

            protected override void OnMouseUp(MouseEventArgs e)
            {
                base.OnMouseUp(e);
                _targetScale = _hovered ? 1.12f : 1f;
                _targetGlyphAlpha = _hovered ? 230 : 0;
                _hoverTimer.Start();
                Invalidate();
            }

            private void HoverTimer_Tick(object sender, EventArgs e)
            {
                float scaleDelta = (_targetScale - _scale) * 0.34f;
                _scale += scaleDelta;

                int alphaDelta = (int)((_targetGlyphAlpha - _glyphAlpha) * 0.34f);
                if (alphaDelta == 0 && _glyphAlpha != _targetGlyphAlpha)
                {
                    alphaDelta = _targetGlyphAlpha > _glyphAlpha ? 1 : -1;
                }

                _glyphAlpha += alphaDelta;

                bool scaleDone = Math.Abs(_targetScale - _scale) < 0.01f;
                bool alphaDone = Math.Abs(_targetGlyphAlpha - _glyphAlpha) <= 1;

                if (scaleDone) _scale = _targetScale;
                if (alphaDone) _glyphAlpha = _targetGlyphAlpha;

                Invalidate();

                if (scaleDone && alphaDone)
                {
                    _hoverTimer.Stop();
                }
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.Clear(_surfaceColor);

                Color baseColor = GetBaseColor();
                Color borderColor = ControlPaint.Dark(baseColor, 0.18f);

                float diameter = Math.Min(Width - 1, Height - 1) * _scale;
                float x = (Width - diameter) / 2f;
                float y = (Height - diameter) / 2f;

                RectangleF circleRect = new RectangleF(x, y, diameter, diameter);

                using (var fill = new SolidBrush(baseColor))
                using (var border = new Pen(borderColor, 1f))
                {
                    e.Graphics.FillEllipse(fill, circleRect);
                    e.Graphics.DrawEllipse(border, circleRect);
                }

                if (_glyphAlpha > 0)
                {
                    using var pen = new Pen(Color.FromArgb(_glyphAlpha, 55, 55, 55), 1.55f)
                    {
                        StartCap = LineCap.Round,
                        EndCap = LineCap.Round
                    };

                    float pad = 4.2f;
                    float left = circleRect.Left + pad;
                    float right = circleRect.Right - pad;
                    float top = circleRect.Top + pad;
                    float bottom = circleRect.Bottom - pad;
                    float midY = circleRect.Top + circleRect.Height / 2f;

                    switch (_kind)
                    {
                        case MacTrafficLightKind.Close:
                            e.Graphics.DrawLine(pen, left, top, right, bottom);
                            e.Graphics.DrawLine(pen, right, top, left, bottom);
                            break;

                        case MacTrafficLightKind.Minimize:
                            e.Graphics.DrawLine(pen, left, midY, right, midY);
                            break;
                    }
                }
            }

            private Color GetBaseColor()
            {
                return _kind switch
                {
                    MacTrafficLightKind.Close => Color.FromArgb(255, 95, 86),
                    MacTrafficLightKind.Minimize => Color.FromArgb(255, 189, 46),
                    _ => Color.Silver
                };
            }
        }
    }
}