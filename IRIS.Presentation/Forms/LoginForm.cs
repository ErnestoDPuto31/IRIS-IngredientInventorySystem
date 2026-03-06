using Guna.UI2.WinForms;
using IRIS.Domain.Entities;
using IRIS.Infrastructure.Data;
using IRIS.Presentation.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using Timer = System.Windows.Forms.Timer;

namespace IRIS.Presentation
{
    public partial class LoginForm : Form
    {
        private readonly IrisDbContext _context;
        private Panel _macTrafficHost;
        private Label _macTitleLabel;
        private MacTrafficLightButton _btnMacClose;
        private MacTrafficLightButton _btnMacMinimize;
        private readonly Color _topBarColor = Color.FromArgb(246, 246, 247);

        public LoginForm()
        {
            InitializeComponent();

            this.AcceptButton = btnLogin;

            var options = new DbContextOptionsBuilder<IrisDbContext>()
                .UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;
                        Database=IRIS_DB;
                        Trusted_Connection=True;")
                .Options;

            _context = new IrisDbContext(options);
            SeedData.Initialize(_context);

            // Initialize the mac-style top bar
            SetupMacTopBar();
        }

        // Method to set up the mac-style top bar
        private void SetupMacTopBar()
        {
            if (guna2Panel1 == null) return;

            FormBorderStyle = FormBorderStyle.None;

            // Set up top bar appearance
            guna2Panel1.Dock = DockStyle.Top;
            guna2Panel1.Height = 42;
            guna2Panel1.FillColor = _topBarColor;
            guna2Panel1.BackColor = _topBarColor;
            guna2Panel1.BorderColor = Color.FromArgb(223, 224, 228);
            guna2Panel1.BorderThickness = 1;
            guna2Panel1.CustomBorderThickness = new Padding(0, 0, 0, 1);
            guna2Panel1.ShadowDecoration.Enabled = false;
            guna2Panel1.Padding = new Padding(14, 8, 14, 8);
            Controls.Add(guna2Panel1); // Add the top bar panel to the form

            // Create the traffic buttons (minimize, close)
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

            // Title Label
            if (_macTitleLabel == null)
            {
                _macTitleLabel = new Label
                {
                    AutoSize = false,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = "IRIS",  // Your app name
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

        // Event for login button click
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();

                var user = _context.Users
                    .FirstOrDefault(u => u.Username.ToLower() == username.ToLower() && u.IsActive);

                // Validate user existence and active status
                if (user == null)
                {
                    ShowError("User does not exist or is inactive.");
                    return;
                }

                // Validate password using ASP.NET Core Hasher
                var hasher = new PasswordHasher<User>();
                var passwordResult = hasher.VerifyHashedPassword(user, user.PasswordHash, password);

                if (passwordResult == PasswordVerificationResult.Failed)
                {
                    ShowError("Incorrect password.");
                    return;
                }

                if (!string.IsNullOrEmpty(user.SessionToken))
                {
                    var overrideResult = MessageBox.Show(
                        "This account is currently active on another device, or the app closed unexpectedly during your last session.\n\nWould you like to force logout the other session and log in here?",
                        "Account in Use",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (overrideResult == DialogResult.No)
                    {
                        return;
                    }
                }

                // Prevent multiple logins on different devices
                if (user.IsLoggedIn)
                {
                    ShowError("This user is already logged in on another device.");
                    return;
                }

                if (user.isFirstLogin)
                {
                    using (var changePasswordForm = new IRIS.Presentation.Forms.ChangePasswordForm(user, _context))
                    {
                        var result = changePasswordForm.ShowDialog(this);

                        if (result != DialogResult.OK)
                        {
                            return;
                        }
                    }
                }

                // SUCCESSFUL LOGIN
                string newToken = Guid.NewGuid().ToString();

                user.SessionToken = newToken;
                _context.SaveChanges();

                UserSession.CurrentUser = user;
                UserSession.CurrentSessionToken = newToken;

                this.Hide();

                using (MainForm mainApp = new MainForm())
                {
                    mainApp.ShowDialog();
                }


                var loggedOutUser = _context.Users.Find(user.UserId);
                if (loggedOutUser != null)
                {
                    if (loggedOutUser.SessionToken == UserSession.CurrentSessionToken)
                    {
                        loggedOutUser.SessionToken = null;
                        _context.SaveChanges();
                    }
                }

                txtPassword.Clear();
                lblError.Visible = false;
                UserSession.CurrentUser = null;
                UserSession.CurrentSessionToken = null;
                this.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during login: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Show error message if login fails
        private void ShowError(string message)
        {
            lblError.Visible = true;
            lblError.Text = message;
        }

        // Exit button click logic
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}