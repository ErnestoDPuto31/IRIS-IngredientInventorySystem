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
            restockTable1 = new IRIS.Presentation.UserControls.Table.RestockTable();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnFilterCategory = new IRIS.Presentation.UserControls.Components.PillButton();
            btnFilterAll = new IRIS.Presentation.UserControls.Components.PillButton();
            btnFilterLow = new IRIS.Presentation.UserControls.Components.PillButton();
            btnFilterEmpty = new IRIS.Presentation.UserControls.Components.PillButton();
            btnFilterWell = new IRIS.Presentation.UserControls.Components.PillButton();
            LowStockItems = new IRIS.Presentation.UserControls.Components.StatusCardUC();
            EmptyItems = new IRIS.Presentation.UserControls.Components.StatusCardUC();
            WellStockedItems = new IRIS.Presentation.UserControls.Components.StatusCardUC();
            btnRefresh = new Guna.UI2.WinForms.Guna2Button();
            irisSearchBar1 = new IRIS.Presentation.UserControls.Components.IrisSearchBar();
            SuspendLayout();
            // 
            // restockTable1
            // 
            restockTable1.BorderStyle = BorderStyle.Fixed3D;
            restockTable1.Location = new Point(150, 402);
            restockTable1.Name = "restockTable1";
            restockTable1.Size = new Size(1375, 450);
            restockTable1.TabIndex = 3;
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel1.Location = new Point(150, 39);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(282, 39);
            guna2HtmlLabel1.TabIndex = 4;
            guna2HtmlLabel1.Text = "Restock Management";
            // 
            // guna2HtmlLabel2
            // 
            guna2HtmlLabel2.BackColor = Color.Transparent;
            guna2HtmlLabel2.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            guna2HtmlLabel2.ForeColor = SystemColors.ControlDarkDark;
            guna2HtmlLabel2.Location = new Point(150, 84);
            guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            guna2HtmlLabel2.Size = new Size(414, 25);
            guna2HtmlLabel2.TabIndex = 5;
            guna2HtmlLabel2.Text = "Manage restocking for low and empty inventory items";
            // 
            // guna2HtmlLabel3
            // 
            guna2HtmlLabel3.BackColor = Color.Transparent;
            guna2HtmlLabel3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel3.ForeColor = SystemColors.ControlDarkDark;
            guna2HtmlLabel3.Location = new Point(610, 350);
            guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            guna2HtmlLabel3.Size = new Size(81, 30);
            guna2HtmlLabel3.TabIndex = 8;
            guna2HtmlLabel3.Text = "FILTERS:";
            // 
            // btnFilterCategory
            // 
            btnFilterCategory.BackColor = Color.White;
            btnFilterCategory.FlatAppearance.BorderSize = 0;
            btnFilterCategory.FlatStyle = FlatStyle.Flat;
            btnFilterCategory.ForeColor = Color.Black;
            btnFilterCategory.Location = new Point(713, 350);
            btnFilterCategory.Name = "btnFilterCategory";
            btnFilterCategory.Size = new Size(150, 33);
            btnFilterCategory.TabIndex = 9;
            btnFilterCategory.Text = "All Categories ▼";
            btnFilterCategory.UseVisualStyleBackColor = false;
            // 
            // btnFilterAll
            // 
            btnFilterAll.BackColor = Color.White;
            btnFilterAll.FlatAppearance.BorderSize = 0;
            btnFilterAll.FlatStyle = FlatStyle.Flat;
            btnFilterAll.ForeColor = Color.Black;
            btnFilterAll.Location = new Point(869, 350);
            btnFilterAll.Name = "btnFilterAll";
            btnFilterAll.Size = new Size(103, 33);
            btnFilterAll.TabIndex = 10;
            btnFilterAll.Text = "All Status";
            btnFilterAll.UseVisualStyleBackColor = false;
            btnFilterAll.Click += btnFilterAll_Click;
            // 
            // btnFilterLow
            // 
            btnFilterLow.BackColor = Color.White;
            btnFilterLow.FlatAppearance.BorderSize = 0;
            btnFilterLow.FlatStyle = FlatStyle.Flat;
            btnFilterLow.ForeColor = Color.Black;
            btnFilterLow.Location = new Point(978, 350);
            btnFilterLow.Name = "btnFilterLow";
            btnFilterLow.Size = new Size(103, 33);
            btnFilterLow.TabIndex = 11;
            btnFilterLow.Text = "Low Stock";
            btnFilterLow.UseVisualStyleBackColor = false;
            btnFilterLow.Click += btnFilterLow_Click;
            // 
            // btnFilterEmpty
            // 
            btnFilterEmpty.BackColor = Color.White;
            btnFilterEmpty.FlatAppearance.BorderSize = 0;
            btnFilterEmpty.FlatStyle = FlatStyle.Flat;
            btnFilterEmpty.ForeColor = Color.Black;
            btnFilterEmpty.Location = new Point(1087, 350);
            btnFilterEmpty.Name = "btnFilterEmpty";
            btnFilterEmpty.Size = new Size(103, 33);
            btnFilterEmpty.TabIndex = 12;
            btnFilterEmpty.Text = "Empty";
            btnFilterEmpty.UseVisualStyleBackColor = false;
            btnFilterEmpty.Click += btnFilterEmpty_Click;
            // 
            // btnFilterWell
            // 
            btnFilterWell.BackColor = Color.White;
            btnFilterWell.FlatAppearance.BorderSize = 0;
            btnFilterWell.FlatStyle = FlatStyle.Flat;
            btnFilterWell.ForeColor = Color.Black;
            btnFilterWell.Location = new Point(1196, 350);
            btnFilterWell.Name = "btnFilterWell";
            btnFilterWell.Size = new Size(103, 33);
            btnFilterWell.TabIndex = 13;
            btnFilterWell.Text = "Well Stocked";
            btnFilterWell.UseVisualStyleBackColor = false;
            btnFilterWell.Click += btnFilterWell_Click;
            // 
            // LowStockItems
            // 
            LowStockItems.BackColor = Color.Transparent;
            LowStockItems.Location = new Point(150, 115);
            LowStockItems.Name = "LowStockItems";
            LowStockItems.Padding = new Padding(25, 10, 10, 10);
            LowStockItems.Size = new Size(450, 200);
            LowStockItems.TabIndex = 14;
            LowStockItems.TypeOfCard = Components.CardType.LowStockItems;
            // 
            // EmptyItems
            // 
            EmptyItems.BackColor = Color.Transparent;
            EmptyItems.Location = new Point(610, 115);
            EmptyItems.Name = "EmptyItems";
            EmptyItems.Size = new Size(450, 200);
            EmptyItems.TabIndex = 15;
            EmptyItems.TypeOfCard = Components.CardType.LowStockItems;
            // 
            // WellStockedItems
            // 
            WellStockedItems.BackColor = Color.Transparent;
            WellStockedItems.Location = new Point(1075, 115);
            WellStockedItems.Name = "WellStockedItems";
            WellStockedItems.Size = new Size(450, 200);
            WellStockedItems.TabIndex = 16;
            WellStockedItems.TypeOfCard = Components.CardType.LowStockItems;
            // 
            // btnRefresh
            // 
            btnRefresh.BorderRadius = 10;
            btnRefresh.CustomizableEdges = customizableEdges1;
            btnRefresh.DisabledState.BorderColor = Color.DarkGray;
            btnRefresh.DisabledState.CustomBorderColor = Color.DarkGray;
            btnRefresh.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnRefresh.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnRefresh.FillColor = Color.Silver;
            btnRefresh.Font = new Font("Segoe UI", 9F);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(1441, 350);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnRefresh.Size = new Size(84, 33);
            btnRefresh.TabIndex = 17;
            btnRefresh.Text = "Refresh";
            btnRefresh.Click += btnRefresh_Click;
            // 
            // irisSearchBar1
            // 
            irisSearchBar1.BackColor = Color.Transparent;
            irisSearchBar1.Location = new Point(150, 342);
            irisSearchBar1.Name = "irisSearchBar1";
            irisSearchBar1.Padding = new Padding(10, 5, 10, 5);
            irisSearchBar1.Size = new Size(450, 41);
            irisSearchBar1.TabIndex = 18;
            // 
            // RestockPage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(irisSearchBar1);
            Controls.Add(btnRefresh);
            Controls.Add(WellStockedItems);
            Controls.Add(EmptyItems);
            Controls.Add(LowStockItems);
            Controls.Add(btnFilterWell);
            Controls.Add(btnFilterEmpty);
            Controls.Add(btnFilterLow);
            Controls.Add(btnFilterAll);
            Controls.Add(btnFilterCategory);
            Controls.Add(guna2HtmlLabel3);
            Controls.Add(guna2HtmlLabel2);
            Controls.Add(guna2HtmlLabel1);
            Controls.Add(restockTable1);
            Name = "RestockPage";
            Size = new Size(1600, 900);
            Load += RestockPage_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Table.RestockTable restockTable1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private Components.PillButton btnFilterCategory;
        private Components.PillButton btnFilterAll;
        private Components.PillButton btnFilterLow;
        private Components.PillButton btnFilterEmpty;
        private Components.PillButton btnFilterWell;
        private Components.StatusCardUC LowStockItems;
        private Components.StatusCardUC EmptyItems;
        private Components.StatusCardUC WellStockedItems;
        private Guna.UI2.WinForms.Guna2Button btnRefresh;
        private Components.IrisSearchBar irisSearchBar1;
    }
}
