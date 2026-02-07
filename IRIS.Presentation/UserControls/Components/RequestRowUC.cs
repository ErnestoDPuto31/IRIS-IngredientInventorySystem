using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using IRIS.Domain.Entities;
using IRIS.Domain.Enums;

namespace IRIS.Presentation.UserControls.Table
{
    public class RequestRowUC : UserControl
    {
        public Request Data { get; private set; }
        public event EventHandler<int> ViewClicked;

        private Button _btnView;
        private bool _isHovered = false;

        private readonly float[] _colWeights = { 0.20f, 0.18f, 0.12f, 0.10f, 0.15f, 0.12f, 0.13f };

        private readonly Color _cTextMain = Color.FromArgb(50, 50, 50);
        private readonly Color _cTextSub = Color.Gray;
        private readonly Color _cLine = Color.FromArgb(240, 240, 250);

        private readonly Color _cHoverBg = Color.FromArgb(240, 235, 255); // Light Indigo
        private readonly Color _cIndigoAccent = Color.FromArgb(75, 0, 130); // Dark Indigo Stripe

        public RequestRowUC(Request item)
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
            _btnView = new Button
            {
                Text = "View",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                BackColor = Color.FromArgb(245, 245, 250),
                ForeColor = Color.FromArgb(75, 0, 130), 
                FlatStyle = FlatStyle.Flat,
                Size = new Size(80, 32),
                Cursor = Cursors.Hand
            };
            _btnView.FlatAppearance.BorderSize = 0;

            using (GraphicsPath path = GetRoundedPath(new RectangleF(0, 0, 80, 32), 10))
            {
                _btnView.Region = new Region(path);
            }

            _btnView.Click += (s, e) => ViewClicked?.Invoke(this, Data.RequestId);
            this.Controls.Add(_btnView);
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
            _btnView.MouseEnter += (s, e) => { _isHovered = true; this.Invalidate(); };
        }

        // -------------------------------------------------------------------------
        // DRAWING LOGIC (OnPaint)
        // -------------------------------------------------------------------------
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
                // Bottom Separator Line
                g.DrawLine(linePen, 20, this.Height - 1, this.Width - 20, this.Height - 1);

                int w = this.Width;

                DrawText(g, Data.Subject, fBold, _cTextMain, 0, w, StringAlignment.Near);
                DrawText(g, Data.FacultyName, fReg, _cTextSub, 1, w, StringAlignment.Near);
                DrawText(g, Data.DateOfUse.ToString("MM/dd/yyyy"), fReg, _cTextMain, 2, w, StringAlignment.Center);
                DrawText(g, Data.StudentCount.ToString(), fBold, _cTextMain, 3, w, StringAlignment.Center);

                string submittedDate = Data.CreatedAt.ToString("MMM dd, yyyy") + "\n" + Data.CreatedAt.ToString("hh:mm tt");
                DrawText(g, submittedDate, fSmall, _cTextSub, 4, w, StringAlignment.Center);

                DrawStatusPill(g, 5, w);
            }
        }

        private void DrawStatusPill(Graphics g, int colIndex, int totalWidth)
        {
            float colX = GetColX(colIndex, totalWidth);
            float colW = totalWidth * _colWeights[colIndex];

            string text = Data.Status.ToString();
            Color bg, fg;

            // STATUS COLORS
            switch (Data.Status)
            {
                case RequestStatus.Approved:
                    bg = Color.FromArgb(232, 245, 233); // Light Green
                    fg = Color.FromArgb(56, 142, 60);   // Dark Green
                    break;
                case RequestStatus.Rejected:
                    bg = Color.FromArgb(255, 235, 238); // Light Red
                    fg = Color.FromArgb(211, 47, 47);   // Dark Red
                    break;
                case RequestStatus.Released:
                    bg = Color.FromArgb(227, 242, 253); // Light Blue
                    fg = Color.FromArgb(33, 150, 243);  // Blue Text
                    break;
                default: // Pending
                    bg = Color.FromArgb(255, 248, 225); // Light Yellow
                    fg = Color.FromArgb(255, 143, 0);   // Dark Orange
                    break;
            }

            using (Font font = new Font("Segoe UI", 8F, FontStyle.Bold))
            {
                int badgeWidth = 90;
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

        private void RepositionButton()
        {
            float startX = GetColX(6, this.Width);
            float colW = this.Width * _colWeights[6];
            _btnView.Location = new Point((int)(startX + (colW - _btnView.Width) / 2), (this.Height - _btnView.Height) / 2);
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