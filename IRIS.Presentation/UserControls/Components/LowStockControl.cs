using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using IRIS.Domain.Entities;


namespace IRIS.Presentation.UserControls.Components
{
    public partial class LowStockControl : UserControl
    {
        // Grid Configuration
        // Weights: Ingredient, Category, Stock, Threshold, Status
        private readonly float[] _colWeights = { 0.25f, 0.20f, 0.20f, 0.15f, 0.20f };
        private readonly string[] _headers = { "Ingredient", "Category", "Current Stock", "Min Threshold", "Status" };

        // Colors
        private readonly Color _accentColor = Color.FromArgb(255, 193, 7); // Dashboard Yellow
        private readonly Color _headerText = Color.Gray;

        public LowStockControl()
        {
            InitializeComponent();

            // Modern styling setup
            this.DoubleBuffered = true;
            this.Padding = new Padding(0);

            // Adjust header
            if (pnlHeader != null) pnlHeader.Height = 45;

            // Hook resize for responsive rows
            flowList.SizeChanged += (s, e) => ResizeRows();

            // Note: LoadDummyData is kept but usually called via LoadTable in the parent page
        }

        // -------------------------------------------------------------------------
        // 1. DATA LOADING
        // -------------------------------------------------------------------------

        public void LoadData(List<LowStockItem> items)
        {
            if (items == null) return;

            flowList.SuspendLayout();

            // Clean up old rows
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
        // 2. RENDERING (Header & Container)
        // -------------------------------------------------------------------------

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Dashboard Card Style: Left Yellow Border
            int borderThickness = 6;
            using (SolidBrush b = new SolidBrush(_accentColor))
            {
                g.FillRectangle(b, 0, 0, borderThickness, this.Height);
            }
        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            float currentX = 0;
            float totalW = pnlHeader.Width;
            int padding = 15;

            using (Font f = new Font("Segoe UI", 9, FontStyle.Bold))
            using (Brush b = new SolidBrush(_headerText))
            {
                StringFormat fmtLeft = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
                StringFormat fmtRight = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };
                StringFormat fmtCenter = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

                for (int i = 0; i < _headers.Length; i++)
                {
                    float colW = totalW * _colWeights[i];
                    StringFormat currentFmt = (i == 0) ? fmtLeft : (i == 2 || i == 3) ? fmtRight : fmtCenter;

                    RectangleF rect = new RectangleF(currentX, 0, colW, pnlHeader.Height);
                    RectangleF textRect = rect;

                    if (i == 0) textRect.X += padding;
                    if (i == 2 || i == 3) textRect.Width -= 20;

                    g.DrawString(_headers[i].ToUpper(), f, b, textRect, currentFmt);
                    currentX += colW;
                }
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
    }

    // -------------------------------------------------------------------------
    // 3. CUSTOM ROW CONTROL
    // -------------------------------------------------------------------------
    public class LowStockRow : UserControl
    {
        private LowStockItem _data;
        private float[] _weights;
        private bool _isHovered = false;

        private Color _cTextMain = Color.FromArgb(33, 37, 41);
        private Color _cTextSub = Color.FromArgb(108, 117, 125);
        private Color _bgNormal = Color.White;
        private Color _bgHover = Color.FromArgb(248, 249, 250);
        private Color _cBorder = Color.FromArgb(233, 236, 239);

        public LowStockRow(LowStockItem data, float[] weights)
        {
            _data = data;
            _weights = weights;
            this.DoubleBuffered = true;
            this.BackColor = _bgNormal;
            this.Cursor = Cursors.Hand;

            this.MouseEnter += (s, e) => { _isHovered = true; this.Invalidate(); };
            this.MouseLeave += (s, e) => { _isHovered = false; this.Invalidate(); };
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Rectangle rect = this.ClientRectangle;
            rect.Width -= 1; rect.Height -= 1;

            using (SolidBrush bg = new SolidBrush(_isHovered ? _bgHover : _bgNormal))
            using (Pen borderPen = new Pen(_cBorder))
            {
                g.FillRectangle(bg, rect);
                g.DrawRectangle(borderPen, rect);
            }

            float currentX = 0;
            float totalW = this.Width;
            int padding = 15;

            using (Font fMain = new Font("Segoe UI", 10, FontStyle.Bold))
            using (Font fSub = new Font("Segoe UI", 9))
            using (Brush bMain = new SolidBrush(_cTextMain))
            using (Brush bSub = new SolidBrush(_cTextSub))
            {
                StringFormat fmtLeft = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
                StringFormat fmtRight = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };

                // COL 1: Ingredient
                float w = totalW * _weights[0];
                g.DrawString(_data.Name, fMain, bMain, new RectangleF(currentX + padding, 0, w - padding, this.Height), fmtLeft);
                currentX += w;

                // COL 2: Category
                w = totalW * _weights[1];
                DrawBadge(g, _data.Category, new RectangleF(currentX, 0, w, this.Height), Color.FromArgb(241, 243, 245), Color.FromArgb(73, 80, 87));
                currentX += w;

                // COL 3: Stock
                w = totalW * _weights[2];
                g.DrawString($"{_data.Stock} {_data.Unit}", fSub, bMain, new RectangleF(currentX, 0, w - 20, this.Height), fmtRight);
                currentX += w;

                // COL 4: Threshold
                w = totalW * _weights[3];
                g.DrawString(_data.Min.ToString(), fSub, bSub, new RectangleF(currentX, 0, w - 20, this.Height), fmtRight);
                currentX += w;

                // COL 5: Status
                w = totalW * _weights[4];
                bool isCritical = _data.Stock <= 0;
                DrawBadge(g, isCritical ? "EMPTY" : "LOW", new RectangleF(currentX, 0, w, this.Height),
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
                using (GraphicsPath path = GetRoundedPath(badgeRect, isStatus ? 10 : 4))
                using (Brush bg = new SolidBrush(bgColor))
                using (Brush fg = new SolidBrush(textColor))
                {
                    g.FillPath(bg, path);
                    g.DrawString(text, f, fg, badgeRect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
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