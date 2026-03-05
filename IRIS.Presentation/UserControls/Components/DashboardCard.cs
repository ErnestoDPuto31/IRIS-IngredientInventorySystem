using IRIS.Domain.Enums;
using IRIS.Presentation.DependencyInjection;
using IRIS.Services.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;
using IRIS.Presentation.Properties;

namespace IRIS.Presentation.UserControls.Components
{
    [DefaultEvent("IconClicked")]
    public partial class DashboardCard : UserControl
    {
        // --- VISUAL SETTINGS ---
        private Color _accentColor = Color.Transparent;
        private int _borderRadius = 18;

        // card theme
        private readonly Color _cardBg = Color.White;
        private readonly Color _cardBorder = Color.FromArgb(225, 228, 233);

        // --- DATA FIELDS ---
        private CardType _cardType = CardType.TotalIngredients;
        private string _titleText = "Statistics";
        private string _numberText = "0";
        private string _subtitleText = "Loading...";

        // --- ICON FIELDS ---
        private Image _currentIcon = null;
        private Rectangle _iconRect;

        // --- SERVICES ---
        private IRequestService _requestService;
        private IIngredientService _ingredientService;

        // --- EVENTS ---
        public event EventHandler IconClicked;

        [Category("IRIS Design")]
        [Description("Determines the style and logic of the dashboard card.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(CardType.TotalIngredients)]
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
        [Description("Sets the main number displayed on the card manually (if needed).")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Value
        {
            get => _numberText;
            set { _numberText = value; Invalidate(); }
        }

        public DashboardCard()
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

            if (_requestService == null) _requestService = ServiceFactory.GetRequestService();
            if (_ingredientService == null) _ingredientService = ServiceFactory.GetIngredientService();

            LoadData();
        }

        // --- DATA LOGIC ---
        public void LoadData()
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime || Program.Services == null)
                return;

            try
            {
                if (_requestService == null)
                    _requestService = (IRequestService)Program.Services.GetService(typeof(IRequestService));

                if (_ingredientService == null)
                    _ingredientService = (IIngredientService)Program.Services.GetService(typeof(IIngredientService));

                if (_requestService == null || _ingredientService == null)
                {
                    _numberText = "ERR";
                    _subtitleText = "Service missing in DI setup";
                    Invalidate();
                    return;
                }

                switch (_cardType)
                {
                    case CardType.TotalIngredients:
                        var ingredients = _ingredientService.GetAllIngredients();
                        _numberText = (ingredients != null ? ingredients.Count() : 0).ToString();
                        _subtitleText = "Items in inventory";
                        break;

                    case CardType.LowStockItems:
                        _numberText = _ingredientService.GetLowStockCount().ToString();
                        _subtitleText = "Requires restocking";
                        break;

                    case CardType.PendingRequests:
                        _numberText = _requestService.GetPendingRequestCount().ToString();
                        _subtitleText = "Awaiting review";
                        break;

                    case CardType.ApprovedRequests:
                        _numberText = _requestService.GetApprovedRequestCount().ToString();
                        _subtitleText = "Ast Dean / Dean approved";
                        break;

                    default:
                        _numberText = "0";
                        _subtitleText = "Unknown metric";
                        break;
                }
                Invalidate();
            }
            catch (Exception ex)
            {
                _numberText = "!";
                _subtitleText = ex.Message;
                Invalidate();
            }
        }

        // --- VISUAL LOGIC ---
        private void UpdateCardDesign()
        {
            switch (_cardType)
            {
                case CardType.TotalIngredients:
                    _titleText = "Total Ingredients";
                    _accentColor = Color.FromArgb(124, 58, 237); // Purple
                    _currentIcon = Resources.total;
                    break;

                case CardType.LowStockItems:
                    _titleText = "Low Stock Items";
                    _accentColor = Color.FromArgb(239, 68, 68); // Red
                    _currentIcon = Resources.icons8_medium_risk_50;
                    break;

                case CardType.PendingRequests:
                    _titleText = "Pending Requests";
                    _accentColor = Color.FromArgb(245, 158, 11); // Amber
                    _currentIcon = Resources.document_icon;
                    break;

                case CardType.ApprovedRequests:
                    _titleText = "Approved Requests";
                    _accentColor = Color.FromArgb(16, 185, 129); // Green
                    _currentIcon = Resources.check_icon;
                    break;

                default:
                    _titleText = "Dashboard";
                    _accentColor = Color.FromArgb(156, 163, 175); // Gray
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

                // solid left strip accent
                g.SetClip(path);
                using (SolidBrush accent = new SolidBrush(Color.FromArgb(235, _accentColor)))
                {
                    int accentWidth = 6;
                    Rectangle strip = new Rectangle(cardRect.X, cardRect.Y, accentWidth, cardRect.Height);
                    g.FillRectangle(accent, strip);
                }
                g.ResetClip();

                // border on top
                using (Pen border = new Pen(_cardBorder, 1))
                    g.DrawPath(border, path);
            }

            // layout
            int left = cardRect.X + 18;
            int y = cardRect.Y + 10;

            // title
            using (Font titleFont = new Font("Segoe UI Semibold", 10F, FontStyle.Regular, GraphicsUnit.Point))
            using (Brush titleBrush = new SolidBrush(Color.FromArgb(17, 24, 39)))
            {
                g.DrawString(_titleText, titleFont, titleBrush, left, y);
            }

            // number 
            y += 40;
            using (Font numberFont = new Font("Segoe UI", 22F, FontStyle.Bold, GraphicsUnit.Point))
            using (Brush numberBrush = new SolidBrush(Color.FromArgb(17, 24, 39)))
            {
                g.DrawString(_numberText, numberFont, numberBrush, left, y);
            }

            // subtitle 
            y += 36;
            using (Font subFont = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point))
            using (Brush subBrush = new SolidBrush(Color.FromArgb(107, 114, 128)))
            {
                g.DrawString(_subtitleText, subFont, subBrush, left, y + 15);
            }

            // icon 
            int iconSize = 18;
            _iconRect = new Rectangle(cardRect.Right - iconSize - 16, cardRect.Y + 16, iconSize, iconSize);

            if (_currentIcon != null)
                g.DrawImage(_currentIcon, _iconRect);
        }

        private void DrawSoftShadow(Graphics g, Rectangle rect, int radius)
        {
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