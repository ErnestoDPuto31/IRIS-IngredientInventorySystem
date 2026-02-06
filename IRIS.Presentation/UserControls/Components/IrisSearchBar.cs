using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace IRIS.Presentation.UserControls.Components
{
    public partial class IrisSearchBar : UserControl
    {
        // -------------------------------------------------------------------------
        // UI & CONTROLS
        // -------------------------------------------------------------------------

        private TextBox txtSearch;
        private Color borderColor = Color.FromArgb(75, 0, 130);
        private bool isFocused = false;
        private const string Placeholder = "Quick search by item name...";

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public new event EventHandler TextChanged;

        public IrisSearchBar()
        {
            this.DoubleBuffered = true;
            this.Size = new Size(300, 35);
            this.Padding = new Padding(10, 5, 10, 5);
            this.BackColor = Color.Transparent;

            txtSearch = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.DimGray,
                Text = Placeholder,
                Width = this.Width - 60,
                Location = new Point(40, 8)
            };

            // Event Wiring
            txtSearch.Enter += TxtSearch_Enter;
            txtSearch.Leave += TxtSearch_Leave;
            txtSearch.TextChanged += TxtSearch_TextChanged;

            this.Controls.Add(txtSearch);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);

            using (GraphicsPath path = GetRoundedPath(rect, Height))
            using (Pen pen = new Pen(borderColor, isFocused ? 2f : 1f))
            {
                e.Graphics.FillPath(Brushes.White, path);
                e.Graphics.DrawPath(pen, path);
            }

            // Draw Magnifying Glass
            using (Pen iconPen = new Pen(borderColor, 2))
            {
                e.Graphics.DrawEllipse(iconPen, 15, 10, 12, 12);
                e.Graphics.DrawLine(iconPen, 25, 20, 30, 25);
            }
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int d = Math.Min(radius, rect.Height);
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (txtSearch != null)
            {
                txtSearch.Width = this.Width - 60;
                txtSearch.Location = new Point(40, (this.Height - txtSearch.Height) / 2);
            }
        }

        // -------------------------------------------------------------------------
        // BACKEND: LOGIC & EVENT HANDLING
        // -------------------------------------------------------------------------

        [Category("Custom Properties")]
        public override string Text
        {
            get => (txtSearch.Text == Placeholder) ? string.Empty : txtSearch.Text;
            set
            {
                if (string.IsNullOrEmpty(value) || value == Placeholder)
                {
                    txtSearch.Text = Placeholder;
                    txtSearch.ForeColor = Color.DimGray;
                }
                else
                {
                    txtSearch.Text = value;
                    txtSearch.ForeColor = Color.Black;
                }
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            // Only notify the parent if the text isn't the placeholder
            if (txtSearch.Focused && txtSearch.Text != Placeholder)
            {
                TextChanged?.Invoke(this, e);
            }
            // If text is cleared while focused, we also want to notify to show all data
            else if (txtSearch.Focused && string.IsNullOrEmpty(txtSearch.Text))
            {
                TextChanged?.Invoke(this, e);
            }
        }

        private void TxtSearch_Enter(object sender, EventArgs e)
        {
            isFocused = true;
            if (txtSearch.Text == Placeholder)
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
            this.Invalidate();
        }

        private void TxtSearch_Leave(object sender, EventArgs e)
        {
            isFocused = false;
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = Placeholder;
                txtSearch.ForeColor = Color.DimGray;
            }
            this.Invalidate();
        }
    }
}   