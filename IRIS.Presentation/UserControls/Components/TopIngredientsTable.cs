using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Reflection;
using System.Windows.Forms;

namespace IRIS.Presentation.UserControls.Components
{
    public partial class TopIngredientsTable : UserControl
    {
        private int _hoverRowIndex = -1;

        // card style
        private static readonly Color CardBorderColor = Color.FromArgb(225, 228, 233);
        private static readonly Color ShadowColor = Color.FromArgb(20, 0, 0, 0);
        private static readonly Color CardBg = Color.White;

        // purple theme (accent + header)
        private static readonly Color AccentColor = Color.FromArgb(124, 58, 237);   // purple
        private static readonly Color HeaderBg = Color.FromArgb(249, 250, 252);
        private static readonly Color TitleColor = Color.FromArgb(33, 37, 41);

        private const int CardRadius = 16;
        private const int OuterAccentWidth = 7;
        private const int OuterAccentInset = 12;
        private const int AccentGutter = 12;

        private const int HeaderHeight = 48;

        private Panel _pnlHeader;
        private Label _lblTitle;

        public TopIngredientsTable()
        {
            InitializeComponent();

            DoubleBuffered = true;
            BackColor = Color.Transparent;
            Padding = new Padding(16);

            if (dgvTopIngredients != null)
                dgvTopIngredients.Dock = DockStyle.None;

            EnsureHeader();
            ApplyModernTableStyling();
            WireVisualEffects();

            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
        }

        // ==============================
        // logic (unchanged)
        // ==============================
        public void LoadData(object dataSource)
        {
            if (dgvTopIngredients.InvokeRequired)
                dgvTopIngredients.Invoke(new Action(() => LoadDataCore(dataSource)));
            else
                LoadDataCore(dataSource);
        }

        private void LoadDataCore(object dataSource)
        {
            dgvTopIngredients.DataSource = null;
            dgvTopIngredients.AutoGenerateColumns = true;
            dgvTopIngredients.DataSource = dataSource;
            dgvTopIngredients.ClearSelection();
        }

        // ==============================
        // header (always exists + always visible)
        // ==============================
        private void EnsureHeader()
        {
            if (_pnlHeader != null && !_pnlHeader.IsDisposed && _lblTitle != null && !_lblTitle.IsDisposed)
                return;

            // always create OUR header (avoid fighting designer random labels)
            _pnlHeader = new Panel
            {
                Name = "pnlTopIngredientsHeader",
                Height = HeaderHeight,
                BackColor = HeaderBg
            };

            _lblTitle = new Label
            {
                Name = "lblTopIngredientsTitle",
                AutoSize = true,
                Text = "Top 5 Most Used Ingredients",
                ForeColor = TitleColor,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                BackColor = Color.Transparent,
                Location = new Point(22, 12),
                Visible = true
            };

            _pnlHeader.Controls.Add(_lblTitle);

            _pnlHeader.Paint -= PnlHeader_Paint;
            _pnlHeader.Paint += PnlHeader_Paint;

            // remove old header if it exists in Controls (optional safety)
            var old = Controls.Find("pnlTopIngredientsHeader", false);
            if (old != null && old.Length > 0 && old[0] != _pnlHeader)
                Controls.Remove(old[0]);

            Controls.Add(_pnlHeader);
            _pnlHeader.BringToFront();
        }

        private void PnlHeader_Paint(object sender, PaintEventArgs e)
        {
            if (_pnlHeader == null || _pnlHeader.IsDisposed) return;

            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            using (var pen = new Pen(Color.FromArgb(230, 233, 238), 1))
                g.DrawLine(pen, 12, _pnlHeader.Height - 1, _pnlHeader.Width - 12, _pnlHeader.Height - 1);
        }

        // ==============================
        // ui styling (table)
        // ==============================
        private void ApplyModernTableStyling()
        {
            EnableDoubleBuffering(dgvTopIngredients);

            dgvTopIngredients.BackgroundColor = Color.White;
            dgvTopIngredients.BorderStyle = BorderStyle.None;

            dgvTopIngredients.GridColor = Color.FromArgb(238, 240, 244);
            dgvTopIngredients.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvTopIngredients.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvTopIngredients.RowHeadersVisible = false;

            dgvTopIngredients.ReadOnly = true;
            dgvTopIngredients.MultiSelect = false;
            dgvTopIngredients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTopIngredients.AllowUserToAddRows = false;
            dgvTopIngredients.AllowUserToDeleteRows = false;
            dgvTopIngredients.AllowUserToResizeRows = false;

            dgvTopIngredients.RowTemplate.Height = 46;
            dgvTopIngredients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTopIngredients.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            dgvTopIngredients.EnableHeadersVisualStyles = false;
            dgvTopIngredients.ColumnHeadersHeight = 46;
            dgvTopIngredients.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvTopIngredients.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(55, 65, 81);
            dgvTopIngredients.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            dgvTopIngredients.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvTopIngredients.ColumnHeadersDefaultCellStyle.Padding = new Padding(12, 0, 12, 0);

            dgvTopIngredients.DefaultCellStyle.Font = new Font("Segoe UI", 10.5F);
            dgvTopIngredients.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            dgvTopIngredients.DefaultCellStyle.BackColor = Color.White;
            dgvTopIngredients.DefaultCellStyle.Padding = new Padding(12, 0, 12, 0);
            dgvTopIngredients.DefaultCellStyle.SelectionBackColor = Color.FromArgb(233, 236, 244);
            dgvTopIngredients.DefaultCellStyle.SelectionForeColor = Color.FromArgb(31, 41, 55);
            dgvTopIngredients.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            dgvTopIngredients.RowsDefaultCellStyle.BackColor = Color.White;
            dgvTopIngredients.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 252);

