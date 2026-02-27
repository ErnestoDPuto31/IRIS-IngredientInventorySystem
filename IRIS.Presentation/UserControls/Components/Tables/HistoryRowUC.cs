using System.Drawing.Drawing2D;
using IRIS.Domain.Entities;

namespace IRIS.Presentation.UserControls.Components
{
    public partial class HistoryRowUC : UserControl
    {
        public InventoryLog Data { get; private set; }

        private bool _isHovered = false;
        private readonly float[] _colWeights = { 0.25f, 0.15f, 0.15f, 0.10f, 0.10f, 0.25f };

        private readonly Color _cTextMain = Color.FromArgb(50, 50, 50);
        private readonly Color _cTextSub = Color.Gray;
        private readonly Color _cLine = Color.FromArgb(240, 240, 250);

        private readonly Color _cHoverBg = Color.FromArgb(240, 235, 255); 
        private readonly Color _cIndigoAccent = Color.FromArgb(75, 0, 130); 

        public HistoryRowUC(InventoryLog item)
        {
            InitializeComponent();

            this.Data = item;
            this.Height = 60;
            this.BackColor = Color.White;
            this.DoubleBuffered = true;
            this.Margin = new Padding(0);

            SetupHoverEvents();
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

            using (Font fBold = new Font("Segoe UI", 10F, FontStyle.Bold))
            using (Font fReg = new Font("Segoe UI", 9F))
            using (Font fSmall = new Font("Segoe UI", 8F))
            using (Pen linePen = new Pen(_cLine, 1))
            {
                g.DrawLine(linePen, 20, this.Height - 1, this.Width - 20, this.Height - 1);

                int w = this.Width;
                string itemName = Data.IngredientName;

                DrawText(g, itemName, fBold, _cTextMain, 0, w, StringAlignment.Near);

                DrawActionPill(g, 1, w);

                string qtyChange = Data.QuantityChanged > 0 ? $"+{Data.QuantityChanged}" : Data.QuantityChanged.ToString();
                Color qtyColor = Data.QuantityChanged > 0 ? Color.FromArgb(56, 142, 60) : (Data.QuantityChanged < 0 ? Color.FromArgb(211, 47, 47) : _cTextMain);
                DrawText(g, qtyChange, fBold, qtyColor, 2, w, StringAlignment.Center);

                DrawText(g, Data.PreviousStock.ToString(), fReg, _cTextSub, 3, w, StringAlignment.Center);
                DrawText(g, Data.NewStock.ToString(), fBold, _cTextMain, 4, w, StringAlignment.Center);

                string formattedDate = Data.Timestamp.ToString("MMM dd, yyyy") + " at " + Data.Timestamp.ToString("hh:mm tt");
                DrawText(g, formattedDate, fReg, _cTextSub, 5, w, StringAlignment.Center);
            }
        }

        private void DrawActionPill(Graphics g, int colIndex, int totalWidth)
        {
            float colX = GetColX(colIndex, totalWidth);
            float colW = totalWidth * _colWeights[colIndex];

            string text = Data.ActionType ?? "Unknown";
            Color bg, fg;

            // STATUS COLORS BASED ON ACTION
            switch (text)
            {
                case "Added":
                    bg = Color.FromArgb(232, 245, 233); // Light Green
                    fg = Color.FromArgb(56, 142, 60);   // Dark Green
                    break;
                case "Removed":
                    bg = Color.FromArgb(255, 235, 238); // Light Red
                    fg = Color.FromArgb(211, 47, 47);   // Dark Red
                    break;
                case "Updated":
                    bg = Color.FromArgb(227, 242, 253); // Light Blue
                    fg = Color.FromArgb(33, 150, 243);  // Blue Text
                    break;
                case "Restocked":
                    bg = Color.FromArgb(243, 229, 245); // Light Purple
                    fg = Color.FromArgb(156, 39, 176);  // Purple
                    break;
                default:
                    bg = Color.FromArgb(245, 245, 245); // Light Gray
                    fg = Color.FromArgb(97, 97, 97);    // Dark Gray
                    break;
            }

            using (Font font = new Font("Segoe UI", 8F, FontStyle.Bold))
            {
                int badgeWidth = 85;
                float pX = colX + (colW - badgeWidth) / 2;
                float pY = (this.Height - 26) / 2;
                RectangleF r = new RectangleF(pX, pY, badgeWidth, 26);

                using (GraphicsPath path = GetRoundedPath(r, 12))
                using (SolidBrush b = new SolidBrush(bg))
                using (SolidBrush tb = new SolidBrush(fg))
                {
                    g.FillPath(b, path);
                    g.DrawString(text.ToUpper(), font, tb, r, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                }
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