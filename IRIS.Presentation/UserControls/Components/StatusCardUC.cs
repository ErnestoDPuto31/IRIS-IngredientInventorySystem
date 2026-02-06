using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using IRIS.Domain.Enums;
using IRIS.Services.Interfaces;
using IRIS.Presentation.DependencyInjection;
using IRIS.Presentation.Properties; // Needed for Resources

namespace IRIS.Presentation.UserControls.Components
{
    public enum CardType
    {
        EmptyItems,
        LowStockItems,
        WellStockedItems
    }

    [DefaultEvent("IconClicked")]
    public partial class StatusCardUC : UserControl
    {
        // --- FIELDS ---
        private Color _accentColor = Color.Indigo;
        private CardType _cardType = CardType.LowStockItems;
        private int _borderRadius = 20;
        private int _stripWidth = 10;

        // Icon Fields
        private Image _currentIcon = null;
        private Rectangle _iconRect; // Defines where the user must click

        // Text Data
        private string _titleText = "Status";
        private string _numberText = "-";
        private string _subtitleText = "Description";

        private readonly IIngredientService _ingredientService;
        private readonly IRestockService _restockService;

        // --- EVENTS ---
        [Category("Action")]
        public event EventHandler IconClicked;

        // --- PROPERTIES ---
        [Category("IRIS Design")]
        [Description("Choose the logic for this card.")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public CardType TypeOfCard
        {
            get => _cardType;
            set
            {
                _cardType = value;
                UpdateCardDesign();
                if (!DesignMode) LoadStatistics();
                this.Invalidate();
            }
        }

        public StatusCardUC()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.Size = new Size(260, 100);
            this.BackColor = Color.Transparent;

            UpdateCardDesign();

            // Setup Services
            if (!this.DesignMode)
            {
                try
                {
                    _ingredientService = ServiceFactory.GetIngredientService();
                    _restockService = ServiceFactory.GetRestockService();

                    if (_restockService != null)
                        _restockService.OnInventoryUpdated += LoadStatistics;

                    LoadStatistics();
                }
                catch { }
            }
        }

        private void UpdateCardDesign()
        {
            try
            {
                switch (_cardType)
                {
                    case CardType.LowStockItems:
                        _titleText = "Low Stock Items";
                        _subtitleText = "Requires attention";
                        _accentColor = Color.Goldenrod;
                        _currentIcon = Resources.icons8_medium_risk_50;
                        break;
                    case CardType.EmptyItems:
                        _titleText = "Empty Items";
                        _subtitleText = "Critical - Out of stock";
                        _accentColor = Color.Crimson;
                        _currentIcon = Resources.icons8_box_64;
                        break;
                    case CardType.WellStockedItems:
                        _titleText = "Well Stocked";
                        _subtitleText = "Above minimum threshold";
                        _accentColor = Color.SeaGreen;
                        _currentIcon = Resources.icons8_arrow_up_48;
                        break;
                }
            }
            catch
            {
                // If image is missing, we just don't show it (prevents crash)
                _currentIcon = null;
            }
        }

        public void LoadStatistics()
        {
            if (this.DesignMode || _ingredientService == null) return;
            if (this.InvokeRequired) { this.Invoke(new Action(LoadStatistics)); return; }

            try
            {
                var allIngredients = _ingredientService.GetAllIngredients();
                int count = 0;

                switch (_cardType)
                {
                    case CardType.EmptyItems:
                        count = allIngredients.Count(i => i.CurrentStock <= 0);
                        break;
                    case CardType.LowStockItems:
                        count = allIngredients.Count(i => i.CurrentStock > 0 && i.CurrentStock <= i.MinimumStock);
                        break;
                    case CardType.WellStockedItems:
                        count = allIngredients.Count(i => i.CurrentStock > i.MinimumStock);
                        break;
                }

                _numberText = count.ToString();
                this.Invalidate();
            }
            catch
            {
                _numberText = "0";
                this.Invalidate();
            }
        }

        // --- MOUSE CLICK LOGIC ---

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            // Check if the click happened inside the Icon's area
            if (_iconRect.Contains(e.Location))
            {
                IconClicked?.Invoke(this, EventArgs.Empty);
            }
        }

        // Optional: Change cursor only when over the icon so user knows it's clickable
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_iconRect.Contains(e.Location))
                this.Cursor = Cursors.Hand;
            else
                this.Cursor = Cursors.Default;
        }

        // --- DRAWING ---

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);

            // 1. Draw Card Shape
            using (GraphicsPath path = GetRoundedPath(rect, _borderRadius))
            {
                using (Pen shadowPen = new Pen(Color.FromArgb(15, Color.Black), 4)) g.DrawPath(shadowPen, path);
                using (SolidBrush bgBrush = new SolidBrush(Color.White)) g.FillPath(bgBrush, path);

                g.SetClip(path);
                using (SolidBrush accentBrush = new SolidBrush(_accentColor)) g.FillRectangle(accentBrush, 0, 0, _stripWidth, this.Height);
                g.ResetClip();

                using (Pen borderPen = new Pen(Color.FromArgb(220, 220, 220), 1)) g.DrawPath(borderPen, path);
            }

            int textLeftPadding = _stripWidth + 15;

            // 2. Draw Text
            using (Font titleFont = new Font("Segoe UI", 14, FontStyle.Regular))
            using (Brush titleBrush = new SolidBrush(Color.Gray))
                g.DrawString(_titleText, titleFont, titleBrush, textLeftPadding, 20);

            using (Font numberFont = new Font("Segoe UI", 20, FontStyle.Bold))
            using (Brush numberBrush = new SolidBrush(Color.Black))
                g.DrawString(_numberText, numberFont, numberBrush, textLeftPadding - 3, 70);

            using (Font subFont = new Font("Segoe UI", 10, FontStyle.Regular))
            using (Brush subBrush = new SolidBrush(Color.Gray))
                g.DrawString(_subtitleText, subFont, subBrush, textLeftPadding, 140);

            // 3. Draw Icon (Static, Top-Right)
            if (_currentIcon != null)
            {
                int iconSize = 40; // Fixed size
                int iconX = this.Width - iconSize - 20; // 20px padding from right
                int iconY = 20; // 20px padding from top

                _iconRect = new Rectangle(iconX, iconY, iconSize, iconSize);

                g.DrawImage(_currentIcon, _iconRect);
            }
            else
            {
                // Safety: define rect even if image missing so click doesn't crash
                _iconRect = new Rectangle(this.Width - 50, 20, 40, 40);
            }
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float diameter = radius * 2f;
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}