            dgvTopIngredients.AdvancedCellBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
            dgvTopIngredients.AdvancedCellBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
            dgvTopIngredients.AdvancedCellBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
            dgvTopIngredients.AdvancedCellBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.Single;
        }

        private void WireVisualEffects()
        {
            dgvTopIngredients.CellMouseEnter += (s, e) =>
            {
                if (e.RowIndex >= 0 && _hoverRowIndex != e.RowIndex)
                {
                    int old = _hoverRowIndex;
                    _hoverRowIndex = e.RowIndex;
                    if (old >= 0) dgvTopIngredients.InvalidateRow(old);
                    dgvTopIngredients.InvalidateRow(_hoverRowIndex);
                }
            };

            dgvTopIngredients.CellMouseLeave += (s, e) =>
            {
                if (_hoverRowIndex >= 0)
                {
                    int old = _hoverRowIndex;
                    _hoverRowIndex = -1;
                    dgvTopIngredients.InvalidateRow(old);
                }
            };

            dgvTopIngredients.RowPrePaint += (s, e) =>
            {
                if (e.RowIndex < 0) return;

                var row = dgvTopIngredients.Rows[e.RowIndex];
                bool isAlt = (e.RowIndex % 2 == 1);
                bool isHover = (e.RowIndex == _hoverRowIndex);
                bool isSelected = row.Selected;

                Color baseColor = isAlt ? Color.FromArgb(249, 250, 252) : Color.White;
                if (isHover && !isSelected) baseColor = Color.FromArgb(244, 246, 250);
                if (isSelected) baseColor = Color.FromArgb(233, 236, 244);

                row.DefaultCellStyle.BackColor = baseColor;
            };

            dgvTopIngredients.Paint += (s, e) =>
            {
                int y = dgvTopIngredients.ColumnHeadersHeight;
                using var pen = new Pen(Color.FromArgb(230, 233, 238), 1);
                e.Graphics.DrawLine(pen, 0, y, dgvTopIngredients.Width, y);
            };

            dgvTopIngredients.Cursor = Cursors.Hand;

            dgvTopIngredients.DataBindingComplete += (s, e) =>
            {
                foreach (DataGridViewColumn col in dgvTopIngredients.Columns)
                {
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                    col.HeaderText = ToTitleCase(col.HeaderText.Replace("_", " "));
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                }
                dgvTopIngredients.ClearSelection();
            };
        }

        // layout: header top + dgv below + keep header always on top
        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);

            if (_pnlHeader == null || _pnlHeader.IsDisposed || _lblTitle == null || _lblTitle.IsDisposed)
                EnsureHeader();

            // force title always visible with default text
            if (_lblTitle != null)
            {
                _lblTitle.Visible = true;
                if (string.IsNullOrWhiteSpace(_lblTitle.Text))
                    _lblTitle.Text = "Top 5 Most Used Ingredients";
            }

            // keep header above dgv
            _pnlHeader?.BringToFront();

            int innerPad = 12;
            int x = Padding.Left + innerPad + AccentGutter;
            int y = Padding.Top + innerPad;
            int w = Width - Padding.Left - Padding.Right - (innerPad * 2) - AccentGutter;
            int h = Height - Padding.Top - Padding.Bottom - (innerPad * 2);

            if (w <= 0 || h <= 0) return;

            _pnlHeader.Location = new Point(x, y);
            _pnlHeader.Size = new Size(w, HeaderHeight);

            dgvTopIngredients.Location = new Point(x, y + HeaderHeight);
            dgvTopIngredients.Size = new Size(w, h - HeaderHeight);
        }

        // card paint: shadow + border + OUTER purple accent
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            Rectangle cardRect = new Rectangle(
                Padding.Left,
                Padding.Top,
                Width - Padding.Left - Padding.Right - 1,
                Height - Padding.Top - Padding.Bottom - 1
            );

            if (cardRect.Width <= 0 || cardRect.Height <= 0) return;

            // soft shadow (same as your other cards)
            Rectangle shadowRect = cardRect;
            shadowRect.Offset(0, 4);
            shadowRect.Inflate(2, 2);

            using (var shadowPath = GetRoundedPath(shadowRect, CardRadius))
            using (var sb = new SolidBrush(ShadowColor))
                g.FillPath(sb, shadowPath);

            // card + SOLID left accent (clipped like ReportCards)
            using (GraphicsPath cardPath = GetRoundedPath(cardRect, CardRadius))
            {
                // fill card
                using (SolidBrush bg = new SolidBrush(CardBg))
                    g.FillPath(bg, cardPath);

                // accent strip (clipped so it follows rounded corners)
                g.SetClip(cardPath);
                using (SolidBrush accent = new SolidBrush(Color.FromArgb(235, AccentColor)))
                {
                    int accentWidth = 6; // match ReportCards (tweak 6-8)
                    Rectangle strip = new Rectangle(cardRect.X, cardRect.Y, accentWidth, cardRect.Height);
                    g.FillRectangle(accent, strip);
                }
                g.ResetClip();

                // border on top
                using (Pen border = new Pen(CardBorderColor, 1))
                    g.DrawPath(border, cardPath);
            }
        }

        // helpers
        private static GraphicsPath GetRoundedPath(Rectangle rect, int radius)
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

        private static void EnableDoubleBuffering(Control c)
        {
            typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)
                ?.SetValue(c, true, null);
        }

        private static string ToTitleCase(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return input;
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
        }
    }
}