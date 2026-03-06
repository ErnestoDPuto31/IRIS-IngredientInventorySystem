using FontAwesome.Sharp;
using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
using IRIS.Presentation.DependencyInjection;
using IRIS.Presentation.Helpers;
using IRIS.Presentation.UserControls.Components;
using IRIS.Services.DTOs;
using IRIS.Services.Interfaces;
using System.Drawing;
using System.Threading.Tasks;
using static Bunifu.UI.WinForms.BunifuButton.BunifuButton;

namespace IRIS.Presentation.UserControls.PagesUC
{
    public partial class ReportsControl : UserControl
    {
        private bool _layoutReady = false;
        private bool _isLoadingData = false;
        private bool _dataBound = false;

        private ReportsDashboardDto? _snapshot;
        private Task<ReportsDashboardDto>? _preloadTask;

        public ReportsControl()
        {
            InitializeComponent();
            DoubleBuffered = true;

            SetupExportButtonsUI();
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

        public void SetPreloadedTask(Task<ReportsDashboardDto> preloadTask)
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

        public async Task EnsureDataLoadedAsync()
        {
            if (_dataBound || _isLoadingData)
                return;

            var reportsService = GetReportsService();
            if (reportsService == null)
                return;

            try
            {
                _isLoadingData = true;
                UseWaitCursor = true;

                if (_snapshot == null)
                {
                    if (_preloadTask == null || _preloadTask.IsCanceled || _preloadTask.IsFaulted)
                        _preloadTask = reportsService.GetDashboardDataAsync(5);

                    _snapshot = await _preloadTask;
                }

                if (!IsDisposed && _snapshot != null)
                    BindSnapshot(_snapshot);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading reports data: " + ex.Message);
            }
            finally
            {
                UseWaitCursor = false;
                _isLoadingData = false;
            }
        }

        private void ReportsControl_Load(object sender, EventArgs e)
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
        }

        private void BindSnapshot(ReportsDashboardDto snapshot)
        {
            if (_dataBound) return;

            _dataBound = true;

            SetupCards(snapshot);
            LoadCharts(snapshot);
            LoadTable(snapshot);

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
            ApplyOutlinedExportStyle(btnExportPDF);

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
                            ?? UserSession.CurrentUser?.Role.ToString()
                            ?? "Unknown User"),
                totalIngredients: snapshot.TotalIngredients,
                totalRequests: snapshot.TotalRequests,
                totalTransactions: snapshot.TotalTransactions,
                approvalRate: snapshot.ApprovalRate,
                inventoryStats: snapshot.InventoryStats,
                requestStats: snapshot.RequestStats,
                categoryStats: snapshot.CategoryStats,
                topIngredients: snapshot.TopUsedIngredients,
                lowStock: snapshot.LowStockIngredients
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
            }

            ExportPdf.ExportAndRevealInExplorer(
                filePath: sfd.FileName,
                exportedBy: (UserSession.CurrentUser?.Role.ToString()
                            ?? UserSession.CurrentUser?.Username
                            ?? UserSession.CurrentUser?.Role.ToString()
                            ?? "Unknown User"),
                totalIngredients: snapshot.TotalIngredients,
                totalRequests: snapshot.TotalRequests,
                totalTransactions: snapshot.TotalTransactions,
                approvalRate: snapshot.ApprovalRate,
                inventoryStats: snapshot.InventoryStats,
                requestStats: snapshot.RequestStats,
                categoryStats: snapshot.CategoryStats,
                topIngredients: snapshot.TopUsedIngredients,
                lowStock: snapshot.LowStockIngredients,
                irisLogoPng: logoPng
            );
        }

        private async Task<ReportsDashboardDto?> GetSnapshotForExportAsync()
        {
            if (_snapshot != null)
                return _snapshot;

            await EnsureDataLoadedAsync();
            return _snapshot;
        }

        private void LoadTable(ReportsDashboardDto snapshot)
        {
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

        private void SetupCards(ReportsDashboardDto snapshot)
        {
            if (TotalIngredientsCard != null)
            {
                ConfigureCard(
                    TotalIngredientsCard,
                    CardType.TotalIngredients,
                    IconChar.Box,
                    snapshot.TotalIngredients.ToString());
            }

            if (TotalRequestCard != null)
            {
                ConfigureCard(
                    TotalRequestCard,
                    CardType.TotalRequests,
                    IconChar.FileAlt,
                    snapshot.TotalRequests.ToString());
            }

            if (ApprovalRateCard != null)
            {
                ConfigureCard(
                    ApprovalRateCard,
                    CardType.ApprovalRate,
                    IconChar.CheckCircle,
                    $"{snapshot.ApprovalRate}%");
            }

            if (TotalTransactionsCard != null)
            {
                ConfigureCard(
                    TotalTransactionsCard,
                    CardType.TotalTransactions,
                    IconChar.ChartLine,
                    snapshot.TotalTransactions.ToString());
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

        private void LoadCharts(ReportsDashboardDto snapshot)
        {
            try
            {
                var invStats = snapshot.InventoryStats;
                if (invStats != null && invStats.Any() && chartInventoryCanvas != null)
                {
                    chartInventoryCanvas.Labels = invStats.Keys.ToArray();

                    if (pieInventory != null)
                    {
                        pieInventory.Data = invStats.Values.ToList();
                        pieInventory.BackgroundColor = new List<Color>
                        {
                            Color.Crimson,
                            Color.Gold,
                            Color.SeaGreen
                        };
                    }

                    chartInventoryCanvas.Update();
                }

                var reqStats = snapshot.RequestStats;
                if (reqStats != null && reqStats.Any() && chartRequestsCanvas != null)
                {
                    List<string> reqLabels = new List<string>();
                    List<double> reqData = new List<double>();
                    List<Color> reqColors = new List<Color>();

                    foreach (var item in reqStats)
                    {
                        reqLabels.Add(item.Key);
                        reqData.Add(item.Value);

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

                var rawStats = snapshot.CategoryStats;
                if (rawStats != null && rawStats.Any() && chartBarCanvas != null)
                {
                    var catStats = rawStats.ToDictionary(k => k.Key, v => v.Value);

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