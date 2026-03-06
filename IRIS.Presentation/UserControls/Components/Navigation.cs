using Guna.UI2.WinForms;
using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
using IRIS.Presentation.DependencyInjection;
using IRIS.Presentation.Forms;
using IRIS.Presentation.UserControls.Components;
using IRIS.Presentation.UserControls.PagesUC;
using IRIS.Services.DTOs;
using IRIS.Services.Interfaces;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using Timer = System.Windows.Forms.Timer;

namespace IRIS.Presentation.UserControls
{
    public partial class NavigationPanel : UserControl
    {
        private const int COLLAPSED_WIDTH = 60;
        private const int EXPANDED_WIDTH = 220;
        private const int ANIM_INTERVAL = 10;

        private bool _isExpanded = false;
        private Timer _animTimer;
        private int _targetWidth;

        private IRequestService _requestService;
        private int _requestNotificationCount = 0;

        private readonly SolidBrush _badgeBrush = new SolidBrush(Color.FromArgb(0, 120, 215));
        private readonly SolidBrush _textBrush = new SolidBrush(Color.White);
        private readonly Font _badgeFont = new Font("Segoe UI", 8F, FontStyle.Bold);

        private readonly IReportsService _reportsService;
        private ReportsControl _reportsControl;
        private Task<ReportsDashboardDto>? _reportsPreloadTask;

        public NavigationPanel()
        {
            InitializeComponent();

            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime || DesignMode)
            {
                return;
            }

            _reportsService = ServiceFactory.GetReportsService();
            _reportsControl = new ReportsControl();

            if (btnRequests != null)
            {
                btnRequests.Paint += btnRequests_Paint;
            }

            DoubleBuffered = true;
            Width = COLLAPSED_WIDTH;

            SetupTimer();
            InitializeNavPanels();
            ApplyCollapsedState();

            PreloadReportsData();
        }

        public void InitializeService(IRequestService requestService)
        {
            _requestService = requestService;
            RefreshBadgeCount();
        }

        public void RefreshBadgeCount()
        {
            if (_requestService == null) return;

            bool isAuthorized = UserSession.CurrentUser != null &&
                               (UserSession.CurrentUser.Role == UserRole.Dean ||
                                UserSession.CurrentUser.Role == UserRole.AssistantDean);

            if (!isAuthorized)
            {
                _requestNotificationCount = 0;
                if (btnRequests != null) btnRequests.Invalidate();
                return;
            }

            try
            {
                _requestNotificationCount = _requestService.GetPendingRequestCount();
                if (btnRequests != null) btnRequests.Invalidate();
            }
            catch
            {
            }
        }

        public void RefreshReportsCache()
        {
            _reportsControl = new ReportsControl();
            _reportsPreloadTask = null;
            PreloadReportsData(true);
        }

        private void PreloadReportsData(bool force = false)
        {
            if (_reportsService == null)
                return;

            if (!force && _reportsPreloadTask != null && !_reportsPreloadTask.IsCanceled && !_reportsPreloadTask.IsFaulted)
            {
                _reportsControl.SetPreloadedTask(_reportsPreloadTask);
                return;
            }

            _reportsPreloadTask = _reportsService.GetDashboardDataAsync(5);
            _reportsControl.SetPreloadedTask(_reportsPreloadTask);
        }

        private void SetupTimer()
        {
            _animTimer = new Timer { Interval = ANIM_INTERVAL };
            _animTimer.Tick += AnimTimer_Tick;
        }

        private void InitializeNavPanels()
        {
            foreach (Control ctrl in Controls)
            {
                if (ctrl is Guna2Panel panel && panel.Name.StartsWith("pnl"))
                {
                    panel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    panel.Height = 50;

                    var btn = panel.Controls.OfType<Guna2Button>().FirstOrDefault();
                    if (btn != null)
                    {
                        btn.Dock = DockStyle.Fill;
                        btn.Animated = true;
                    }
                }
            }
        }

        private void btnHamburger_Click(object sender, EventArgs e)
        {
            BringToFront();
            _isExpanded = !_isExpanded;
            _targetWidth = _isExpanded ? EXPANDED_WIDTH : COLLAPSED_WIDTH;

            btnHamburger.Image = _isExpanded ? Properties.Resources.arrowleft : Properties.Resources.hamburger;

            if (_isExpanded) ApplyExpandedState();
            else ApplyCollapsedState();

            _animTimer.Start();
            Invalidate();
        }

