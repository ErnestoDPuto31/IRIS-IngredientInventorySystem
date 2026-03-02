using FontAwesome.Sharp;
using IRIS.Domain.Enums;
using IRIS.Presentation.UserControls.Components;
using IRIS.Services.Interfaces;
using IRIS.Presentation.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IRIS.Presentation.UserControls.PagesUC
{
    public partial class ReportsControl : UserControl
    {
        private readonly IReportsService _reportsService;

        // --- THE NUCLEAR ANTI-SMEAR OVERRIDE ---
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        public ReportsControl()
        {
            InitializeComponent();

            // Your ServiceFactory is working perfectly here!
            _reportsService = ServiceFactory.GetReportsService();

            chartInventoryCanvas.BackColor = Color.White;
            chartRequestsCanvas.BackColor = Color.White;
            chartBarCanvas.BackColor = Color.White;

            typeof(Panel).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, pnlMain, new object[] { true });
        }

        private async void ReportsControl_Load(object sender, EventArgs e)
        {
            // 🛑 THE FIX: Wrap EVERYTHING in a try/catch. 
            // This prevents the application from hard-crashing if EF Core hits a data error.
            try
            {
                int totalIngredients = await _reportsService.GetTotalIngredientsAsync();

                pnlMain.Dock = DockStyle.None;
                pnlMain.Size = new Size(this.Width + 25, this.Height);
                pnlMain.Location = new Point(0, 0);
                pnlMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

                bunifuVScrollBar1.Dock = DockStyle.Right;
                bunifuVScrollBar1.Maximum = pnlMain.DisplayRectangle.Height;
                bunifuVScrollBar1.LargeChange = pnlMain.ClientSize.Height;
                bunifuVScrollBar1.BringToFront();

                bunifuVScrollBar1.Scroll += BunifuVScrollBar1_Scroll;
                pnlMain.MouseWheel += PnlMain_MouseWheel;

                // Load all data
                await SetupCardsAsync();
                await LoadChartsAsync();
                await LoadTableAsync();

                var topIngredients = await _reportsService.GetTopUsedIngredientsAsync(5);
                if (topIngredientsControl != null)
                {
                    topIngredientsControl.LoadData(topIngredients);
                }
            }
            catch (Exception ex)
            {
                // The app survives! It will show you exactly what caused the crash.
                MessageBox.Show($"Data Load Failed:\n\n{ex.Message}", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BunifuVScrollBar1_Scroll(object sender, Bunifu.UI.WinForms.BunifuVScrollBar.ScrollEventArgs e)
        {
            int maxPanelScroll = pnlMain.VerticalScroll.Maximum - pnlMain.VerticalScroll.LargeChange;

            if (e.Value >= bunifuVScrollBar1.Maximum - bunifuVScrollBar1.LargeChange)
                pnlMain.AutoScrollPosition = new Point(0, maxPanelScroll);
            else
                pnlMain.AutoScrollPosition = new Point(0, e.Value);

            pnlMain.Refresh();
        }

        private void PnlMain_MouseWheel(object sender, MouseEventArgs e)
        {
            bunifuVScrollBar1.Maximum = pnlMain.VerticalScroll.Maximum;
            bunifuVScrollBar1.LargeChange = pnlMain.VerticalScroll.LargeChange;

            int maxPanelScroll = pnlMain.VerticalScroll.Maximum - pnlMain.VerticalScroll.LargeChange;
            int currentScroll = Math.Abs(pnlMain.AutoScrollPosition.Y);

            if (currentScroll >= maxPanelScroll - 1)
                bunifuVScrollBar1.Value = bunifuVScrollBar1.Maximum;
            else
                bunifuVScrollBar1.Value = currentScroll;

            pnlMain.Refresh();
        }

        private async Task LoadTableAsync()
        {
            try
            {
                var lowStockData = await _reportsService.GetLowStockIngredientsAsync();
                if (lowStockControl != null)
                {
                    lowStockControl.LoadData(lowStockData);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading low stock table: " + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async Task SetupCardsAsync()
        {
            try
            {
                var totalIngredients = await _reportsService.GetTotalIngredientsAsync();
                var totalRequests = await _reportsService.GetTotalRequestsAsync();
                var approvalRate = await _reportsService.GetApprovalRateAsync();
                var totalTransactions = await _reportsService.GetTotalTransactionsAsync();

                ConfigureCard(TotalIngredientsCard, CardType.TotalIngredients, IconChar.Box, totalIngredients.ToString());
                ConfigureCard(TotalRequestCard, CardType.TotalRequests, IconChar.FileAlt, totalRequests.ToString());
                ConfigureCard(ApprovalRateCard, CardType.ApprovalRate, IconChar.CheckCircle, $"{approvalRate}%");
                ConfigureCard(TotalTransactionsCard, CardType.TotalTransactions, IconChar.ChartLine, totalTransactions.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading cards: " + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ConfigureCard(ReportCards card, CardType type, IconChar icon, string value)
        {
            if (card == null) return;

            card.TypeOfCard = type;
            card.Value = value;

            Control[] foundControls = card.Controls.Find("iconPictureBox", true);
            if (foundControls.Length > 0 && foundControls[0] is PictureBox pb)
            {
                pb.Image = icon.ToBitmap(Color.White, 45);
                pb.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private async Task LoadChartsAsync()
        {
            try
            {
                chartInventoryCanvas.ShowXAxis = false;
                chartInventoryCanvas.ShowYAxis = false;
                chartRequestsCanvas.ShowXAxis = false;
                chartRequestsCanvas.ShowYAxis = false;

                // --- 1. INVENTORY CHART ---
                var invStats = await _reportsService.GetInventoryStatsAsync();
                if (invStats != null && invStats.Any())
                {
                    chartInventoryCanvas.Labels = invStats.Keys.ToArray();
                    pieInventory.Data = invStats.Values.ToList();
                    pieInventory.BackgroundColor = new List<Color> { Color.Crimson, Color.Gold, Color.SeaGreen };
                    chartInventoryCanvas.Update();
                }

                // --- 2. REQUEST CHART ---
                var reqStats = await _reportsService.GetRequestStatsAsync();
                if (reqStats != null && reqStats.Any())
                {
                    List<string> reqLabels = new List<string>();
                    List<double> reqData = new List<double>();
                    List<Color> reqColors = new List<Color>();

                    foreach (var item in reqStats)
                    {
                        reqLabels.Add(item.Key);
                        reqData.Add((double)item.Value);
                        string statusKey = item.Key ?? "";

                        if (string.Equals(statusKey, nameof(RequestStatus.Pending), StringComparison.OrdinalIgnoreCase))
                            reqColors.Add(Color.DarkOrange);
                        else if (string.Equals(statusKey, nameof(RequestStatus.Approved), StringComparison.OrdinalIgnoreCase))
                            reqColors.Add(Color.DodgerBlue);
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
                    chartRequestsCanvas.Update();
                }

                // --- 3. CATEGORY CHART ---
                var rawStats = await _reportsService.GetCategoryStatsAsync();
                if (rawStats != null && rawStats.Any())
                {
                    var catStats = rawStats.ToDictionary(k => k.Key, v => (double)v.Value);

                    int minimumSlots = 25;
                    if (catStats.Count < minimumSlots)
                    {
                        while (catStats.Count < minimumSlots)
                        {
                            catStats.Add(new string(' ', catStats.Count + 1), 0.0);
                        }
                    }

                    chartBarCanvas.Labels = catStats.Keys.ToArray();
                    barCategory.Data = catStats.Values.ToList();

                    var barColors = new List<Color>();
                    for (int i = 0; i < catStats.Count; i++) barColors.Add(Color.Indigo);
                    barCategory.BackgroundColor = barColors;
                    chartBarCanvas.Update();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading charts: " + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}