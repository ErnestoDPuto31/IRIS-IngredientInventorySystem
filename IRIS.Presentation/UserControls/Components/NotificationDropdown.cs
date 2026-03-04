using IRIS.Domain.Entities;
using IRIS.Presentation.Forms;
using IRIS.Presentation.UserControls.PagesUC;
using IRIS.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;
using System.ComponentModel;

namespace IRIS.Presentation.UserControls.Components
{
    public partial class NotificationDropdown : UserControl
    {
        public event EventHandler NotificationClicked;

        // modern dropdown palette
        private readonly Color _dropdownBg = Color.White;
        private readonly Color _listBg = Color.FromArgb(245, 247, 252);
        // --- bubble animation fields ---
        private readonly System.Windows.Forms.Timer _animTimer = new System.Windows.Forms.Timer();
        private bool _animShowing;
        private bool _animRunning;
        private DateTime _animStart;
        private int _animDurationMs = 220;

        private Rectangle _targetBounds;
        private Rectangle _startBounds;

        private int _collapsedHeight = 10;   // start height when showing
        private int _liftPx = 10;            // start a bit higher then drop into place
        private int _cornerRadius = 14;      // rounded dropdown corners
        public NotificationDropdown()
        {
            InitializeComponent();
            // bubble animation timer
            _animTimer.Interval = 15;
            _animTimer.Tick += AnimTick;

            // make the dropdown corners rounded always
            ApplyRoundedRegion(_cornerRadius);
            this.SizeChanged += (s, e) => ApplyRoundedRegion(_cornerRadius);

            // smoother rendering
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.UserPaint |
                     ControlStyles.ResizeRedraw, true);

            BackColor = _dropdownBg;
            BorderStyle = BorderStyle.None;
            Padding = new Padding(8);

            if (flpNotifications != null)
            {
                flpNotifications.AutoScroll = true;
                flpNotifications.WrapContents = false;
                flpNotifications.FlowDirection = FlowDirection.TopDown;

                flpNotifications.BackColor = _listBg;

                // nice breathing space around cards
                flpNotifications.Padding = new Padding(12, 12, 12, 14);

                // reduce flicker when scrolling
                EnableDoubleBuffering(flpNotifications);

                flpNotifications.SizeChanged += (s, e) => ReflowCardWidths();
            }
        }

        private void AnimTick(object sender, EventArgs e)
        {
            double elapsed = (DateTime.Now - _animStart).TotalMilliseconds;
            float t = (float)(elapsed / _animDurationMs);
            if (t >= 1f) t = 1f;

            if (_animShowing)
            {
                // overshoot “bubble” for height, smooth for y
                float eh = EaseOutBack(t);
                float ey = EaseOutCubic(t);

                int h = (int)Lerp(_startBounds.Height, _targetBounds.Height, eh);
                int y = (int)Lerp(_startBounds.Y, _targetBounds.Y, ey);

                this.SetBounds(_targetBounds.X, y, _targetBounds.Width, h);
            }
            else
            {
                // reverse: smooth shrink + lift
                float eh = EaseInCubic(t);
                float ey = EaseInCubic(t);

                int h = (int)Lerp(_startBounds.Height, _targetBounds.Height, eh);
                int y = (int)Lerp(_startBounds.Y, _targetBounds.Y, ey);

                this.SetBounds(_startBounds.X, y, _startBounds.Width, h);
            }

            ApplyRoundedRegion(_cornerRadius);

            if (t >= 1f)
            {
                _animTimer.Stop();
                _animRunning = false;

                // finish clean
                if (_animShowing)
                {
                    this.Bounds = _targetBounds;
                    ApplyRoundedRegion(_cornerRadius);
                }
                else
                {
                    this.Bounds = _startBounds; // collapsed
                    this.Visible = false;
                }
            }
        }
        private void ApplyRoundedRegion(int radius)
        {
            if (radius <= 0) { this.Region = null; return; }
            if (this.Width <= 2 || this.Height <= 2) return;

            using (var path = new GraphicsPath())
            {
                int d = radius * 2;
                Rectangle r = new Rectangle(0, 0, this.Width, this.Height);

                path.AddArc(r.X, r.Y, d, d, 180, 90);
                path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
                path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
                path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
                path.CloseFigure();

                this.Region = new Region(path);
            }
        }
        private static float Lerp(float a, float b, float t) => a + (b - a) * t;

        private static float EaseOutCubic(float t)
        {
            float p = 1f - t;
            return 1f - (p * p * p);
        }

        private static float EaseInCubic(float t) => t * t * t;

        // bubble overshoot
        private static float EaseOutBack(float t)
        {
            const float c1 = 1.70158f;
            const float c3 = c1 + 1f;
            float p = t - 1f;
            return 1f + (c3 * p * p * p) + (c1 * p * p);
        }

