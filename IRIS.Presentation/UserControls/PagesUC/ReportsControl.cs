using FontAwesome.Sharp;
using IRIS.Domain.Enums; // This is needed for your RequestStatus Enum
using IRIS.Presentation.UserControls.Components;
using IRIS.Services.Interfaces;
using IRIS.Presentation.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace IRIS.Presentation.UserControls.PagesUC
{
    public partial class ReportsControl : UserControl
    {
        private readonly IReportsService _reportsService;

        public ReportsControl()
        {
            InitializeComponent();
            _reportsService = ServiceFactory.GetReportsService();
        }


        private void ReportsControl_Load(object sender, EventArgs e)
        {
            SetupCards();
            LoadCharts();
            LoadTable();
            // Fetch the top 5 ingredients
            var topIngredients = _reportsService.GetTopUsedIngredients(5);

            // Pass them into your UI control
            topIngredientsControl.LoadData(topIngredients);
            // The Off-Screen Push
            pnlMain.Dock = DockStyle.None;
            pnlMain.Size = new Size(this.Width + 25, this.Height);
            pnlMain.Location = new Point(0, 0);
            pnlMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // --- NEW FIXES FOR THE "LITTLE" SCROLLBAR ---

            bunifuVScrollBar1.Dock = DockStyle.Right;

            bunifuVScrollBar1.Maximum = pnlMain.DisplayRectangle.Height;

            bunifuVScrollBar1.LargeChange = pnlMain.ClientSize.Height;

            bunifuVScrollBar1.BringToFront();

            // Wire up events
            bunifuVScrollBar1.Scroll += BunifuVScrollBar1_Scroll;
            pnlMain.MouseWheel += PnlMain_MouseWheel;
        }

        private void BunifuVScrollBar1_Scroll(object sender, Bunifu.UI.WinForms.BunifuVScrollBar.ScrollEventArgs e)
        {
            int maxPanelScroll = pnlMain.VerticalScroll.Maximum - pnlMain.VerticalScroll.LargeChange;

            if (e.Value >= bunifuVScrollBar1.Maximum - bunifuVScrollBar1.LargeChange)
            {
                pnlMain.AutoScrollPosition = new Point(0, maxPanelScroll);
            }
            else
            {
                pnlMain.AutoScrollPosition = new Point(0, e.Value);
            }
        }

        private void PnlMain_MouseWheel(object sender, MouseEventArgs e) {
            bunifuVScrollBar1.Maximum = pnlMain.VerticalScroll.Maximum;
            bunifuVScrollBar1.LargeChange = pnlMain.VerticalScroll.LargeChange;

            int maxPanelScroll = pnlMain.VerticalScroll.Maximum - pnlMain.VerticalScroll.LargeChange;
            int currentScroll = Math.Abs(pnlMain.AutoScrollPosition.Y);

            if (currentScroll >= maxPanelScroll - 1)
            {
                bunifuVScrollBar1.Value = bunifuVScrollBar1.Maximum;
            }
            else
            {
                bunifuVScrollBar1.Value = currentScroll;
            }
        }

        private void LoadTable()
        {
            try
            {
                var lowStockData = _reportsService.GetLowStockIngredients();

                if (lowStockControl != null)
                {
                    lowStockControl.LoadData(lowStockData);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading low stock: " + ex.Message);
            }
        }
        private void SetupCards()
        {
            ConfigureCard(TotalIngredientsCard, CardType.TotalIngredients,
                          IconChar.Box,
                          _reportsService.GetTotalIngredients().ToString());

            ConfigureCard(TotalRequestCard, CardType.TotalRequests,
                          IconChar.FileAlt,
                          _reportsService.GetTotalRequests().ToString());

            ConfigureCard(ApprovalRateCard, CardType.ApprovalRate,
                          IconChar.CheckCircle,
                          $"{_reportsService.GetApprovalRate()}%");

            ConfigureCard(TotalTransactionsCard, CardType.TotalTransactions, IconChar.ChartLine,
                          _reportsService.GetTotalTransactions().ToString());
        }

        private void ConfigureCard(ReportCards card, CardType type, IconChar icon, string value)
        {
            card.TypeOfCard = type;
            card.Value = value;

            Control[] foundControls = card.Controls.Find("iconPictureBox", true);
            if (foundControls.Length > 0 && foundControls[0] is PictureBox pb)
            {
                pb.Image = icon.ToBitmap(Color.White, 45);
                pb.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void LoadCharts()
        {
            try
            {
                // --- 1. INVENTORY CHART ---
                var invStats = _reportsService.GetInventoryStats();

                // Debugging Check: If database is empty, graph stays empty
                if (invStats.Any())
                {
                    chartInventoryCanvas.Labels = invStats.Keys.ToArray();
                    pieInventory.Data = invStats.Values.ToList();
                    pieInventory.BackgroundColor = new List<Color> { Color.Crimson, Color.Gold, Color.SeaGreen };

                    // *** THE FIX: Force the canvas to render ***
                    chartInventoryCanvas.Update();
                }

                // --- 2. REQUEST CHART ---
                var reqStats = _reportsService.GetRequestStats();

                if (reqStats.Any())
                {
                    List<string> reqLabels = new List<string>();
                    List<double> reqData = new List<double>();
                    List<Color> reqColors = new List<Color>();

                    foreach (var item in reqStats)
                    {
                        reqLabels.Add(item.Key);
                        reqData.Add((double)item.Value);

                        string statusKey = item.Key;

                        if (string.Equals(statusKey, nameof(RequestStatus.Pending), StringComparison.OrdinalIgnoreCase))
                            reqColors.Add(Color.Gold);
                        else if (string.Equals(statusKey, nameof(RequestStatus.Approved), StringComparison.OrdinalIgnoreCase))
                            reqColors.Add(Color.SeaGreen);
                        else if (string.Equals(statusKey, nameof(RequestStatus.Released), StringComparison.OrdinalIgnoreCase))
                            reqColors.Add(Color.DarkBlue);
                        else if (string.Equals(statusKey, nameof(RequestStatus.Rejected), StringComparison.OrdinalIgnoreCase))
                            reqColors.Add(Color.Crimson);
                        else
                            reqColors.Add(Color.Indigo);
                    }

                    chartRequestsCanvas.Labels = reqLabels.ToArray();
                    pieRequests.Data = reqData;
                    pieRequests.BackgroundColor = reqColors;

                    // *** THE FIX: Force the canvas to render ***
                    chartRequestsCanvas.Update();
                }

                // --- 3. CATEGORY CHART ---
                var rawStats = _reportsService.GetCategoryStats();

                if (rawStats.Any())
                {
                    var catStats = rawStats.ToDictionary(k => k.Key, v => (double)v.Value);

                    // Your thin-bar logic
                    int minimumSlots = 25;
                    if (catStats.Count < minimumSlots)
                    {
                        while (catStats.Count < minimumSlots)
                        {
                            // Ensure unique keys for the spacing
                            catStats.Add(new string(' ', catStats.Count + 1), 0.0);
                        }
                    }

                    chartBarCanvas.Labels = catStats.Keys.ToArray();
                    barCategory.Data = catStats.Values.ToList();

                    var barColors = new List<Color>();
                    for (int i = 0; i < catStats.Count; i++) barColors.Add(Color.Indigo);
                    barCategory.BackgroundColor = barColors;

                    // *** THE FIX: Force the canvas to render ***
                    chartBarCanvas.Update();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading charts: " + ex.Message);
            }
        }
    }
}