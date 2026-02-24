using IRIS.Domain.Entities;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace IRIS.Presentation.UserControls.Components.Tables
{
    public partial class IngredientRowUC : UserControl
    {
        public Ingredient Data { get; private set; }
        public event EventHandler<Ingredient> SelectClicked;

        private Button _btnSelect;
        private bool _isHovered = false;

        // Adjusted weights for 6 columns (Total = 1.0f)
        private readonly float[] _colWeights = { 0.30f, 0.20f, 0.15f, 0.10f, 0.15f, 0.10f };

        private readonly Color _cTextMain = Color.FromArgb(50, 50, 50);
        private readonly Color _cTextSub = Color.Gray;
        private readonly Color _cLine = Color.FromArgb(240, 240, 250);
        private readonly Color _cHoverBg = Color.FromArgb(240, 235, 255);
        private readonly Color _cIndigoAccent = Color.FromArgb(75, 0, 130);
        private readonly Color _cWarningText = Color.FromArgb(211, 47, 47); // Red for low stock

        public IngredientRowUC(Ingredient item)
        {
            this.Data = item;
            this.Height = 60;
            this.BackColor = Color.White;
            this.DoubleBuffered = true;
            this.Margin = new Padding(0);

            InitializeButton();
            SetupHoverEvents();

            this.Resize += (s, e) => RepositionButton();
        }

        private void InitializeButton()
        {
            _btnSelect = new Button
            {
                Text = "Select",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                BackColor = Color.FromArgb(245, 245, 250),
                ForeColor = _cIndigoAccent,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(80, 32),
                Cursor = Cursors.Hand
            };
            _btnSelect.FlatAppearance.BorderSize = 0;

            using (GraphicsPath path = GetRoundedPath(new RectangleF(0, 0, 80, 32), 10))
            {
                _btnSelect.Region = new Region(path);
            }

            // Pass the whole Ingredient object up when clicked
            _btnSelect.Click += (s, e) => SelectClicked?.Invoke(this, Data);
            this.Controls.Add(_btnSelect);
        }

        private void SetupHoverEvents()
        {
            this.MouseEnter += (s, e) => { _isHovered = true; this.Invalidate(); };
            this.MouseLeave += (s, e) => {
                if (!this.ClientRectangle.Contains(this.PointToClient(Control.MousePosition)))
                {
                    _isHovered = false;
                    this.Invalidate();
                }
            };
            _btnSelect.MouseEnter += (s, e) => { _isHovered = true; this.Invalidate(); };
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            if (_isHovered)
            {
                using (SolidBrush hoverBrush = new SolidBrush(_cHoverBg))
                    g.FillRectangle(hoverBrush, 0, 0, this.Width, this.Height);

                using (SolidBrush accent = new SolidBrush(_cIndigoAccent))
                    g.FillRectangle(accent, 0, 0, 4, this.Height);
            }

            using (Font fBold = new Font("Poppins", 10F, FontStyle.Bold))
            using (Font fReg = new Font("Poppins", 9F))
            using (Pen linePen = new Pen(_cLine, 1))
            {
                g.DrawLine(linePen, 20, this.Height - 1, this.Width - 20, this.Height - 1);

                int w = this.Width;
                DrawText(g, Data.Name, fBold, _cTextMain, 0, w, StringAlignment.Near);
                DrawText(g, Data.Category.ToString(), fReg, _cTextSub, 1, w, StringAlignment.Near);
                Color stockColor = Data.CurrentStock <= Data.MinimumStock ? _cWarningText : _cTextMain;
                DrawText(g, Data.CurrentStock.ToString("N2"), fBold, stockColor, 2, w, StringAlignment.Center);
                DrawText(g, Data.Unit.ToString(), fReg, _cTextSub, 3, w, StringAlignment.Center);
                DrawText(g, Data.MinimumStock.ToString("N2"), fReg, _cTextSub, 4, w, StringAlignment.Center);
            }
        }

        private void DrawText(Graphics g, string text, Font font, Color color, int colIndex, int totalWidth, StringAlignment align)
        {
            float x = GetColX(colIndex, totalWidth);
            float width = totalWidth * _colWeights[colIndex];
            RectangleF rect = new RectangleF(x + 10, 0, width - 20, this.Height);

            using (Brush b = new SolidBrush(color))
                g.DrawString(text, font, b, rect, new StringFormat { LineAlignment = StringAlignment.Center, Alignment = align, Trimming = StringTrimming.EllipsisCharacter });
        }

        private float GetColX(int index, int totalWidth)
        {
            float x = 0;
            for (int i = 0; i < index; i++) x += totalWidth * _colWeights[i];
            return x;
        }

        private void RepositionButton()
        {
            float startX = GetColX(5, this.Width);
            float colW = this.Width * _colWeights[5];
            _btnSelect.Location = new Point((int)(startX + (colW - _btnSelect.Width) / 2), (this.Height - _btnSelect.Height) / 2);
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