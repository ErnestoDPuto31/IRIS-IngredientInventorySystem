using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

// NAMESPACE MUST MATCH YOUR DESIGNER FILE EXACTLY
namespace IRIS.Presentation.UserControls.Components
{
    // ADDED 'partial' KEYWORD HERE
    public partial class IrisSearchBar : UserControl
    {
        private TextBox txtSearch;
        private Color borderColor = Color.FromArgb(230, 230, 230);
        private Color focusBorderColor = Color.FromArgb(100, 100, 100);
        private Color fillColor = Color.White;
        private bool isFocused = false;

        // Custom Events
        public new event EventHandler TextChanged;

        public IrisSearchBar()
        {
            // This call is required when using a Designer file
            InitializeComponent();

            // 1. Setup Container
            this.DoubleBuffered = true;
            this.BackColor = Color.Transparent;
            this.Padding = new Padding(10, 5, 10, 5);
            this.Size = new Size(300, 33);
            this.Cursor = Cursors.IBeam;

            // 2. Setup TextBox
            txtSearch = new TextBox();
            txtSearch.BorderStyle = BorderStyle.None;
            txtSearch.BackColor = this.fillColor;
            txtSearch.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            txtSearch.ForeColor = Color.DimGray;
            txtSearch.Text = "Quick search by item name...";

            // Positioning 
            txtSearch.Location = new Point(40, (this.Height - txtSearch.Height) / 2);
            txtSearch.Width = this.Width - 50;

            // Events
            txtSearch.Enter += TxtSearch_Enter;
            txtSearch.Leave += TxtSearch_Leave;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            txtSearch.KeyDown += (s, e) => OnKeyDown(e);

            this.Controls.Add(txtSearch);
        }

        // --- PROPERTIES ---
        [Category("Custom Properties")]
        public override string Text
        {
            get { return txtSearch.Text == "Quick search by item name..." ? "" : txtSearch.Text; }
            set
            {
                txtSearch.Text = value;
                if (string.IsNullOrEmpty(value)) TxtSearch_Leave(null, null);
            }
        }

        // --- EVENTS ---
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "Quick search by item name...")
            {
                TextChanged?.Invoke(this, e);
            }
        }

        private void TxtSearch_Enter(object sender, EventArgs e)
        {
            isFocused = true;
            if (txtSearch.Text == "Quick search by item name...")
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
                txtSearch.Text = "Quick search by item name...";
                txtSearch.ForeColor = Color.DarkGray;
            }
            this.Invalidate();
        }

        // --- DRAWING ---
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // 1. Draw Rounded Background
            using (GraphicsPath path = GetRoundedPath(ClientRectangle, ClientRectangle.Height))
            using (SolidBrush brush = new SolidBrush(fillColor))
            using (Pen pen = new Pen(isFocused ? focusBorderColor : borderColor, 1.5f))
            {
                e.Graphics.FillPath(brush, path);
                e.Graphics.DrawPath(pen, path);
            }

            // 2. Draw Magnifying Glass Icon
            using (Pen iconPen = new Pen(Color.Gray, 2))
            {
                int iconSize = 12;
                int iconX = 15;
                int iconY = (this.Height - iconSize) / 2 - 2;

                e.Graphics.DrawEllipse(iconPen, iconX, iconY, iconSize, iconSize);
                e.Graphics.DrawLine(iconPen, iconX + 9, iconY + 9, iconX + 14, iconY + 14);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (txtSearch != null)
            {
                txtSearch.Location = new Point(40, (this.Height - txtSearch.Height) / 2);
                txtSearch.Width = this.Width - 50;
            }
            this.Invalidate();
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            rect.Inflate(-1, -1);

            if (radius > rect.Height) radius = rect.Height;

            int d = radius;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void OnClick(EventArgs e)
        {
            txtSearch.Focus();
        }
    }
}