        private void AnimTimer_Tick(object sender, EventArgs e)
        {
            int distance = Math.Abs(Width - _targetWidth);
            int step = Math.Max(2, distance / 4);

            if (Width != _targetWidth)
            {
                int newWidth = (Width < _targetWidth) ? Width + step : Width - step;
                if (Math.Abs(newWidth - _targetWidth) < step) newWidth = _targetWidth;
                Width = newWidth;

                if (btnRequests != null) btnRequests.Invalidate();

                Invalidate();
            }
            else
            {
                _animTimer.Stop();
                if (!_isExpanded)
                {
                    Control dimmer = Parent?.Controls.Find("pnlDimmer", false).FirstOrDefault();
                    if (dimmer != null) dimmer.Visible = false;
                }
            }
        }

        private void btnRequests_Paint(object sender, PaintEventArgs e)
        {
            bool isAuthorized = UserSession.CurrentUser != null &&
                    (UserSession.CurrentUser.Role == UserRole.Dean ||
                     UserSession.CurrentUser.Role == UserRole.AssistantDean);

            if (!isAuthorized || _requestNotificationCount <= 0) return;

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (!_isExpanded)
            {
                int dotSize = 10;
                int dotX = btnRequests.Width - 20;
                int dotY = 10;

                g.FillEllipse(_badgeBrush, dotX, dotY, dotSize, dotSize);
            }
            else
            {
                int circleSize = 22;
                int circleX = btnRequests.Width - circleSize - 15;
                int circleY = (btnRequests.Height - circleSize) / 2;

                g.FillEllipse(_badgeBrush, circleX, circleY, circleSize, circleSize);

                string countText = _requestNotificationCount > 99 ? "99+" : _requestNotificationCount.ToString();

                using (StringFormat format = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                })
                {
                    RectangleF textRect = new RectangleF(circleX, circleY, circleSize, circleSize);
                    g.DrawString(countText, _badgeFont, _textBrush, textRect, format);
                }
            }
        }

        private void ApplyCollapsedState()
        {
            SuspendLayout();

            foreach (var btn in GetAllNavButtons())
            {
                btn.Text = string.Empty;
                btn.ImageAlign = System.Windows.Forms.HorizontalAlignment.Center;
                btn.Padding = new Padding(0);
            }

            ResumeLayout();
        }

        private void ApplyExpandedState()
        {
            SuspendLayout();

            foreach (var btn in GetAllNavButtons())
            {
                string btnText = btn.Name.Replace("btn", "");
                btn.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
                btn.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
                btn.Padding = new Padding(15, 0, 0, 0);
                btn.TextOffset = new Point(15, 0);
                btn.Text = btnText;
            }

            ResumeLayout();
        }

        private Guna2Button[] GetAllNavButtons()
        {
            var buttons = Controls
                .OfType<Guna2Panel>()
                .Where(p => p.Name.StartsWith("pnl"))
                .SelectMany(p => p.Controls.OfType<Guna2Button>())
                .ToList();

            if (btnLogout != null) buttons.Add(btnLogout);

            return buttons.ToArray();
        }

        private void btnDashboard_Click(object sender, EventArgs e) 
        { 
            if (ParentForm is MainForm main)
            {
                main.LoadPage(new DashboardControl());
                if (_isExpanded) btnHamburger_Click(null, null);
            }
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            if (ParentForm is MainForm main)
            {
                main.LoadPage(new InventoryControl());
                if (_isExpanded) btnHamburger_Click(null, null);
            }
        }

        private void btnRequests_Click(object sender, EventArgs e)
        {
            if (ParentForm is MainForm main)
            {
                main.LoadPage(new RequestControl());
                if (_isExpanded) btnHamburger_Click(null, null);
            }
        }

        private void btnRestock_Click(object sender, EventArgs e)
        {
            if (ParentForm is MainForm main)
            {
                main.LoadPage(new RestockPage());
                if (_isExpanded) btnHamburger_Click(null, null);
            }
        }

        private async void btnReports_Click(object sender, EventArgs e)
        {
            if (ParentForm is MainForm main)
            {
                if (_reportsControl == null || _reportsControl.IsDisposed)
                {
                    _reportsControl = new ReportsControl();
                    _reportsPreloadTask = null;
                    PreloadReportsData(true);
                }

                main.LoadPage(_reportsControl);

                if (_isExpanded)
                    btnHamburger_Click(null, null);

                await _reportsControl.EnsureDataLoadedAsync();
            }
        }

        private void btnHistory_Click(object sender, EventArgs e) 
        {
            if (ParentForm is MainForm main)
            {
                main.LoadPage(new HistoryControl());
                if (_isExpanded) btnHamburger_Click(null, null);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                UserSession.CurrentUser = null;
                if (ParentForm != null)
                {
                    ParentForm.Close();
                }
            }
        }
    }
}