namespace IRIS.Presentation.UserControls.PagesUC
{
    partial class RestockPage
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            LowStockItems = new IRIS.Presentation.UserControls.Components.StatusCardUC();
            btnRefresh = new Guna.UI2.WinForms.Guna2Button();
            EmptyItems = new IRIS.Presentation.UserControls.Components.StatusCardUC();
            WellStockedItems = new IRIS.Presentation.UserControls.Components.StatusCardUC();
            cmbFilter = new Guna.UI2.WinForms.Guna2ComboBox();
            guna2ImageButton1 = new Guna.UI2.WinForms.Guna2ImageButton();
            restockTableuc1 = new IRIS.Presentation.UserControls.Table.RestockTableUC();
            pnlMainContent = new Guna.UI2.WinForms.Guna2Panel();
            vScrollBar1 = new VScrollBar();
            pnlMainContent.SuspendLayout();
            SuspendLayout();
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Font = new Font("Poppins", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel1.Location = new Point(150, 27);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(487, 72);
            guna2HtmlLabel1.TabIndex = 4;
            guna2HtmlLabel1.Text = "RESTOCK MANAGEMENT";
            // 
            // guna2HtmlLabel2
            // 
            guna2HtmlLabel2.BackColor = Color.Transparent;
            guna2HtmlLabel2.Font = new Font("Poppins", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            guna2HtmlLabel2.ForeColor = SystemColors.ControlDarkDark;
            guna2HtmlLabel2.Location = new Point(150, 81);
            guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            guna2HtmlLabel2.Size = new Size(629, 42);
            guna2HtmlLabel2.TabIndex = 5;
            guna2HtmlLabel2.Text = "Manage restocking for low and empty inventory items";
            // 
            // LowStockItems
            // 
            LowStockItems.BackColor = Color.Transparent;
            LowStockItems.Location = new Point(150, 146);
            LowStockItems.Name = "LowStockItems";
            LowStockItems.Padding = new Padding(25, 10, 10, 10);
            LowStockItems.Size = new Size(450, 200);
            LowStockItems.TabIndex = 14;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.Transparent;
            btnRefresh.BorderRadius = 10;
            btnRefresh.CustomizableEdges = customizableEdges1;
            btnRefresh.DisabledState.BorderColor = Color.DarkGray;
            btnRefresh.DisabledState.CustomBorderColor = Color.DarkGray;
            btnRefresh.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnRefresh.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnRefresh.FillColor = Color.Transparent;
            btnRefresh.Font = new Font("Segoe UI", 9F);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Image = Properties.Resources.icons8_sync_64;
            btnRefresh.ImageSize = new Size(40, 40);
            btnRefresh.Location = new Point(545, 370);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnRefresh.Size = new Size(49, 36);
            btnRefresh.TabIndex = 17;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // EmptyItems
            // 
            EmptyItems.BackColor = Color.Transparent;
            EmptyItems.Location = new Point(615, 146);
            EmptyItems.Name = "EmptyItems";
            EmptyItems.Size = new Size(450, 200);
            EmptyItems.TabIndex = 19;
            // 
            // WellStockedItems
            // 
            WellStockedItems.BackColor = Color.Transparent;
            WellStockedItems.Location = new Point(1080, 146);
            WellStockedItems.Name = "WellStockedItems";
            WellStockedItems.Size = new Size(450, 200);
            WellStockedItems.TabIndex = 20;
            // 
            // cmbFilter
            // 
            cmbFilter.BackColor = Color.Transparent;
            cmbFilter.BorderColor = Color.Indigo;
            cmbFilter.BorderRadius = 15;
            cmbFilter.CustomizableEdges = customizableEdges3;
            cmbFilter.DrawMode = DrawMode.OwnerDrawFixed;
            cmbFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilter.FocusedColor = Color.White;
            cmbFilter.FocusedState.BorderColor = Color.White;
            cmbFilter.FocusedState.FillColor = Color.Indigo;
            cmbFilter.FocusedState.ForeColor = Color.White;
            cmbFilter.Font = new Font("Segoe UI", 10F);
            cmbFilter.ForeColor = Color.FromArgb(68, 88, 112);
            cmbFilter.HoverState.BorderColor = Color.White;
            cmbFilter.HoverState.FillColor = Color.Indigo;
            cmbFilter.HoverState.ForeColor = Color.White;
            cmbFilter.ItemHeight = 30;
            cmbFilter.ItemsAppearance.BackColor = Color.White;
            cmbFilter.ItemsAppearance.ForeColor = Color.Black;
            cmbFilter.ItemsAppearance.SelectedBackColor = Color.Indigo;
            cmbFilter.ItemsAppearance.SelectedForeColor = Color.White;
            cmbFilter.Location = new Point(185, 370);
            cmbFilter.Name = "cmbFilter";
            cmbFilter.ShadowDecoration.CustomizableEdges = customizableEdges4;
            cmbFilter.Size = new Size(354, 36);
            cmbFilter.TabIndex = 22;
            cmbFilter.SelectedIndexChanged += cmbFilter_SelectedIndexChanged;
            // 
            // guna2ImageButton1
            // 
            guna2ImageButton1.CheckedState.ImageSize = new Size(64, 64);
            guna2ImageButton1.HoverState.ImageSize = new Size(64, 64);
            guna2ImageButton1.Image = Properties.Resources.icons8_filter_100;
            guna2ImageButton1.ImageOffset = new Point(0, 0);
            guna2ImageButton1.ImageRotate = 0F;
            guna2ImageButton1.ImageSize = new Size(24, 24);
            guna2ImageButton1.Location = new Point(150, 370);
            guna2ImageButton1.Name = "guna2ImageButton1";
            guna2ImageButton1.PressedState.ImageSize = new Size(64, 64);
            guna2ImageButton1.ShadowDecoration.CustomizableEdges = customizableEdges5;
            guna2ImageButton1.Size = new Size(29, 36);
            guna2ImageButton1.TabIndex = 23;
            // 
            // restockTableuc1
            // 
            restockTableuc1.BackColor = Color.White;
            restockTableuc1.Location = new Point(150, 424);
            restockTableuc1.Name = "restockTableuc1";
            restockTableuc1.Padding = new Padding(25);
            restockTableuc1.Size = new Size(1380, 647);
            restockTableuc1.TabIndex = 24;
            // 
            // pnlMainContent
            // 
            pnlMainContent.AllowDrop = true;
            pnlMainContent.AutoScroll = true;
            pnlMainContent.Controls.Add(vScrollBar1);
            pnlMainContent.Controls.Add(guna2HtmlLabel2);
            pnlMainContent.Controls.Add(LowStockItems);
            pnlMainContent.Controls.Add(EmptyItems);
            pnlMainContent.Controls.Add(WellStockedItems);
            pnlMainContent.Controls.Add(btnRefresh);
            pnlMainContent.Controls.Add(guna2HtmlLabel1);
            pnlMainContent.Controls.Add(restockTableuc1);
            pnlMainContent.Controls.Add(cmbFilter);
            pnlMainContent.Controls.Add(guna2ImageButton1);
            pnlMainContent.CustomizableEdges = customizableEdges6;
            pnlMainContent.Dock = DockStyle.Fill;
            pnlMainContent.Location = new Point(0, 0);
            pnlMainContent.Name = "pnlMainContent";
            pnlMainContent.ShadowDecoration.CustomizableEdges = customizableEdges7;
            pnlMainContent.Size = new Size(1600, 900);
            pnlMainContent.TabIndex = 25;
            // 
            // vScrollBar1
            // 
            vScrollBar1.Location = new Point(1575, 0);
            vScrollBar1.Name = "vScrollBar1";
            vScrollBar1.Size = new Size(25, 900);
            vScrollBar1.TabIndex = 25;
            // 
            // RestockPage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(pnlMainContent);
            Name = "RestockPage";
            Size = new Size(1600, 900);
            Load += RestockPage_Load;
            pnlMainContent.ResumeLayout(false);
            pnlMainContent.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Components.StatusCardUC LowStockItems;
        private Guna.UI2.WinForms.Guna2Button btnRefresh;
        private Components.StatusCardUC EmptyItems;
        private Components.StatusCardUC WellStockedItems;
        private Guna.UI2.WinForms.Guna2ComboBox cmbFilter;
        private Guna.UI2.WinForms.Guna2ImageButton guna2ImageButton1;
        private Table.RestockTableUC restockTableuc1;
        private Guna.UI2.WinForms.Guna2Panel pnlMainContent;
        private VScrollBar vScrollBar1;
    }
}
