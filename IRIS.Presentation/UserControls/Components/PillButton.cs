using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;

namespace IRIS.Presentation.UserControls.Components
{
    public class PillButton : Button
    {
        private bool _isSelected = false;
        private bool _showDropdownArrow = false;

        [Category("IRIS Design")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                Invalidate(); // Repaint when state changes
            }
        }

        [Category("IRIS Design")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ShowDropdownArrow
        {
            get => _showDropdownArrow;
            set
            {
                _showDropdownArrow = value;
                Invalidate();
            }
        }

        [Category("IRIS Design")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color SelectedBackColor { get; set; } = Color.FromArgb(75, 0, 130); // Indigo

        [Category("IRIS Design")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color SelectedForeColor { get; set; } = Color.White;

        public PillButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Size = new Size(100, 35);
            this.BackColor = Color.White;
            this.ForeColor = Color.Black;
            this.Cursor = Cursors.Hand;
            this.DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            var g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // 1. Clear background
            g.Clear(this.Parent.BackColor);

            // 2. Define the rounded "Pill" shape
            Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            int radius = this.Height; // Full roundness
            using (GraphicsPath path = GetRoundedPath(rect, radius))
            {
                // 3. Determine Colors based on State
                Color fill = IsSelected ? SelectedBackColor : Color.White;
                Color text = IsSelected ? SelectedForeColor : Color.DimGray;
                Color border = IsSelected ? SelectedBackColor : Color.LightGray;

                // 4. Fill Background
                using (SolidBrush brush = new SolidBrush(fill))
                {
                    g.FillPath(brush, path);
                }

                // 5. Draw Border
                using (Pen pen = new Pen(border, 1))
                {
                    g.DrawPath(pen, path);
                }
            }

            // 6. Draw Text (Centered)
            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
            Rectangle textRect = this.ClientRectangle;

            // Adjust text if there's an arrow
            if (ShowDropdownArrow) textRect.Width -= 15;

            TextRenderer.DrawText(g, this.Text, this.Font, textRect,
                IsSelected ? SelectedForeColor : Color.DimGray, flags);

            // 7. Draw Dropdown Arrow (if enabled)
            if (ShowDropdownArrow)
            {
                int arrowX = this.Width - 25;
                int arrowY = (this.Height / 2) - 2;
                Point[] arrows = {
                    new Point(arrowX, arrowY),
                    new Point(arrowX + 8, arrowY),
                    new Point(arrowX + 4, arrowY + 4)
                };
                g.FillPolygon(Brushes.Gray, arrows);
            }
        }

        // Helper for rounded path
        private GraphicsPath GetRoundedPath(Rectangle rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            float r = rect.Height; // Make it fully rounded based on height
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, r, r, 90, 180);
            path.AddArc(rect.Right - r, rect.Y, r, r, 270, 180);
            path.CloseFigure();
            return path;
        }
    }
}