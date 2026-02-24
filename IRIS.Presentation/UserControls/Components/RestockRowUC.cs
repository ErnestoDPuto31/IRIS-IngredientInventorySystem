using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection; 
using IRIS.Domain.Entities;
using IRIS.Domain.Enums;

namespace IRIS.Presentation.UserControls.Table
{
    public partial class RestockRowUC : UserControl
    {
        // -------------------------------------------------------------------------
        // UI & CONTROLS
        // -------------------------------------------------------------------------

        public Restock Data { get; private set; }
        public event EventHandler<Restock> RestockClicked;

        private Button _btnRestock;
        private bool _isHovered = false;
        // Columns: Name, Category, Stock, Min, Request Qty, Status, Action
        private readonly float[] _colWeights = { 0.18f, 0.12f, 0.14f, 0.14f, 0.16f, 0.10f, 0.16f };

        private readonly Color _cTextMain = Color.FromArgb(50, 50, 50);
        private readonly Color _cTextOrange = Color.FromArgb(230, 140, 20);
        private readonly Color _cTextPurple = Color.FromArgb(75, 0, 130);
        private readonly Color _cHoverBg = Color.FromArgb(252, 250, 255);
        private Bunifu.Charts.WinForms.ChartTypes.BunifuBarChart bunifuBarChart1;
        private System.ComponentModel.IContainer components;
        private readonly Color _cLine = Color.FromArgb(240, 240, 250);

        public RestockRowUC(Restock item)
        {
            this.Data = item;
            this.Height = 60;
            this.BackColor = Color.White;
            this.DoubleBuffered = true;

            InitializeButton();
            SetupHoverEvents();

            this.Resize += (s, e) => RepositionButton();
        }

        private void InitializeButton()
        {
            _btnRestock = new Button
            {
                Text = "Restock",
                Font = new Font("Poppins", 9F, FontStyle.Bold),
                BackColor = _cTextPurple,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(90, 32),
                Cursor = Cursors.IBeam
            };
            _btnRestock.FlatAppearance.BorderSize = 0;
            UpdateButtonShape();
            _btnRestock.Click += (s, e) => RestockClicked?.Invoke(this, Data);
            this.Controls.Add(_btnRestock);
        }

        private void SetupHoverEvents()
        {
            this.MouseEnter += (s, e) => { _isHovered = true; this.Invalidate(); };
            this.MouseLeave += (s, e) =>
            {
                if (!this.ClientRectangle.Contains(this.PointToClient(Control.MousePosition)))
                {
                    _isHovered = false;
                    this.Invalidate();
                }
            };
            foreach (Control c in this.Controls)
                c.MouseEnter += (s, e) => { _isHovered = true; this.Invalidate(); };
        }

        private void UpdateButtonShape()
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                int r = 10;
                Rectangle rect = new Rectangle(0, 0, _btnRestock.Width, _btnRestock.Height);
                path.AddArc(rect.X, rect.Y, r, r, 180, 90);
                path.AddArc(rect.Right - r, rect.Y, r, r, 270, 90);
                path.AddArc(rect.Right - r, rect.Bottom - r, r, r, 0, 90);
                path.AddArc(rect.X, rect.Bottom - r, r, r, 90, 90);
                path.CloseFigure();
                _btnRestock.Region = new Region(path);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // Safe accessors 
            string ingName = Data.Ingredient?.Name ?? "Unknown";
            string ingUnit = Data.Ingredient?.Unit ?? "units";

            // --- UPDATED: Safely parse Enum display name ---
            string ingCat = Data.Ingredient != null ? GetEnumDisplayName(Data.Ingredient.Category) : "Uncategorized";

            decimal currentStock = Data.Ingredient?.CurrentStock ?? 0;
            decimal minStock = Data.Ingredient?.MinimumStock ?? 0;
            decimal suggestedStock = Data.SuggestedRestockQuantity;

            if (_isHovered)
            {
                using (SolidBrush hoverBrush = new SolidBrush(_cHoverBg))
                    g.FillRectangle(hoverBrush, 0, 0, this.Width, this.Height);
                using (SolidBrush accent = new SolidBrush(_cTextPurple))
                    g.FillRectangle(accent, 0, 0, 4, this.Height);
            }

