using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using IRIS.Domain.Entities;
using IRIS.Services.Interfaces;
using IRIS.Presentation.DependencyInjection;
using IRIS.Presentation.UserControls.Components; // Ensure this matches your namespace

namespace IRIS.Presentation.UserControls.Table
{
    public class RestockTableUC : UserControl
    {
        private IRestockService _restockService;
        private Panel _headerPanel;
        private FlowLayoutPanel _itemsPanel;
        private IrisSearchBar _searchBar;
        private List<Restock> _allData = new List<Restock>(); // Master list for filtering

        private readonly float[] _colWeights = { 0.18f, 0.12f, 0.14f, 0.14f, 0.16f, 0.10f, 0.16f };
        private readonly string[] _headers = { "Ingredient", "Category", "Current Stock", "Min Threshold", "Suggested", "Status", "Action" };

        private Color _cIndigo = Color.FromArgb(75, 0, 130);
        private int _borderRadius = 15;
        private int _hoveredHeaderIndex = -1;

        public RestockTableUC()
        {
            this.Size = new Size(1200, 700);
            this.BackColor = Color.White;
            this.Padding = new Padding(25);
            this.DoubleBuffered = true;

            BuildLayout();
        }

        private void BuildLayout()
        {
            // 1. Setup Search Bar
            _searchBar = new IrisSearchBar
            {
                Location = new Point(this.Width - 350, 20),
                Width = 300,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            _searchBar.TextChanged += (s, e) => ApplyFilter(_searchBar.Text);
            this.Controls.Add(_searchBar);

            // 2. Setup Header Panel
            _headerPanel = new Panel
            {
                Location = new Point(25, 70),
                Height = 45,
                Width = this.Width - 50,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.White
            };

            _headerPanel.Paint += HeaderPanel_Paint;
            _headerPanel.MouseMove += HeaderPanel_MouseMove;
            _headerPanel.MouseLeave += (s, e) => { _hoveredHeaderIndex = -1; _headerPanel.Invalidate(); };
            _headerPanel.Resize += (s, e) => _headerPanel.Invalidate();

            this.Controls.Add(_headerPanel);

            // 3. Setup Items Panel
            _itemsPanel = new FlowLayoutPanel
            {
                Location = new Point(25, 115),
                Width = this.Width - 50,
                Height = this.Height - 140,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.White
            };
            _itemsPanel.SizeChanged += (s, e) => ResizeRows();
            this.Controls.Add(_itemsPanel);
        }

        // -------------------------------------------------------------------------
        // BACKEND: DATA LOADING & FILTERING LOGIC
        // -------------------------------------------------------------------------

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                try
                {
                    _restockService = ServiceFactory.GetRestockService();
                    if (_restockService != null)
                    {
                        _restockService.RefreshRestockData();
                        LoadData();
                    }
                }
                catch { }
            }
        }

        public void LoadData()
        {
            if (_restockService == null) return;
            // Store results in master list so searching is instant
            _allData = _restockService.GetRestockList().ToList();
            ApplyFilter(_searchBar.Text);
        }

        private void ApplyFilter(string query)
        {
            if (_allData == null) return;

            // Filter by Name or Category
            var filtered = string.IsNullOrWhiteSpace(query)
                ? _allData
                : _allData.Where(x =>
                    x.IngredientName.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    x.Category.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0
                ).ToList();

            SetData(filtered);
        }

        private int GetSortPriority(Restock item)
        {
            if (item.CurrentStock <= 0) return 0; // Empty
            if (item.CurrentStock <= item.MinimumThreshold) return 1; // Low
            return 2; // Stocked
        }

        public void SetData(List<Restock> items)
        {
            _itemsPanel.SuspendLayout();

            // Re-apply sorting to the filtered list
            var sortedItems = items
                .OrderBy(x => GetSortPriority(x))
                .ThenBy(x => x.IngredientName)
                .ToList();

            while (_itemsPanel.Controls.Count > 0)
            {
                var c = _itemsPanel.Controls[0];
                _itemsPanel.Controls.Remove(c);
                c.Dispose();
            }

            foreach (var item in sortedItems)
            {
                var row = new RestockRowUC(item);
                row.Width = _itemsPanel.ClientSize.Width - (SystemInformation.VerticalScrollBarWidth);

                row.RestockClicked += (s, data) =>
                {
                    string input = Microsoft.VisualBasic.Interaction.InputBox(
                        $"Restock amount for {data.IngredientName}:", "Restock", "0");

                    if (decimal.TryParse(input, out decimal amt) && amt > 0)
                    {
                        _restockService.ProcessRestock(data.RestockId, amt);
                        LoadData(); // Reloads master list and re-filters
                    }
                };

                _itemsPanel.Controls.Add(row);
            }

            _itemsPanel.ResumeLayout();
        }

        // -------------------------------------------------------------------------
        // FRONTEND: PAINTING & UI METHODS
        // -------------------------------------------------------------------------

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Rectangle rect = this.ClientRectangle;
            rect.Inflate(-1, -1);

            using (GraphicsPath path = GetRoundedPath(rect, _borderRadius))
            using (Pen pen = new Pen(Color.LightGray, 1.5f))
            {
                e.Graphics.FillPath(Brushes.White, path);
                e.Graphics.DrawPath(pen, path);
            }

            string title = "Items Requiring Restock";
            using (Font titleFont = new Font("Segoe UI", 14, FontStyle.Bold))
            using (Brush titleBrush = new SolidBrush(_cIndigo))
            {
                g.DrawString(title, titleFont, titleBrush, 30, 25);
            }
        }

        private void HeaderPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            using (Pen p = new Pen(Color.FromArgb(230, 230, 230), 2))
                g.DrawLine(p, 0, _headerPanel.Height - 1, _headerPanel.Width, _headerPanel.Height - 1);

            float currentX = 0;
            float totalW = _headerPanel.Width;

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
                    Brush activeBrush = (i == _hoveredHeaderIndex) ? bHover : bNormal;

                    if (i == _hoveredHeaderIndex)
                    {
                        using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(245, 240, 255)))
                            g.FillRectangle(bgBrush, currentX, 5, colW, _headerPanel.Height - 10);
                    }

                    g.DrawString(_headers[i], font, activeBrush, rect, i == 0 ? fmtLeft : fmtCenter);
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
            if (newIndex != _hoveredHeaderIndex) { _hoveredHeaderIndex = newIndex; _headerPanel.Invalidate(); }
        }

        private void ResizeRows()
        {
            _itemsPanel.SuspendLayout();
            int w = _itemsPanel.ClientSize.Width - (SystemInformation.VerticalScrollBarWidth);
            foreach (Control c in _itemsPanel.Controls) { if (c.Width != w) c.Width = w; }
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
    }
}