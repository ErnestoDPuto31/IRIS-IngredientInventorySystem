using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using IRIS.Domain.Entities;

namespace IRIS.Presentation.UserControls.Components
{
    public partial class LowStockControl : UserControl
    {
        // Grid Configuration
        private readonly float[] _colWeights = { 0.25f, 0.20f, 0.20f, 0.15f, 0.20f };
        private readonly string[] _headers = { "Ingredient", "Category", "Current Stock", "Min Threshold", "Status" };

        // Colors
        private readonly Color _accentColor = Color.FromArgb(255, 193, 7); // yellow
        private readonly Color _headerText = Color.Gray;

        // Card colors (container UI)
        private readonly Color _cardBg = Color.White;
        private readonly Color _cardBorder = Color.FromArgb(225, 228, 233);
        private readonly Color _shadowColor = Color.FromArgb(20, 0, 0, 0);
        private readonly Color _headerBg = Color.FromArgb(249, 250, 252);

        // Card radius
        private const int CardRadius = 18;

        public LowStockControl()
        {
            InitializeComponent();

            DoubleBuffered = true;
            BackColor = Color.Transparent;

            // padding gives space for shadow feel
            Padding = new Padding(14);

            // header strip feel
            if (pnlHeader != null)
            {
                pnlHeader.Height = 48;
                pnlHeader.BackColor = _headerBg;
                pnlHeader.Padding = new Padding(0);
            }

            // list surface feel
            if (flowList != null)
            {
                flowList.BackColor = Color.White;
                flowList.Padding = new Padding(0, 10, 0, 0);
            }

            TryFixTitleQuote();

            flowList.SizeChanged += (s, e) => ResizeRows();

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
        }

        private void TryFixTitleQuote()
        {
            try
            {
                var titleLabel = Controls.Find("lblTitle", true);
                if (titleLabel != null && titleLabel.Length > 0 && titleLabel[0] is Label lbl)
                {
                    lbl.Text = CleanLeadingQuotes(lbl.Text);
                    lbl.Padding = new Padding(18, 0, 0, 0);
                    lbl.ForeColor = Color.FromArgb(33, 37, 41);
                    lbl.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                }
            }
            catch { }
        }

        private static string CleanLeadingQuotes(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return s;

            s = s.TrimStart();
            while (s.Length > 0)
            {
                char c = s[0];
                if (c == '"' || c == '\'' || c == '`' || c == '’' || c == '“' || c == '”')
                {
                    s = s.Substring(1).TrimStart();
                    continue;
                }
                break;
            }
            return s;
        }

        // -------------------------------------------------------------------------
        // 1. DATA LOADING
        // -------------------------------------------------------------------------
        public void LoadData(List<LowStockItem> items)
        {
            if (items == null) return;

            flowList.SuspendLayout();

            while (flowList.Controls.Count > 0)
            {
                var c = flowList.Controls[0];
                flowList.Controls.Remove(c);
                c.Dispose();
            }

            int w = GetRowWidth();

            foreach (var item in items)
            {
                var row = new LowStockRow(item, _colWeights)
                {
                    Width = w,
                    Height = 55,
                    Margin = new Padding(0, 0, 0, 8)
                };
                flowList.Controls.Add(row);
            }

            flowList.ResumeLayout();
        }

        // -------------------------------------------------------------------------
        // 2. LAYOUT (Keep content inside the card)
        // -------------------------------------------------------------------------
        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);

            // NOTE: add a small gutter so content doesn't feel tight to the accent strip
            int accentGutter = 10;

            int x = Padding.Left + 12 + accentGutter;
            int y = Padding.Top + 12;
            int w = Width - Padding.Left - Padding.Right - 24 - accentGutter;
            int h = Height - Padding.Top - Padding.Bottom - 24;

            if (w <= 0 || h <= 0) return;

            if (pnlHeader != null)
            {
                pnlHeader.Location = new Point(x, y);
                pnlHeader.Width = w;
            }

