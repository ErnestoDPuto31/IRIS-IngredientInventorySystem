using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using IRIS.Domain.Enums;
using IRIS.Services.Interfaces;
using IRIS.Presentation.DependencyInjection;

namespace IRIS.Presentation.UserControls.Components
{
    public enum CardType
    {
        EmptyItems,
        LowStockItems,
        WellStockedItems
    }

    public partial class StatusCardUC : UserControl
    {
        // --- FIELDS ---
        private Color _accentColor = Color.Indigo;
        private CardType _cardType = CardType.LowStockItems;
        private int _borderRadius = 20;
        private int _stripWidth = 10;

        // Text Data
        private string _titleText = "Status";
        private string _numberText = "-";
        private string _subtitleText = "Description";

        private readonly IIngredientService _ingredientService;
        private readonly IRestockService _restockService;

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

                // FIX: Recalculate numbers immediately when type changes
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

                    // Initial Load
                    LoadStatistics();
                }
                catch { /* Ignore designer errors */ }
            }
        }

        private void UpdateCardDesign()
        {
            switch (_cardType)
            {
                case CardType.LowStockItems:
                    _titleText = "Low Stock Items";
                    _subtitleText = "Requires attention";
                    _accentColor = Color.Goldenrod;
                    break;
                case CardType.EmptyItems:
                    _titleText = "Empty Items";
                    _subtitleText = "Critical - Out of stock";
                    _accentColor = Color.Crimson;
                    break;
                case CardType.WellStockedItems:
                    _titleText = "Well Stocked";
                    _subtitleText = "Above minimum threshold";
                    _accentColor = Color.SeaGreen;
                    break;
            }
        }

        public void LoadStatistics()
        {
            // Safety checks
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
                        // Only count items that have stock but are below minimum
                        count = allIngredients.Count(i => i.CurrentStock > 0 && i.CurrentStock <= i.MinimumStock);
                        break;
                    case CardType.WellStockedItems:
                        count = allIngredients.Count(i => i.CurrentStock > i.MinimumStock);
                        break;
                }

                _numberText = count.ToString();
                this.Invalidate(); // Redraw with new number
            }
            catch
            {
                _numberText = "0";
                this.Invalidate();
            }
        }

        // --- DRAWING ---
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
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

            // Draw Title
            using (Font titleFont = new Font("Segoe UI", 9, FontStyle.Regular))
            using (Brush titleBrush = new SolidBrush(Color.Gray))
                g.DrawString(_titleText, titleFont, titleBrush, textLeftPadding, 15);

            // Draw Number
            using (Font numberFont = new Font("Segoe UI", 24, FontStyle.Bold))
            using (Brush numberBrush = new SolidBrush(Color.Black))
                g.DrawString(_numberText, numberFont, numberBrush, textLeftPadding - 3, 35);

            // Draw Subtitle
            using (Font subFont = new Font("Segoe UI", 8, FontStyle.Regular))
            using (Brush subBrush = new SolidBrush(Color.Gray))
                g.DrawString(_subtitleText, subFont, subBrush, textLeftPadding, 78);
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