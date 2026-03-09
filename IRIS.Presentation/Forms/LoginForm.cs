using Guna.UI2.WinForms;
using IRIS.Domain.Entities;
using IRIS.Infrastructure.Data;
using IRIS.Presentation.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

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
        private AnimatedHeroPanel _heroPanel;

        private readonly Color _topBarColor = Color.LightGray;

        private const int AW_BLEND = 0x00080000;
        private const int AW_ACTIVATE = 0x00020000;

        private readonly Timer loginButtonHoverTimer = new Timer();
        private readonly Timer _introTimer = new Timer();

        private double _currentButtonScale = 1.0;
        private double _targetButtonScale = 1.0;
        private bool _buttonHovering = false;
        private bool _buttonPressed = false;
        private Rectangle _btnLoginBaseBounds;

        private Point _panel2BaseLocation;
        private Point _panel1BaseLocation;
        private int _introTick = 0;
        private bool _introPlayed = false;

        [DllImport("user32.dll")]
        private static extern bool AnimateWindow(IntPtr hWnd, int dwTime, int dwFlags);
        #endregion

        #region Constructor
        public LoginForm()
        {
            InitializeComponent();

            AcceptButton = btnLogin;

            var options = new DbContextOptionsBuilder<IrisDbContext>()
                .UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;
                        Database=IRIS_DB;
                        Trusted_Connection=True;")
                .Options;

            _context = new IrisDbContext(options);

            Load += LoginForm_Load;
            Resize += LoginForm_Resize;

            btnLogin.MouseEnter += btnLogin_MouseEnter;
            btnLogin.MouseLeave += btnLogin_MouseLeave;
            btnLogin.MouseDown += btnLogin_MouseDown;
            btnLogin.MouseUp += btnLogin_MouseUp;

            loginButtonHoverTimer.Interval = 12;
            loginButtonHoverTimer.Tick += LoginButtonHoverTimer_Tick;

            _introTimer.Interval = 15;
            _introTimer.Tick += IntroTimer_Tick;

            SetupMacTopBar();
        }
        #endregion

        #region Load / Show / Resize
        private void LoginForm_Load(object sender, EventArgs e)
        {
            ApplyRuntimeStyles();
            SetupRuntimeHeroPanel();

            _btnLoginBaseBounds = btnLogin.Bounds;
            _panel2BaseLocation = guna2Panel2.Location;
            _panel1BaseLocation = guna2Panel1.Location;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            AnimateWindow(Handle, 180, AW_BLEND | AW_ACTIVATE);

            if (!_introPlayed)
            {
                _introPlayed = true;

                _panel2BaseLocation = guna2Panel2.Location;
                _panel1BaseLocation = guna2Panel1.Location;

                guna2Panel2.Location = new Point(_panel2BaseLocation.X - 24, _panel2BaseLocation.Y);
                guna2Panel1.Location = new Point(_panel1BaseLocation.X + 24, _panel1BaseLocation.Y);

                _introTick = 0;
                _introTimer.Start();
            }
        }

        private void LoginForm_Resize(object sender, EventArgs e)
        {
            if (_topBarPanel != null)
                PositionMacButtons(_topBarPanel);

            if (!_buttonHovering && !_buttonPressed)
                _btnLoginBaseBounds = btnLogin.Bounds;
        }

        private void IntroTimer_Tick(object sender, EventArgs e)
        {
            _introTick++;

            const int totalTicks = 16;
            double t = Math.Min(1.0, _introTick / (double)totalTicks);
            double eased = EaseOutCubic(t);

            guna2Panel2.Location = new Point(
                _panel2BaseLocation.X - (int)Math.Round((1.0 - eased) * 24),
                _panel2BaseLocation.Y);

            guna2Panel1.Location = new Point(
                _panel1BaseLocation.X + (int)Math.Round((1.0 - eased) * 24),
                _panel1BaseLocation.Y);

            if (t >= 1.0)
            {
                guna2Panel2.Location = _panel2BaseLocation;
                guna2Panel1.Location = _panel1BaseLocation;
                _introTimer.Stop();
            }
        }
        #endregion

        #region Runtime Styling
        private void ApplyRuntimeStyles()
        {
            btnLogin.Animated = true;
            txtUsername.Animated = true;
            txtPassword.Animated = true;

            btnLogin.Image = null;
            btnLogin.TextOffset = Point.Empty;
            btnLogin.Cursor = Cursors.Hand;

            txtUsername.HoverState.BorderColor = Color.FromArgb(160, 95, 225);
            txtPassword.HoverState.BorderColor = Color.FromArgb(160, 95, 225);

            chkShowPassword.Animated = true;
            chkShowPassword.BackColor = Color.Transparent;
            chkShowPassword.Cursor = Cursors.Hand;
            chkShowPassword.TabStop = false;
            chkShowPassword.AutoSize = true;

            chkShowPassword.CheckedState.BorderColor = Color.FromArgb(137, 65, 208);
            chkShowPassword.CheckedState.BorderRadius = 6;
            chkShowPassword.CheckedState.BorderThickness = 1;
            chkShowPassword.CheckedState.FillColor = Color.White;
            chkShowPassword.CheckMarkColor = Color.FromArgb(137, 65, 208);

            chkShowPassword.UncheckedState.BorderColor = Color.FromArgb(215, 215, 225);
            chkShowPassword.UncheckedState.BorderRadius = 6;
            chkShowPassword.UncheckedState.BorderThickness = 1;
            chkShowPassword.UncheckedState.FillColor = Color.White;

            guna2Panel1.FillColor = Color.Transparent;
            guna2Panel1.BackColor = Color.Transparent;
        }

        private void SetupRuntimeHeroPanel()
        {
            if (_heroPanel != null) return;

            _heroPanel = new AnimatedHeroPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Margin = Padding.Empty
            };

            guna2Panel1.Controls.Add(_heroPanel);
            _heroPanel.BringToFront();
        }
        #endregion

        #region Login Button Hover Animation
        private void btnLogin_MouseEnter(object sender, EventArgs e)
        {
            _buttonHovering = true;
            _buttonPressed = false;
            _targetButtonScale = 1.03;
            loginButtonHoverTimer.Start();
        }

        private void btnLogin_MouseLeave(object sender, EventArgs e)
        {
            _buttonHovering = false;
            _buttonPressed = false;
            _targetButtonScale = 1.0;
            loginButtonHoverTimer.Start();
        }

        private void btnLogin_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            _buttonPressed = true;
            _targetButtonScale = 0.985;
            loginButtonHoverTimer.Start();
        }

        private void btnLogin_MouseUp(object sender, MouseEventArgs e)
        {
            _buttonPressed = false;
            _targetButtonScale = _buttonHovering ? 1.03 : 1.0;
            loginButtonHoverTimer.Start();
        }

        private void LoginButtonHoverTimer_Tick(object sender, EventArgs e)
        {
            _currentButtonScale = AnimateTowardsDouble(_currentButtonScale, _targetButtonScale, 0.16);
            ApplyButtonScale();

            bool done = Math.Abs(_currentButtonScale - _targetButtonScale) < 0.0015;
            if (done)
            {
                _currentButtonScale = _targetButtonScale;
                loginButtonHoverTimer.Stop();
            }
        }

        private void ApplyButtonScale()
        {
            int baseW = _btnLoginBaseBounds.Width;
            int baseH = _btnLoginBaseBounds.Height;

            int newW = (int)Math.Round(baseW * _currentButtonScale);
            int newH = (int)Math.Round(baseH * _currentButtonScale);

            int newX = _btnLoginBaseBounds.X - ((newW - baseW) / 2);
            int newY = _btnLoginBaseBounds.Y - ((newH - baseH) / 2);

            btnLogin.SetBounds(newX, newY, newW, newH);
        }

        private double AnimateTowardsDouble(double current, double target, double easing)
        {
            double delta = target - current;

            if (Math.Abs(delta) <= 0.0001)
                return target;

            return current + (delta * easing);
        }

        private static double EaseOutCubic(double t)
        {
            return 1.0 - Math.Pow(1.0 - t, 3.0);
        }
        #endregion

        #region Checkbox
        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;

            BeginInvoke(new Action(() =>
            {
                btnLogin.Focus();
            }));
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

                _topBarPanel.ShadowDecoration.Enabled = false;

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
                _macTrafficHost.BringToFront();
            }

            PositionMacButtons(_topBarPanel);
            WireDragToControl(_topBarPanel);
        }

        private void PositionMacButtons(Guna2Panel topBarPanel)
        {
            if (topBarPanel == null || _macTrafficHost == null) return;

            int rightPadding = 14;
            int y = Math.Max(8, (topBarPanel.Height - _macTrafficHost.Height) / 2);
            int x = topBarPanel.Width - rightPadding - _macTrafficHost.Width;

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

        #region Animated Hero Panel
        private sealed class AnimatedHeroPanel : Control
        {
            private readonly Timer _animationTimer;
            private readonly List<FloatingParticle> _particles = new();
            private readonly List<FloatingBlob> _blobs = new();
            private readonly Random _random = new Random();

            private float _time;

            public AnimatedHeroPanel()
            {
                SetStyle(ControlStyles.AllPaintingInWmPaint |
                         ControlStyles.OptimizedDoubleBuffer |
                         ControlStyles.ResizeRedraw |
                         ControlStyles.UserPaint |
                         ControlStyles.SupportsTransparentBackColor, true);

                BackColor = Color.Transparent;

                _animationTimer = new Timer { Interval = 30 };
                _animationTimer.Tick += AnimationTimer_Tick;

                CreateScene();
                _animationTimer.Start();
            }

            private void AnimationTimer_Tick(object sender, EventArgs e)
            {
                _time += 0.015f;

                for (int i = 0; i < _particles.Count; i++)
                {
                    var p = _particles[i];
                    p.Y += p.Speed;
                    p.X += (float)Math.Sin(_time + p.Phase) * 0.25f;

                    if (p.Y > Height + 20)
                    {
                        p.Y = -20;
                        p.X = _random.Next(20, Math.Max(21, Width - 20));
                    }

                    _particles[i] = p;
                }

                Invalidate();
            }

            protected override void OnResize(EventArgs e)
            {
                base.OnResize(e);
                CreateScene();
            }

            private void CreateScene()
            {
                _particles.Clear();
                _blobs.Clear();

                if (Width <= 0 || Height <= 0) return;

                _blobs.Add(new FloatingBlob
                {
                    Bounds = new RectangleF(-50, Height * 0.35f, Width * 0.72f, Height * 0.60f),
                    ColorA = Color.FromArgb(170, 122, 68, 255),
                    ColorB = Color.FromArgb(145, 240, 199, 133),
                    Speed = 0.45f,
                    Phase = 0.2f
                });

                _blobs.Add(new FloatingBlob
                {
                    Bounds = new RectangleF(Width * 0.46f, -20, Width * 0.52f, Height * 0.42f),
                    ColorA = Color.FromArgb(165, 255, 212, 152),
                    ColorB = Color.FromArgb(130, 192, 132, 255),
                    Speed = 0.35f,
                    Phase = 1.4f
                });

                _blobs.Add(new FloatingBlob
                {
                    Bounds = new RectangleF(Width * 0.20f, Height * 0.05f, Width * 0.55f, Height * 0.48f),
                    ColorA = Color.FromArgb(115, 151, 88, 255),
                    ColorB = Color.FromArgb(90, 250, 220, 180),
                    Speed = 0.28f,
                    Phase = 2.1f
                });

                for (int i = 0; i < 14; i++)
                {
                    _particles.Add(new FloatingParticle
                    {
                        X = _random.Next(20, Math.Max(21, Width - 20)),
                        Y = _random.Next(0, Math.Max(1, Height)),
                        Size = _random.Next(3, 8),
                        Speed = (float)(_random.NextDouble() * 2.0 + 1.2),
                        Phase = (float)(_random.NextDouble() * Math.PI * 2)
                    });
                }
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                e.Graphics.CompositingQuality = CompositingQuality.HighQuality;

                using GraphicsPath rounded = GetRoundedPath(ClientRectangle, 40);
                e.Graphics.SetClip(rounded);

                using (LinearGradientBrush bg = new LinearGradientBrush(
                    ClientRectangle,
                    Color.FromArgb(245, 223, 191),
                    Color.FromArgb(151, 93, 255),
                    45f))
                {
                    e.Graphics.FillRectangle(bg, ClientRectangle);
                }

                foreach (var blob in _blobs)
                {
                    float offsetX = (float)Math.Sin(_time * blob.Speed + blob.Phase) * 12f;
                    float offsetY = (float)Math.Cos(_time * blob.Speed + blob.Phase) * 10f;

                    RectangleF rect = new RectangleF(
                        blob.Bounds.X + offsetX,
                        blob.Bounds.Y + offsetY,
                        blob.Bounds.Width,
                        blob.Bounds.Height);

                    using GraphicsPath path = CreateSoftBlobPath(rect);
                    using PathGradientBrush brush = new PathGradientBrush(path)
                    {
                        CenterColor = blob.ColorA,
                        SurroundColors = new[] { blob.ColorB }
                    };
                    e.Graphics.FillPath(brush, path);
                }

                foreach (var p in _particles)
                {
                    using SolidBrush particleBrush = new SolidBrush(Color.FromArgb(70, 255, 255, 255));
                    e.Graphics.FillEllipse(particleBrush, p.X, p.Y, p.Size, p.Size);
                }

                using SolidBrush textBrush = new SolidBrush(Color.FromArgb(51, 0, 87));
                using Font titleFont = new Font("Poppins", 30f, FontStyle.Bold, GraphicsUnit.Point);

                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Near
                };

                RectangleF textRect = new RectangleF(34, Height * 0.38f, Width - 60, 180);
                e.Graphics.DrawString("Welcome\nBack!", titleFont, textBrush, textRect, sf);

                e.Graphics.ResetClip();
            }

            private static GraphicsPath GetRoundedPath(Rectangle bounds, int radius)
            {
                int diameter = radius * 2;
                GraphicsPath path = new GraphicsPath();

                Rectangle arc = new Rectangle(bounds.Location, new Size(diameter, diameter));

                path.AddArc(arc, 180, 90);
                arc.X = bounds.Right - diameter;
                path.AddArc(arc, 270, 90);
                arc.Y = bounds.Bottom - diameter;
                path.AddArc(arc, 0, 90);
                arc.X = bounds.Left;
                path.AddArc(arc, 90, 90);
                path.CloseFigure();

                return path;
            }

            private static GraphicsPath CreateSoftBlobPath(RectangleF rect)
            {
                GraphicsPath path = new GraphicsPath();

                float w = rect.Width;
                float h = rect.Height;
                float x = rect.X;
                float y = rect.Y;

                PointF p1 = new PointF(x + w * 0.12f, y + h * 0.22f);
                PointF p2 = new PointF(x + w * 0.70f, y + h * 0.02f);
                PointF p3 = new PointF(x + w * 0.95f, y + h * 0.36f);
                PointF p4 = new PointF(x + w * 0.82f, y + h * 0.90f);
                PointF p5 = new PointF(x + w * 0.22f, y + h * 0.92f);
                PointF p6 = new PointF(x + w * 0.02f, y + h * 0.58f);

                path.StartFigure();
                path.AddBezier(p1, new PointF(x + w * 0.32f, y - h * 0.02f), new PointF(x + w * 0.56f, y + h * 0.00f), p2);
                path.AddBezier(p2, new PointF(x + w * 0.94f, y + h * 0.10f), new PointF(x + w * 1.02f, y + h * 0.24f), p3);
                path.AddBezier(p3, new PointF(x + w * 1.00f, y + h * 0.66f), new PointF(x + w * 0.96f, y + h * 0.84f), p4);
                path.AddBezier(p4, new PointF(x + w * 0.66f, y + h * 1.04f), new PointF(x + w * 0.36f, y + h * 1.02f), p5);
                path.AddBezier(p5, new PointF(x + w * 0.04f, y + h * 0.94f), new PointF(x - w * 0.02f, y + h * 0.74f), p6);
                path.AddBezier(p6, new PointF(x - w * 0.04f, y + h * 0.40f), new PointF(x + w * 0.00f, y + h * 0.26f), p1);
                path.CloseFigure();

                return path;
            }

            private struct FloatingParticle
            {
                public float X;
                public float Y;
                public int Size;
                public float Speed;
                public float Phase;
            }

            private struct FloatingBlob
            {
                public RectangleF Bounds;
                public Color ColorA;
                public Color ColorB;
                public float Speed;
                public float Phase;
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

                if (user.IsLoggedIn)
                {
                    ShowError("This user is already logged in on another device.");
                    return;
                }

                if (user.isFirstLogin)
                {
                    using (var changePasswordForm = new ChangePasswordForm(user, _context))
                    {
                        var result = changePasswordForm.ShowDialog(this);

                        if (result != DialogResult.OK)
                        {
                            return;
                        }
                    }
                }

                string newToken = Guid.NewGuid().ToString();

                user.SessionToken = newToken;
                _context.SaveChanges();

                UserSession.CurrentUser = user;
                UserSession.CurrentSessionToken = newToken;

                Hide();

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
                Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"An error occurred during login: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ShowError(string message)
        {
            lblError.Visible = true;
            lblError.Text = message;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion
    }
}