using FontAwesome.Sharp;
using IRIS.Domain.Enums;
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
        private bool _isLoading = false;

        public ReportsControl()
        {
            InitializeComponent();

            // ONLY this for smoothness - nothing else
            this.DoubleBuffered = true;
        }

        private IReportsService GetReportsService()
        {
            var svc = ServiceFactory.GetReportsService();
            if (svc == null)
            {
                MessageBox.Show(
                    "IReportsService could not be resolved. Ensure IReportsService is registered in Program.Main (DI).",
                    "Configuration Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            return svc;
        }

        private void ReportsControl_Load(object sender, EventArgs e)
        {
            if (_isLoading) return;

            var reportsService = GetReportsService();
            if (reportsService == null) return;

            try
            {
                _isLoading = true;

                // KEEP IT SIMPLE - DON'T move/reset anything!
                // Just use what the designer already created

                // Hide Bunifu scrollbar (we don't need it)
                bunifuVScrollBar1.Visible = false;
                bunifuVScrollBar1.Enabled = false;

                // Make sure panel scrolls
                pnlMain.AutoScroll = true;

                // Load data - THAT'S IT!
                SetupCards(reportsService);
                LoadCharts(reportsService);
                LoadTable(reportsService);

                var topIngredients = reportsService.GetTopUsedIngredients(5);
                if (topIngredientsControl != null)
                    topIngredientsControl.LoadData(topIngredients);
            }
            finally
            {
                _isLoading = false;
            }
        }

        private void LoadTable(IReportsService reportsService)
        {
            if (reportsService == null) return;
            try
            {
                var lowStockData = reportsService.GetLowStockIngredients();
                if (lowStockControl != null)
                    lowStockControl.LoadData(lowStockData);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading low stock: " + ex.Message);
            }
        }

        private void SetupCards(IReportsService reportsService)
        {
            if (reportsService == null) return;

            if (TotalIngredientsCard != null)
            {
                var total = reportsService.GetTotalIngredients();
                ConfigureCard(TotalIngredientsCard, CardType.TotalIngredients, IconChar.Box, total.ToString());
            }

            if (TotalRequestCard != null)
            {
                var totalRequests = reportsService.GetTotalRequests();
                ConfigureCard(TotalRequestCard, CardType.TotalRequests, IconChar.FileAlt, totalRequests.ToString());
            }

            if (ApprovalRateCard != null)
            {
                var approvalRate = reportsService.GetApprovalRate();
                ConfigureCard(ApprovalRateCard, CardType.ApprovalRate, IconChar.CheckCircle, $"{approvalRate}%");
            }

            if (TotalTransactionsCard != null)
            {
                var totalTransactions = reportsService.GetTotalTransactions();
                ConfigureCard(TotalTransactionsCard, CardType.TotalTransactions, IconChar.ChartLine, totalTransactions.ToString());
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

        private void LoadCharts(IReportsService reportsService)
        {
            if (reportsService == null) return;

            try
            {
                // INVENTORY CHART
                var invStats = reportsService.GetInventoryStats();
                if (invStats != null && invStats.Any() && chartInventoryCanvas != null)
                {
                    chartInventoryCanvas.Labels = invStats.Keys.ToArray();
                    if (pieInventory != null)
                    {
                        pieInventory.Data = invStats.Values.ToList();
                        pieInventory.BackgroundColor = new List<Color> { Color.Crimson, Color.Gold, Color.SeaGreen };
                    }
                    chartInventoryCanvas.Update();
                }

                // REQUESTS CHART
                var reqStats = reportsService.GetRequestStats();
                if (reqStats != null && reqStats.Any() && chartRequestsCanvas != null)
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
                    if (pieRequests != null)
                    {
                        pieRequests.Data = reqData;
                        pieRequests.BackgroundColor = reqColors;
                    }
                    chartRequestsCanvas.Update();
                }

                // CATEGORY CHART
                var rawStats = reportsService.GetCategoryStats();
                if (rawStats != null && rawStats.Any() && chartBarCanvas != null)
                {
                    var catStats = rawStats.ToDictionary(k => k.Key, v => (double)v.Value);

                    chartBarCanvas.Labels = catStats.Keys.ToArray();

                    if (barCategory != null)
                    {
                        barCategory.Data = catStats.Values.ToList();
                        var barColors = new List<Color>();
                        for (int i = 0; i < catStats.Count; i++)
                            barColors.Add(Color.Indigo);
                        barCategory.BackgroundColor = barColors;
                    }
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