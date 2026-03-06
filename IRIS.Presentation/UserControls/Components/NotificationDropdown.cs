// NotificationDropdown.cs

using IRIS.Domain.Entities;
using IRIS.Presentation.Forms;
using IRIS.Presentation.UserControls.PagesUC;
using IRIS.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Reflection;

namespace IRIS.Presentation.UserControls.Components
{
    public partial class NotificationDropdown : UserControl
    {
        public event EventHandler NotificationClicked;

        private readonly Color _dropdownBg = Color.White;
        private readonly Color _listBg = Color.FromArgb(245, 247, 252);
        private readonly Color _borderColor = Color.FromArgb(224, 228, 236);

        private readonly System.Windows.Forms.Timer _animTimer = new System.Windows.Forms.Timer();

        private bool _animShowing;
        private bool _animRunning;
        private DateTime _animStart;

        private int _animDurationMs = 230;
        private int _collapsedHeight = 12;
        private int _liftPx = 12;
        private int _cornerRadius = 18;

        private Rectangle _targetBounds;
        private Rectangle _startBounds;

        public NotificationDropdown()
        {
            InitializeComponent();

            Size = new Size(390, 430);
            MinimumSize = new Size(340, 240);
            Visible = false;
            BackColor = _dropdownBg;
            BorderStyle = BorderStyle.None;
            Padding = new Padding(10);

            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.UserPaint |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);

            _animTimer.Interval = 15;
            _animTimer.Tick += AnimTick;

            if (flpNotifications != null)
            {
                flpNotifications.Dock = DockStyle.Fill;
                flpNotifications.AutoScroll = true;
                flpNotifications.WrapContents = false;
                flpNotifications.FlowDirection = FlowDirection.TopDown;
                flpNotifications.BackColor = _listBg;
                flpNotifications.Padding = new Padding(12, 12, 12, 14);
                flpNotifications.Margin = new Padding(0);

                EnableDoubleBuffering(flpNotifications);

                flpNotifications.SizeChanged += (s, e) =>
                {
                    ApplyRoundedRegionToList();
                    ReflowCardWidths();
                };
            }

            SizeChanged += (s, e) =>
            {
                ApplyRoundedRegionToSelf();
                ApplyRoundedRegionToList();
                ReflowCardWidths();
                Invalidate();
            };

            ApplyRoundedRegionToSelf();
            ApplyRoundedRegionToList();
        }

        public void ShowBubble()
        {
            if (_animRunning)
            {
                _animTimer.Stop();
                _animRunning = false;
            }

            BringToFront();

            _targetBounds = Bounds;
            _startBounds = new Rectangle(
                _targetBounds.X,
                _targetBounds.Y - _liftPx,
                _targetBounds.Width,
                _collapsedHeight
            );

            Bounds = _startBounds;
            Visible = true;

            _animShowing = true;
            _animRunning = true;
            _animStart = DateTime.Now;

            _animTimer.Start();
        }

        public void HideBubble()
        {
            if (_animRunning)
            {
                _animTimer.Stop();
                _animRunning = false;
            }

            if (!Visible) return;

            _startBounds = Bounds;
            _targetBounds = new Rectangle(
                _startBounds.X,
                _startBounds.Y - _liftPx,
                _startBounds.Width,
                _collapsedHeight
            );

            _animShowing = false;
            _animRunning = true;
            _animStart = DateTime.Now;

            _animTimer.Start();
        }

