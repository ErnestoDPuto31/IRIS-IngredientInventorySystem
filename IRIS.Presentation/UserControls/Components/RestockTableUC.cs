using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // Added for DisplayAttribute in Search
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using IRIS.Domain.Entities;
using IRIS.Services.Interfaces;
using IRIS.Presentation.DependencyInjection;
using IRIS.Presentation.UserControls.Components;
// using Guna.UI2.WinForms; // Ensure this is available

namespace IRIS.Presentation.UserControls.Table
{
    public partial class RestockTableUC : UserControl
    {
        private readonly Color _cIndigo = Color.FromArgb(75, 0, 130);
        private readonly Color _cBackground = Color.White;
        private readonly Color _cHoverHeader = Color.FromArgb(245, 240, 255);

        private readonly float[] _colWeights = { 0.18f, 0.12f, 0.14f, 0.14f, 0.16f, 0.10f, 0.16f };
        private readonly string[] _headers = { "Ingredient", "Category", "Current Stock", "Min Threshold", "Suggested", "Status", "Action" };
        private int _borderRadius = 15;
        private int _hoveredHeaderIndex = -1;

        // Data State
        private IRestockService _restockService;
        private List<Restock> _allData = new List<Restock>();

        // -------------------------------------------------------------------------
        // 2. UI CONTROLS
        // -------------------------------------------------------------------------
        private Panel _headerPanel;
        private FlowLayoutPanel _itemsPanel;
        private SearchBar _searchBar;

        public RestockTableUC()
        {
            this.Size = new Size(1200, 700);
            this.BackColor = _cBackground;
            this.Padding = new Padding(25);
            this.DoubleBuffered = true;

            InitializeLayout();
        }

        private void InitializeLayout()
        {
            SetupSearchBar();
            SetupHeaderPanel();
            SetupItemsGrid();
        }

        private void SetupSearchBar()
        {
            _searchBar = new SearchBar();
            _searchBar.Location = new Point(this.Width - 365, 15);
            _searchBar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _searchBar.SearchTextChanged += (s, searchText) =>
            {
                ApplyFilter(searchText);
            };

            this.Controls.Add(_searchBar);
        }

        private void SetupHeaderPanel()
        {
            _headerPanel = new Panel
            {
                Location = new Point(25, 75),
                Height = 45,
                Width = this.Width - 50,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.White
            };

            _headerPanel.Paint += HeaderPanel_Paint;
            _headerPanel.MouseMove += HeaderPanel_MouseMove;
            _headerPanel.MouseLeave += (s, e) => {
                _hoveredHeaderIndex = -1;
                _headerPanel.Invalidate();
            };

            this.Controls.Add(_headerPanel);
        }

        private void SetupItemsGrid()
        {
            _itemsPanel = new FlowLayoutPanel
            {
                Location = new Point(25, 120),
                Width = this.Width - 50,
                Height = this.Height - 145,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = Color.White
            };

            EnableDoubleBuffering(_itemsPanel);

            _itemsPanel.SizeChanged += (s, e) => ResizeRows();
            this.Controls.Add(_itemsPanel);

            // Trigger our custom scrollbar setup
            SetupIndigoScrollBar();
        }

        // --- NEW METHOD FOR SCROLLBAR ---
        private void SetupIndigoScrollBar()
        {
            // 1. Forcefully kill the native horizontal scrollbar
            _itemsPanel.AutoScroll = false;
            _itemsPanel.HorizontalScroll.Maximum = 0;
            _itemsPanel.HorizontalScroll.Visible = false;
            _itemsPanel.AutoScroll = true;

            // 2. Create a sleek Guna Vertical Scrollbar programmatically
            var vScroll = new Guna.UI2.WinForms.Guna2VScrollBar
            {
                BindingContainer = _itemsPanel,
                ThumbColor = _cIndigo,             // Using your existing private color variable
                HoverState = { ThumbColor = Color.DarkViolet },
                BorderRadius = 4,
                Width = 10,
                FillColor = Color.Transparent,
                Margin = new Padding(0, 5, 2, 5)
            };
        }

