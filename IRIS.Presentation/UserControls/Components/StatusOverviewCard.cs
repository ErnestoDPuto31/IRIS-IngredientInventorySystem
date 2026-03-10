using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace IRIS.Presentation.UserControls.Components
{
    public enum OverviewWidgetType { Stock, Requests }

    public partial class StatusOverviewCard : UserControl
    {
        private OverviewWidgetType _widgetType = OverviewWidgetType.Stock;
        private int _value1 = 8;
        private int _value2 = 5;
        private int _value3 = 1;

        [Category("IRIS Design")]
        [DefaultValue(OverviewWidgetType.Stock)]
        public OverviewWidgetType WidgetType
        {
            get => _widgetType;
            set { _widgetType = value; Invalidate(); }
        }

        [Category("IRIS Data")]
        [DefaultValue(8)]
        public int Value1 { get => _value1; set { _value1 = value; Invalidate(); } }

        [Category("IRIS Data")]
        [DefaultValue(5)]
        public int Value2 { get => _value2; set { _value2 = value; Invalidate(); } }

        [Category("IRIS Data")]
        [DefaultValue(1)]
        public int Value3 { get => _value3; set { _value3 = value; Invalidate(); } }

        public StatusOverviewCard()
        {
            DoubleBuffered = true;
            Size = new Size(350, 180);
            BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            // Outer border
            Rectangle cardRect = new Rectangle(1, 1, Width - 3, Height - 3);
            using (GraphicsPath path = GetRoundedPath(cardRect, 12))
            using (SolidBrush bg = new SolidBrush(Color.White))
            using (Pen border = new Pen(Color.FromArgb(225, 228, 233), 1))
            {
                g.FillPath(bg, path);
                g.DrawPath(border, path);
            }

            // Setup Data based on type
            string title = _widgetType == OverviewWidgetType.Stock ? "Stock Status Overview" : "Request Status Overview";
            string label1 = _widgetType == OverviewWidgetType.Stock ? "Well Stocked" : "Pending";
            string label2 = _widgetType == OverviewWidgetType.Stock ? "Low Stocked" : "Approved";
            string label3 = _widgetType == OverviewWidgetType.Stock ? "Empty" : "Released";

            Color c1 = _widgetType == OverviewWidgetType.Stock ? Color.FromArgb(16, 185, 129) : Color.FromArgb(139, 92, 246);
            Color c2 = _widgetType == OverviewWidgetType.Stock ? Color.FromArgb(245, 158, 11) : Color.FromArgb(16, 185, 129);
            Color c3 = _widgetType == OverviewWidgetType.Stock ? Color.FromArgb(239, 68, 68) : Color.FromArgb(109, 40, 217);

            // Title (Leave room on the left for you to drop a PictureBox with your icon if you want)
            using (Font titleFont = new Font("Poppins", 11F, FontStyle.Bold))
            using (Brush titleBrush = new SolidBrush(Color.FromArgb(17, 24, 39)))
            {
                g.DrawString(title, titleFont, titleBrush, new PointF(38, 18));
            }

            // Calculate max for progress bars
            int max = Math.Max(_value1, Math.Max(_value2, _value3));
            if (max == 0) max = 1; // Prevent divide by zero

            int startY = 65;
            int rowHeight = 35;

            // Draw the 3 rows
            DrawRow(g, label1, _value1, max, c1, startY);
            DrawRow(g, label2, _value2, max, c2, startY + rowHeight);
            DrawRow(g, label3, _value3, max, c3, startY + rowHeight * 2);
        }

        private void DrawRow(Graphics g, string label, int val, int max, Color barColor, int y)
        {
            // Draw Label
            using (Font font = new Font("Poppins", 9.5F))
            using (Brush brush = new SolidBrush(Color.FromArgb(55, 65, 81)))
                g.DrawString(label, font, brush, new PointF(20, y));

            // Draw Number
            using (Font fontNum = new Font("Poppins", 9.5F))
            using (Brush brushNum = new SolidBrush(Color.Black))
            {
                string vStr = val.ToString();
                SizeF size = g.MeasureString(vStr, fontNum);
                g.DrawString(vStr, fontNum, brushNum, new PointF(Width - 20 - size.Width, y));
            }

            // Draw Progress Bar
            int barX = 130;
            int maxBarWidth = Width - barX - 60;
            int barHeight = 8;
            int barY = y + 7;

            Rectangle bgRect = new Rectangle(barX, barY, maxBarWidth, barHeight);
            using (GraphicsPath bgPath = GetRoundedPath(bgRect, barHeight / 2))
            using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(243, 244, 246)))
                g.FillPath(bgBrush, bgPath);

            int fillWidth = (int)((double)val / max * maxBarWidth);
            if (fillWidth > 0)
            {
                if (fillWidth < barHeight) fillWidth = barHeight; // Keep corners rounded
                Rectangle fgRect = new Rectangle(barX, barY, fillWidth, barHeight);
                using (GraphicsPath fgPath = GetRoundedPath(fgRect, barHeight / 2))
                using (SolidBrush fgBrush = new SolidBrush(barColor))
                    g.FillPath(fgBrush, fgPath);
            }
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float d = radius * 2f;
            if (rect.Width <= 0 || rect.Height <= 0) return path;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}