        private void AnimTick(object sender, EventArgs e)
        {
            double elapsed = (DateTime.Now - _animStart).TotalMilliseconds;
            float t = (float)(elapsed / _animDurationMs);

            if (t >= 1f)
                t = 1f;

            if (_animShowing)
            {
                float heightEase = EaseOutBack(t);
                float yEase = EaseOutCubic(t);

                int h = (int)Lerp(_startBounds.Height, _targetBounds.Height, heightEase);
                int y = (int)Lerp(_startBounds.Y, _targetBounds.Y, yEase);

                SetBounds(_targetBounds.X, y, _targetBounds.Width, h);
            }
            else
            {
                float heightEase = EaseInCubic(t);
                float yEase = EaseInCubic(t);

                int h = (int)Lerp(_startBounds.Height, _targetBounds.Height, heightEase);
                int y = (int)Lerp(_startBounds.Y, _targetBounds.Y, yEase);

                SetBounds(_startBounds.X, y, _startBounds.Width, h);
            }

            ApplyRoundedRegionToSelf();
            ApplyRoundedRegionToList();

            if (t >= 1f)
            {
                _animTimer.Stop();
                _animRunning = false;

                if (_animShowing)
                {
                    Bounds = _targetBounds;
                    ApplyRoundedRegionToSelf();
                    ApplyRoundedRegionToList();
                }
                else
                {
                    Visible = false;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.Clear(Parent?.BackColor ?? Color.White);

            Rectangle shadowRect = ClientRectangle;
            shadowRect.Inflate(-2, -2);
            shadowRect.Offset(0, 2);

            using (var shadowPath = RoundedRect(shadowRect, _cornerRadius))
            using (var shadowBrush = new SolidBrush(Color.FromArgb(26, 0, 0, 0)))
            {
                e.Graphics.FillPath(shadowBrush, shadowPath);
            }

            Rectangle rect = ClientRectangle;
            rect.Inflate(-1, -1);

            using (var path = RoundedRect(rect, _cornerRadius))
            using (var fill = new SolidBrush(_dropdownBg))
            using (var border = new Pen(_borderColor, 1f))
            {
                e.Graphics.FillPath(fill, path);
                e.Graphics.DrawPath(border, path);
            }

            base.OnPaint(e);
        }

        public void LoadNotifications(List<NotificationDto> notifications)
        {
            if (flpNotifications == null) return;

            flpNotifications.SuspendLayout();
            flpNotifications.Controls.Clear();

            if (notifications == null || notifications.Count == 0)
            {
                flpNotifications.Controls.Add(CreateEmptyStateCard());
                flpNotifications.ResumeLayout();
                ReflowCardWidths();
                return;
            }

            foreach (var notif in notifications.Where(n => n != null))
            {
                var statusColor = GetStatusColor(notif.Message);
                var card = CreateNotificationCard(notif, statusColor);
                flpNotifications.Controls.Add(card);
            }

            flpNotifications.ResumeLayout();
            ReflowCardWidths();
        }

        private Control CreateEmptyStateCard()
        {
            var card = new RoundedShadowCard(_listBg)
            {
                AccentColor = Color.FromArgb(180, 180, 190),
                CornerRadius = 16,
                HoverLift = false,
                Margin = new Padding(0, 0, 0, 10),
                Padding = new Padding(16, 14, 16, 14),
                Height = 64
            };

            var lbl = new Label
            {
                Dock = DockStyle.Fill,
                Text = "no new notifications",
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(120, 120, 130),
                Font = new Font("Segoe UI", 9f, FontStyle.Italic),
                BackColor = Color.White
            };

            card.Controls.Add(lbl);
            card.WireHoverToChildren();

            return card;
        }

        private Control CreateNotificationCard(NotificationDto notif, Color statusColor)
        {
            var card = new RoundedShadowCard(_listBg)
            {
                AccentColor = statusColor,
                CornerRadius = 16,
                Margin = new Padding(0, 0, 0, 10),
                Padding = new Padding(16, 12, 14, 12),
                Cursor = Cursors.Hand,
                Height = 84
            };

            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                ColumnCount = 2,
                RowCount = 2,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };

            root.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 22));
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 22));

            var dot = new CircleDot(statusColor)
            {
                Margin = new Padding(2, 4, 0, 0),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            var rtbMessage = new RichTextBox
            {
                BorderStyle = BorderStyle.None,
                BackColor = Color.White,
                ReadOnly = true,
                ScrollBars = RichTextBoxScrollBars.None,
                DetectUrls = false,
                TabStop = false,
                Font = new Font("Segoe UI", 9.75f),
                ForeColor = Color.FromArgb(40, 40, 45),
                Dock = DockStyle.Fill,
                Margin = new Padding(0)
            };

            var footer = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };

            var lblTime = new Label
            {
                AutoSize = true,
                Text = notif.TimeAgo,
                ForeColor = Color.FromArgb(120, 120, 130),
                Font = new Font("Segoe UI", 8.5f),
                Margin = new Padding(0, 2, 8, 0),
                BackColor = Color.White
            };

            var link = new LinkLabel
            {
                AutoSize = true,
                Text = "see now",
                LinkColor = Color.Indigo,
                ActiveLinkColor = Color.Indigo,
                VisitedLinkColor = Color.Indigo,
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                Margin = new Padding(0, 1, 0, 0),
                BackColor = Color.White,
                LinkBehavior = LinkBehavior.NeverUnderline
            };

            link.MouseEnter += (s, e) => link.LinkBehavior = LinkBehavior.AlwaysUnderline;
            link.MouseLeave += (s, e) => link.LinkBehavior = LinkBehavior.NeverUnderline;

            footer.Controls.Add(lblTime);
            footer.Controls.Add(link);

            root.Controls.Add(dot, 0, 0);
            root.SetRowSpan(dot, 2);
            root.Controls.Add(rtbMessage, 1, 0);
            root.Controls.Add(footer, 1, 1);

            card.Controls.Add(root);

            rtbMessage.Text = notif.Message ?? string.Empty;

            string[] keywords =
            {
                "Approved", "Rejected", "Released", "Pending", "New", "Critically", "Low on stock", "Out of stock"
            };

            foreach (var word in keywords)
            {
                int index = rtbMessage.Text.IndexOf(word, StringComparison.OrdinalIgnoreCase);
                while (index >= 0)
                {
                    rtbMessage.Select(index, word.Length);
                    rtbMessage.SelectionColor = GetStatusColor(word);
                    rtbMessage.SelectionFont = new Font(rtbMessage.Font, FontStyle.Bold);
                    index = rtbMessage.Text.IndexOf(word, index + word.Length, StringComparison.OrdinalIgnoreCase);
                }
            }

            rtbMessage.DeselectAll();

            rtbMessage.ContentsResized += (s, e) =>
            {
                int messageHeight = Math.Max(24, e.NewRectangle.Height + 2);
                rtbMessage.Height = messageHeight;
                card.Height = card.Padding.Vertical + messageHeight + 24 + 10;
                card.Invalidate();
            };

            void handleClick(object sender, EventArgs e) => HandleNotificationAction(notif);

            link.Click += handleClick;
            card.Click += handleClick;
            root.Click += handleClick;
            dot.Click += handleClick;
            rtbMessage.Click += handleClick;
            footer.Click += handleClick;
            lblTime.Click += handleClick;

            card.WireHoverToChildren();

            return card;
        }

        private void HandleNotificationAction(NotificationDto notif)
        {
            if (ParentForm is MainForm main)
            {
                if (notif.TargetPage == "RestockPage")
                {
                    main.LoadPage(new RestockPage());
                }
                else if (notif.TargetPage == "RequestControl")
                {
                    main.LoadPage(new RequestControl());
                }

                var notifService = Program.Services.GetService<INotificationService>();
                notifService?.MarkActionTaken(notif.NotificationId, UserSession.CurrentUser?.Username ?? "System");
            }

            NotificationClicked?.Invoke(this, EventArgs.Empty);
            HideBubble();
        }

        private void ReflowCardWidths()
        {
            if (flpNotifications == null) return;

            int scrollbar = flpNotifications.VerticalScroll.Visible
                ? SystemInformation.VerticalScrollBarWidth
                : 0;

            int w = flpNotifications.ClientSize.Width
                    - flpNotifications.Padding.Horizontal
                    - scrollbar
                    - 2;

            foreach (Control c in flpNotifications.Controls)
            {
                c.Width = Math.Max(220, w);
            }
        }

        private Color GetStatusColor(string message)
        {
            if (string.IsNullOrWhiteSpace(message)) return Color.Indigo;

            string msg = message.ToLowerInvariant();

            if (msg.Contains("critically") || msg.Contains("out of stock")) return Color.Crimson;
            if (msg.Contains("low on stock") || msg.Contains("running low")) return Color.Goldenrod;
            if (msg.Contains("approved")) return Color.ForestGreen;
            if (msg.Contains("rejected")) return Color.Crimson;
            if (msg.Contains("released")) return Color.DodgerBlue;
            if (msg.Contains("new") || msg.Contains("pending")) return Color.DarkOrange;

            return Color.Indigo;
        }

        private void ApplyRoundedRegionToSelf()
        {
            if (Width <= 2 || Height <= 2) return;

            using var path = RoundedRect(new Rectangle(0, 0, Width, Height), _cornerRadius);
            Region = new Region(path);
        }

        private void ApplyRoundedRegionToList()
        {
            if (flpNotifications == null || flpNotifications.Width <= 2 || flpNotifications.Height <= 2) return;

            using var path = RoundedRect(new Rectangle(0, 0, flpNotifications.Width, flpNotifications.Height), 12);
            flpNotifications.Region = new Region(path);
        }

        private static GraphicsPath RoundedRect(Rectangle r, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();

            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            return path;
        }

        private static float Lerp(float a, float b, float t) => a + ((b - a) * t);

        private static float EaseOutCubic(float t)
        {
            float p = 1f - t;
            return 1f - (p * p * p);
        }

        private static float EaseInCubic(float t) => t * t * t;

        private static float EaseOutBack(float t)
        {
            const float c1 = 1.70158f;
            const float c3 = c1 + 1f;
            float p = t - 1f;
            return 1f + (c3 * p * p * p) + (c1 * p * p);
        }

        private static void EnableDoubleBuffering(Control ctrl)
        {
            try
            {
                typeof(Control).InvokeMember(
                    "DoubleBuffered",
                    BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                    null,
                    ctrl,
                    new object[] { true });
            }
            catch
            {
            }
        }

        private sealed class CircleDot : Control
        {
            private readonly Color _color;

            public CircleDot(Color color)
            {
                _color = color;
                Size = new Size(10, 10);
                BackColor = Color.White;

                SetStyle(ControlStyles.AllPaintingInWmPaint |
                         ControlStyles.OptimizedDoubleBuffer |
                         ControlStyles.UserPaint |
                         ControlStyles.ResizeRedraw, true);
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                using var b = new SolidBrush(_color);
                e.Graphics.FillEllipse(b, 0, 0, 9, 9);
            }
        }

        private sealed class RoundedShadowCard : Panel
        {
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public Color AccentColor { get; set; } = Color.Indigo;

            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public int CornerRadius { get; set; } = 16;

            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public bool HoverLift { get; set; } = true;

            private readonly Color _outsideBg;
            private bool _hover;

            public RoundedShadowCard(Color outsideBackground)
            {
                _outsideBg = outsideBackground;
                BackColor = outsideBackground;
                ForeColor = Color.Black;

                SetStyle(ControlStyles.AllPaintingInWmPaint |
                         ControlStyles.OptimizedDoubleBuffer |
                         ControlStyles.UserPaint |
                         ControlStyles.ResizeRedraw, true);

                MouseEnter += (_, __) =>
                {
                    _hover = true;
                    Invalidate();
                };

                MouseLeave += (_, __) =>
                {
                    _hover = false;
                    Invalidate();
                };

                Resize += (_, __) => UpdateRoundedRegion();
                UpdateRoundedRegion();
            }

            public void WireHoverToChildren()
            {
                void wire(Control parent)
                {
                    foreach (Control child in parent.Controls)
                    {
                        child.MouseEnter += (_, __) =>
                        {
                            _hover = true;
                            Invalidate();
                        };

                        child.MouseLeave += (_, __) =>
                        {
                            var p = PointToClient(Cursor.Position);
                            if (ClientRectangle.Contains(p)) return;
                            _hover = false;
                            Invalidate();
                        };

                        if (child.HasChildren)
                            wire(child);
                    }
                }

                wire(this);
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.Clear(_outsideBg);

                int lift = (HoverLift && _hover) ? 2 : 1;
                int shadowAlpha = (HoverLift && _hover) ? 42 : 28;

                Rectangle shadowRect = ClientRectangle;
                shadowRect.Inflate(-2, -2);
                shadowRect.Offset(0, lift);

                using (var shadowPath = RoundedRect(shadowRect, CornerRadius))
                using (var shadowBrush = new SolidBrush(Color.FromArgb(shadowAlpha, 0, 0, 0)))
                {
                    e.Graphics.FillPath(shadowBrush, shadowPath);
                }

                Rectangle rect = ClientRectangle;
                rect.Inflate(-1, -1);

                using (var path = RoundedRect(rect, CornerRadius))
                using (var surfaceBrush = new SolidBrush(Color.White))
                using (var borderPen = new Pen(_hover ? Color.FromArgb(198, 204, 218) : Color.FromArgb(226, 231, 240), 1f))
                {
                    e.Graphics.FillPath(surfaceBrush, path);
                    e.Graphics.DrawPath(borderPen, path);

                    Region oldClip = e.Graphics.Clip;
                    e.Graphics.SetClip(path);

                    using (var accentBrush = new SolidBrush(AccentColor))
                    {
                        e.Graphics.FillRectangle(accentBrush, rect.X, rect.Y, 5, rect.Height);
                    }

                    e.Graphics.Clip = oldClip;
                }
            }

            private void UpdateRoundedRegion()
            {
                if (Width <= 2 || Height <= 2) return;

                using var path = RoundedRect(new Rectangle(0, 0, Width, Height), CornerRadius);
                Region = new Region(path);
            }

            private static GraphicsPath RoundedRect(Rectangle r, int radius)
            {
                int d = radius * 2;
                var path = new GraphicsPath();

                path.AddArc(r.X, r.Y, d, d, 180, 90);
                path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
                path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
                path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
                path.CloseFigure();

                return path;
            }
        }
    }
}