        // -------------------------------------------------------------------------
        // 4. DATA LOGIC (Search, Filter, Sort)
        // -------------------------------------------------------------------------

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                _restockService = ServiceFactory.GetRestockService();
                LoadData();
            }
        }

        public void LoadData()
        {
            if (_restockService == null) return;
            _restockService.RefreshRestockData();
            SetData(_restockService.GetRestockList()?.ToList());
        }

        public void SetData(List<Restock> items)
        {
            _allData = items ?? new List<Restock>();

            // Use the public property from your SearchBar to maintain state
            string currentSearch = _searchBar != null ? _searchBar.SearchText : "";
            ApplyFilter(currentSearch);
        }

        private void ApplyFilter(string query)
        {
            _itemsPanel.SuspendLayout();

            // 1. Cleanup old controls
            while (_itemsPanel.Controls.Count > 0)
            {
                var c = _itemsPanel.Controls[0];
                _itemsPanel.Controls.Remove(c);
                c.Dispose();
            }

            // 2. Filter Logic - Updated to search by Enum Display Name
            var filtered = string.IsNullOrWhiteSpace(query)
                ? _allData
                : _allData.Where(x =>
                    (x.Ingredient != null && x.Ingredient.Name.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (x.Ingredient != null && GetEnumDisplayName(x.Ingredient.Category).IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0)
                ).ToList();

            // 3. Sort Logic
            var sorted = filtered
                .OrderBy(x => GetSortPriority(x))
                .ThenBy(x => x.Ingredient?.Name ?? "")
                .ToList();

            // 4. Render
            int rowWidth = _itemsPanel.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;

            foreach (var item in sorted)
            {
                var row = new RestockRowUC(item)
                {
                    Width = rowWidth,
                    Margin = new Padding(0, 0, 0, 5)
                };
                row.RestockClicked += (s, data) => HandleRestock(data);
                _itemsPanel.Controls.Add(row);
            }

            _itemsPanel.ResumeLayout();
        }

        // --- HELPER TO ALLOW SEARCHING BY ENUM DISPLAY NAME ---
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

        private int GetSortPriority(Restock item)
        {
            if (item.Ingredient == null) return 2;

            if (item.Ingredient.CurrentStock <= 0) return 0; // Critical
            if (item.Ingredient.CurrentStock <= item.Ingredient.MinimumStock) return 1; // Low
            return 2; // OK
        }

        private void HandleRestock(Restock data)
        {
            string name = data.Ingredient?.Name ?? "Item";
            string input = Microsoft.VisualBasic.Interaction.InputBox($"Restock amount for {name}:", "Restock Inventory", "0");

            if (decimal.TryParse(input, out decimal amt) && amt > 0)
            {
                _restockService.ProcessRestock(data.RestockId, amt);
                LoadData();
            }
        }

        // -------------------------------------------------------------------------
        // 5. GRAPHICS & RENDERING
        // -------------------------------------------------------------------------

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // Draw Rounded Background Border
            Rectangle rect = this.ClientRectangle;
            rect.Inflate(-1, -1);
            using (GraphicsPath path = GetRoundedPath(rect, _borderRadius))
            using (Pen pen = new Pen(Color.LightGray, 1.5f))
            {
                e.Graphics.FillPath(Brushes.White, path);
                e.Graphics.DrawPath(pen, path);
            }

            // Draw Title
            using (Font titleFont = new Font("Poppins", 14, FontStyle.Bold))
            using (Brush titleBrush = new SolidBrush(_cIndigo))
            {
                g.DrawString("Inventory Restock Manager", titleFont, titleBrush, 30, 25);
            }
        }

        private void HeaderPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            float currentX = 0;
            float totalW = _headerPanel.Width;

            using (Pen p = new Pen(Color.FromArgb(230, 230, 230), 2))
                g.DrawLine(p, 0, _headerPanel.Height - 1, totalW, _headerPanel.Height - 1);

            using (Font font = new Font("Segoe UI", 9F, FontStyle.Bold))
            using (Brush bNormal = new SolidBrush(Color.DimGray))
            using (Brush bHover = new SolidBrush(_cIndigo))
            {
                StringFormat fmtLeft = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
                StringFormat fmtCenter = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

                for (int i = 0; i < _headers.Length; i++)
                {
                    float colW = totalW * _colWeights[i];
                    RectangleF rect = new RectangleF(currentX + 10, 0, colW - 20, _headerPanel.Height);

                    if (i == _hoveredHeaderIndex)
                    {
                        using (SolidBrush bgBrush = new SolidBrush(_cHoverHeader))
                            g.FillRectangle(bgBrush, currentX, 5, colW, _headerPanel.Height - 10);
                    }

                    g.DrawString(_headers[i], font, (i == _hoveredHeaderIndex) ? bHover : bNormal, rect, i == 0 ? fmtLeft : fmtCenter);
                    currentX += colW;
                }
            }
        }

        private void HeaderPanel_MouseMove(object sender, MouseEventArgs e)
        {
            float currentX = 0;
            int newIndex = -1;
            for (int i = 0; i < _colWeights.Length; i++)
            {
                float w = _headerPanel.Width * _colWeights[i];
                if (e.X >= currentX && e.X < currentX + w) { newIndex = i; break; }
                currentX += w;
            }
            if (newIndex != _hoveredHeaderIndex)
            {
                _hoveredHeaderIndex = newIndex;
                _headerPanel.Invalidate();
            }
        }

        // -------------------------------------------------------------------------
        // 6. HELPERS
        // -------------------------------------------------------------------------

        private void ResizeRows()
        {
            _itemsPanel.SuspendLayout();
            int w = _itemsPanel.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;
            foreach (Control c in _itemsPanel.Controls)
            {
                if (c.Width != w) c.Width = w;
            }
            _itemsPanel.ResumeLayout();
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int d = radius * 2;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        public static void EnableDoubleBuffering(Control control)
        {
            typeof(Control).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, control, new object[] { true });
        }
    }
}