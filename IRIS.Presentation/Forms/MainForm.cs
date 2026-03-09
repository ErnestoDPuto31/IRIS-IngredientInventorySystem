using Guna.UI2.WinForms;
using IRIS.Domain.Entities;
using IRIS.Presentation.UserControls;
using IRIS.Presentation.UserControls.Components;
using IRIS.Presentation.UserControls.PagesUC;
using IRIS.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Drawing.Drawing2D;
using System.Reflection;
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
        private MacTrafficLightButton _btnMacClose;
        private MacTrafficLightButton _btnMacMinimize;

        private readonly Color _topBarColor = Color.LightGray;

        private Timer _pageIntroTimer;
        private UserControl _animatingPage;
        private Rectangle _pageIntroStartBounds;
        private Rectangle _pageIntroEndBounds;
        private int _pageIntroTick;
        private bool _initialPageLoaded;

        private const int PageIntroOffsetY = 28;
        private const int PageIntroTotalTicks = 14;

        private const int FixedFormWidth = 1600;
        private const int FixedFormHeight = 900;
        private const int ShellGap = 16;
        private const int TopPanelHeight = 80;
        private const int TopToContentGap = 12;
        #endregion

        public MainForm()
        {
            InitializeComponent();

            ClientSize = new Size(FixedFormWidth, FixedFormHeight);
            MinimumSize = new Size(FixedFormWidth, FixedFormHeight);
            MaximumSize = new Size(FixedFormWidth, FixedFormHeight);
            StartPosition = FormStartPosition.CenterScreen;

            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw,
                true);

            DoubleBuffered = true;
            UpdateStyles();

            EnableDoubleBufferingRecursive(this);

            SetupClock();
            SetupMacTopBar();
            SetupNavigation();
            SetupNotifications();
            SetupUserDisplay();
            SetupPageIntroAnimation();
            ApplyShellLayout();

            Shown += MainForm_Shown;
        }

        #region Startup / Initial Load
        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (_initialPageLoaded) return;
            _initialPageLoaded = true;

            BeginInvoke(new Action(() =>
            {
                if (IsDisposed) return;
                LoadPage(new DashboardControl(), true);
            }));
        }

        private void SetupPageIntroAnimation()
        {
            _pageIntroTimer = new Timer { Interval = 15 };
            _pageIntroTimer.Tick += PageIntroTimer_Tick;
        }

        private void StartPageIntro(UserControl page)
        {
            if (page == null || pnlMainContent == null) return;

            Rectangle targetBounds = pnlMainContent.ClientRectangle;
            if (targetBounds.Width <= 0 || targetBounds.Height <= 0)
            {
                page.Dock = DockStyle.Fill;
                return;
            }

            _animatingPage = page;
            _pageIntroEndBounds = targetBounds;
            _pageIntroStartBounds = new Rectangle(
                targetBounds.X,
                targetBounds.Y + PageIntroOffsetY,
                targetBounds.Width,
                targetBounds.Height);

            page.Dock = DockStyle.None;
            page.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            page.Bounds = _pageIntroStartBounds;
            page.Visible = true;

            _pageIntroTick = 0;
            _pageIntroTimer.Stop();
            _pageIntroTimer.Start();
        }

        private void StopPageIntro(bool snapToEnd = true)
        {
            _pageIntroTimer?.Stop();

            if (_animatingPage != null && !_animatingPage.IsDisposed)
            {
                if (snapToEnd)
                    _animatingPage.Bounds = _pageIntroEndBounds;

                _animatingPage.Dock = DockStyle.Fill;
            }

            _animatingPage = null;
        }

        private void PageIntroTimer_Tick(object sender, EventArgs e)
        {
            if (_animatingPage == null || _animatingPage.IsDisposed)
            {
                StopPageIntro(false);
                return;
            }

            _pageIntroTick++;

            double t = Math.Min(1.0, _pageIntroTick / (double)PageIntroTotalTicks);
            double eased = EaseOutCubic(t);

            int y = _pageIntroStartBounds.Y - (int)Math.Round(PageIntroOffsetY * eased);

            _animatingPage.Bounds = new Rectangle(
                _pageIntroEndBounds.X,
                y,
                _pageIntroEndBounds.Width,
                _pageIntroEndBounds.Height);

            if (t >= 1.0)
                StopPageIntro(true);
        }

        private static double EaseOutCubic(double t)
        {
            return 1.0 - Math.Pow(1.0 - t, 3.0);
        }
        #endregion

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
            _navigationPanel = new NavigationPanel
            {
                Dock = DockStyle.Left
            };

            Controls.Add(_navigationPanel);
            _navigationPanel.BringToFront();

            var requestService = Program.Services.GetService<IRequestService>();
            if (requestService != null)
                _navigationPanel.InitializeService(requestService);

            _badgeTimer = new Timer { Interval = 3000 };
            _badgeTimer.Tick += (s, e) => _navigationPanel?.RefreshBadgeCount();
            _badgeTimer.Start();

            guna2Panel1?.BringToFront();
            ApplyShellLayout();
        }

        private void SetupNotifications()
        {
            _notificationService = Program.Services.GetService<INotificationService>();

            _dropdownPanel = new NotificationDropdown
            {
                Visible = false,
                Size = new Size(390, 430)
            };

            Controls.Add(_dropdownPanel);
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

        private void SetupMacTopBar()
        {
            if (guna2Panel1 == null) return;

            FormBorderStyle = FormBorderStyle.None;
            guna2Panel1.Dock = DockStyle.Top;
            guna2Panel1.Height = 42;
            guna2Panel1.FillColor = _topBarColor;
            guna2Panel1.BackColor = _topBarColor;
            guna2Panel1.BorderColor = Color.FromArgb(210, 210, 210);
            guna2Panel1.BorderThickness = 1;
            guna2Panel1.CustomBorderThickness = new Padding(0, 0, 0, 1);

            if (btnExit != null)
                btnExit.Visible = false;

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

            PositionMacButtons();
            WireDragToControl(guna2Panel1);
        }
        #endregion

        #region Layout
        private void ApplyShellLayout()
        {
            if (guna2Panel1 == null || pnlTop == null || pnlMainContent == null) return;

            int navWidth = 0;
            if (_navigationPanel != null && !_navigationPanel.IsDisposed && _navigationPanel.Visible)
                navWidth = _navigationPanel.Width;

            int left = navWidth + ShellGap;
            int top = guna2Panel1.Bottom + ShellGap;
            int availableWidth = ClientSize.Width - left - ShellGap;

            if (availableWidth < 200)
                availableWidth = 200;

            pnlTop.Location = new Point(left, top);
            pnlTop.Size = new Size(availableWidth, TopPanelHeight);

            int contentTop = pnlTop.Bottom + TopToContentGap;
            int contentHeight = ClientSize.Height - contentTop - ShellGap;

            if (contentHeight < 200)
                contentHeight = 200;

            pnlMainContent.Location = new Point(left, contentTop);
            pnlMainContent.Size = new Size(availableWidth, contentHeight);
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

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            ApplyShellLayout();
            PositionMacButtons();

            if (_dropdownPanel != null && _dropdownPanel.Visible)
                PositionDropdownUnderBadge();

            if (_animatingPage != null && !_animatingPage.IsDisposed && pnlMainContent != null)
                _pageIntroEndBounds = pnlMainContent.ClientRectangle;
        }
        #endregion

        #region Logic Methods
        public void LoadPage(UserControl newPage)
        {
            LoadPage(newPage, true);
        }

        public void LoadPage(UserControl newPage, bool animate)
        {
            if (newPage == null || pnlMainContent == null) return;

            StopPageIntro(false);

            pnlMainContent.SuspendLayout();

            try
            {
                EnableDoubleBufferingRecursive(newPage);

                pnlMainContent.Controls.Clear();

                if (!animate)
                {
                    newPage.Dock = DockStyle.Fill;
                    pnlMainContent.Controls.Add(newPage);
                    return;
                }

                newPage.Visible = false;
                newPage.Dock = DockStyle.None;
                newPage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                newPage.Bounds = pnlMainContent.ClientRectangle;

                pnlMainContent.Controls.Add(newPage);
                newPage.BringToFront();
            }
            finally
            {
                pnlMainContent.ResumeLayout(true);
            }

            if (animate)
                StartPageIntro(newPage);
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
            if (notificationBadge == null || notificationBadge.Parent == null || _dropdownPanel == null) return;

            Control anchor = notificationBadge.Parent;
            Point screenLoc = anchor.PointToScreen(new Point(anchor.Width - _dropdownPanel.Width, anchor.Height + 5));
            Point clientLoc = PointToClient(screenLoc);

            _dropdownPanel.Location = clientLoc;
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

                if (notificationBadge != null)
                    notificationBadge.Count = unread;

                if (_dropdownPanel != null && _dropdownPanel.Visible)
                {
                    _dropdownPanel.LoadNotifications(list);
                    PositionDropdownUnderBadge();
                }
            }
            catch
            {
            }
        }

        private void notificationBadge_Click(object sender, EventArgs e)
        {
            if (UserSession.CurrentUser == null || _notificationService == null || _dropdownPanel == null) return;

            if (_dropdownPanel.Visible)
            {
                _dropdownPanel.HideBubble();
                return;
            }

            var list = _notificationService.GetNotificationsForUser(UserSession.CurrentUser);
            var ids = list.Select(n => n.NotificationId).ToList();

            if (ids.Any())
                _notificationService.MarkNotificationsAsRead(ids);

            if (notificationBadge != null)
                notificationBadge.Count = 0;

            PositionDropdownUnderBadge();
            _dropdownPanel.LoadNotifications(list);
            _dropdownPanel.Visible = true;
            _dropdownPanel.BringToFront();
            _dropdownPanel.ShowBubble();
        }
        #endregion

        #region Dragging
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

        #region Rendering Helpers
        private static void EnableDoubleBufferingRecursive(Control root)
        {
            if (root == null) return;

            TryEnableDoubleBuffer(root);

            foreach (Control child in root.Controls)
                EnableDoubleBufferingRecursive(child);
        }

        private static void TryEnableDoubleBuffer(Control control)
        {
            if (control == null) return;

            try
            {
                typeof(Control)
                    .GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.SetValue(control, true, null);
            }
            catch
            {
            }

            try
            {
                typeof(Control)
                    .GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.Invoke(control, new object[]
                    {
                        ControlStyles.AllPaintingInWmPaint |
                        ControlStyles.UserPaint |
                        ControlStyles.OptimizedDoubleBuffer |
                        ControlStyles.ResizeRedraw,
                        true
                    });

                typeof(Control)
                    .GetMethod("UpdateStyles", BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.Invoke(control, null);
            }
            catch
            {
            }
        }
        #endregion

        #region Mac Buttons
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

                SetStyle(
                    ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.ResizeRedraw |
                    ControlStyles.UserPaint,
                    true);

                _hoverTimer = new Timer { Interval = 15 };
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
                    alphaDelta = _targetGlyphAlpha > _glyphAlpha ? 1 : -1;

                _glyphAlpha += alphaDelta;

                bool scaleDone = Math.Abs(_targetScale - _scale) < 0.01f;
                bool alphaDone = Math.Abs(_targetGlyphAlpha - _glyphAlpha) <= 1;

                if (scaleDone) _scale = _targetScale;
                if (alphaDone) _glyphAlpha = _targetGlyphAlpha;

                Invalidate();

                if (scaleDone && alphaDone)
                    _hoverTimer.Stop();
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
        #endregion
    }
}