            using (Font fBold = new Font("Poppins", 10F, FontStyle.Bold))
            using (Font fReg = new Font("Poppins", 10F))
            using (Pen linePen = new Pen(_cLine, 1))
            {
                g.DrawLine(linePen, 20, this.Height - 1, this.Width - 20, this.Height - 1);

                DrawText(g, ingName, fBold, _cTextMain, 0, this.Width, StringAlignment.Near);
                DrawCategoryPill(g, ingCat, 1, this.Width);

                Color stockColor;
                if (currentStock <= 0) stockColor = Color.Crimson;
                else if (currentStock <= minStock) stockColor = _cTextOrange;
                else stockColor = Color.ForestGreen;

                DrawText(g, $"{currentStock} {ingUnit}", fBold, stockColor, 2, this.Width, StringAlignment.Center);
                DrawText(g, $"{minStock} {ingUnit}", fReg, _cTextMain, 3, this.Width, StringAlignment.Center);
                string suggestedText = suggestedStock > 0 ? $"{suggestedStock} {ingUnit}" : "-";
                DrawText(g, suggestedText, fBold, _cTextPurple, 4, this.Width, StringAlignment.Center);

                DrawStatusPill(g, 5, this.Width, currentStock, minStock);
            }
        }

        // --- HELPER TO EXTRACT DISPLAY NAME ---
        private string GetEnumDisplayName(Enum enumValue)
        {
            FieldInfo field = enumValue.GetType().GetField(enumValue.ToString());
            if (field != null)
            {
                var displayAttribute = (DisplayAttribute)Attribute.GetCustomAttribute(field, typeof(DisplayAttribute));
                if (displayAttribute != null)
                {
                    return displayAttribute.Name;
                }
            }
            return enumValue.ToString();
        }

        private void DrawStatusPill(Graphics g, int colIndex, int totalWidth, decimal currentStock, decimal minStock)
        {
            float colX = GetColX(colIndex, totalWidth);
            float colW = totalWidth * _colWeights[colIndex];

            string statusText;
            Color bg, fg;

            if (currentStock <= 0)
            {
                statusText = "Empty";
                bg = Color.FromArgb(255, 235, 235);
                fg = Color.Crimson;
            }
            else if (currentStock <= minStock)
            {
                statusText = "Low";
                bg = Color.FromArgb(255, 248, 225);
                fg = Color.FromArgb(230, 140, 20);
            }
            else
            {
                statusText = "Stocked";
                bg = Color.FromArgb(232, 250, 232);
                fg = Color.ForestGreen;
            }

            using (Font font = new Font("Segoe UI", 8F, FontStyle.Bold))
            {
                int badgeWidth = 85;
                float pX = colX + (colW - badgeWidth) / 2;
                float pY = (this.Height - 24) / 2;
                RectangleF r = new RectangleF(pX, pY, badgeWidth, 24);

                using (GraphicsPath path = GetRoundedPath(r, 12))
                using (SolidBrush b = new SolidBrush(bg))
                using (SolidBrush tb = new SolidBrush(fg))
                {
                    g.FillPath(b, path);
                    g.DrawString(statusText.ToUpper(), font, tb, r, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                }
            }
        }

        // -------------------------------------------------------------------------
        //  LOGIC & CALCULATIONS
        // -------------------------------------------------------------------------

        private void DrawCategoryPill(Graphics g, string text, int colIndex, int totalWidth)
        {
            float colX = GetColX(colIndex, totalWidth);
            float colW = totalWidth * _colWeights[colIndex];
            using (Font font = new Font("Segoe UI", 8F))
            {
                SizeF size = g.MeasureString(text, font);
                float pH = 22, pW = size.Width + 20;
                float pX = colX + (colW - pW) / 2;
                float pY = (this.Height - pH) / 2;
                RectangleF r = new RectangleF(pX, pY, pW, pH);
                using (GraphicsPath path = GetRoundedPath(r, 11))
                using (Pen p = new Pen(Color.FromArgb(220, 220, 220)))
                using (Brush b = new SolidBrush(Color.DimGray))
                {
                    g.DrawPath(p, path);
                    g.DrawString(text, font, b, r, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                }
            }
        }

        private void DrawText(Graphics g, string text, Font font, Color color, int colIndex, int totalWidth, StringAlignment align)
        {
            float x = GetColX(colIndex, totalWidth);
            float width = totalWidth * _colWeights[colIndex];
            RectangleF rect = new RectangleF(x + 10, 0, width - 20, this.Height);
            using (Brush b = new SolidBrush(color))
                g.DrawString(text, font, b, rect, new StringFormat { LineAlignment = StringAlignment.Center, Alignment = align });
        }

        private GraphicsPath GetRoundedPath(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            float d = radius;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void RepositionButton()
        {
            float startX = 0;
            for (int i = 0; i < 6; i++) startX += this.Width * _colWeights[i];
            float colW = this.Width * _colWeights[6];
            _btnRestock.Location = new Point((int)(startX + (colW - _btnRestock.Width) / 2), (this.Height - _btnRestock.Height) / 2);
        }

        private void InitializeComponent()
        {

        }

        private float GetColX(int index, int totalWidth)
        {
            float x = 0;
            for (int i = 0; i < index; i++) x += totalWidth * _colWeights[i];
            return x;
        }
    }
}