            if (flowList != null)
            {
                int headerH = pnlHeader?.Height ?? 0;
                flowList.Location = new Point(x, y + headerH);
                flowList.Size = new Size(w, h - headerH);
            }
        }

        // -------------------------------------------------------------------------
        // 3. RENDERING (Card Container) - ReportCards accent logic
        // -------------------------------------------------------------------------
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle cardRect = new Rectangle(
                Padding.Left,
                Padding.Top,
                Width - Padding.Left - Padding.Right - 1,
                Height - Padding.Top - Padding.Bottom - 1
            );

            if (cardRect.Width <= 0 || cardRect.Height <= 0) return;

            // shadow (soft, offset)
            Rectangle shadowRect = cardRect;
            shadowRect.Offset(0, 4);
            shadowRect.Inflate(2, 2);

            using (GraphicsPath shadowPath = GetRoundedPath(shadowRect, CardRadius))
            using (SolidBrush sb = new SolidBrush(_shadowColor))
                g.FillPath(sb, shadowPath);

            // card + SOLID left accent (clipped like ReportCards)
            using (GraphicsPath cardPath = GetRoundedPath(cardRect, CardRadius))
            {
                // fill card
                using (SolidBrush bg = new SolidBrush(_cardBg))
                    g.FillPath(bg, cardPath);

                // accent strip (clipped so it respects rounded corners)
                g.SetClip(cardPath);
                using (SolidBrush accent = new SolidBrush(Color.FromArgb(235, _accentColor)))
                {
                    int accentWidth = 6; // match ReportCards
                    Rectangle strip = new Rectangle(cardRect.X, cardRect.Y, accentWidth, cardRect.Height);
                    g.FillRectangle(accent, strip);
                }
                g.ResetClip();

                // border on top
                using (Pen border = new Pen(_cardBorder, 1))
                    g.DrawPath(border, cardPath);
            }
        }

        // header paint (your existing header logic is fine)
        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            float currentX = 0;
            float totalW = GetRowWidth();
            int padding = 15;

            using (Font f = new Font("Segoe UI", 9, FontStyle.Bold))
            using (Brush b = new SolidBrush(_headerText))
            {
                StringFormat fmtLeft = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
                StringFormat fmtCenter = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

                for (int i = 0; i < _headers.Length; i++)
                {
                    float colW = totalW * _colWeights[i];
                    StringFormat currentFmt = (i == 0) ? fmtLeft : fmtCenter;

                    RectangleF textRect = new RectangleF(currentX, 0, colW, pnlHeader.Height);

                    if (i == 0)
                    {
                        // no extra +18 anymore since accent is removed
                        textRect.X += padding;
                        textRect.Width -= padding;
                    }

                    g.DrawString(_headers[i].ToUpper(), f, b, textRect, currentFmt);
                    currentX += colW;
                }
            }

            // subtle bottom divider
            using (Pen p = new Pen(Color.FromArgb(230, 233, 238), 1))
            {
                g.DrawLine(p, 12, pnlHeader.Height - 1, pnlHeader.Width - 12, pnlHeader.Height - 1);
            }
        }

        private void ResizeRows()
        {
            flowList.SuspendLayout();
            int w = GetRowWidth();
            foreach (Control c in flowList.Controls)
            {
                if (c.Width != w) c.Width = w;
            }
            flowList.ResumeLayout();
        }

        private int GetRowWidth()
        {
            int scrollMargin = flowList.VerticalScroll.Visible ? SystemInformation.VerticalScrollBarWidth : 0;
            return flowList.ClientSize.Width - scrollMargin - 2;
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int d = radius * 2;

            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            return path;
        }
    }

    // -------------------------------------------------------------------------
    // 4. CUSTOM ROW CONTROL (ROW UI ONLY — no card container paint here)
    // -------------------------------------------------------------------------
    public class LowStockRow : UserControl
    {
        private readonly LowStockItem _data;
        private readonly float[] _weights;
        private bool _isHovered = false;

        private readonly Color _cTextMain = Color.FromArgb(33, 37, 41);
        private readonly Color _cTextSub = Color.FromArgb(108, 117, 125);
        private readonly Color _bgNormal = Color.White;
        private readonly Color _bgHover = Color.FromArgb(248, 249, 250);
        private readonly Color _cDivider = Color.FromArgb(235, 238, 242);

        public LowStockRow(LowStockItem data, float[] weights)
        {
            _data = data;
            _weights = weights;

            DoubleBuffered = true;
            BackColor = _bgNormal;
            Cursor = Cursors.Hand;

            MouseEnter += (s, e) => { _isHovered = true; Invalidate(); };
            MouseLeave += (s, e) => { _isHovered = false; Invalidate(); };
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            Rectangle rect = ClientRectangle;
            rect.Width -= 1;
            rect.Height -= 1;

            using (SolidBrush bg = new SolidBrush(_isHovered ? _bgHover : _bgNormal))
            using (Pen divider = new Pen(_cDivider, 1))
            {
                g.FillRectangle(bg, rect);
                g.DrawLine(divider, rect.Left + 12, rect.Bottom, rect.Right - 12, rect.Bottom);
            }

            float currentX = 0;
            float totalW = Width;
            int padding = 15;

            using (Font fMain = new Font("Segoe UI", 10, FontStyle.Bold))
            using (Font fSub = new Font("Segoe UI", 9))
            using (Brush bMain = new SolidBrush(_cTextMain))
            using (Brush bSub = new SolidBrush(_cTextSub))
            {
                StringFormat fmtLeft = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
                StringFormat fmtCenter = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

                // Ingredient
                float w = totalW * _weights[0];
                g.DrawString(_data.Name, fMain, bMain, new RectangleF(currentX + padding, 0, w - padding, Height), fmtLeft);
                currentX += w;

                // Category badge
                w = totalW * _weights[1];
                DrawBadge(g, _data.Category, new RectangleF(currentX, 0, w, Height),
                    Color.FromArgb(241, 243, 245), Color.FromArgb(73, 80, 87));
                currentX += w;

                // Stock
                w = totalW * _weights[2];
                g.DrawString($"{_data.Stock} {_data.Unit}", fSub, bMain, new RectangleF(currentX, 0, w, Height), fmtCenter);
                currentX += w;

                // Threshold
                w = totalW * _weights[3];
                g.DrawString(_data.Min.ToString(), fSub, bSub, new RectangleF(currentX, 0, w, Height), fmtCenter);
                currentX += w;

                // Status badge
                w = totalW * _weights[4];
                bool isCritical = _data.Stock <= 0;
                DrawBadge(g, isCritical ? "EMPTY" : "LOW", new RectangleF(currentX, 0, w, Height),
                    isCritical ? Color.FromArgb(255, 226, 229) : Color.FromArgb(255, 244, 229),
                    isCritical ? Color.FromArgb(201, 36, 43) : Color.FromArgb(205, 123, 46), true);
            }
        }

        private void DrawBadge(Graphics g, string text, RectangleF cellRect, Color bgColor, Color textColor, bool isStatus = false)
        {
            using (Font f = new Font("Segoe UI", 8, FontStyle.Bold))
            {
                SizeF size = g.MeasureString(text, f);
                float badgeW = size.Width + 24;
                float badgeH = size.Height + 8;

                float x = cellRect.X + (cellRect.Width - badgeW) / 2;
                float y = cellRect.Y + (cellRect.Height - badgeH) / 2;

                RectangleF badgeRect = new RectangleF(x, y, badgeW, badgeH);

                using (GraphicsPath path = GetRoundedPath(badgeRect, isStatus ? 10 : 6))
                using (Brush bg = new SolidBrush(bgColor))
                using (Brush fg = new SolidBrush(textColor))
                {
                    g.FillPath(bg, path);
                    g.DrawString(text, f, fg, badgeRect,
                        new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                }
            }
        }

        private GraphicsPath GetRoundedPath(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            float d = radius * 2;

            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            return path;
        }
    }
}