        public void ShowBubble()
        {
            if (_animRunning) return;

            BringToFront();

            // save final bounds
            _targetBounds = this.Bounds;

            // start state: small + slightly above
            _startBounds = new Rectangle(
                _targetBounds.X,
                _targetBounds.Y - _liftPx,
                _targetBounds.Width,
                _collapsedHeight
            );

            this.Bounds = _startBounds;
            this.Visible = true;

            _animShowing = true;
            _animRunning = true;
            _animStart = DateTime.Now;

            _animTimer.Start();
        }

        public void HideBubble()
        {
            if (_animRunning) return;
            if (!this.Visible) return;

            // animate from current to collapsed
            _startBounds = this.Bounds;
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

        // optional: draw a subtle border around the dropdown
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            var rect = ClientRectangle;
            rect.Inflate(-1, -1);

            using (var pen = new Pen(Color.FromArgb(220, 225, 235), 1))
            {
                e.Graphics.DrawRectangle(pen, rect);
            }
        }

        private static void EnableDoubleBuffering(Control ctrl)
        {
            try
            {
                typeof(Control).InvokeMember("DoubleBuffered",
                    BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                    null, ctrl, new object[] { true });
            }
            catch { /* ignore */ }
        }

        private Color GetStatusColor(string message)
        {
            if (string.IsNullOrEmpty(message)) return Color.Indigo;

            string msg = message.ToLower();

            if (msg.Contains("critically") || msg.Contains("out of stock")) return Color.Crimson;
            if (msg.Contains("low on stock") || msg.Contains("running low")) return Color.Goldenrod;

            if (msg.Contains("approved")) return Color.ForestGreen;
            if (msg.Contains("rejected")) return Color.Crimson;
            if (msg.Contains("released")) return Color.DodgerBlue;
            if (msg.Contains("new") || msg.Contains("pending")) return Color.DarkOrange;

            return Color.Indigo;
        }

        public void LoadNotifications(List<NotificationDto> notifications)
        {
            flpNotifications.Controls.Clear();

            if (notifications == null || notifications.Count == 0)
            {
                flpNotifications.Controls.Add(CreateEmptyStateCard());
                ReflowCardWidths();
                return;
            }

            foreach (var notif in notifications)
            {
                var statusColor = GetStatusColor(notif.Message);
                var card = CreateNotificationCard(notif, statusColor);
                flpNotifications.Controls.Add(card);
            }

            ReflowCardWidths();
        }

