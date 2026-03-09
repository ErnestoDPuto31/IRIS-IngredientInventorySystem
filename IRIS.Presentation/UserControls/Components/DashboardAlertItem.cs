using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace IRIS.Presentation.UserControls.Components
{
    public enum AlertBadgeType { Stock, Approval }

    public partial class DashboardAlertItem : UserControl
    {
        private string _message = "Whole Chicken stock is below minimum threshold";
        private string _dateText = "1/26/2026, 7:00:00 AM";
        private AlertBadgeType _badgeType = AlertBadgeType.Stock;

        [Category("IRIS Design")]
        [DefaultValue("Whole Chicken stock is below minimum threshold")]
        public string Message
        {
            get => _message;
            set { _message = value; Invalidate(); }
        }

        [Category("IRIS Design")]
        [DefaultValue("1/26/2026, 7:00:00 AM")]
        public string DateText
        {
            get => _dateText;
            set { _dateText = value; Invalidate(); }
        }

        [Category("IRIS Design")]
        [DefaultValue(AlertBadgeType.Stock)]
        public AlertBadgeType BadgeType
        {
            get => _badgeType;
            set { _badgeType = value; Invalidate(); }
        }

        public DashboardAlertItem()
        {
            DoubleBuffered = true;
            Size = new Size(500, 60); 
            BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            // 1. Draw Gray Background
            Rectangle rect = new Rectangle(1, 1, Width - 3, Height - 3);
            using (GraphicsPath path = GetRoundedPath(rect, 8))
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(249, 250, 251)))
            {
                g.FillPath(brush, path);
            }

            // 2. Draw Message
            using (Font msgFont = new Font("Poppins", 10F, FontStyle.Regular))
            using (Brush textBrush = new SolidBrush(Color.FromArgb(31, 41, 55)))
            {
                g.DrawString(_message, msgFont, textBrush, new PointF(15, 12));
            }

            // 3. Draw Date
            using (Font dateFont = new Font("Poppins", 8.5F, FontStyle.Regular))
            using (Brush dateBrush = new SolidBrush(Color.FromArgb(107, 114, 128)))
            {
                g.DrawString(_dateText, dateFont, dateBrush, new PointF(15, 35));
            }

            // 4. Draw Pill Badge
            string badgeText = _badgeType == AlertBadgeType.Stock ? "Stock" : "Approval";
            Color badgeColor = _badgeType == AlertBadgeType.Stock ? Color.FromArgb(220, 38, 38) : Color.FromArgb(139, 92, 246);

            using (Font badgeFont = new Font("Poppins", 8.5F, FontStyle.Bold))
            {
                SizeF textSize = g.MeasureString(badgeText, badgeFont);
                int badgeWidth = (int)textSize.Width + 24;
                int badgeHeight = 24;
                Rectangle badgeRect = new Rectangle(Width - badgeWidth - 15, (Height - badgeHeight) / 2, badgeWidth, badgeHeight);

                using (GraphicsPath badgePath = GetRoundedPath(badgeRect, badgeHeight / 2))
                using (SolidBrush badgeBg = new SolidBrush(badgeColor))
                using (SolidBrush badgeFg = new SolidBrush(Color.White))
                {
                    g.FillPath(badgeBg, badgePath);
                    g.DrawString(badgeText, badgeFont, badgeFg,
                        badgeRect.X + (badgeWidth - textSize.Width) / 2,
                        badgeRect.Y + (badgeHeight - textSize.Height) / 2);
                }
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