using Guna.UI2.WinForms;
using IRIS.Domain.Entities;
using IRIS.Infrastructure.Data;
using IRIS.Presentation.Forms;
using IRIS.Presentation.UserControls.Components; 
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Configuration;
using IRIS.Presentation.Helpers;

namespace IRIS.Presentation
{
    public partial class LoginForm : Form
    {
        #region Declared Fields
        private readonly IrisDbContext _context;
        private Panel _macTrafficHost;
        private MacTrafficLightButton _btnMacClose;
        private MacTrafficLightButton _btnMacMinimize;
        private Guna2Panel _topBarPanel;

        private readonly Color _topBarColor = Color.LightGray;

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        #endregion

        #region Constructor
        public LoginForm()
        {
            InitializeComponent();

            FontManager.ApplyFont(this);
            AcceptButton = btnLogin;

            string connectionString = ConfigurationManager.ConnectionStrings["IrisConnection"].ConnectionString;

            var options = new DbContextOptionsBuilder<IrisDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            _context = new IrisDbContext(options);

            Load += LoginForm_Load;
            Resize += LoginForm_Resize;

            SetupMacTopBar();
        }
        #endregion

        #region Load / Resize
        private void LoginForm_Load(object sender, EventArgs e)
        {
            ApplyRuntimeStyles();
        }

        private void LoginForm_Resize(object sender, EventArgs e)
        {
            if (_topBarPanel != null)
                PositionMacButtons(_topBarPanel);
        }
        #endregion

        #region Runtime Styling
        private void ApplyRuntimeStyles()
        {
            btnLogin.Animated = false;
            txtUsername.Animated = false;
            txtPassword.Animated = false;

            btnLogin.Cursor = Cursors.Hand;

            txtUsername.HoverState.BorderColor = Color.FromArgb(160, 95, 225);
            txtPassword.HoverState.BorderColor = Color.FromArgb(160, 95, 225);

            chkShowPassword.BackColor = Color.Transparent;
            chkShowPassword.Cursor = Cursors.Hand;
            chkShowPassword.TabStop = false;
            chkShowPassword.AutoSize = true;

        }
        #endregion

        #region Checkbox
        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
            btnLogin.Focus();
        }
        #endregion

        #region Mac Top Bar
        private void SetupMacTopBar()
        {
            FormBorderStyle = FormBorderStyle.None;

            if (_topBarPanel == null)
            {
                _topBarPanel = new Guna2Panel
                {
                    Dock = DockStyle.Top,
                    Height = 42,
                    FillColor = _topBarColor,
                    BackColor = _topBarColor,
                    BorderColor = Color.FromArgb(218, 218, 218),
                    BorderThickness = 1,
                    CustomBorderThickness = new Padding(0, 0, 0, 1),
                    Padding = new Padding(14, 8, 14, 8)
                };
                Controls.Add(_topBarPanel);
                _topBarPanel.BringToFront();
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
                _topBarPanel.Controls.Add(_macTrafficHost);
            }

            PositionMacButtons(_topBarPanel);
            WireDragToControl(_topBarPanel);
        }

        private void PositionMacButtons(Guna2Panel topBarPanel)
        {
            if (topBarPanel == null || _macTrafficHost == null) return;

            int rightPadding = 14;
            int y = (topBarPanel.Height - _macTrafficHost.Height) / 2;
            int x = topBarPanel.Width - rightPadding - _macTrafficHost.Width;

            _macTrafficHost.Location = new Point(x, y);
            _macTrafficHost.BringToFront();
        }

        private void WireDragToControl(Control control)
        {
            if (control == null) return;
            control.MouseDown += DragSurface_MouseDown;

            foreach (Control child in control.Controls)
            {
                if (child is MacTrafficLightButton) continue;
                WireDragToControl(child);
            }
        }

        private void DragSurface_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }
        #endregion

        #region Login Logic
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();

                var user = _context.Users
                    .FirstOrDefault(u => u.Username.ToLower() == username.ToLower() && u.IsActive);

                if (user == null)
                {
                    ShowError("User does not exist or is inactive.");
                    return;
                }

                var hasher = new PasswordHasher<User>();
                if (hasher.VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Failed)
                {
                    ShowError("Incorrect password.");
                    return;
                }
                if (user.isFirstLogin)
                {
                    using (var changePwdForm = new ChangePasswordForm(user, _context))
                    {
                        this.Hide();
                        var result = changePwdForm.ShowDialog();

                        if (result != DialogResult.OK)
                        {
                            this.Show();
                            return;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(user.SessionToken))
                {
                    if (MessageBox.Show("Account in use. Force logout other session?", "Notice", MessageBoxButtons.YesNo) == DialogResult.No)
                        return;
                }

                string newToken = Guid.NewGuid().ToString();
                user.SessionToken = newToken;
                _context.SaveChanges();

                UserSession.CurrentUser = user;
                UserSession.CurrentSessionToken = newToken;

                this.Hide();
                using (MainForm mainApp = new MainForm()) { mainApp.ShowDialog(); }

                user.SessionToken = null;
                _context.SaveChanges();
                txtPassword.Clear();
                this.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void ShowError(string message)
        {
            lblError.Visible = true;
            lblError.Text = message;
        }
        #endregion

        private void btnForgotPassword_Click(object sender, EventArgs e)
        {
            using (var forgotForm = new ForgotPasswordForm(_context))
            {
                this.Hide();
                forgotForm.ShowDialog();
                this.Show();
            }
        }

        private void LoginForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}