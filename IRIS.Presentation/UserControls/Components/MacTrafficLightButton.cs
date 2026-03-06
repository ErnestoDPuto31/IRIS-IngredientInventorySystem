using System.Drawing.Drawing2D;

namespace IRIS.Presentation.UserControls.Components
{
    public enum MacTrafficLightKind { Close, Minimize }

    public sealed class MacTrafficLightButton : Control
    {
        private readonly MacTrafficLightKind _kind;
        private readonly System.Windows.Forms.Timer _hoverTimer;
        private readonly Color _surfaceColor;
        private float _scale = 1f;
        private float _targetScale = 1f;
        private int _glyphAlpha = 0;
        private int _targetGlyphAlpha = 0;
        private bool _hovered;

        public MacTrafficLightButton(MacTrafficLightKind kind, Color surfaceColor)
        {
            _kind = kind;
            _surfaceColor = surfaceColor;
            Size = new Size(14, 14);
            Cursor = Cursors.Hand;
            BackColor = surfaceColor;
            DoubleBuffered = true;

            _hoverTimer = new System.Windows.Forms.Timer { Interval = 15 };
            _hoverTimer.Tick += (s, e) => {
                _scale += (_targetScale - _scale) * 0.34f;
                _glyphAlpha += (int)((_targetGlyphAlpha - _glyphAlpha) * 0.34f);
                if (Math.Abs(_targetScale - _scale) < 0.01f && Math.Abs(_targetGlyphAlpha - _glyphAlpha) < 1) _hoverTimer.Stop();
                Invalidate();
            };
        }

        protected override void OnMouseEnter(EventArgs e) { _targetScale = 1.12f; _targetGlyphAlpha = 230; _hoverTimer.Start(); }
        protected override void OnMouseLeave(EventArgs e) { _targetScale = 1f; _targetGlyphAlpha = 0; _hoverTimer.Start(); }
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Color baseColor = _kind == MacTrafficLightKind.Close ? Color.FromArgb(255, 95, 86) : Color.FromArgb(255, 189, 46);
            float d = Math.Min(Width - 1, Height - 1) * _scale;
            RectangleF rect = new RectangleF((Width - d) / 2f, (Height - d) / 2f, d, d);
            using var sb = new SolidBrush(baseColor);
            e.Graphics.FillEllipse(sb, rect);
            if (_glyphAlpha > 0)
            {
                using var p = new Pen(Color.FromArgb(_glyphAlpha, 40, 40, 40), 1.5f) { StartCap = LineCap.Round, EndCap = LineCap.Round };
                if (_kind == MacTrafficLightKind.Close)
                {
                    e.Graphics.DrawLine(p, rect.X + 4, rect.Y + 4, rect.Right - 4, rect.Bottom - 4);
                    e.Graphics.DrawLine(p, rect.Right - 4, rect.X + 4, rect.X + 4, rect.Bottom - 4);
                }
                else e.Graphics.DrawLine(p, rect.X + 4, rect.Y + (rect.Height / 2), rect.Right - 4, rect.Y + (rect.Height / 2));
            }
        }
    }
}