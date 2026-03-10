using FontAwesome.Sharp;
using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
using IRIS.Presentation.DependencyInjection;
using IRIS.Presentation.Helpers;
using IRIS.Presentation.UserControls.Components;
using IRIS.Services.Interfaces;
using static Bunifu.UI.WinForms.BunifuButton.BunifuButton;
using IRIS.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IRIS.Presentation.UserControls.PagesUC
{
    public partial class ReportsControl : UserControl
    {
        private bool _layoutReady = false;
        private bool _isLoadingData = false;
        private bool _dataBound = false;

        private ReportsDashboardSummary? _snapshot;
        private Task<ReportsDashboardSummary>? _preloadTask;

        public ReportsControl()
        {
            InitializeComponent();
            DoubleBuffered = true;

            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime || DesignMode)
            {
                return;
            }
            SetupExportButtonsUI();
        }

        private IReportsService? GetReportsService()
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

        public void SetPreloadedTask(Task<ReportsDashboardSummary> preloadTask)
        {
            if (_preloadTask == null)
                _preloadTask = preloadTask;
        }

        public void ResetCachedData()
        {
            _snapshot = null;
            _preloadTask = null;
            _dataBound = false;
            _isLoadingData = false;
        }

        protected override async void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (this.Visible && !this.DesignMode)
            {
                await EnsureDataLoadedAsync(true);
            }
        }

        public async Task EnsureDataLoadedAsync(bool forceReload = false)
        {
            if (_isLoadingData) return;

            var reportsService = GetReportsService();
            if (reportsService == null) return;

            try
            {
                _isLoadingData = true;
                UseWaitCursor = true;

                if (forceReload || _snapshot == null)
                {
                    ResetCachedData();
                    _preloadTask = reportsService.GetDashboardDataAsync(5);
                }

                if (_preloadTask == null || _preloadTask.IsCanceled || _preloadTask.IsFaulted)
                {
                    // Force a brand new query to the database
                    _preloadTask = reportsService.GetDashboardDataAsync(5);
                }

                _snapshot = await _preloadTask;

                if (_snapshot != null && !IsDisposed)
                {
                    BindSnapshot(_snapshot);
                }
            }
            finally
            {
                UseWaitCursor = false;
                _isLoadingData = false;
            }
        }

        private async void ReportsControl_Load(object sender, EventArgs e)
        {
            if (_layoutReady) return;

            _layoutReady = true;

            if (bunifuVScrollBar1 != null)
            {
                bunifuVScrollBar1.Visible = false;
                bunifuVScrollBar1.Enabled = false;
            }

            if (pnlMain != null)
                pnlMain.AutoScroll = true;

            await EnsureDataLoadedAsync(true);
        }

        private void BindSnapshot(ReportsDashboardSummary snapshot)
        {
            if (_dataBound || snapshot == null) return;

            _dataBound = true;

            // Setup and load data for cards, charts, and tables using the pre-loaded DTO
            SetupCards(snapshot);
            LoadCharts(snapshot);
            LoadTable(snapshot);

            // Load top ingredients into the control
            if (topIngredientsControl != null)
                topIngredientsControl.LoadData(snapshot.TopUsedIngredients);
        }

        private void SetupExportButtonsUI()
        {
            if (pnlMain == null) return;

            if (btnExportCSV == null)
            {
                btnExportCSV = new Bunifu.UI.WinForms.BunifuButton.BunifuButton
                {
                    Name = "btnExportCSV"
                };
                pnlMain.Controls.Add(btnExportCSV);
                btnExportCSV.BringToFront();
            }

            Size btnSize = new Size(160, 36);
            int gap = 10;
            int rightPadding = 96;

            btnExportCSV.Size = btnSize;
            if (btnExportPDF != null) btnExportPDF.Size = btnSize;

            btnExportCSV.ButtonText = "Export CSV";
            if (btnExportPDF != null) btnExportPDF.ButtonText = "Export PDF";

            var purple = Color.FromArgb(124, 58, 237);
            btnExportCSV.IconLeft = IconChar.Download.ToBitmap(purple, 16);
            if (btnExportPDF != null) btnExportPDF.IconLeft = IconChar.Download.ToBitmap(purple, 16);

            ApplyOutlinedExportStyle(btnExportCSV);
            if (btnExportPDF != null) ApplyOutlinedExportStyle(btnExportPDF);

            int top = (btnExportPDF != null) ? btnExportPDF.Top : 89;

            if (btnExportPDF != null)
            {
                btnExportPDF.Location = new Point(pnlMain.ClientSize.Width - rightPadding - btnSize.Width, top);
                btnExportCSV.Location = new Point(btnExportPDF.Left - gap - btnSize.Width, top);
            }
            else
            {
                btnExportCSV.Location = new Point(pnlMain.ClientSize.Width - rightPadding - (btnSize.Width * 2) - gap, top);
            }

            SizeChanged -= ReportsControl_SizeChanged;
            SizeChanged += ReportsControl_SizeChanged;

            btnExportCSV.Click -= btnExportCSV_Click;
            btnExportCSV.Click += btnExportCSV_Click;

            if (UserSession.CurrentUser?.Role == UserRole.OfficeStaff)
            {
                if (btnExportCSV != null) btnExportCSV.Visible = false;
                if (btnExportPDF != null) btnExportPDF.Visible = false;
            }
            else
            {
                if (btnExportCSV != null) btnExportCSV.Visible = true;
                if (btnExportPDF != null) btnExportPDF.Visible = true;
            }
        }

        private void ReportsControl_SizeChanged(object sender, EventArgs e)
        {
            if (pnlMain == null || btnExportCSV == null || btnExportPDF == null) return;

            int gap = 10;
            int rightPadding = 96;

            Size btnSize = btnExportPDF.Size;
            int top = btnExportPDF.Top;

            btnExportPDF.Location = new Point(pnlMain.ClientSize.Width - rightPadding - btnSize.Width, top);
            btnExportCSV.Location = new Point(btnExportPDF.Left - gap - btnSize.Width, top);
        }

        private void ApplyOutlinedExportStyle(Bunifu.UI.WinForms.BunifuButton.BunifuButton btn)
        {
            if (btn == null) return;

            var purple = Color.FromArgb(124, 58, 237);
            var idleFill = Color.FromArgb(248, 249, 252);
            var idleBorder = Color.FromArgb(223, 226, 233);
            var hoverFill = Color.FromArgb(245, 243, 255);
            var pressedFill = Color.FromArgb(237, 233, 254);
            var text = Color.FromArgb(17, 24, 39);

            btn.UseDefaultRadiusAndThickness = false;
            btn.AllowAnimations = true;
            btn.AllowMouseEffects = true;
            btn.AnimationSpeed = 160;
            btn.AutoGenerateColors = false;
            btn.AutoRoundBorders = false;

            btn.BackColor = Color.Transparent;
            btn.BackColor1 = idleFill;
            btn.BorderStyle = BorderStyles.Solid;
            btn.Cursor = Cursors.Hand;

            btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btn.ForeColor = text;

            btn.TextAlign = ContentAlignment.MiddleCenter;
            btn.TextAlignment = HorizontalAlignment.Center;

            btn.IconLeftAlign = ContentAlignment.MiddleLeft;
            btn.IconLeftPadding = new Padding(12, 3, 6, 3);
            btn.IconPadding = 5;
            btn.IconSize = 16;

            btn.IdleBorderRadius = 14;
            btn.IdleBorderThickness = 1;
            btn.IdleBorderColor = idleBorder;
            btn.IdleFillColor = idleFill;

            btn.OnIdleState.BorderStyle = BorderStyles.Solid;
            btn.OnIdleState.BorderRadius = 14;
            btn.OnIdleState.BorderThickness = 1;
            btn.OnIdleState.BorderColor = idleBorder;
            btn.OnIdleState.FillColor = idleFill;
            btn.OnIdleState.ForeColor = text;
            btn.OnIdleState.IconLeftImage = btn.IconLeft;

            btn.onHoverState.BorderStyle = BorderStyles.Solid;
            btn.onHoverState.BorderRadius = 14;
            btn.onHoverState.BorderThickness = 1;
            btn.onHoverState.BorderColor = purple;
            btn.onHoverState.FillColor = hoverFill;
            btn.onHoverState.ForeColor = text;
            btn.onHoverState.IconLeftImage = btn.IconLeft;

            btn.OnPressedState.BorderStyle = BorderStyles.Solid;
            btn.OnPressedState.BorderRadius = 14;
            btn.OnPressedState.BorderThickness = 1;
            btn.OnPressedState.BorderColor = purple;
            btn.OnPressedState.FillColor = pressedFill;
            btn.OnPressedState.ForeColor = text;
            btn.OnPressedState.IconLeftImage = btn.IconLeft;

            btn.OnDisabledState.BorderStyle = BorderStyles.Solid;
            btn.OnDisabledState.BorderRadius = 14;
            btn.OnDisabledState.BorderThickness = 1;
            btn.OnDisabledState.BorderColor = Color.FromArgb(230, 230, 230);
            btn.OnDisabledState.FillColor = Color.FromArgb(245, 245, 245);
            btn.OnDisabledState.ForeColor = Color.FromArgb(160, 160, 160);
        }

        private async void btnExportCSV_Click(object sender, EventArgs e)
        {
            var snapshot = await GetSnapshotForExportAsync();
            if (snapshot == null) return;

            using var sfd = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx",
                FileName = $"IRIS_Reports_{DateTime.Now:yyyyMMdd_HHmm}.xlsx"
            };

            if (sfd.ShowDialog() != DialogResult.OK) return;

            ExportExcel.ExportAndRevealInExplorer(
                filePath: sfd.FileName,
                exportedBy: (UserSession.CurrentUser?.Role.ToString()
                            ?? UserSession.CurrentUser?.Username
                            ?? "Unknown User"),
                totalIngredients: snapshot.TotalIngredients,
                totalRequests: snapshot.TotalRequests,
                totalTransactions: snapshot.TotalTransactions,
                approvalRate: GetCalculatedApprovalRate(snapshot),
                inventoryStats: PrepareDisplayStats(snapshot.InventoryStats),
                requestStats: NormalizeRequestStatsForDisplay(snapshot.RequestStats),
                categoryStats: PrepareDisplayStats(snapshot.CategoryStats),
                topIngredients: PrepareDisplayItems(snapshot.TopUsedIngredients),
                lowStock: PrepareDisplayItems(snapshot.LowStockIngredients)
            );
        }

        private async void btnExportPDF_Click_1(object sender, EventArgs e)
        {
            var snapshot = await GetSnapshotForExportAsync();
            if (snapshot == null) return;

            using var sfd = new SaveFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                FileName = $"IRIS_Reports_{DateTime.Now:yyyyMMdd_HHmm}.pdf"
            };

            if (sfd.ShowDialog() != DialogResult.OK) return;

            byte[]? logoPng = null;
            try
            {
                logoPng = ExportPdf.ImageToPngBytes(IRIS.Presentation.Properties.Resources.IRIS_Logo);
            }
            catch
            {
                // Ignore gracefully if logo doesn't exist or crashes
            }

            ExportPdf.ExportAndRevealInExplorer(
                filePath: sfd.FileName,
                exportedBy: (UserSession.CurrentUser?.Role.ToString()
                            ?? UserSession.CurrentUser?.Username
                            ?? "Unknown User"),
                totalIngredients: snapshot.TotalIngredients,
                totalRequests: snapshot.TotalRequests,
                totalTransactions: snapshot.TotalTransactions,
                approvalRate: GetCalculatedApprovalRate(snapshot),
                inventoryStats: PrepareDisplayStats(snapshot.InventoryStats),
                requestStats: NormalizeRequestStatsForDisplay(snapshot.RequestStats),
                categoryStats: PrepareDisplayStats(snapshot.CategoryStats),
                topIngredients: PrepareDisplayItems(snapshot.TopUsedIngredients),
                lowStock: PrepareDisplayItems(snapshot.LowStockIngredients),
                irisLogoPng: logoPng
            );
        }

        private async Task<ReportsDashboardSummary?> GetSnapshotForExportAsync()
        {
            if (_snapshot != null)
                return _snapshot;

            await EnsureDataLoadedAsync();
            return _snapshot;
        }

        // ==========================================
        // DTO SNAPSHOT METHODS (SYNC UI UPDATES)
        // ==========================================

        private void LoadTable(ReportsDashboardSummary snapshot)
        {
            if (snapshot == null) return;

            try
            {
                if (lowStockControl != null)
                    lowStockControl.LoadData(snapshot.LowStockIngredients);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading low stock: " + ex.Message);
            }
        }

        private void SetupCards(ReportsDashboardSummary snapshot)
        {
            if (snapshot == null) return;

            double approvalRate = GetCalculatedApprovalRate(snapshot);
            int totalTransactions = GetCalculatedReleasedCount(snapshot);

            if (TotalIngredientsCard != null)
                ConfigureCard(TotalIngredientsCard, CardType.TotalIngredients, IconChar.Box, snapshot.TotalIngredients.ToString());

            if (TotalRequestCard != null)
                ConfigureCard(TotalRequestCard, CardType.TotalRequests, IconChar.FileAlt, snapshot.TotalRequests.ToString());

            if (ApprovalRateCard != null)
                ConfigureCard(ApprovalRateCard, CardType.ApprovalRate, IconChar.CheckCircle, FormatWholePercent(approvalRate));

            if (TotalTransactionsCard != null)
                ConfigureCard(TotalTransactionsCard, CardType.TotalTransactions, IconChar.ChartLine, FormatWholeNumber(totalTransactions));
        }

        private void LoadCharts(ReportsDashboardSummary snapshot)
        {
            if (snapshot == null) return;

            try
            {
                ClearCharts();

                var invStats = PrepareDisplayStats(snapshot.InventoryStats);
                if (invStats.Any() && chartInventoryCanvas != null)
                {
                    chartInventoryCanvas.Labels = invStats.Keys.ToArray();
                    if (pieInventory != null)
                    {
                        pieInventory.Data = invStats.Values.ToList();
                        pieInventory.BackgroundColor = new List<Color> { Color.Crimson, Color.Gold, Color.SeaGreen };
                    }
                    chartInventoryCanvas.Update();
                }

                var reqStats = NormalizeRequestStatsForDisplay(snapshot.RequestStats);
                if (chartRequestsCanvas != null)
                {
                    var orderedLabels = new List<string> { "Approved", "Pending", "Rejected", "Released" };
                    var orderedData = new List<double>
                    {
                        ToWholeChartValue(reqStats.ContainsKey("Approved") ? reqStats["Approved"] : 0),
                        ToWholeChartValue(reqStats.ContainsKey("Pending") ? reqStats["Pending"] : 0),
                        ToWholeChartValue(reqStats.ContainsKey("Rejected") ? reqStats["Rejected"] : 0),
                        ToWholeChartValue(reqStats.ContainsKey("Released") ? reqStats["Released"] : 0)
                    };

                    chartRequestsCanvas.Labels = orderedLabels.ToArray();

                    if (pieRequests != null)
                    {
                        pieRequests.Data = new List<double>(orderedData);
                        pieRequests.BackgroundColor = new List<Color>
                        {
                            Color.SeaGreen,   // Approved
                            Color.Gold,       // Pending
                            Color.Crimson,    // Rejected
                            Color.DarkBlue    // Released
                        };
                    }

                    chartRequestsCanvas.Update();
                }

                var catStats = PrepareDisplayStats(snapshot.CategoryStats);
                if (catStats.Any() && chartBarCanvas != null)
                {
                    chartBarCanvas.Labels = catStats.Keys.ToArray();

                    if (barCategory != null)
                    {
                        barCategory.Data = catStats.Values
                            .Select(v => ToWholeChartValue(Convert.ToDouble(v)))
                            .ToList();

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

        private void ClearCharts()
        {
            try
            {
                if (chartRequestsCanvas != null)
                    chartRequestsCanvas.Labels = Array.Empty<string>();

                if (pieRequests != null)
                {
                    pieRequests.Data = new List<double>();
                    pieRequests.BackgroundColor = new List<Color>();
                }

                if (chartRequestsCanvas != null)
                {
                    chartRequestsCanvas.Update();
                    chartRequestsCanvas.Refresh();
                }

                if (chartBarCanvas != null)
                {
                    chartBarCanvas.Labels = Array.Empty<string>();
                    chartBarCanvas.Update();
                    chartBarCanvas.Refresh();
                }

                if (barCategory != null)
                {
                    barCategory.Data = new List<double>();
                    barCategory.BackgroundColor = new List<Color>();
                }
            }
            catch
            {
                // Silently bypass drawing errors on clearing
            }
        }

        private static Dictionary<string, double> NormalizeRequestStatsForDisplay(IDictionary<string, double>? rawStats)
        {
            var result = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase)
            {
                { "Approved", 0 },
                { "Pending", 0 },
                { "Rejected", 0 },
                { "Released", 0 }
            };

            if (rawStats == null)
                return result;

            foreach (var item in rawStats)
            {
                string normalizedKey = NormalizeRequestStatusKey(item.Key);

                if (!result.ContainsKey(normalizedKey))
                    result[normalizedKey] = 0;

                result[normalizedKey] += item.Value;
            }

            return result;
        }

        private static string NormalizeRequestStatusKey(string? raw)
        {
            string clean = (raw ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(clean))
                return "Unknown";

            clean = clean.Replace("_", " ").Replace("-", " ");
            clean = System.Text.RegularExpressions.Regex.Replace(clean, @"([a-z0-9])([A-Z])", "$1 $2");
            clean = System.Text.RegularExpressions.Regex.Replace(clean, @"\s+", " ").Trim().ToLowerInvariant();

            if (clean == "pending") return "Pending";
            if (clean == "approved" || clean == "approve") return "Approved";
            if (clean == "released" || clean == "release") return "Released";
            if (clean == "rejected" || clean == "reject" || clean == "declined" || clean == "denied") return "Rejected";

            return "Unknown";
        }

        private static double GetCalculatedApprovalRate(ReportsDashboardSummary snapshot)
        {
            var reqStats = NormalizeRequestStatsForDisplay(snapshot.RequestStats);

            double totalRequests = reqStats.Values.Sum();
            if (totalRequests <= 0)
                totalRequests = snapshot.TotalRequests;

            if (totalRequests <= 0)
                return 0;

            double approvedCount = 0;

            if (reqStats.TryGetValue("Approved", out double approved))
                approvedCount += approved;

            if (reqStats.TryGetValue("Released", out double released))
                approvedCount += released;

            return (approvedCount / totalRequests) * 100.0;
        }

        private static int GetCalculatedReleasedCount(ReportsDashboardSummary snapshot)
        {
            var reqStats = NormalizeRequestStatsForDisplay(snapshot.RequestStats);

            if (reqStats.TryGetValue("Released", out double released))
                return Convert.ToInt32(Math.Round(released, 0, MidpointRounding.AwayFromZero));

            return snapshot.TotalTransactions;
        }

        private static double ToWholeChartValue(double value)
        {
            return Math.Round(value, 0, MidpointRounding.AwayFromZero);
        }

        private static string FormatWholeNumber(double value)
        {
            return Math.Round(value, 0, MidpointRounding.AwayFromZero).ToString("N0");
        }

        private static string FormatWholeNumber(int value)
        {
            return value.ToString("N0");
        }

        private static string FormatWholePercent(double value)
        {
            return $"{Math.Round(value, 0, MidpointRounding.AwayFromZero):N0}%";
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

        // ==========================================
        // ASYNC FETCHING METHODS
        // ==========================================

        private async Task LoadTableAsync(IReportsService reportsService)
        {
            if (reportsService == null) return;

            try
            {
                var lowStockData = await reportsService.GetLowStockIngredientsAsync();
                if (lowStockControl != null)
                    lowStockControl.LoadData(lowStockData);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading low stock: " + ex.Message);
            }
        }

        private async Task SetupCardsAsync(IReportsService reportsService)
        {
            if (reportsService == null) return;

            if (TotalIngredientsCard != null)
            {
                var total = await reportsService.GetTotalIngredientsAsync();
                ConfigureCard(TotalIngredientsCard, CardType.TotalIngredients, IconChar.Box, total.ToString());
            }

            if (TotalRequestCard != null)
            {
                var totalRequests = await reportsService.GetTotalRequestsAsync();
                ConfigureCard(TotalRequestCard, CardType.TotalRequests, IconChar.FileAlt, totalRequests.ToString());
            }

            if (ApprovalRateCard != null)
            {
                var approvalRate = await reportsService.GetApprovalRateAsync();
                ConfigureCard(ApprovalRateCard, CardType.ApprovalRate, IconChar.CheckCircle, $"{approvalRate}%");
            }

            if (TotalTransactionsCard != null)
            {
                var totalTransactions = await reportsService.GetTotalTransactionsAsync();
                ConfigureCard(TotalTransactionsCard, CardType.TotalTransactions, IconChar.ChartLine, totalTransactions.ToString());
            }
        }

        private async Task LoadChartsAsync(IReportsService reportsService)
        {
            if (reportsService == null) return;

            try
            {
                var invStats = await reportsService.GetInventoryStatsAsync();
                if (invStats != null && invStats.Any() && chartInventoryCanvas != null)
                {
                    chartInventoryCanvas.Labels = invStats.Keys.ToArray();
                    if (pieInventory != null)
                    {
                        pieInventory.Data = invStats.Values.Select(v => Convert.ToDouble(v)).ToList();
                        pieInventory.BackgroundColor = new List<Color> { Color.Crimson, Color.Gold, Color.SeaGreen };
                    }
                    chartInventoryCanvas.Update();
                }

                var reqStats = await reportsService.GetRequestStatsAsync();
                if (reqStats != null && reqStats.Any() && chartRequestsCanvas != null)
                {
                    List<string> reqLabels = new List<string>();
                    List<double> reqData = new List<double>();
                    List<Color> reqColors = new List<Color>();

                    foreach (var item in reqStats)
                    {
                        reqLabels.Add(item.Key);
                        reqData.Add(Convert.ToDouble(item.Value));

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

                var catStats = await reportsService.GetCategoryStatsAsync();
                if (catStats != null && catStats.Any() && chartBarCanvas != null)
                {
                    chartBarCanvas.Labels = catStats.Keys.ToArray();

                    if (barCategory != null)
                    {
                        // Safely convert values to double for the chart data
                        barCategory.Data = catStats.Values.Select(v => Convert.ToDouble(v)).ToList();

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

        // ==========================================
        // EXPORT FORMATTING HELPERS
        // ==========================================

        private Dictionary<string, double> PrepareDisplayStats<TValue>(IDictionary<string, TValue>? rawStats)
        {
            var result = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);
            if (rawStats == null) return result;

            foreach (var kvp in rawStats)
            {
                string displayKey = FormatDisplayText(kvp.Key);

                double value = Convert.ToDouble(kvp.Value);

                if (result.ContainsKey(displayKey))
                    result[displayKey] += value;
                else
                    result[displayKey] = value;
            }

            return result;
        }

        private static string FormatDisplayText(string? rawText)
        {
            if (string.IsNullOrWhiteSpace(rawText))
                return string.Empty;

            string clean = rawText.Trim().Replace("_", " ").Replace("-", " ");

            clean = System.Text.RegularExpressions.Regex.Replace(clean, @"([a-z0-9])([A-Z])", "$1 $2");
            return System.Text.RegularExpressions.Regex.Replace(clean, @"\s+", " ").Trim();
        }

        private List<T> PrepareDisplayItems<T>(IEnumerable<T>? items) where T : class
        {
            var result = new List<T>();
            if (items == null) return result;

            // If the collection is just strings, handle it specifically
            if (typeof(T) == typeof(string))
            {
                foreach (var item in items)
                {
                    if (item is string strItem)
                    {
                        result.Add((T)(object)FormatDisplayText(strItem));
                    }
                }
                return result;
            }

            // Otherwise, it's a DTO. Process string properties dynamically
            var properties = typeof(T).GetProperties()
                .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string))
                .ToList();

            foreach (var item in items)
            {
                if (item == null) continue;

                T clonedItem = (T)item.GetType().GetMethod("MemberwiseClone", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)?.Invoke(item, null) ?? item;

                foreach (var prop in properties)
                {
                    string? currentValue = prop.GetValue(clonedItem) as string;
                    if (!string.IsNullOrWhiteSpace(currentValue))
                    {
                        if (prop.Name.Equals("Status", StringComparison.OrdinalIgnoreCase) ||
                            prop.Name.Equals("RequestStatus", StringComparison.OrdinalIgnoreCase))
                        {
                            prop.SetValue(clonedItem, NormalizeRequestStatusKey(currentValue));
                        }
                        else
                        {
                            prop.SetValue(clonedItem, FormatDisplayText(currentValue));
                        }
                    }
                }

                result.Add(clonedItem);
            }

            return result;
        }
    }
}