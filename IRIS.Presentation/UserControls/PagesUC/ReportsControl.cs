using FontAwesome.Sharp;
using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
using IRIS.Presentation.DependencyInjection;
using IRIS.Presentation.Helpers;
using IRIS.Presentation.UserControls.Components;
using IRIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static Bunifu.UI.WinForms.BunifuButton.BunifuButton;

namespace IRIS.Presentation.UserControls.PagesUC
{
    public partial class ReportsControl : UserControl
    {
        private bool _isLoading = false;

        public ReportsControl()
        {
            InitializeComponent();
            DoubleBuffered = true;

            // create + style csv/pdf buttons to match your screenshot (gray idle, purple hover)
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

        private void ReportsControl_Load(object sender, EventArgs e)
        {
            if (_isLoading) return;

            var reportsService = GetReportsService();
            if (reportsService == null) return;

            try
            {
                _isLoading = true;

                if (bunifuVScrollBar1 != null)
                {
                    bunifuVScrollBar1.Visible = false;
                    bunifuVScrollBar1.Enabled = false;
                }

                if (pnlMain != null)
                    pnlMain.AutoScroll = true;

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

        // ==========================================
        // EXPORT BUTTONS UI (gray idle like screenshot)
        // ==========================================
        private void SetupExportButtonsUI()
        {
            // make sure pnlMain exists
            if (pnlMain == null) return;

            // ensure CSV exists (designer declares it but doesn't create it)
            if (btnExportCSV == null)
            {
                btnExportCSV = new Bunifu.UI.WinForms.BunifuButton.BunifuButton
                {
                    Name = "btnExportCSV"
                };

                pnlMain.Controls.Add(btnExportCSV);
                btnExportCSV.BringToFront();
            }

            // ---- sizing (wider so text won't cut) ----
            Size btnSize = new Size(160, 36);
            int gap = 10;
            int rightPadding = 96;

            // apply size to both
            btnExportCSV.Size = btnSize;
            if (btnExportPDF != null)
                btnExportPDF.Size = btnSize;

            // ---- text ----
            btnExportCSV.ButtonText = "Export CSV";
            if (btnExportPDF != null)
                btnExportPDF.ButtonText = "Export PDF";

            // ---- icons ----
            var purple = Color.FromArgb(124, 58, 237);
            btnExportCSV.IconLeft = IconChar.Download.ToBitmap(purple, 16);
            if (btnExportPDF != null)
                btnExportPDF.IconLeft = IconChar.Download.ToBitmap(purple, 16);

            // ---- style both ----
            ApplyOutlinedExportStyle(btnExportCSV);
            ApplyOutlinedExportStyle(btnExportPDF);

            // ---- positioning (anchor as a pair to the right) ----
            int top = (btnExportPDF != null) ? btnExportPDF.Top : 89;

            if (btnExportPDF != null)
            {
                btnExportPDF.Location = new Point(pnlMain.ClientSize.Width - rightPadding - btnSize.Width, top);
                btnExportCSV.Location = new Point(btnExportPDF.Left - gap - btnSize.Width, top);
            }
            else
            {
                // fallback (shouldn't happen in your case)
                btnExportCSV.Location = new Point(pnlMain.ClientSize.Width - rightPadding - (btnSize.Width * 2) - gap, top);
            }

            // keep them from overlapping when the control/panel resizes
            this.SizeChanged -= ReportsControl_SizeChanged;
            this.SizeChanged += ReportsControl_SizeChanged;

            // hook csv click
            btnExportCSV.Click -= btnExportCSV_Click;
            btnExportCSV.Click += btnExportCSV_Click;
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

            // idle like your screenshot
            var idleFill = Color.FromArgb(248, 249, 252);
            var idleBorder = Color.FromArgb(223, 226, 233);

            // hover/pressed
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

            // smaller so "Export PDF/CSV" fits perfectly
            btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btn.ForeColor = text;

            btn.TextAlign = ContentAlignment.MiddleCenter;
            btn.TextAlignment = HorizontalAlignment.Center;

            btn.IconLeftAlign = ContentAlignment.MiddleLeft;
            btn.IconLeftPadding = new Padding(12, 3, 6, 3);
            btn.IconPadding = 5;
            btn.IconSize = 16;

            // ===== idle (gray) =====
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

            // ===== hover (purple border, light purple fill) =====
            btn.onHoverState.BorderStyle = BorderStyles.Solid;
            btn.onHoverState.BorderRadius = 14;
            btn.onHoverState.BorderThickness = 1;
            btn.onHoverState.BorderColor = purple;
            btn.onHoverState.FillColor = hoverFill;
            btn.onHoverState.ForeColor = text;
            btn.onHoverState.IconLeftImage = btn.IconLeft;

            // ===== pressed =====
            btn.OnPressedState.BorderStyle = BorderStyles.Solid;
            btn.OnPressedState.BorderRadius = 14;
            btn.OnPressedState.BorderThickness = 1;
            btn.OnPressedState.BorderColor = purple;
            btn.OnPressedState.FillColor = pressedFill;
            btn.OnPressedState.ForeColor = text;
            btn.OnPressedState.IconLeftImage = btn.IconLeft;

            // ===== disabled =====
            btn.OnDisabledState.BorderStyle = BorderStyles.Solid;
            btn.OnDisabledState.BorderRadius = 14;
            btn.OnDisabledState.BorderThickness = 1;
            btn.OnDisabledState.BorderColor = Color.FromArgb(230, 230, 230);
            btn.OnDisabledState.FillColor = Color.FromArgb(245, 245, 245);
            btn.OnDisabledState.ForeColor = Color.FromArgb(160, 160, 160);
        }

        private void btnExportCSV_Click(object sender, EventArgs e)
        {
            var reportsService = GetReportsService();
            if (reportsService == null) return;

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
                totalIngredients: reportsService.GetTotalIngredients(),
                totalRequests: reportsService.GetTotalRequests(),
                totalTransactions: reportsService.GetTotalTransactions(),
                approvalRate: reportsService.GetApprovalRate(),
                inventoryStats: reportsService.GetInventoryStats(),
                requestStats: reportsService.GetRequestStats(),
                categoryStats: reportsService.GetCategoryStats(),
                topIngredients: reportsService.GetTopUsedIngredients(5),
                lowStock: reportsService.GetLowStockIngredients()
            );
        }
        // ==========================================
        // TABLE
        // ==========================================
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

        // ==========================================
        // CARDS
        // ==========================================
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

        // ==========================================
        // CHARTS
        // ==========================================
        private void LoadCharts(IReportsService reportsService)
        {
            if (reportsService == null) return;

            try
            {
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

        // ==========================================
        // PDF EXPORT
        // ==========================================
        private void btnExportPDF_Click_1(object sender, EventArgs e)
        {
            var reportsService = GetReportsService();
            if (reportsService == null) return;

            using var sfd = new SaveFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                FileName = $"IRIS_Reports_{DateTime.Now:yyyyMMdd_HHmm}.pdf"
            };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            byte[]? logoPng = null;
            try { logoPng = ExportPdf.ImageToPngBytes(IRIS.Presentation.Properties.Resources.IRIS_Logo); } catch { }

            ExportPdf.ExportAndRevealInExplorer(
                filePath: sfd.FileName,
                exportedBy: (UserSession.CurrentUser?.Role.ToString()
                            ?? UserSession.CurrentUser?.Username
                            ?? UserSession.CurrentUser?.Role.ToString()
                            ?? "Unknown User"),
                totalIngredients: reportsService.GetTotalIngredients(),
                totalRequests: reportsService.GetTotalRequests(),
                totalTransactions: reportsService.GetTotalTransactions(),
                approvalRate: reportsService.GetApprovalRate(),
                inventoryStats: reportsService.GetInventoryStats(),
                requestStats: reportsService.GetRequestStats(),
                categoryStats: reportsService.GetCategoryStats(),
                topIngredients: reportsService.GetTopUsedIngredients(5),
                lowStock: reportsService.GetLowStockIngredients(),
                irisLogoPng: logoPng
            );
        }
    }
}