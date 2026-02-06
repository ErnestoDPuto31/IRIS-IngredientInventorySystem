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
        private bool _isHovered = false;

        // --- Colors ---
        // "Indigo" typically is (75, 0, 130)
        private Color _primaryColor = Color.FromArgb(75, 0, 130);
        private Color _whiteColor = Color.White;

        [Category("IRIS Design")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                Invalidate();
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

        public PillButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Size = new Size(100, 35);
            this.BackColor = Color.White;
            this.Cursor = Cursors.Hand;
            this.DoubleBuffered = true;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _isHovered = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _isHovered = false;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            var g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // 1. Clear background (match parent)
            g.Clear(this.Parent.BackColor);

            // 2. Define Shape
            Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            int radius = this.Height;

            using (GraphicsPath path = GetRoundedPath(rect, radius))
            {
                // --- COLOR LOGIC ---
                // If Hovered OR Selected -> Filled Indigo, White Text
                // Else (Normal)         -> White Back, Indigo Text, Indigo Border

                bool isActive = _isSelected || _isHovered;

                Color fillColor = isActive ? _primaryColor : _whiteColor;
                Color textColor = isActive ? _whiteColor : _primaryColor;
                Color borderColor = _primaryColor; // Border is always Indigo

                // 3. Fill Background
                using (SolidBrush brush = new SolidBrush(fillColor))
                {
                    g.FillPath(brush, path);
                }

                // 4. Draw Border
                using (Pen pen = new Pen(borderColor, 1))
                {
                    g.DrawPath(pen, path);
                }

                // 5. Draw Text
                TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
                Rectangle textRect = this.ClientRectangle;
                if (ShowDropdownArrow) textRect.Width -= 15;

                TextRenderer.DrawText(g, this.Text, this.Font, textRect, textColor, flags);

                // 6. Draw Dropdown Arrow (if enabled)
                if (ShowDropdownArrow)
                {
                    int arrowX = this.Width - 25;
                    int arrowY = (this.Height / 2) - 2;
                    Point[] arrows = {
                        new Point(arrowX, arrowY),
                        new Point(arrowX + 8, arrowY),
                        new Point(arrowX + 4, arrowY + 4)
                    };

                    // Arrow color matches text color
                    using (SolidBrush arrowBrush = new SolidBrush(textColor))
                    {
                        g.FillPolygon(arrowBrush, arrows);
                    }
                }
            }
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            float r = rect.Height;
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, r, r, 90, 180);
            path.AddArc(rect.Right - r, rect.Y, r, r, 270, 180);
            path.CloseFigure();
            return path;
        }
    }
}