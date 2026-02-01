using System;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Timer = System.Windows.Forms.Timer;

namespace IRIS.Presentation.components.navigator
{
    public partial class NavigationPanel : UserControl
    {
        private const int COLLAPSED_WIDTH = 60;
        private const int EXPANDED_WIDTH = 220;

        private bool _isExpanded = false;
        private Timer _animTimer;
        private int _targetWidth;

        private const int ANIM_STEP = 10;
        private const int ANIM_INTERVAL = 12;

        public NavigationPanel()
        {
            InitializeComponent();

            // Start collapsed
            Width = COLLAPSED_WIDTH;

            // Hamburger button
            btnHamburger.Dock = DockStyle.Top;
            btnHamburger.Anchor = AnchorStyles.Top
                | AnchorStyles.Right;
            btnHamburger.Image = Properties.Resources.hamburger;

            // Bottom button (Logout) always at the bottom
            btnLogout.Dock = DockStyle.Bottom;
            btnLogout.Height = 50;

            // Initialize top/middle nav panels
            InitializeNavPanels();

            ApplyCollapsedState();
        }

        // ===============================
        // INITIALIZE ALL NAV PANELS (except bottom button)
        // ===============================
        private void InitializeNavPanels()
        {
            foreach (Control ctrl in Controls)
            {
                if (ctrl is Guna2Panel panel && panel.Name.StartsWith("pnl"))
                {
                    // Skip panel containing the bottom button
                    if (panel.Controls.OfType<Guna2Button>().Any(b => b == btnLogout))
                        continue;

                    panel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    panel.Height = 50;

                    var btn = panel.Controls.OfType<Guna2Button>().FirstOrDefault();
                    if (btn != null)
                    {
                        btn.Dock = DockStyle.Fill;
                    }
                }
            }
        }

        // ===============================
        // HAMBURGER TOGGLE
        // ===============================
        private void btnHamburger_Click(object sender, EventArgs e)
        {
            ToggleNavigation(!_isExpanded);
        }

        private void ToggleNavigation(bool expand)
        {
            _isExpanded = expand;
            _targetWidth = expand ? EXPANDED_WIDTH : COLLAPSED_WIDTH;

            btnHamburger.Image = expand
                ? Properties.Resources.arrowleft
                : Properties.Resources.hamburger;

            if (_animTimer == null)
            {
                _animTimer = new Timer { Interval = ANIM_INTERVAL };
                _animTimer.Tick += AnimTimer_Tick;
            }

            _animTimer.Start();
        }

        // ===============================
        // ANIMATION
        // ===============================
        private void AnimTimer_Tick(object sender, EventArgs e)
        {
            if (Width < _targetWidth)
            {
                Width += ANIM_STEP;

                if (Width >= EXPANDED_WIDTH)
                {
                    Width = EXPANDED_WIDTH;
                    ApplyExpandedState();
                    _animTimer.Stop();
                }
            }
            else if (Width > _targetWidth)
            {
                Width -= ANIM_STEP;

                if (Width <= COLLAPSED_WIDTH)
                {
                    Width = COLLAPSED_WIDTH;
                    ApplyCollapsedState();
                    _animTimer.Stop();
                }
            }
        }

        // ===============================
        // UI STATES
        // ===============================
        private void ApplyCollapsedState()
        {
            foreach (var btn in GetAllNavButtons())
            {
                btn.Text = "";
                btn.ImageAlign = HorizontalAlignment.Center;
                btn.TextAlign = HorizontalAlignment.Center;
                btn.Padding = Padding.Empty;
            }
        }

        private void ApplyExpandedState()
        {
            foreach (var btn in GetAllNavButtons())
            {
                // Automatically get text from button name (remove "btn" prefix)
                btn.Text = btn.Name.Replace("btn", "");
                btn.ImageAlign = HorizontalAlignment.Left;
                btn.TextAlign = HorizontalAlignment.Left;

                // Padding between icon and text
                btn.Padding = new Padding(12, 0, 0, 0);
            }
        }

        // ===============================
        // GET ALL NAV BUTTONS (including bottom button)
        // ===============================
        private Guna2Button[] GetAllNavButtons()
        {
            return Controls
                .OfType<Guna2Panel>()
                .Where(p => p.Name.StartsWith("pnl"))
                .SelectMany(p => p.Controls.OfType<Guna2Button>())
                .ToArray();
        }

        // ===============================
        // BUTTON EVENTS (NAVIGATION)
        // ===============================
        private void btnDashboard_Click(object sender, EventArgs e) { }
        private void btnInventory_Click(object sender, EventArgs e) { }
        private void btnRestock_Click(object sender, EventArgs e) { }
        private void btnRequests_Click(object sender, EventArgs e) { }
        private void btnReports_Click(object sender, EventArgs e) { }
        private void btnHistory_Click(object sender, EventArgs e) { }
        private void btnLogout_Click(object sender, EventArgs e) { }
    }
}
