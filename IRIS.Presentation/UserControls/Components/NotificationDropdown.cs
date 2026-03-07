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
using System.Linq;

namespace IRIS.Presentation.UserControls.Components
{
    public partial class NotificationDropdown : UserControl
    {
        public event EventHandler NotificationClicked;

        private readonly Color _dropdownBg = Color.White;
        private readonly Color _listBg = Color.FromArgb(245, 247, 252);

        private readonly System.Windows.Forms.Timer _animTimer = new System.Windows.Forms.Timer();
        private bool _animShowing;
        private bool _animRunning;
        private DateTime _animStart;

        private int _animDurationMs = 220;
        private int _collapsedHeight = 10;
        private int _liftPx = 10;
        private int _cornerRadius = 14;

        private Rectangle _targetBounds;
        private Rectangle _startBounds;

        // Soft delete tracking for dismissed notifications
        private readonly HashSet<string> _hiddenNotificationIds = new HashSet<string>();

        public NotificationDropdown()
        {
            InitializeComponent();

            _animTimer.Interval = 15;
            _animTimer.Tick += AnimTick;

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
                flpNotifications.Padding = new Padding(12, 12, 12, 14);

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
                float eh = EaseOutBack(t);
                float ey = EaseOutCubic(t);

                int h = (int)Lerp(_startBounds.Height, _targetBounds.Height, eh);
                int y = (int)Lerp(_startBounds.Y, _targetBounds.Y, ey);

                this.SetBounds(_targetBounds.X, y, _targetBounds.Width, h);
            }
            else
            {
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

        private static float Lerp(float a, float b, float t) => a + (b - a) * t;

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
            Visible = true;  // Ensure the dropdown is visible before starting the animation

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

        public void LoadNotifications(List<NotificationDto> notifications)
        {
            flpNotifications.Controls.Clear();

            var visibleNotifications = notifications?
    .Where(n => !_hiddenNotificationIds.Contains(n.NotificationId.ToString()))
    .ToList() ?? new List<NotificationDto>();

            if (visibleNotifications.Count == 0)
            {
                flpNotifications.Controls.Add(CreateEmptyStateCard());
                ReflowCardWidths();
                return;
            }

            foreach (var notif in visibleNotifications)
            {
                var statusColor = GetStatusColor(notif.Message);
                var card = CreateNotificationCard(notif, statusColor);  // Create notification card
                flpNotifications.Controls.Add(card);
            }

            flpNotifications.Controls.Add(CreateClearAllButton(visibleNotifications));
            ReflowCardWidths();
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

        // ** CreateNotificationCard method ** 
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

            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                ColumnCount = 3,
                RowCount = 2
            };

            root.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 22));
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 24));

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
                Dock = DockStyle.Fill
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
            root.SetColumnSpan(footer, 2);

            card.Controls.Add(root);

            rtbMessage.Text = notif.Message ?? "";

            return card;
        }

        private Control CreateClearAllButton(List<NotificationDto> visibleNotifications)
        {
            var pnl = new Panel
            {
                Name = "ClearAllBtn",
                Height = 36,
                Margin = new Padding(0, 0, 0, 4),
                Cursor = Cursors.Hand
            };

            var linkClearAll = new LinkLabel
            {
                Text = "Clear all notifications",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                LinkColor = Color.FromArgb(140, 140, 150),
                ActiveLinkColor = Color.Crimson,
                Font = new Font("Segoe UI", 9f, FontStyle.Regular),
                LinkBehavior = LinkBehavior.HoverUnderline
            };

            pnl.Controls.Add(linkClearAll);

            linkClearAll.Click += (s, e) =>
            {
                var notifService = (INotificationService)Program.Services.GetService(typeof(INotificationService));

                foreach (var notif in visibleNotifications)
                {
                    _hiddenNotificationIds.Add(notif.NotificationId.ToString());
                    notifService?.DismissNotification(notif.NotificationId);
                }

                flpNotifications.Controls.Clear();
                flpNotifications.Controls.Add(CreateEmptyStateCard());
                ReflowCardWidths();
            };

            return pnl;
        }

        private void ReflowCardWidths()
        {
            if (flpNotifications == null) return;

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

                BackColor = outsideBackground;
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

                var shadowRect = rect;
                shadowRect.Offset(0, lift);

                using (var pathShadow = RoundedRect(shadowRect, CornerRadius))
                using (var sb = new SolidBrush(Color.FromArgb(shadowAlpha, 0, 0, 0)))
                {
                    e.Graphics.FillPath(sb, pathShadow);
                }

                using (var path = RoundedRect(rect, CornerRadius))
                using (var surfaceBrush = new SolidBrush(Color.White))
                {
                    e.Graphics.FillPath(surfaceBrush, path);

                    using (var pen = new Pen(_hover ? Color.FromArgb(200, 205, 220) : Color.FromArgb(225, 230, 240), 1))
                    {
                        e.Graphics.DrawPath(pen, path);
                    }

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

        // Helper Method to Enable Double Buffering for controls
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
    }
}