using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using IRIS.Presentation.UserControls.Components;

namespace IRIS.Presentation.UserControls.PagesUC
{
    partial class ReportsControl
    {
        private System.ComponentModel.IContainer components = null;
        // Clean up timer when control is disposed
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {

            }
            components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportsControl));
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges2 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            pnlMain = new Panel();
            label1 = new Label();
            label2 = new Label();
            btnExportPDF = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            TotalIngredientsCard = new ReportCards();
            TotalRequestCard = new ReportCards();
            ApprovalRateCard = new ReportCards();
            TotalTransactionsCard = new ReportCards();
            chartInventoryCanvas = new Bunifu.Charts.WinForms.BunifuChartCanvas();
            chartRequestsCanvas = new Bunifu.Charts.WinForms.BunifuChartCanvas();
            chartBarCanvas = new Bunifu.Charts.WinForms.BunifuChartCanvas();
            lowStockControl = new LowStockControl();
            topIngredientsControl = new TopIngredientsTable();
            pieInventory = new Bunifu.Charts.WinForms.ChartTypes.BunifuPieChart(components);
            pieRequests = new Bunifu.Charts.WinForms.ChartTypes.BunifuPieChart(components);
            barCategory = new Bunifu.Charts.WinForms.ChartTypes.BunifuBarChart(components);
            bunifuVScrollBar1 = new Bunifu.UI.WinForms.BunifuVScrollBar();
            pnlMain.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.AutoScroll = true;
            pnlMain.Controls.Add(label1);
            pnlMain.Controls.Add(label2);
            pnlMain.Controls.Add(btnExportPDF);
            pnlMain.Controls.Add(TotalIngredientsCard);
            pnlMain.Controls.Add(TotalRequestCard);
            pnlMain.Controls.Add(ApprovalRateCard);
            pnlMain.Controls.Add(TotalTransactionsCard);
            pnlMain.Controls.Add(chartInventoryCanvas);
            pnlMain.Controls.Add(chartRequestsCanvas);
            pnlMain.Controls.Add(chartBarCanvas);
            pnlMain.Controls.Add(lowStockControl);
            pnlMain.Controls.Add(topIngredientsControl);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 0);
            pnlMain.Margin = new Padding(2);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(1600, 900);
            pnlMain.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Poppins", 24F, FontStyle.Bold);
            label1.Location = new Point(102, 34);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(836, 70);
            label1.TabIndex = 0;
            label1.Text = "REPORTS AND ANALYTICS MANAGEMENT";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Poppins", 13.8F);
            label2.ForeColor = SystemColors.ControlDarkDark;
            label2.Location = new Point(107, 95);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(504, 40);
            label2.TabIndex = 1;
            label2.Text = "Comprehensive insights and data exports";
            // 
            // btnExportPDF
            // 
            btnExportPDF.AllowAnimations = true;
            btnExportPDF.AllowMouseEffects = true;
            btnExportPDF.AllowToggling = false;
            btnExportPDF.AnimationSpeed = 200;
            btnExportPDF.AutoGenerateColors = false;
            btnExportPDF.AutoRoundBorders = false;
            btnExportPDF.AutoSizeLeftIcon = true;
            btnExportPDF.AutoSizeRightIcon = true;
            btnExportPDF.BackColor = Color.Transparent;
            btnExportPDF.BackColor1 = Color.FromArgb(234, 84, 85);
            btnExportPDF.BackgroundImage = (Image)resources.GetObject("btnExportPDF.BackgroundImage");
            btnExportPDF.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            btnExportPDF.ButtonText = "Export PDF";
            btnExportPDF.ButtonTextMarginLeft = 0;
            btnExportPDF.ColorContrastOnClick = 45;
            btnExportPDF.ColorContrastOnHover = 45;
            btnExportPDF.Cursor = Cursors.Hand;
            borderEdges2.BottomLeft = true;
            borderEdges2.BottomRight = true;
            borderEdges2.TopLeft = true;
            borderEdges2.TopRight = true;
            btnExportPDF.CustomizableEdges = borderEdges2;
            btnExportPDF.DialogResult = DialogResult.None;
            btnExportPDF.DisabledBorderColor = Color.FromArgb(191, 191, 191);
            btnExportPDF.DisabledFillColor = Color.Empty;
            btnExportPDF.DisabledForecolor = Color.Empty;
            btnExportPDF.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Pressed;
            btnExportPDF.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnExportPDF.ForeColor = Color.White;
            btnExportPDF.IconLeft = null;
            btnExportPDF.IconLeftAlign = ContentAlignment.MiddleLeft;
            btnExportPDF.IconLeftCursor = Cursors.Default;
            btnExportPDF.IconLeftPadding = new Padding(11, 3, 3, 3);
            btnExportPDF.IconMarginLeft = 11;
            btnExportPDF.IconPadding = 10;
            btnExportPDF.IconRight = null;
            btnExportPDF.IconRightAlign = ContentAlignment.MiddleRight;
            btnExportPDF.IconRightCursor = Cursors.Default;
            btnExportPDF.IconRightPadding = new Padding(3, 3, 7, 3);
            btnExportPDF.IconSize = 25;
            btnExportPDF.IdleBorderColor = Color.FromArgb(234, 84, 85);
            btnExportPDF.IdleBorderRadius = 40;
            btnExportPDF.IdleBorderThickness = 0;
            btnExportPDF.IdleFillColor = Color.FromArgb(234, 84, 85);
            btnExportPDF.IdleIconLeftImage = null;
            btnExportPDF.IdleIconRightImage = null;
            btnExportPDF.IndicateFocus = false;
            btnExportPDF.Location = new Point(1384, 89);
            btnExportPDF.Margin = new Padding(2);
            btnExportPDF.Name = "btnExportPDF";
            btnExportPDF.OnDisabledState.BorderColor = Color.FromArgb(191, 191, 191);
            btnExportPDF.OnDisabledState.BorderRadius = 1;
            btnExportPDF.OnDisabledState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            btnExportPDF.OnDisabledState.BorderThickness = 1;
            btnExportPDF.OnDisabledState.FillColor = Color.FromArgb(204, 204, 204);
            btnExportPDF.OnDisabledState.ForeColor = Color.FromArgb(168, 160, 168);
            btnExportPDF.OnDisabledState.IconLeftImage = null;
            btnExportPDF.OnDisabledState.IconRightImage = null;
            btnExportPDF.onHoverState.BorderColor = Color.FromArgb(254, 182, 146);
            btnExportPDF.onHoverState.BorderRadius = 1;
            btnExportPDF.onHoverState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            btnExportPDF.onHoverState.BorderThickness = 1;
            btnExportPDF.onHoverState.FillColor = Color.FromArgb(254, 182, 146);
            btnExportPDF.onHoverState.ForeColor = Color.White;
            btnExportPDF.onHoverState.IconLeftImage = null;
            btnExportPDF.onHoverState.IconRightImage = null;
            btnExportPDF.OnIdleState.BorderColor = Color.DodgerBlue;
            btnExportPDF.OnIdleState.BorderRadius = 1;
            btnExportPDF.OnIdleState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            btnExportPDF.OnIdleState.BorderThickness = 1;
            btnExportPDF.OnIdleState.FillColor = Color.DodgerBlue;
            btnExportPDF.OnIdleState.ForeColor = Color.White;
            btnExportPDF.OnIdleState.IconLeftImage = null;
            btnExportPDF.OnIdleState.IconRightImage = null;
            btnExportPDF.OnPressedState.BorderColor = Color.FromArgb(40, 96, 144);
            btnExportPDF.OnPressedState.BorderRadius = 1;
            btnExportPDF.OnPressedState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            btnExportPDF.OnPressedState.BorderThickness = 1;
            btnExportPDF.OnPressedState.FillColor = Color.FromArgb(40, 96, 144);
            btnExportPDF.OnPressedState.ForeColor = Color.White;
            btnExportPDF.OnPressedState.IconLeftImage = null;
            btnExportPDF.OnPressedState.IconRightImage = null;
            btnExportPDF.Size = new Size(120, 36);
            btnExportPDF.TabIndex = 12;
            btnExportPDF.TextAlign = ContentAlignment.MiddleCenter;
            btnExportPDF.TextAlignment = HorizontalAlignment.Center;
            btnExportPDF.TextMarginLeft = 0;
            btnExportPDF.TextPadding = new Padding(0);
            btnExportPDF.UseDefaultRadiusAndThickness = true;
            // 
            // TotalIngredientsCard
            // 
            TotalIngredientsCard.BackColor = Color.Transparent;
            TotalIngredientsCard.Location = new Point(110, 146);
            TotalIngredientsCard.Margin = new Padding(2);
            TotalIngredientsCard.Name = "TotalIngredientsCard";
            TotalIngredientsCard.Size = new Size(330, 130);
            TotalIngredientsCard.TabIndex = 2;
            // 
            // TotalRequestCard
            // 
            TotalRequestCard.BackColor = Color.Transparent;
            TotalRequestCard.Location = new Point(1174, 146);
            TotalRequestCard.Margin = new Padding(2);
            TotalRequestCard.Name = "TotalRequestCard";
            TotalRequestCard.Size = new Size(330, 130);
            TotalRequestCard.TabIndex = 3;
            // 
            // ApprovalRateCard
            // 
            ApprovalRateCard.BackColor = Color.Transparent;
            ApprovalRateCard.Location = new Point(470, 146);
            ApprovalRateCard.Margin = new Padding(2);
            ApprovalRateCard.Name = "ApprovalRateCard";
            ApprovalRateCard.Size = new Size(330, 130);
            ApprovalRateCard.TabIndex = 4;
            // 
            // TotalTransactionsCard
            // 
            TotalTransactionsCard.BackColor = Color.Transparent;
            TotalTransactionsCard.Location = new Point(821, 146);
            TotalTransactionsCard.Margin = new Padding(2);
            TotalTransactionsCard.Name = "TotalTransactionsCard";
            TotalTransactionsCard.Size = new Size(330, 130);
            TotalTransactionsCard.TabIndex = 5;
            // 
            // chartInventoryCanvas
            // 
            chartInventoryCanvas.AnimationDuration = 550;
            chartInventoryCanvas.AnimationType = Bunifu.Charts.WinForms.BunifuChartCanvas.AnimationOptions.easeOutQuart;
            chartInventoryCanvas.AutoRender = true;
            chartInventoryCanvas.BackColor = Color.Transparent;
            chartInventoryCanvas.CanvasPadding = new Padding(60, 10, 10, 25);
            chartInventoryCanvas.Font = new Font("Poppins", 30F, FontStyle.Bold);
            chartInventoryCanvas.Labels = null;
            chartInventoryCanvas.LegendAlignment = Bunifu.Charts.WinForms.BunifuChartCanvas.LegendAlignmentOptions.center;
            chartInventoryCanvas.LegendDisplay = true;
            chartInventoryCanvas.LegendFont = new Font("Segoe UI", 24F);
            chartInventoryCanvas.LegendForeColor = Color.DarkGray;
            chartInventoryCanvas.LegendFullWidth = true;
            chartInventoryCanvas.LegendPosition = Bunifu.Charts.WinForms.BunifuChartCanvas.PositionOptions.top;
            chartInventoryCanvas.LegendRevese = false;
            chartInventoryCanvas.LegendRTL = false;
            chartInventoryCanvas.Location = new Point(102, 299);
            chartInventoryCanvas.Margin = new Padding(4);
            chartInventoryCanvas.Name = "chartInventoryCanvas";
            chartInventoryCanvas.ShowXAxis = false;
            chartInventoryCanvas.ShowYAxis = false;
            chartInventoryCanvas.Size = new Size(679, 360);
            chartInventoryCanvas.TabIndex = 6;
            chartInventoryCanvas.Title = "Inventory Status Distribution";
            chartInventoryCanvas.TitleLineHeight = 1.2D;
            chartInventoryCanvas.TitlePadding = 10;
            chartInventoryCanvas.TitlePosition = Bunifu.Charts.WinForms.BunifuChartCanvas.PositionOptions.top;
            chartInventoryCanvas.TooltipBackgroundColor = Color.FromArgb(100, 0, 0, 0);
            chartInventoryCanvas.TooltipFont = new Font("Segoe UI", 14F);
            chartInventoryCanvas.TooltipForeColor = Color.WhiteSmoke;
            chartInventoryCanvas.TooltipMode = Bunifu.Charts.WinForms.BunifuChartCanvas.TooltipModeOptions.nearest;
            chartInventoryCanvas.TooltipsEnabled = true;
            chartInventoryCanvas.XAxesBeginAtZero = true;
            chartInventoryCanvas.XAxesDrawTicks = false;
            chartInventoryCanvas.XAxesFont = new Font("Segoe UI", 12F);
            chartInventoryCanvas.XAxesForeColor = SystemColors.ControlText;
            chartInventoryCanvas.XAxesGridColor = Color.Transparent;
            chartInventoryCanvas.XAxesGridLines = false;
            chartInventoryCanvas.XAxesLabel = "";
            chartInventoryCanvas.XAxesLabelFont = new Font("Segoe UI", 12F);
            chartInventoryCanvas.XAxesLabelForeColor = SystemColors.ControlText;
            chartInventoryCanvas.XAxesLineWidth = 0;
            chartInventoryCanvas.XAxesStacked = false;
            chartInventoryCanvas.XAxesZeroLineColor = Color.Transparent;
            chartInventoryCanvas.XAxesZeroLineWidth = 0;
            chartInventoryCanvas.YAxesBeginAtZero = true;
            chartInventoryCanvas.YAxesDrawTicks = false;
            chartInventoryCanvas.YAxesFont = new Font("Segoe UI", 12F);
            chartInventoryCanvas.YAxesForeColor = SystemColors.ControlText;
            chartInventoryCanvas.YAxesGridColor = Color.Transparent;
            chartInventoryCanvas.YAxesGridLines = false;
            chartInventoryCanvas.YAxesLabel = "";
            chartInventoryCanvas.YAxesLabelFont = new Font("Segoe UI", 12F);
            chartInventoryCanvas.YAxesLabelForeColor = SystemColors.ControlText;
            chartInventoryCanvas.YAxesLineWidth = 0;
            chartInventoryCanvas.YAxesStacked = false;
            chartInventoryCanvas.YAxesZeroLineColor = Color.Transparent;
            chartInventoryCanvas.YAxesZeroLineWidth = 0;
            // 
            // chartRequestsCanvas
            // 
            chartRequestsCanvas.AnimationDuration = 550;
            chartRequestsCanvas.AnimationType = Bunifu.Charts.WinForms.BunifuChartCanvas.AnimationOptions.easeOutQuart;
            chartRequestsCanvas.AutoRender = true;
            chartRequestsCanvas.BackColor = Color.Transparent;
            chartRequestsCanvas.CanvasPadding = new Padding(60, 10, 10, 25);
            chartRequestsCanvas.Font = new Font("Poppins", 30F, FontStyle.Bold);
            chartRequestsCanvas.Labels = null;
            chartRequestsCanvas.LegendAlignment = Bunifu.Charts.WinForms.BunifuChartCanvas.LegendAlignmentOptions.center;
            chartRequestsCanvas.LegendDisplay = true;
            chartRequestsCanvas.LegendFont = new Font("Segoe UI", 24F);
            chartRequestsCanvas.LegendForeColor = Color.DarkGray;
            chartRequestsCanvas.LegendFullWidth = true;
            chartRequestsCanvas.LegendPosition = Bunifu.Charts.WinForms.BunifuChartCanvas.PositionOptions.top;
            chartRequestsCanvas.LegendRevese = false;
            chartRequestsCanvas.LegendRTL = false;
            chartRequestsCanvas.Location = new Point(825, 300);
            chartRequestsCanvas.Margin = new Padding(4);
            chartRequestsCanvas.Name = "chartRequestsCanvas";
            chartRequestsCanvas.ShowXAxis = false;
            chartRequestsCanvas.ShowYAxis = false;
            chartRequestsCanvas.Size = new Size(679, 360);
            chartRequestsCanvas.TabIndex = 7;
            chartRequestsCanvas.Title = "Request Status Distribution";
            chartRequestsCanvas.TitleLineHeight = 1.2D;
            chartRequestsCanvas.TitlePadding = 10;
            chartRequestsCanvas.TitlePosition = Bunifu.Charts.WinForms.BunifuChartCanvas.PositionOptions.top;
            chartRequestsCanvas.TooltipBackgroundColor = Color.FromArgb(100, 0, 0, 0);
            chartRequestsCanvas.TooltipFont = new Font("Segoe UI", 14F);
            chartRequestsCanvas.TooltipForeColor = Color.WhiteSmoke;
            chartRequestsCanvas.TooltipMode = Bunifu.Charts.WinForms.BunifuChartCanvas.TooltipModeOptions.nearest;
            chartRequestsCanvas.TooltipsEnabled = true;
            chartRequestsCanvas.XAxesBeginAtZero = true;
            chartRequestsCanvas.XAxesDrawTicks = false;
            chartRequestsCanvas.XAxesFont = new Font("Segoe UI", 12F);
            chartRequestsCanvas.XAxesForeColor = SystemColors.ControlText;
            chartRequestsCanvas.XAxesGridColor = Color.Transparent;
            chartRequestsCanvas.XAxesGridLines = false;
            chartRequestsCanvas.XAxesLabel = "";
            chartRequestsCanvas.XAxesLabelFont = new Font("Segoe UI", 12F);
            chartRequestsCanvas.XAxesLabelForeColor = SystemColors.ControlText;
            chartRequestsCanvas.XAxesLineWidth = 0;
            chartRequestsCanvas.XAxesStacked = false;
            chartRequestsCanvas.XAxesZeroLineColor = Color.Transparent;
            chartRequestsCanvas.XAxesZeroLineWidth = 0;
            chartRequestsCanvas.YAxesBeginAtZero = true;
            chartRequestsCanvas.YAxesDrawTicks = false;
            chartRequestsCanvas.YAxesFont = new Font("Segoe UI", 12F);
            chartRequestsCanvas.YAxesForeColor = SystemColors.ControlText;
            chartRequestsCanvas.YAxesGridColor = Color.Transparent;
            chartRequestsCanvas.YAxesGridLines = false;
            chartRequestsCanvas.YAxesLabel = "";
            chartRequestsCanvas.YAxesLabelFont = new Font("Segoe UI", 12F);
            chartRequestsCanvas.YAxesLabelForeColor = SystemColors.ControlText;
            chartRequestsCanvas.YAxesLineWidth = 0;
            chartRequestsCanvas.YAxesStacked = false;
            chartRequestsCanvas.YAxesZeroLineColor = Color.Transparent;
            chartRequestsCanvas.YAxesZeroLineWidth = 0;
            // 
            // chartBarCanvas
            // 
            chartBarCanvas.AnimationDuration = 550;
            chartBarCanvas.AnimationType = Bunifu.Charts.WinForms.BunifuChartCanvas.AnimationOptions.easeOutQuart;
            chartBarCanvas.AutoRender = true;
            chartBarCanvas.BackColor = Color.Transparent;
            chartBarCanvas.CanvasPadding = new Padding(30, 0, 30, 110);
            chartBarCanvas.Font = new Font("Poppins", 30F, FontStyle.Bold);
            chartBarCanvas.Labels = null;
            chartBarCanvas.LegendAlignment = Bunifu.Charts.WinForms.BunifuChartCanvas.LegendAlignmentOptions.center;
            chartBarCanvas.LegendDisplay = true;
            chartBarCanvas.LegendFont = new Font("Segoe UI", 24F);
            chartBarCanvas.LegendForeColor = Color.DarkGray;
            chartBarCanvas.LegendFullWidth = true;
            chartBarCanvas.LegendPosition = Bunifu.Charts.WinForms.BunifuChartCanvas.PositionOptions.top;
            chartBarCanvas.LegendRevese = false;
            chartBarCanvas.LegendRTL = false;
            chartBarCanvas.Location = new Point(102, 643);
            chartBarCanvas.Margin = new Padding(4);
            chartBarCanvas.Name = "chartBarCanvas";
            chartBarCanvas.ShowXAxis = true;
            chartBarCanvas.ShowYAxis = true;
            chartBarCanvas.Size = new Size(1402, 520);
            chartBarCanvas.TabIndex = 8;
            chartBarCanvas.Title = "Ingredients by Category";
            chartBarCanvas.TitleLineHeight = 1.2D;
            chartBarCanvas.TitlePadding = 0;
            chartBarCanvas.TitlePosition = Bunifu.Charts.WinForms.BunifuChartCanvas.PositionOptions.top;
            chartBarCanvas.TooltipBackgroundColor = Color.FromArgb(100, 0, 0, 0);
            chartBarCanvas.TooltipFont = new Font("Segoe UI", 12F);
            chartBarCanvas.TooltipForeColor = Color.WhiteSmoke;
            chartBarCanvas.TooltipMode = Bunifu.Charts.WinForms.BunifuChartCanvas.TooltipModeOptions.nearest;
            chartBarCanvas.TooltipsEnabled = true;
            chartBarCanvas.XAxesBeginAtZero = true;
            chartBarCanvas.XAxesDrawTicks = true;
            chartBarCanvas.XAxesFont = new Font("Segoe UI", 12F);
            chartBarCanvas.XAxesForeColor = SystemColors.ControlText;
            chartBarCanvas.XAxesGridColor = Color.FromArgb(100, 0, 0, 0);
            chartBarCanvas.XAxesGridLines = true;
            chartBarCanvas.XAxesLabel = "";
            chartBarCanvas.XAxesLabelFont = new Font("Segoe UI", 12F);
            chartBarCanvas.XAxesLabelForeColor = SystemColors.ControlText;
            chartBarCanvas.XAxesLineWidth = 1;
            chartBarCanvas.XAxesStacked = false;
            chartBarCanvas.XAxesZeroLineColor = Color.FromArgb(100, 0, 0, 0);
            chartBarCanvas.XAxesZeroLineWidth = 1;
            chartBarCanvas.YAxesBeginAtZero = true;
            chartBarCanvas.YAxesDrawTicks = true;
            chartBarCanvas.YAxesFont = new Font("Segoe UI", 12F);
            chartBarCanvas.YAxesForeColor = SystemColors.ControlText;
            chartBarCanvas.YAxesGridColor = Color.FromArgb(100, 0, 0, 0);
            chartBarCanvas.YAxesGridLines = true;
            chartBarCanvas.YAxesLabel = "";
            chartBarCanvas.YAxesLabelFont = new Font("Segoe UI", 12F);
            chartBarCanvas.YAxesLabelForeColor = SystemColors.ControlText;
            chartBarCanvas.YAxesLineWidth = 1;
            chartBarCanvas.YAxesStacked = false;
            chartBarCanvas.YAxesZeroLineColor = Color.FromArgb(100, 0, 0, 0);
            chartBarCanvas.YAxesZeroLineWidth = 1;
            // 
            // lowStockControl
            // 
            lowStockControl.BackColor = Color.White;
            lowStockControl.Font = new Font("Segoe UI", 9.75F);
            lowStockControl.Location = new Point(110, 1540);
            lowStockControl.Margin = new Padding(8);
            lowStockControl.Name = "lowStockControl";
            lowStockControl.Padding = new Padding(1);
            lowStockControl.Size = new Size(1379, 360);
            lowStockControl.TabIndex = 9;
            // 
            // topIngredientsControl
            // 
            topIngredientsControl.BackColor = Color.White;
            topIngredientsControl.Font = new Font("Segoe UI", 9.75F);
            topIngredientsControl.Location = new Point(110, 1150);
            topIngredientsControl.Margin = new Padding(8);
            topIngredientsControl.Name = "topIngredientsControl";
            topIngredientsControl.Padding = new Padding(1);
            topIngredientsControl.Size = new Size(1379, 360);
            topIngredientsControl.TabIndex = 10;
            // 
            // pieInventory
            // 
            pieInventory.BackgroundColor = null;
            pieInventory.BorderColor = null;
            pieInventory.BorderWidth = 0;
            pieInventory.Data = null;
            pieInventory.HoverBackgroundColor = Color.Empty;
            pieInventory.HoverBorderColor = Color.Empty;
            pieInventory.HoverBorderWidth = 0;
            pieInventory.Label = "Products";
            pieInventory.TargetCanvas = chartInventoryCanvas;
            // 
            // pieRequests
            // 
            pieRequests.BackgroundColor = null;
            pieRequests.BorderColor = null;
            pieRequests.BorderWidth = 0;
            pieRequests.Data = null;
            pieRequests.HoverBackgroundColor = Color.Empty;
            pieRequests.HoverBorderColor = Color.Empty;
            pieRequests.HoverBorderWidth = 0;
            pieRequests.Label = "Label here";
            pieRequests.TargetCanvas = chartRequestsCanvas;
            // 
            // barCategory
            // 
            barCategory.BackgroundColor = null;
            barCategory.BorderColor = null;
            barCategory.BorderSkipped = null;
            barCategory.BorderWidth = 0;
            barCategory.Data = null;
            barCategory.HoverBackgroundColor = Color.Empty;
            barCategory.HoverBorderColor = Color.Empty;
            barCategory.HoverBorderWidth = 0;
            barCategory.Label = "Products";
            barCategory.TargetCanvas = chartBarCanvas;
            // 
            // bunifuVScrollBar1
            // 
            bunifuVScrollBar1.AllowCursorChanges = true;
            bunifuVScrollBar1.AllowHomeEndKeysDetection = false;
            bunifuVScrollBar1.AllowIncrementalClickMoves = true;
            bunifuVScrollBar1.AllowMouseDownEffects = true;
            bunifuVScrollBar1.AllowMouseHoverEffects = true;
            bunifuVScrollBar1.AllowScrollingAnimations = true;
            bunifuVScrollBar1.AllowScrollKeysDetection = true;
            bunifuVScrollBar1.AllowScrollOptionsMenu = true;
            bunifuVScrollBar1.AllowShrinkingOnFocusLost = false;
            bunifuVScrollBar1.BackgroundColor = Color.White;
            bunifuVScrollBar1.BackgroundImage = (Image)resources.GetObject("bunifuVScrollBar1.BackgroundImage");
            bunifuVScrollBar1.BindingContainer = pnlMain;
            bunifuVScrollBar1.BorderColor = Color.White;
            bunifuVScrollBar1.BorderRadius = 14;
            bunifuVScrollBar1.BorderThickness = 1;
            bunifuVScrollBar1.Dock = DockStyle.Right;
            bunifuVScrollBar1.DurationBeforeShrink = 2000;
            bunifuVScrollBar1.LargeChange = 50;
            bunifuVScrollBar1.Location = new Point(1584, 0);
            bunifuVScrollBar1.Margin = new Padding(2);
            bunifuVScrollBar1.Maximum = 100;
            bunifuVScrollBar1.Minimum = 0;
            bunifuVScrollBar1.MinimumThumbLength = 18;
            bunifuVScrollBar1.Name = "bunifuVScrollBar1";
            bunifuVScrollBar1.OnDisable.ScrollBarBorderColor = Color.Silver;
            bunifuVScrollBar1.OnDisable.ScrollBarColor = Color.Transparent;
            bunifuVScrollBar1.OnDisable.ThumbColor = Color.Silver;
            bunifuVScrollBar1.ScrollBarBorderColor = Color.White;
            bunifuVScrollBar1.ScrollBarColor = Color.White;
            bunifuVScrollBar1.ShrinkSizeLimit = 3;
            bunifuVScrollBar1.Size = new Size(16, 900);
            bunifuVScrollBar1.SmallChange = 10;
            bunifuVScrollBar1.TabIndex = 1;
            bunifuVScrollBar1.ThumbColor = Color.Gray;
            bunifuVScrollBar1.ThumbLength = 444;
            bunifuVScrollBar1.ThumbMargin = 1;
            bunifuVScrollBar1.ThumbStyle = Bunifu.UI.WinForms.BunifuVScrollBar.ThumbStyles.Inset;
            bunifuVScrollBar1.Value = 0;
            // 
            // ReportsControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(bunifuVScrollBar1);
            Controls.Add(pnlMain);
            Margin = new Padding(2);
            Name = "ReportsControl";
            Size = new Size(1600, 900);
            Load += ReportsControl_Load;
            pnlMain.ResumeLayout(false);
            pnlMain.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

        private IRIS.Presentation.UserControls.Components.ReportCards TotalRequestCard;
        private IRIS.Presentation.UserControls.Components.ReportCards TotalIngredientsCard;
        private IRIS.Presentation.UserControls.Components.ReportCards TotalTransactionsCard;
        private IRIS.Presentation.UserControls.Components.ReportCards ApprovalRateCard;

        private Bunifu.UI.WinForms.BunifuButton.BunifuButton btnExportCSV;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton btnExportPDF;

        private Bunifu.Charts.WinForms.BunifuChartCanvas chartInventoryCanvas;
        private Bunifu.Charts.WinForms.BunifuChartCanvas chartRequestsCanvas;
        private Bunifu.Charts.WinForms.BunifuChartCanvas chartBarCanvas;

        private IRIS.Presentation.UserControls.Components.LowStockControl lowStockControl;
        private IRIS.Presentation.UserControls.Components.TopIngredientsTable topIngredientsControl;
        private Bunifu.UI.WinForms.BunifuVScrollBar bunifuVScrollBar1;

        private Bunifu.Charts.WinForms.ChartTypes.BunifuPieChart pieInventory;
        private Bunifu.Charts.WinForms.ChartTypes.BunifuPieChart pieRequests;
        private Bunifu.Charts.WinForms.ChartTypes.BunifuBarChart barCategory;
    }
}