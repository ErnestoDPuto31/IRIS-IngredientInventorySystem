using IRIS.Domain.Enums;
using IRIS.Presentation.DependencyInjection;
using IRIS.Services.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using IRIS.Presentation.Properties;

namespace IRIS.Presentation.UserControls.Components
{
    [DefaultEvent("IconClicked")]
    public partial class ReportCards : UserControl
    {
        // --- VISUAL SETTINGS ---
        private Color _accentColor = Color.Transparent;
        private int _borderRadius = 18;

        // this is the accent stroke like your screenshot
        private int _accentThickness = 3;
        private int _accentInset = 12; // space from top/bottom so it doesn't hit rounded corners

        // card theme
        private readonly Color _cardBg = Color.White;
        private readonly Color _cardBorder = Color.FromArgb(225, 228, 233);

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
                Invalidate();
            }
        }

        [Category("IRIS Design")]
        [Description("Sets the main number displayed on the card.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Value
        {
            get => _numberText;
            set { _numberText = value; Invalidate(); }
        }

        public ReportCards()
        {
            InitializeComponent();
            DoubleBuffered = true;

            BackColor = Color.Transparent;
            Padding = new Padding(10); // space for shadow feel

            Size = new Size(330, 130);
            UpdateCardDesign();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
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
            if (DesignMode || _reportsService == null) return;

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
                Invalidate();
            }
            catch
            {
                _numberText = "-";
                _subtitleText = "Error loading data";
                Invalidate();
            }
        }

        // --- VISUAL LOGIC (ONLY COLORS/ICONS HERE) ---
        private void UpdateCardDesign()
        {
            switch (_cardType)
            {
                case CardType.TotalIngredients:
                    _titleText = "Total Ingredients";
                    _accentColor = Color.FromArgb(124, 58, 237); // purple
                    _currentIcon = Resources.icons8_cardboard_box_100;
                    break;

                case CardType.TotalRequests:
                    _titleText = "Total Requests";
                    _accentColor = Color.FromArgb(124, 58, 237); // keep purple family like screenshot
                    _currentIcon = Resources.document_icon;
                    break;

                case CardType.ApprovalRate:
                    _titleText = "Approval Rate";
                    _accentColor = Color.FromArgb(34, 197, 94); // green
                    _currentIcon = Resources.check_icon;
                    break;

                case CardType.TotalTransactions:
                    _titleText = "Total Transactions";
                    _accentColor = Color.FromArgb(59, 130, 246); // blue
                    _currentIcon = Resources.stocks_icon;
                    break;

                case CardType.LowStockItems:
                    _titleText = "Low Stock";
                    _accentColor = Color.FromArgb(249, 115, 22);
                    _currentIcon = Resources.icons8_medium_risk_50;
                    break;

                case CardType.EmptyItems:
                    _titleText = "Empty Items";
                    _accentColor = Color.FromArgb(239, 68, 68);
                    _currentIcon = Resources.icons8_box_64;
                    break;

                case CardType.WellStockedItems:
                    _titleText = "Well Stocked";
                    _accentColor = Color.FromArgb(16, 185, 129);
                    _currentIcon = Resources.icons8_arrow_up_48;
                    break;

                default:
                    _titleText = "Report";
                    _accentColor = Color.FromArgb(156, 163, 175);
                    _currentIcon = null;
                    break;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            Rectangle cardRect = new Rectangle(
                Padding.Left,
                Padding.Top,
                Width - Padding.Left - Padding.Right - 1,
                Height - Padding.Top - Padding.Bottom - 1
            );
            if (cardRect.Width <= 0 || cardRect.Height <= 0) return;

            // shadow first
            DrawSoftShadow(g, cardRect, _borderRadius);

            // card + SOLID left accent (clipped)
            using (GraphicsPath path = GetRoundedPath(cardRect, _borderRadius))
            {
                // fill card
                using (SolidBrush bg = new SolidBrush(_cardBg))
                    g.FillPath(bg, path);

                // solid left strip accent (no doodle)
                g.SetClip(path);
                using (SolidBrush accent = new SolidBrush(Color.FromArgb(235, _accentColor))) // soften a bit
                {
                    int accentWidth = 6; // try 6-8
                    Rectangle strip = new Rectangle(cardRect.X, cardRect.Y, accentWidth, cardRect.Height);
                    g.FillRectangle(accent, strip);
                }
                g.ResetClip();

                // border on top
                using (Pen border = new Pen(_cardBorder, 1))
                    g.DrawPath(border, path);
            }

            // layout (tight)
            int left = cardRect.X + 18;
            int y = cardRect.Y + 10; // was +16 (less top padding)

            // title
            using (Font titleFont = new Font("Segoe UI Semibold", 10F, FontStyle.Regular, GraphicsUnit.Point))
            using (Brush titleBrush = new SolidBrush(Color.FromArgb(17, 24, 39)))
            {
                g.DrawString(_titleText, titleFont, titleBrush, left, y);
            }

            // number (closer to title)
            y += 20; // space after title (tweak 18-22)
            using (Font numberFont = new Font("Segoe UI", 22F, FontStyle.Bold, GraphicsUnit.Point))
            using (Brush numberBrush = new SolidBrush(Color.FromArgb(17, 24, 39)))
            {
                g.DrawString(_numberText, numberFont, numberBrush, left, y);
            }

            // subtitle (directly under number, not stuck to bottom)
            y += 36; // space after big number (tweak 32-40)
            using (Font subFont = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point))
            using (Brush subBrush = new SolidBrush(Color.FromArgb(107, 114, 128)))
            {
                g.DrawString(_subtitleText, subFont, subBrush, left, y + 15);
            }

            // icon (top-right)
            int iconSize = 18;
            _iconRect = new Rectangle(cardRect.Right - iconSize - 16, cardRect.Y + 16, iconSize, iconSize);

            if (_currentIcon != null)
                g.DrawImage(_currentIcon, _iconRect);
        }

        private void DrawSoftShadow(Graphics g, Rectangle rect, int radius)
        {
            // a few faint layers = modern shadow
            for (int i = 1; i <= 4; i++)
            {
                int spread = i * 2;
                Rectangle r = new Rectangle(rect.X - spread, rect.Y - spread + 2, rect.Width + spread * 2, rect.Height + spread * 2);
                int alpha = 10 - (i * 2);
                if (alpha < 2) alpha = 2;

                using (GraphicsPath p = GetRoundedPath(r, radius + spread))
                using (SolidBrush b = new SolidBrush(Color.FromArgb(alpha, 0, 0, 0)))
                    g.FillPath(b, p);
            }
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float d = radius * 2f;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            return path;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (_iconRect.Contains(e.Location))
                IconClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}