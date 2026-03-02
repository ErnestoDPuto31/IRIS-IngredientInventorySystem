using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using IRIS.Presentation.UserControls.Components;

namespace IRIS.Presentation.UserControls.PagesUC
{
    partial class ReportsControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportsControl));
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges2 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();

            this.pnlMain = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TotalIngredientsCard = new IRIS.Presentation.UserControls.Components.ReportCards();
            this.TotalRequestCard = new IRIS.Presentation.UserControls.Components.ReportCards();
            this.ApprovalRateCard = new IRIS.Presentation.UserControls.Components.ReportCards();
            this.TotalTransactionsCard = new IRIS.Presentation.UserControls.Components.ReportCards();

            this.btnExportCSV = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.btnExportPDF = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();

            this.chartInventoryCanvas = new Bunifu.Charts.WinForms.BunifuChartCanvas();
            this.chartRequestsCanvas = new Bunifu.Charts.WinForms.BunifuChartCanvas();
            this.chartBarCanvas = new Bunifu.Charts.WinForms.BunifuChartCanvas();
            this.lowStockControl = new IRIS.Presentation.UserControls.Components.LowStockControl();
            this.topIngredientsControl = new IRIS.Presentation.UserControls.Components.TopIngredientsTable();
            this.pieInventory = new Bunifu.Charts.WinForms.ChartTypes.BunifuPieChart(this.components);
            this.pieRequests = new Bunifu.Charts.WinForms.ChartTypes.BunifuPieChart(this.components);
            this.barCategory = new Bunifu.Charts.WinForms.ChartTypes.BunifuBarChart(this.components);
            this.bunifuVScrollBar1 = new Bunifu.UI.WinForms.BunifuVScrollBar();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.AutoScroll = true;
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.btnExportCSV);
            this.pnlMain.Controls.Add(this.btnExportPDF);
            this.pnlMain.Controls.Add(this.TotalIngredientsCard);
            this.pnlMain.Controls.Add(this.TotalRequestCard);
            this.pnlMain.Controls.Add(this.ApprovalRateCard);
            this.pnlMain.Controls.Add(this.TotalTransactionsCard);
            this.pnlMain.Controls.Add(this.chartInventoryCanvas);
            this.pnlMain.Controls.Add(this.chartRequestsCanvas);
            this.pnlMain.Controls.Add(this.chartBarCanvas);
            this.pnlMain.Controls.Add(this.lowStockControl);
            this.pnlMain.Controls.Add(this.topIngredientsControl);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(2);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(2000, 1125);
            this.pnlMain.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Poppins", 24F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(127, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1002, 84);
            this.label1.TabIndex = 0;
            this.label1.Text = "REPORTS AND ANALYTICS MANAGEMENT";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Poppins", 13.8F);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(133, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(607, 50);
            this.label2.TabIndex = 1;
            this.label2.Text = "Comprehensive insights and data exports";
            // 
            // btnExportCSV
            // 
            this.btnExportCSV.AllowAnimations = true;
            this.btnExportCSV.AllowMouseEffects = true;
            this.btnExportCSV.AllowToggling = false;
            this.btnExportCSV.AnimationSpeed = 200;
            this.btnExportCSV.AutoGenerateColors = false;
            this.btnExportCSV.AutoRoundBorders = false;
            this.btnExportCSV.AutoSizeLeftIcon = true;
            this.btnExportCSV.AutoSizeRightIcon = true;
            this.btnExportCSV.BackColor = System.Drawing.Color.Transparent;
            this.btnExportCSV.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(84)))), ((int)(((byte)(85)))));
            this.btnExportCSV.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExportCSV.BackgroundImage")));
            this.btnExportCSV.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnExportCSV.ButtonText = "Export CSV";
            this.btnExportCSV.ButtonTextMarginLeft = 0;
            this.btnExportCSV.ColorContrastOnClick = 45;
            this.btnExportCSV.ColorContrastOnHover = 45;
            this.btnExportCSV.Cursor = System.Windows.Forms.Cursors.Hand;
            borderEdges1.BottomLeft = true;
            borderEdges1.BottomRight = true;
            borderEdges1.TopLeft = true;
            borderEdges1.TopRight = true;
            this.btnExportCSV.CustomizableEdges = borderEdges1;
            this.btnExportCSV.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnExportCSV.ForeColor = System.Drawing.Color.White;
            this.btnExportCSV.Location = new System.Drawing.Point(1450, 360);
            this.btnExportCSV.Name = "btnExportCSV";
            this.btnExportCSV.Size = new System.Drawing.Size(150, 45);
            this.btnExportCSV.TabIndex = 11;
            this.btnExportCSV.Visible = true;
            this.btnExportCSV.IdleBorderRadius = 40;
            this.btnExportCSV.IdleBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(84)))), ((int)(((byte)(85)))));
            this.btnExportCSV.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(84)))), ((int)(((byte)(85)))));
            this.btnExportCSV.onHoverState.BorderRadius = 40;
            this.btnExportCSV.onHoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(182)))), ((int)(((byte)(146)))));
            this.btnExportCSV.onHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(182)))), ((int)(((byte)(146)))));
            // 
            // btnExportPDF
            // 
            this.btnExportPDF.AllowAnimations = true;
            this.btnExportPDF.AllowMouseEffects = true;
            this.btnExportPDF.AllowToggling = false;
            this.btnExportPDF.AnimationSpeed = 200;
            this.btnExportPDF.AutoGenerateColors = false;
            this.btnExportPDF.AutoRoundBorders = false;
            this.btnExportPDF.AutoSizeLeftIcon = true;
            this.btnExportPDF.AutoSizeRightIcon = true;
            this.btnExportPDF.BackColor = System.Drawing.Color.Transparent;
            this.btnExportPDF.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(84)))), ((int)(((byte)(85)))));
            this.btnExportPDF.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExportPDF.BackgroundImage")));
            this.btnExportPDF.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnExportPDF.ButtonText = "Export PDF";
            this.btnExportPDF.ButtonTextMarginLeft = 0;
            this.btnExportPDF.ColorContrastOnClick = 45;
            this.btnExportPDF.ColorContrastOnHover = 45;
            this.btnExportPDF.Cursor = System.Windows.Forms.Cursors.Hand;
            borderEdges2.BottomLeft = true;
            borderEdges2.BottomRight = true;
            borderEdges2.TopLeft = true;
            borderEdges2.TopRight = true;
            this.btnExportPDF.CustomizableEdges = borderEdges2;
            this.btnExportPDF.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnExportPDF.ForeColor = System.Drawing.Color.White;
            this.btnExportPDF.Location = new System.Drawing.Point(1610, 360);
            this.btnExportPDF.Name = "btnExportPDF";
            this.btnExportPDF.Size = new System.Drawing.Size(150, 45);
            this.btnExportPDF.TabIndex = 12;
            this.btnExportPDF.Visible = true;
            this.btnExportPDF.IdleBorderRadius = 40;
            this.btnExportPDF.IdleBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(84)))), ((int)(((byte)(85)))));
            this.btnExportPDF.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(84)))), ((int)(((byte)(85)))));
            this.btnExportPDF.onHoverState.BorderRadius = 40;
            this.btnExportPDF.onHoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(182)))), ((int)(((byte)(146)))));
            this.btnExportPDF.onHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(182)))), ((int)(((byte)(146)))));
            // 
            // TotalIngredientsCard
            // 
            this.TotalIngredientsCard.BackColor = System.Drawing.Color.Transparent;
            this.TotalIngredientsCard.Location = new System.Drawing.Point(138, 182);
            this.TotalIngredientsCard.Name = "TotalIngredientsCard";
            this.TotalIngredientsCard.Size = new System.Drawing.Size(412, 162);
            this.TotalIngredientsCard.TabIndex = 2;
            // 
            // TotalRequestCard
            // 
            this.TotalRequestCard.BackColor = System.Drawing.Color.Transparent;
            this.TotalRequestCard.Location = new System.Drawing.Point(575, 182);
            this.TotalRequestCard.Name = "TotalRequestCard";
            this.TotalRequestCard.Size = new System.Drawing.Size(412, 162);
            this.TotalRequestCard.TabIndex = 3;
            // 
            // ApprovalRateCard
            // 
            this.ApprovalRateCard.BackColor = System.Drawing.Color.Transparent;
            this.ApprovalRateCard.Location = new System.Drawing.Point(1012, 182);
            this.ApprovalRateCard.Name = "ApprovalRateCard";
            this.ApprovalRateCard.Size = new System.Drawing.Size(412, 162);
            this.ApprovalRateCard.TabIndex = 4;
            // 
            // TotalTransactionsCard
            // 
            this.TotalTransactionsCard.BackColor = System.Drawing.Color.Transparent;
            this.TotalTransactionsCard.Location = new System.Drawing.Point(1450, 182);
            this.TotalTransactionsCard.Name = "TotalTransactionsCard";
            this.TotalTransactionsCard.Size = new System.Drawing.Size(412, 162);
            this.TotalTransactionsCard.TabIndex = 5;
            // 
            // chartInventoryCanvas
            // 
            this.chartInventoryCanvas.AnimationDuration = 950;
            this.chartInventoryCanvas.AnimationType = Bunifu.Charts.WinForms.BunifuChartCanvas.AnimationOptions.easeOutQuart;
            this.chartInventoryCanvas.AutoRender = true;
            this.chartInventoryCanvas.BackColor = System.Drawing.Color.Transparent;
            this.chartInventoryCanvas.CanvasPadding = new System.Windows.Forms.Padding(20, 20, 20, 85);
            this.chartInventoryCanvas.Font = new System.Drawing.Font("Poppins", 30F, System.Drawing.FontStyle.Bold);
            this.chartInventoryCanvas.Labels = null;
            this.chartInventoryCanvas.LegendAlignment = Bunifu.Charts.WinForms.BunifuChartCanvas.LegendAlignmentOptions.center;
            this.chartInventoryCanvas.LegendDisplay = true;
            this.chartInventoryCanvas.LegendFont = new System.Drawing.Font("Segoe UI", 24F);
            this.chartInventoryCanvas.LegendForeColor = System.Drawing.Color.DarkGray;
            this.chartInventoryCanvas.LegendFullWidth = true;
            this.chartInventoryCanvas.LegendPosition = Bunifu.Charts.WinForms.BunifuChartCanvas.PositionOptions.top;
            this.chartInventoryCanvas.LegendRevese = false;
            this.chartInventoryCanvas.LegendRTL = false;
            this.chartInventoryCanvas.Location = new System.Drawing.Point(138, 420);
            this.chartInventoryCanvas.Margin = new System.Windows.Forms.Padding(5);
            this.chartInventoryCanvas.Name = "chartInventoryCanvas";
            this.chartInventoryCanvas.ShowXAxis = false;
            this.chartInventoryCanvas.ShowYAxis = false;
            this.chartInventoryCanvas.Size = new System.Drawing.Size(849, 450);
            this.chartInventoryCanvas.TabIndex = 6;
            this.chartInventoryCanvas.Title = "Inventory Status Distribution";
            this.chartInventoryCanvas.TitleLineHeight = 1.2D;
            this.chartInventoryCanvas.TitlePadding = 10;
            this.chartInventoryCanvas.TitlePosition = Bunifu.Charts.WinForms.BunifuChartCanvas.PositionOptions.top;
            this.chartInventoryCanvas.TooltipBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.chartInventoryCanvas.TooltipFont = new System.Drawing.Font("Segoe UI", 14F);
            this.chartInventoryCanvas.TooltipForeColor = System.Drawing.Color.WhiteSmoke;
            this.chartInventoryCanvas.TooltipMode = Bunifu.Charts.WinForms.BunifuChartCanvas.TooltipModeOptions.nearest;
            this.chartInventoryCanvas.TooltipsEnabled = true;
            this.chartInventoryCanvas.XAxesBeginAtZero = true;
            this.chartInventoryCanvas.XAxesDrawTicks = false;
            this.chartInventoryCanvas.XAxesFont = new System.Drawing.Font("Segoe UI", 12F);
            this.chartInventoryCanvas.XAxesForeColor = System.Drawing.SystemColors.ControlText;
            this.chartInventoryCanvas.XAxesGridColor = System.Drawing.Color.Transparent;
            this.chartInventoryCanvas.XAxesGridLines = false;
            this.chartInventoryCanvas.XAxesLabel = "";
            this.chartInventoryCanvas.XAxesLabelFont = new System.Drawing.Font("Segoe UI", 12F);
            this.chartInventoryCanvas.XAxesLabelForeColor = System.Drawing.SystemColors.ControlText;
            this.chartInventoryCanvas.XAxesLineWidth = 0;
            this.chartInventoryCanvas.XAxesStacked = false;
            this.chartInventoryCanvas.XAxesZeroLineColor = System.Drawing.Color.Transparent;
            this.chartInventoryCanvas.XAxesZeroLineWidth = 0;
            this.chartInventoryCanvas.YAxesBeginAtZero = true;
            this.chartInventoryCanvas.YAxesDrawTicks = false;
            this.chartInventoryCanvas.YAxesFont = new System.Drawing.Font("Segoe UI", 12F);
            this.chartInventoryCanvas.YAxesForeColor = System.Drawing.SystemColors.ControlText;
            this.chartInventoryCanvas.YAxesGridColor = System.Drawing.Color.Transparent;
            this.chartInventoryCanvas.YAxesGridLines = false;
            this.chartInventoryCanvas.YAxesLabel = "";
            this.chartInventoryCanvas.YAxesLabelFont = new System.Drawing.Font("Segoe UI", 12F);
            this.chartInventoryCanvas.YAxesLabelForeColor = System.Drawing.SystemColors.ControlText;
            this.chartInventoryCanvas.YAxesLineWidth = 0;
            this.chartInventoryCanvas.YAxesStacked = false;
            this.chartInventoryCanvas.YAxesZeroLineColor = System.Drawing.Color.Transparent;
            this.chartInventoryCanvas.YAxesZeroLineWidth = 0;
            // 
            // chartRequestsCanvas
            // 
            this.chartRequestsCanvas.AnimationDuration = 950;
            this.chartRequestsCanvas.AnimationType = Bunifu.Charts.WinForms.BunifuChartCanvas.AnimationOptions.easeOutQuart;
            this.chartRequestsCanvas.AutoRender = true;
            this.chartRequestsCanvas.BackColor = System.Drawing.Color.Transparent;
            this.chartRequestsCanvas.CanvasPadding = new System.Windows.Forms.Padding(20, 20, 20, 85);
            this.chartRequestsCanvas.Font = new System.Drawing.Font("Poppins", 30F, System.Drawing.FontStyle.Bold);
            this.chartRequestsCanvas.Labels = null;
            this.chartRequestsCanvas.LegendAlignment = Bunifu.Charts.WinForms.BunifuChartCanvas.LegendAlignmentOptions.center;
            this.chartRequestsCanvas.LegendDisplay = true;
            this.chartRequestsCanvas.LegendFont = new System.Drawing.Font("Segoe UI", 24F);
            this.chartRequestsCanvas.LegendForeColor = System.Drawing.Color.DarkGray;
            this.chartRequestsCanvas.LegendFullWidth = true;
            this.chartRequestsCanvas.LegendPosition = Bunifu.Charts.WinForms.BunifuChartCanvas.PositionOptions.top;
            this.chartRequestsCanvas.LegendRevese = false;
            this.chartRequestsCanvas.LegendRTL = false;
            this.chartRequestsCanvas.Location = new System.Drawing.Point(1013, 420);
            this.chartRequestsCanvas.Margin = new System.Windows.Forms.Padding(5);
            this.chartRequestsCanvas.Name = "chartRequestsCanvas";
            this.chartRequestsCanvas.ShowXAxis = false;
            this.chartRequestsCanvas.ShowYAxis = false;
            this.chartRequestsCanvas.Size = new System.Drawing.Size(849, 450);
            this.chartRequestsCanvas.TabIndex = 7;
            this.chartRequestsCanvas.Title = "Request Status Distribution";
            this.chartRequestsCanvas.TitleLineHeight = 1.2D;
            this.chartRequestsCanvas.TitlePadding = 10;
            this.chartRequestsCanvas.TitlePosition = Bunifu.Charts.WinForms.BunifuChartCanvas.PositionOptions.top;
            this.chartRequestsCanvas.TooltipBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.chartRequestsCanvas.TooltipFont = new System.Drawing.Font("Segoe UI", 14F);
            this.chartRequestsCanvas.TooltipForeColor = System.Drawing.Color.WhiteSmoke;
            this.chartRequestsCanvas.TooltipMode = Bunifu.Charts.WinForms.BunifuChartCanvas.TooltipModeOptions.nearest;
            this.chartRequestsCanvas.TooltipsEnabled = true;
            this.chartRequestsCanvas.XAxesBeginAtZero = true;
            this.chartRequestsCanvas.XAxesDrawTicks = false;
            this.chartRequestsCanvas.XAxesFont = new System.Drawing.Font("Segoe UI", 12F);
            this.chartRequestsCanvas.XAxesForeColor = System.Drawing.SystemColors.ControlText;
            this.chartRequestsCanvas.XAxesGridColor = System.Drawing.Color.Transparent;
            this.chartRequestsCanvas.XAxesGridLines = false;
            this.chartRequestsCanvas.XAxesLabel = "";
            this.chartRequestsCanvas.XAxesLabelFont = new System.Drawing.Font("Segoe UI", 12F);
            this.chartRequestsCanvas.XAxesLabelForeColor = System.Drawing.SystemColors.ControlText;
            this.chartRequestsCanvas.XAxesLineWidth = 0;
            this.chartRequestsCanvas.XAxesStacked = false;
            this.chartRequestsCanvas.XAxesZeroLineColor = System.Drawing.Color.Transparent;
            this.chartRequestsCanvas.XAxesZeroLineWidth = 0;
            this.chartRequestsCanvas.YAxesBeginAtZero = true;
            this.chartRequestsCanvas.YAxesDrawTicks = false;
            this.chartRequestsCanvas.YAxesFont = new System.Drawing.Font("Segoe UI", 12F);
            this.chartRequestsCanvas.YAxesForeColor = System.Drawing.SystemColors.ControlText;
            this.chartRequestsCanvas.YAxesGridColor = System.Drawing.Color.Transparent;
            this.chartRequestsCanvas.YAxesGridLines = false;
            this.chartRequestsCanvas.YAxesLabel = "";
            this.chartRequestsCanvas.YAxesLabelFont = new System.Drawing.Font("Segoe UI", 12F);
            this.chartRequestsCanvas.YAxesLabelForeColor = System.Drawing.SystemColors.ControlText;
            this.chartRequestsCanvas.YAxesLineWidth = 0;
            this.chartRequestsCanvas.YAxesStacked = false;
            this.chartRequestsCanvas.YAxesZeroLineColor = System.Drawing.Color.Transparent;
            this.chartRequestsCanvas.YAxesZeroLineWidth = 0;
            // 
            // chartBarCanvas
            // 
            this.chartBarCanvas.AnimationDuration = 950;
            this.chartBarCanvas.AnimationType = Bunifu.Charts.WinForms.BunifuChartCanvas.AnimationOptions.easeOutQuart;
            this.chartBarCanvas.AutoRender = true;
            this.chartBarCanvas.BackColor = System.Drawing.Color.Transparent;
            this.chartBarCanvas.CanvasPadding = new System.Windows.Forms.Padding(0, 0, 0, 110);
            this.chartBarCanvas.Font = new System.Drawing.Font("Poppins", 30F, System.Drawing.FontStyle.Bold);
            this.chartBarCanvas.Labels = null;
            this.chartBarCanvas.LegendAlignment = Bunifu.Charts.WinForms.BunifuChartCanvas.LegendAlignmentOptions.center;
            this.chartBarCanvas.LegendDisplay = true;
            this.chartBarCanvas.LegendFont = new System.Drawing.Font("Segoe UI", 24F);
            this.chartBarCanvas.LegendForeColor = System.Drawing.Color.DarkGray;
            this.chartBarCanvas.LegendFullWidth = true;
            this.chartBarCanvas.LegendPosition = Bunifu.Charts.WinForms.BunifuChartCanvas.PositionOptions.top;
            this.chartBarCanvas.LegendRevese = false;
            this.chartBarCanvas.LegendRTL = false;
            this.chartBarCanvas.Location = new System.Drawing.Point(138, 900);
            this.chartBarCanvas.Margin = new System.Windows.Forms.Padding(5);
            this.chartBarCanvas.Name = "chartBarCanvas";
            this.chartBarCanvas.ShowXAxis = true;
            this.chartBarCanvas.ShowYAxis = true;
            // CHANGED HERE: Increased Height from 450 to 650
            this.chartBarCanvas.Size = new System.Drawing.Size(1724, 650);
            this.chartBarCanvas.TabIndex = 8;
            this.chartBarCanvas.Title = "Ingredients by Category";
            this.chartBarCanvas.TitleLineHeight = 1.2D;
            this.chartBarCanvas.TitlePadding = 0;
            this.chartBarCanvas.TitlePosition = Bunifu.Charts.WinForms.BunifuChartCanvas.PositionOptions.top;
            this.chartBarCanvas.TooltipBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.chartBarCanvas.TooltipFont = new System.Drawing.Font("Segoe UI", 12F);
            this.chartBarCanvas.TooltipForeColor = System.Drawing.Color.WhiteSmoke;
            this.chartBarCanvas.TooltipMode = Bunifu.Charts.WinForms.BunifuChartCanvas.TooltipModeOptions.nearest;
            this.chartBarCanvas.TooltipsEnabled = true;
            this.chartBarCanvas.XAxesBeginAtZero = true;
            this.chartBarCanvas.XAxesDrawTicks = true;
            this.chartBarCanvas.XAxesFont = new System.Drawing.Font("Segoe UI", 12F);
            this.chartBarCanvas.XAxesForeColor = System.Drawing.SystemColors.ControlText;
            this.chartBarCanvas.XAxesGridColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.chartBarCanvas.XAxesGridLines = true;
            this.chartBarCanvas.XAxesLabel = "";
            this.chartBarCanvas.XAxesLabelFont = new System.Drawing.Font("Segoe UI", 12F);
            this.chartBarCanvas.XAxesLabelForeColor = System.Drawing.SystemColors.ControlText;
            this.chartBarCanvas.XAxesLineWidth = 1;
            this.chartBarCanvas.XAxesStacked = false;
            this.chartBarCanvas.XAxesZeroLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.chartBarCanvas.XAxesZeroLineWidth = 1;
            this.chartBarCanvas.YAxesBeginAtZero = true;
            this.chartBarCanvas.YAxesDrawTicks = true;
            this.chartBarCanvas.YAxesFont = new System.Drawing.Font("Segoe UI", 12F);
            this.chartBarCanvas.YAxesForeColor = System.Drawing.SystemColors.ControlText;
            this.chartBarCanvas.YAxesGridColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.chartBarCanvas.YAxesGridLines = true;
            this.chartBarCanvas.YAxesLabel = "";
            this.chartBarCanvas.YAxesLabelFont = new System.Drawing.Font("Segoe UI", 12F);
            this.chartBarCanvas.YAxesLabelForeColor = System.Drawing.SystemColors.ControlText;
            this.chartBarCanvas.YAxesLineWidth = 1;
            this.chartBarCanvas.YAxesStacked = false;
            this.chartBarCanvas.YAxesZeroLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.chartBarCanvas.YAxesZeroLineWidth = 1;
            // 
            // lowStockControl
            // 
            this.lowStockControl.BackColor = System.Drawing.Color.White;
            this.lowStockControl.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            // CHANGED HERE: Pushed down from Y=1370 to Y=1570
            this.lowStockControl.Location = new System.Drawing.Point(138, 1570);
            this.lowStockControl.Margin = new System.Windows.Forms.Padding(10);
            this.lowStockControl.Name = "lowStockControl";
            this.lowStockControl.Padding = new System.Windows.Forms.Padding(1);
            this.lowStockControl.Size = new System.Drawing.Size(1724, 450);
            this.lowStockControl.TabIndex = 9;
            // 
            // topIngredientsControl
            // 
            this.topIngredientsControl.BackColor = System.Drawing.Color.White;
            this.topIngredientsControl.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            // CHANGED HERE: Pushed down from Y=1840 to Y=2040
            this.topIngredientsControl.Location = new System.Drawing.Point(138, 2040);
            this.topIngredientsControl.Margin = new System.Windows.Forms.Padding(10);
            this.topIngredientsControl.Name = "topIngredientsControl";
            this.topIngredientsControl.Padding = new System.Windows.Forms.Padding(1);
            this.topIngredientsControl.Size = new System.Drawing.Size(1724, 450);
            this.topIngredientsControl.TabIndex = 10;
            // 
            // pieInventory
            // 
            this.pieInventory.BackgroundColor = null;
            this.pieInventory.BorderColor = null;
            this.pieInventory.BorderWidth = 0;
            this.pieInventory.Data = null;
            this.pieInventory.HoverBackgroundColor = System.Drawing.Color.Empty;
            this.pieInventory.HoverBorderColor = System.Drawing.Color.Empty;
            this.pieInventory.HoverBorderWidth = 0;
            this.pieInventory.Label = "Products";
            this.pieInventory.TargetCanvas = this.chartInventoryCanvas;
            // 
            // pieRequests
            // 
            this.pieRequests.BackgroundColor = null;
            this.pieRequests.BorderColor = null;
            this.pieRequests.BorderWidth = 0;
            this.pieRequests.Data = null;
            this.pieRequests.HoverBackgroundColor = System.Drawing.Color.Empty;
            this.pieRequests.HoverBorderColor = System.Drawing.Color.Empty;
            this.pieRequests.HoverBorderWidth = 0;
            this.pieRequests.Label = "Label here";
            this.pieRequests.TargetCanvas = this.chartRequestsCanvas;
            // 
            // barCategory
            // 
            this.barCategory.BackgroundColor = null;
            this.barCategory.BorderColor = null;
            this.barCategory.BorderSkipped = null;
            this.barCategory.BorderWidth = 0;
            this.barCategory.Data = null;
            this.barCategory.HoverBackgroundColor = System.Drawing.Color.Empty;
            this.barCategory.HoverBorderColor = System.Drawing.Color.Empty;
            this.barCategory.HoverBorderWidth = 0;
            this.barCategory.Label = "Products";
            this.barCategory.TargetCanvas = this.chartBarCanvas;
            // 
            // bunifuVScrollBar1
            // 
            this.bunifuVScrollBar1.AllowCursorChanges = true;
            this.bunifuVScrollBar1.AllowHomeEndKeysDetection = false;
            this.bunifuVScrollBar1.AllowIncrementalClickMoves = true;
            this.bunifuVScrollBar1.AllowMouseDownEffects = true;
            this.bunifuVScrollBar1.AllowMouseHoverEffects = true;
            this.bunifuVScrollBar1.AllowScrollingAnimations = true;
            this.bunifuVScrollBar1.AllowScrollKeysDetection = true;
            this.bunifuVScrollBar1.AllowScrollOptionsMenu = true;
            this.bunifuVScrollBar1.AllowShrinkingOnFocusLost = false;
            this.bunifuVScrollBar1.BackgroundColor = System.Drawing.Color.White;
            this.bunifuVScrollBar1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bunifuVScrollBar1.BackgroundImage")));
            this.bunifuVScrollBar1.BindingContainer = this.pnlMain;
            this.bunifuVScrollBar1.BorderColor = System.Drawing.Color.White;
            this.bunifuVScrollBar1.BorderRadius = 14;
            this.bunifuVScrollBar1.BorderThickness = 1;
            this.bunifuVScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.bunifuVScrollBar1.DurationBeforeShrink = 2000;
            this.bunifuVScrollBar1.LargeChange = 50;
            this.bunifuVScrollBar1.Location = new System.Drawing.Point(1980, 0);
            this.bunifuVScrollBar1.Maximum = 100;
            this.bunifuVScrollBar1.Minimum = 0;
            this.bunifuVScrollBar1.MinimumThumbLength = 18;
            this.bunifuVScrollBar1.Name = "bunifuVScrollBar1";
            this.bunifuVScrollBar1.OnDisable.ScrollBarBorderColor = System.Drawing.Color.Silver;
            this.bunifuVScrollBar1.OnDisable.ScrollBarColor = System.Drawing.Color.Transparent;
            this.bunifuVScrollBar1.OnDisable.ThumbColor = System.Drawing.Color.Silver;
            this.bunifuVScrollBar1.ScrollBarBorderColor = System.Drawing.Color.White;
            this.bunifuVScrollBar1.ScrollBarColor = System.Drawing.Color.White;
            this.bunifuVScrollBar1.ShrinkSizeLimit = 3;
            this.bunifuVScrollBar1.Size = new System.Drawing.Size(20, 1125);
            this.bunifuVScrollBar1.SmallChange = 10;
            this.bunifuVScrollBar1.TabIndex = 1;
            this.bunifuVScrollBar1.ThumbColor = System.Drawing.Color.Gray;
            this.bunifuVScrollBar1.ThumbLength = 555;
            this.bunifuVScrollBar1.ThumbMargin = 1;
            this.bunifuVScrollBar1.ThumbStyle = Bunifu.UI.WinForms.BunifuVScrollBar.ThumbStyles.Inset;
            this.bunifuVScrollBar1.Value = 0;
            // 
            // ReportsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bunifuVScrollBar1);
            this.Controls.Add(this.pnlMain);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ReportsControl";
            this.Size = new System.Drawing.Size(2000, 1125);
            this.Load += new System.EventHandler(this.ReportsControl_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

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