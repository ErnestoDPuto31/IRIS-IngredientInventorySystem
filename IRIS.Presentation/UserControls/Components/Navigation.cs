using Guna.UI2.WinForms;
using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
using IRIS.Presentation.Forms;
using IRIS.Presentation.UserControls.Components;
using IRIS.Presentation.UserControls.PagesUC;
using IRIS.Services.Interfaces;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
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

        // --- SERVICE & NOTIFICATION STATE ---
        private IRequestService _requestService;
        private int _requestNotificationCount = 0;

        // --- NOTIFICATION BADGE TOOLS ---
        private readonly SolidBrush _badgeBrush = new SolidBrush(Color.FromArgb(0, 120, 215));
        private readonly SolidBrush _textBrush = new SolidBrush(Color.White);
        private readonly Font _badgeFont = new Font("Segoe UI", 8F, FontStyle.Bold);

        public NavigationPanel()
        {
            InitializeComponent();

            // Attach the paint event specifically to the button to avoid layering issues
            if (btnRequests != null)
            {
                btnRequests.Paint += btnRequests_Paint;
            }

            this.DoubleBuffered = true;
            this.Width = COLLAPSED_WIDTH;

            SetupTimer();
            InitializeNavPanels();
            ApplyCollapsedState();
        }

        // ---> Call this from MainForm to connect the service safely!
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
            catch { }
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
            this.BringToFront();
            _isExpanded = !_isExpanded;
            _targetWidth = _isExpanded ? EXPANDED_WIDTH : COLLAPSED_WIDTH;

            btnHamburger.Image = _isExpanded ? Properties.Resources.arrowleft : Properties.Resources.hamburger;

            if (_isExpanded) ApplyExpandedState();
            else ApplyCollapsedState();

            _animTimer.Start();
            this.Invalidate();
        }

        private void AnimTimer_Tick(object sender, EventArgs e)
        {
            int distance = Math.Abs(Width - _targetWidth);
            int step = Math.Max(2, distance / 4);

            if (Width != _targetWidth)
            {
                int newWidth = (Width < _targetWidth) ? Width + step : Width - step;
                if (Math.Abs(newWidth - _targetWidth) < step) newWidth = _targetWidth;
                this.Width = newWidth;

                // Keep the button badge redrawing during the slide
                if (btnRequests != null) btnRequests.Invalidate();

                this.Invalidate();
            }
            else
            {
                _animTimer.Stop();
                if (!_isExpanded)
                {
                    Control dimmer = this.Parent?.Controls.Find("pnlDimmer", false).FirstOrDefault();
                    if (dimmer != null) dimmer.Visible = false;
                }
            }
        }

        // --- NEW PAINT EVENT HANDLER (Draws ON TOP of the button) ---
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
                // COLLAPSED: Small dot on top right of the button icon
                int dotSize = 10;
                int dotX = btnRequests.Width - 20;
                int dotY = 10;

                g.FillEllipse(_badgeBrush, dotX, dotY, dotSize, dotSize);
            }
            else
            {
                // EXPANDED: Circle with number on the right side
                int circleSize = 22;
                int circleX = btnRequests.Width - circleSize - 15;
                int circleY = (btnRequests.Height - circleSize) / 2;

                g.FillEllipse(_badgeBrush, circleX, circleY, circleSize, circleSize);

                string countText = _requestNotificationCount > 99 ? "99+" : _requestNotificationCount.ToString();

                using (StringFormat format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                {
                    RectangleF textRect = new RectangleF(circleX, circleY, circleSize, circleSize);
                    g.DrawString(countText, _badgeFont, _textBrush, textRect, format);
                }
            }
        }

        private void ApplyCollapsedState()
        {
            this.SuspendLayout();
            foreach (var btn in GetAllNavButtons())
            {
                btn.Text = string.Empty;
                btn.ImageAlign = System.Windows.Forms.HorizontalAlignment.Center;
                btn.Padding = new Padding(0);
            }
            this.ResumeLayout();
        }

        private void ApplyExpandedState()
        {
            this.SuspendLayout();
            foreach (var btn in GetAllNavButtons())
            {
                string btnText = btn.Name.Replace("btn", "");
                btn.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
                btn.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
                btn.Padding = new Padding(15, 0, 0, 0);
                btn.TextOffset = new Point(15, 0);
                btn.Text = btnText;
            }
            this.ResumeLayout();
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

        // --- Event Handlers ---
        private void btnDashboard_Click(object sender, EventArgs e) { }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            if (this.ParentForm is MainForm main)
            {
                main.LoadPage(new InventoryControl());
                if (_isExpanded) btnHamburger_Click(null, null);
            }
        }

        private void btnRequests_Click(object sender, EventArgs e)
        {
            if (this.ParentForm is MainForm main)
            {
                main.LoadPage(new RequestControl());
                if (_isExpanded) btnHamburger_Click(null, null);
            }
        }

        private void btnRestock_Click(object sender, EventArgs e)
        {
            if (this.ParentForm is MainForm main)
            {
                main.LoadPage(new RestockPage());
                if (_isExpanded) btnHamburger_Click(null, null);
            }
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            if (this.ParentForm is MainForm main)
            {
                main.LoadPage(new ReportsControl());
                if (_isExpanded) btnHamburger_Click(null, null);
            }
        }

        private void btnHistory_Click(object sender, EventArgs e) { }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                UserSession.CurrentUser = null;
                if (this.ParentForm != null)
                {
                    this.ParentForm.Close();
                }
            }
        }
    }
}