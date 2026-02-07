using IRIS.Domain.Entities;
using IRIS.Infrastructure.Data;      
using IRIS.Presentation.UserControls.Components;
using IRIS.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace IRIS.Presentation.UserControls.Table
{
    public class RequestTableUC : UserControl
    {

        private readonly Color _cIndigo = Color.FromArgb(75, 0, 130);
        private readonly Color _cBackground = Color.White;
        private readonly Color _cHoverHeader = Color.FromArgb(245, 240, 255);

        private readonly float[] _colWeights = { 0.20f, 0.18f, 0.12f, 0.10f, 0.15f, 0.12f, 0.13f };
        private readonly string[] _headers = { "Subject", "Faculty", "Date of Use", "Students", "Submitted", "Status", "" };

        private int _borderRadius = 15;
        private int _hoveredHeaderIndex = -1;

        private IrisDbContext _context;
        private RequestService _requestService;
        private List<Request> _allData = new List<Request>();

        private Panel _headerPanel;
        private FlowLayoutPanel _itemsPanel;
        private SearchBar _searchBar;

        public RequestTableUC()
        {
            this.Size = new Size(1200, 700);
            this.BackColor = _cBackground;
            this.Padding = new Padding(25);
            this.DoubleBuffered = true;

            InitializeLayout();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                try
                {
                    var optionsBuilder = new DbContextOptionsBuilder<IrisDbContext>();
                    optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=IRIS_DB;Trusted_Connection=True;");

                    _context = new IrisDbContext(optionsBuilder.Options);
                    _requestService = new RequestService(_context);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Connection Error: {ex.Message}");
                }
            }
        }

        private void InitializeLayout()
        {
            SetupSearchBar();
            SetupHeaderPanel();
            SetupItemsGrid();
        }


        public void LoadData()
        {
            if (_requestService == null) return;

            var data = _requestService.GetAllRequests();
            SetData(data);
        }

        public void SetData(List<Request> items)
        {
            _allData = items ?? new List<Request>();

            string currentSearch = _searchBar != null ? _searchBar.SearchText : "";
            ApplyFilter(currentSearch);
        }

        private void ApplyFilter(string query)
        {
            _itemsPanel.SuspendLayout();

            while (_itemsPanel.Controls.Count > 0)
            {
                var c = _itemsPanel.Controls[0];
                _itemsPanel.Controls.Remove(c);
                c.Dispose();
            }

            var filtered = string.IsNullOrWhiteSpace(query)
                ? _allData
                : _allData.Where(x =>
                    (x.Subject != null && x.Subject.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (x.FacultyName != null && x.FacultyName.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0)
                ).ToList();

            int rowWidth = _itemsPanel.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;

            foreach (var item in filtered)
            {
                var row = new RequestRowUC(item)
                {
                    Width = rowWidth
                };
                row.ViewClicked += Row_ViewClicked;
                _itemsPanel.Controls.Add(row);
            }

            _itemsPanel.ResumeLayout();
        }

        private void Row_ViewClicked(object sender, int requestId)
        {
            MessageBox.Show($"Opening Request Details ID: {requestId}");
        }


        private void SetupSearchBar()
        {
            _searchBar = new SearchBar();
            _searchBar.Location = new Point(this.Width - 365, 15);
            _searchBar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _searchBar.SearchTextChanged += (s, searchText) => ApplyFilter(searchText);
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
            _headerPanel.MouseLeave += (s, e) => { _hoveredHeaderIndex = -1; _headerPanel.Invalidate(); };

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
                AutoScroll = true,
                BackColor = Color.White
            };

            EnableDoubleBuffering(_itemsPanel);
            _itemsPanel.SizeChanged += (s, e) => ResizeRows();
            this.Controls.Add(_itemsPanel);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = this.ClientRectangle;
            rect.Inflate(-1, -1);
            using (GraphicsPath path = GetRoundedPath(rect, _borderRadius))
            using (Pen pen = new Pen(Color.LightGray, 1.5f))
            {
                e.Graphics.FillPath(Brushes.White, path);
                e.Graphics.DrawPath(pen, path);
            }

            using (Font titleFont = new Font("Segoe UI", 14, FontStyle.Bold)) 
            using (Brush titleBrush = new SolidBrush(_cIndigo))
            {
                g.DrawString("Laboratory Requests", titleFont, titleBrush, 30, 25);
            }
        }

        private void HeaderPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            float currentX = 0;
            float totalW = _headerPanel.Width;

            using (Pen p = new Pen(Color.FromArgb(230, 230, 230), 2))
                g.DrawLine(p, 0, _headerPanel.Height - 1, totalW, _headerPanel.Height - 1);

            using (Font font = new Font("Segoe UI", 9F, FontStyle.Bold))
            using (Brush bNormal = new SolidBrush(Color.DimGray))
            using (Brush bHover = new SolidBrush(_cIndigo))
            {
                StringFormat fmt = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                StringFormat fmtLeft = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };

                for (int i = 0; i < _headers.Length; i++)
                {
                    float colW = totalW * _colWeights[i];
                    RectangleF rect = new RectangleF(currentX + 10, 0, colW - 20, _headerPanel.Height);

                    if (i == _hoveredHeaderIndex)
                    {
                        using (SolidBrush bgBrush = new SolidBrush(_cHoverHeader))
                            g.FillRectangle(bgBrush, currentX, 5, colW, _headerPanel.Height - 10);
                    }
                    var align = (i <= 1) ? fmtLeft : fmt;

                    g.DrawString(_headers[i], font, (i == _hoveredHeaderIndex) ? bHover : bNormal, rect, align);
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

        private void ResizeRows()
        {
            _itemsPanel.SuspendLayout();
            int w = _itemsPanel.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;
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

        public static void EnableDoubleBuffering(Control control)
        {
            typeof(Control).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, control, new object[] { true });
        }
    }
}