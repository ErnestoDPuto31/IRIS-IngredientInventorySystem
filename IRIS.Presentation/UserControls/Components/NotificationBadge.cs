using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace IRIS.Presentation.UserControls.Components
{
    public partial class NotificationBadge : UserControl
    {
        private int _count = 0;
        private Color _badgeColor = Color.Red;
        private Color _textColor = Color.White;

        // Track whether the mouse is hovering over the bell
        private bool _isHovered = false;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Count
        {
            get => _count;
            set
            {
                _count = value;
                this.Invalidate();
            }
        }

        public NotificationBadge()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.BackColor = Color.Transparent;
            this.Cursor = Cursors.Hand;
            this.Size = new Size(50, 50); // Made slightly larger to fit the hover expansion safely
        }

        // --- HOVER LOGIC ---
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _isHovered = true;
            this.Invalidate(); // Force redraw with bigger icon
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _isHovered = false;
            this.Invalidate(); // Force redraw back to normal size
        }
        // -------------------

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Improves image quality when scaling up
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            Image bellIcon = Properties.Resources.icons8_notification_321;
            int targetWidth = _isHovered ? bellIcon.Width + 6 : bellIcon.Width;
            int targetHeight = _isHovered ? bellIcon.Height + 6 : bellIcon.Height;

            // Keep the icon perfectly centered regardless of size
            int iconX = (this.Width - targetWidth) / 2;
            int iconY = (this.Height - targetHeight) / 2;

            g.DrawImage(bellIcon, iconX, iconY, targetWidth, targetHeight);

            // If no notifications, stop here! We only want the bell.
            if (_count <= 0) return;

            // 2. Draw the Red Circle on the top-right edge of the bell
            int badgeSize = 16;

            // The badge naturally shifts outward when the targetWidth increases!
            int badgeX = iconX + targetWidth - badgeSize + 2;
            int badgeY = iconY - 2;

            using (SolidBrush brush = new SolidBrush(_badgeColor))
            {
                g.FillEllipse(brush, badgeX, badgeY, badgeSize, badgeSize);
            }

            // 3. Draw the Number inside the red circle
            string text = _count > 9 ? "9+" : _count.ToString();
            using (Font font = new Font("Segoe UI", 7, FontStyle.Bold))
            using (SolidBrush textBrush = new SolidBrush(_textColor))
            {
                var size = g.MeasureString(text, font);
                float textX = badgeX + (badgeSize - size.Width) / 2;
                float textY = badgeY + (badgeSize - size.Height) / 2;
                g.DrawString(text, font, textBrush, textX, textY);
            }
        }
    }
}