        private Control CreateEmptyStateCard()
        {
            var card = new RoundedShadowCard(_listBg)
            {
                AccentColor = Color.FromArgb(180, 180, 190),
                CornerRadius = 14,
                HoverLift = false,
                Margin = new Padding(0, 0, 0, 10),
                Padding = new Padding(16, 14, 16, 14),
                Height = 56
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
            return card;
        }

        private Control CreateNotificationCard(NotificationDto notif, Color statusColor)
        {
            var card = new RoundedShadowCard(_listBg)
            {
                AccentColor = statusColor,
                CornerRadius = 14,
                Margin = new Padding(0, 0, 0, 10),
                Padding = new Padding(16, 12, 14, 12),
                Cursor = Cursors.Hand
            };

            // layout (dot + message + footer)
            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                ColumnCount = 2,
                RowCount = 2
            };

            root.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 22)); // dot column
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100)); // content

            root.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // message
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 22)); // footer

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
                Dock = DockStyle.Fill
            };

            // footer
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
                BackColor = Color.White
            };
            link.LinkBehavior = LinkBehavior.NeverUnderline;

            // hover underline effect
            link.MouseEnter += (s, e) => link.LinkBehavior = LinkBehavior.AlwaysUnderline;
            link.MouseLeave += (s, e) => link.LinkBehavior = LinkBehavior.NeverUnderline;

            footer.Controls.Add(lblTime);
            footer.Controls.Add(link);

            // add to layout
            root.Controls.Add(dot, 0, 0);
            root.SetRowSpan(dot, 2);

            root.Controls.Add(rtbMessage, 1, 0);
            root.Controls.Add(footer, 1, 1);

            card.Controls.Add(root);

            // content + highlight keywords
            rtbMessage.Text = notif.Message ?? "";

            string[] keywords =
            {
                "Approved", "Rejected", "Released", "Pending", "New", "Critically", "Low on stock", "Out of stock"
            };

            foreach (var word in keywords)
            {
                int index = rtbMessage.Text.IndexOf(word, StringComparison.OrdinalIgnoreCase);
                if (index != -1)
                {
                    rtbMessage.Select(index, word.Length);
                    rtbMessage.SelectionColor = GetStatusColor(word);
                    rtbMessage.SelectionFont = new Font(rtbMessage.Font, FontStyle.Bold);
                }
            }
            rtbMessage.DeselectAll();

            // auto height based on message size
            rtbMessage.ContentsResized += (s, e) =>
            {
                var h = Math.Max(22, e.NewRectangle.Height + 2);
                rtbMessage.Height = h;

                // card height = padding + message + footer + a bit
                card.Height = card.Padding.Vertical + h + 22 + 8;
                card.Invalidate();
            };

            // click anywhere = open notif
            void handleClick(object sender, EventArgs e) => HandleNotificationAction(notif);

            link.Click += handleClick;
            card.Click += handleClick;
            root.Click += handleClick;
            dot.Click += handleClick;
            rtbMessage.Click += handleClick;
            footer.Click += handleClick;
            lblTime.Click += handleClick;

            // make hover work even when hovering children
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

                var notifService = (INotificationService)Program.Services.GetService(typeof(INotificationService));
                notifService?.MarkActionTaken(notif.NotificationId, UserSession.CurrentUser?.Username ?? "System");
            }

            NotificationClicked?.Invoke(this, EventArgs.Empty);
            Visible = false;
        }

        private void ReflowCardWidths()
        {
            if (flpNotifications == null) return;

            // subtract scroll bar when needed so cards dont get clipped
            int scrollbar = flpNotifications.VerticalScroll.Visible ? SystemInformation.VerticalScrollBarWidth : 0;

            int w = flpNotifications.ClientSize.Width
                    - flpNotifications.Padding.Horizontal
                    - scrollbar
                    - 2;

            foreach (Control c in flpNotifications.Controls)
            {
                c.Width = Math.Max(220, w);
            }
        }

        // --------- small helper controls for modern card look ---------

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

                using (var b = new SolidBrush(_color))
                {
                    e.Graphics.FillEllipse(b, 0, 0, 9, 9);
                }
            }
        }

        private sealed class RoundedShadowCard : Panel
        {
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public Color AccentColor { get; set; } = Color.Indigo;
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public int CornerRadius { get; set; } = 14;

            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public bool HoverLift { get; set; } = true;

            private readonly Color _outsideBg;
            private bool _hover;

            public RoundedShadowCard(Color outsideBackground)
            {
                _outsideBg = outsideBackground;

                BackColor = outsideBackground; // outside area (shows rounded corners)
                ForeColor = Color.Black;

                SetStyle(ControlStyles.AllPaintingInWmPaint |
                         ControlStyles.OptimizedDoubleBuffer |
                         ControlStyles.UserPaint |
                         ControlStyles.ResizeRedraw, true);

                MouseEnter += (_, __) => { _hover = true; Invalidate(); };
                MouseLeave += (_, __) => { _hover = false; Invalidate(); };
            }

            public void WireHoverToChildren()
            {
                void wire(Control parent)
                {
                    foreach (Control child in parent.Controls)
                    {
                        child.MouseEnter += (_, __) => { _hover = true; Invalidate(); };
                        child.MouseLeave += (_, __) =>
                        {
                            // if mouse left child but still inside card, keep hover
                            var p = PointToClient(Cursor.Position);
                            if (ClientRectangle.Contains(p)) return;
                            _hover = false;
                            Invalidate();
                        };
                        if (child.HasChildren) wire(child);
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
                int shadowAlpha = (HoverLift && _hover) ? 38 : 26;

                var rect = ClientRectangle;
                rect.Inflate(-1, -1);

                // shadow (soft)
                var shadowRect = rect;
                shadowRect.Offset(0, lift);

                using (var pathShadow = RoundedRect(shadowRect, CornerRadius))
                using (var sb = new SolidBrush(Color.FromArgb(shadowAlpha, 0, 0, 0)))
                {
                    e.Graphics.FillPath(sb, pathShadow);
                }

                // surface
                using (var path = RoundedRect(rect, CornerRadius))
                using (var surfaceBrush = new SolidBrush(Color.White))
                {
                    e.Graphics.FillPath(surfaceBrush, path);

                    // subtle border
                    using (var pen = new Pen(_hover ? Color.FromArgb(200, 205, 220) : Color.FromArgb(225, 230, 240), 1))
                    {
                        e.Graphics.DrawPath(pen, path);
                    }

                    // clip accent strip into rounded shape
                    var oldClip = e.Graphics.Clip;
                    e.Graphics.SetClip(path);

                    using (var accentBrush = new SolidBrush(AccentColor))
                    {
                        e.Graphics.FillRectangle(accentBrush, rect.X, rect.Y, 5, rect.Height);
                    }

                    e.Graphics.Clip = oldClip;
                }
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