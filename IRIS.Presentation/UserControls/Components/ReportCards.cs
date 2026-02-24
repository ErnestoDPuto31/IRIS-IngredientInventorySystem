using IRIS.Domain.Enums;
using IRIS.Presentation.DependencyInjection;
using IRIS.Services.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using IRIS.Presentation.Properties; // Ensure this points to your actual resources

namespace IRIS.Presentation.UserControls.Components
{
    [DefaultEvent("IconClicked")]
    public partial class ReportCards : UserControl
    {
        // --- VISUAL SETTINGS ---
        private Color _accentColor = Color.Transparent;
        private int _borderRadius = 20;
        private int _stripWidth = 10;

        // --- DATA FIELDS ---
        private CardType _cardType = CardType.TotalRequests;
        private string _titleText = "Statistics";
        private string _numberText = "0";
        private string _subtitleText = "Loading...";

        // --- ICON FIELDS ---
        private Image _currentIcon = null;
        private Rectangle _iconRect;

        // --- SERVICES ---
        private IRequestService _requestService;
        private IIngredientService _ingredientService;
        private IReportsService _reportsService;

        // --- EVENTS ---
        public event EventHandler IconClicked;

        // --- PROPERTIES ---
        [Category("IRIS Design")]
        [Description("Determines the style and logic of the card.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(CardType.TotalRequests)]
        public CardType TypeOfCard
        {
            get => _cardType;
            set
            {
                _cardType = value;
                UpdateCardDesign();
                if (!DesignMode) LoadData();
                this.Invalidate();
            }
        }

        [Category("IRIS Design")]
        [Description("Sets the main number displayed on the card.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Value
        {
            get => _numberText;
            set
            {
                _numberText = value;
                this.Invalidate();
            }
        }

        public ReportCards()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Size = new Size(260, 100);
            this.BackColor = Color.Transparent;
            UpdateCardDesign();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                _numberText = "0";
                _subtitleText = "Design Mode";
                return;
            }

            if (_reportsService == null) _reportsService = ServiceFactory.GetReportsService();
            if (_requestService == null) _requestService = ServiceFactory.GetRequestService();
            if (_ingredientService == null) _ingredientService = ServiceFactory.GetIngredientService();

            LoadData();
        }

        // --- DATA LOGIC (ONLY TEXT/NUMBERS HERE) ---
        public void LoadData()
        {
            if (this.DesignMode || _reportsService == null) return;

            try
            {
                switch (_cardType)
                {
                    case CardType.TotalIngredients:
                        _numberText = _reportsService.GetTotalIngredients().ToString();
                        _subtitleText = "Items in inventory";
                        break;
                    case CardType.TotalRequests:
                        _numberText = _reportsService.GetTotalRequests().ToString();
                        _subtitleText = "All time requests";
                        break;
                    case CardType.TotalTransactions:
                        _numberText = _reportsService.GetTotalTransactions().ToString();
                        _subtitleText = "Completed movements";
                        break;
                    case CardType.ApprovalRate:
                        _numberText = $"{_reportsService.GetApprovalRate()}%";
                        _subtitleText = "Request approval ratio";
                        break;
                    default:
                        _numberText = "0";
                        break;
                }
                this.Invalidate();
            }
            catch
            {
                _numberText = "-";
                _subtitleText = "Error loading data";
            }
        }

        // --- VISUAL LOGIC (ONLY COLORS/ICONS HERE) ---
        private void UpdateCardDesign()
        {
            switch (_cardType)
            {
                case CardType.TotalRequests:
                    _titleText = "Total Requests";
                    _accentColor = Color.DarkBlue;
                    _currentIcon = Resources.document_icon;
                    break;
                case CardType.TotalIngredients:
                    _titleText = "Total Ingredients";
                    _accentColor = Color.Indigo;
                    _currentIcon = Resources.icons8_cardboard_box_100;
                    break;
                case CardType.TotalTransactions:
                    _titleText = "Total Transactions";
                    _accentColor = Color.Yellow;
                    _currentIcon = Resources.stocks_icon;
                    break;
                case CardType.ApprovalRate:
                    _titleText = "Approval Rate";
                    _accentColor = Color.Green;
                    _currentIcon = Resources.check_icon;
                    break;
                case CardType.LowStockItems:
                    _titleText = "Low Stock";
                    _accentColor = Color.Orange;
                    _currentIcon = Resources.icons8_medium_risk_50;
                    break;
                case CardType.EmptyItems:
                    _titleText = "Empty Items";
                    _accentColor = Color.Crimson;
                    _currentIcon = Resources.icons8_box_64;
                    break;
                case CardType.WellStockedItems:
                    _titleText = "Well Stocked";
                    _accentColor = Color.LightGreen;
                    _currentIcon = Resources.icons8_arrow_up_48;
                    break;
                default:
                    _titleText = "Report";
                    _accentColor = Color.Gray;
                    _currentIcon = null;
                    break;
            }
        }

        // --- PAINTING ---
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);

            using (GraphicsPath path = GetRoundedPath(rect, _borderRadius))
            {
                using (Pen shadowPen = new Pen(Color.FromArgb(20, Color.Black), 4))
                    g.DrawPath(shadowPen, path);

                using (SolidBrush bgBrush = new SolidBrush(Color.White))
                    g.FillPath(bgBrush, path);

                g.SetClip(path);
                using (SolidBrush accentBrush = new SolidBrush(_accentColor))
                    g.FillRectangle(accentBrush, 0, 0, _stripWidth, this.Height);
                g.ResetClip();

                using (Pen borderPen = new Pen(Color.FromArgb(220, 220, 220), 1))
                    g.DrawPath(borderPen, path);
            }

            int textLeftPadding = _stripWidth + 15;

            using (Font titleFont = new Font("Segoe UI", 12, FontStyle.Regular))
            using (Brush titleBrush = new SolidBrush(Color.Gray))
                g.DrawString(_titleText, titleFont, titleBrush, textLeftPadding, 15);

            using (Font numberFont = new Font("Segoe UI", 22, FontStyle.Bold))
            using (Brush numberBrush = new SolidBrush(Color.Black))
                g.DrawString(_numberText, numberFont, numberBrush, textLeftPadding - 2, 45);

            using (Font subFont = new Font("Segoe UI", 9, FontStyle.Regular))
            using (Brush subBrush = new SolidBrush(Color.Gray))
                g.DrawString(_subtitleText, subFont, subBrush, textLeftPadding, 95);

            int iconSize = 40;
            _iconRect = new Rectangle(this.Width - iconSize - 15, 15, iconSize, iconSize);

            if (_currentIcon != null)
            {
                g.DrawImage(_currentIcon, _iconRect);
            }
            else
            {
                // Fallback debug square if image is still null
                g.FillRectangle(Brushes.Red, _iconRect);
                g.DrawString("NULL", new Font("Segoe UI", 8), Brushes.White, _iconRect.X, _iconRect.Y + 10);
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

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (_iconRect.Contains(e.Location))
            {
                IconClicked?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}