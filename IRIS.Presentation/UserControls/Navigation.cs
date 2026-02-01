using Guna.UI2.WinForms;
using IRIS.Domain.Entities;
using IRIS.Presentation.Forms;
using System.Data;
using Timer = System.Windows.Forms.Timer;

namespace IRIS.Presentation.UserControls
{
    public partial class NavigationPanel : UserControl
    {
        private const int COLLAPSED_WIDTH = 60;
        private const int EXPANDED_WIDTH = 220;
        private const int ANIM_INTERVAL = 10;
        private const int MAX_DIM_ALPHA = 130; // 0-255: How dark the background gets

        private bool _isExpanded = false;
        private Timer _animTimer;
        private int _targetWidth;

        public NavigationPanel()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.Width = COLLAPSED_WIDTH;

            SetupTimer();
            InitializeNavPanels();
            ApplyCollapsedState();
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

            // Sync the Dimmer state
            Control dimmer = this.Parent?.Controls.Find("pnlDimmer", false).FirstOrDefault();
            if (_isExpanded && dimmer != null)
            {
                dimmer.Visible = true;
                dimmer.BringToFront();
                this.BringToFront(); // Ensure sidebar stays on top of the dim layer
            }

            if (_isExpanded) ApplyExpandedState();
            else ApplyCollapsedState();

            _animTimer.Start();
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

                Control dimmer = this.Parent?.Controls.Find("pnlDimmer", false).FirstOrDefault();
                if (dimmer != null)
                {
                    float ratio = (float)(this.Width - COLLAPSED_WIDTH) / (EXPANDED_WIDTH - COLLAPSED_WIDTH);
                    int alpha = (int)(ratio * MAX_DIM_ALPHA);

                    dimmer.BackColor = Color.FromArgb(alpha, 0, 0, 0);
                }
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

        private void ApplyCollapsedState()
        {
            this.SuspendLayout();
            foreach (var btn in GetAllNavButtons())
            {
                btn.Text = string.Empty;
                btn.ImageAlign = HorizontalAlignment.Center;
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
                btn.ImageAlign = HorizontalAlignment.Left;
                btn.TextAlign = HorizontalAlignment.Left;
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
        private void btnDashboard_Click(object sender, EventArgs e) 
        {
        }
        private void btnInventory_Click(object sender, EventArgs e) 
        {
            if (this.ParentForm is MainForm main)
            {
                main.LoadPage(new InventoryControl());
                if (_isExpanded) btnHamburger_Click(null, null);
            }
        }
        private void btnRestock_Click(object sender, EventArgs e) { }
        private void btnRequests_Click(object sender, EventArgs e) { }
        private void btnReports_Click(object sender, EventArgs e) { }
        private void btnHistory_Click(object sender, EventArgs e) { }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Logout",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                /* * DATABASE HOOK: 
                 * 1. Update UserSession log in DB: userSession.LogoutTime = DateTime.Now;
                 * 2. _context.SaveChanges();
                 */

                // Clear the session so the next user has a clean slate
                UserSession.CurrentUser = null;

                if (this.ParentForm != null)
                {
                    this.ParentForm.Close();
                }
            }
        }
    }
}