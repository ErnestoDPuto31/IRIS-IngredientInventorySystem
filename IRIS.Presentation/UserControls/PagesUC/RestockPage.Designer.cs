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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnFilterCategory = new IRIS.Presentation.UserControls.Components.PillButton();
            btnFilterAll = new IRIS.Presentation.UserControls.Components.PillButton();
            btnFilterLow = new IRIS.Presentation.UserControls.Components.PillButton();
            btnFilterEmpty = new IRIS.Presentation.UserControls.Components.PillButton();
            btnFilterWell = new IRIS.Presentation.UserControls.Components.PillButton();
            LowStockItems = new IRIS.Presentation.UserControls.Components.StatusCardUC();
            btnRefresh = new Guna.UI2.WinForms.Guna2Button();
            EmptyItems = new IRIS.Presentation.UserControls.Components.StatusCardUC();
            WellStockedItems = new IRIS.Presentation.UserControls.Components.StatusCardUC();
            restockTableuc1 = new IRIS.Presentation.UserControls.Table.RestockTableUC();
            SuspendLayout();
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel1.Location = new Point(150, 71);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(397, 47);
            guna2HtmlLabel1.TabIndex = 4;
            guna2HtmlLabel1.Text = "RESTOCK MANAGEMENT";
            // 
            // guna2HtmlLabel2
            // 
            guna2HtmlLabel2.BackColor = Color.Transparent;
            guna2HtmlLabel2.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            guna2HtmlLabel2.ForeColor = SystemColors.ControlDarkDark;
            guna2HtmlLabel2.Location = new Point(150, 115);
            guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            guna2HtmlLabel2.Size = new Size(414, 25);
            guna2HtmlLabel2.TabIndex = 5;
            guna2HtmlLabel2.Text = "Manage restocking for low and empty inventory items";
            // 
            // guna2HtmlLabel3
            // 
            guna2HtmlLabel3.BackColor = Color.Transparent;
            guna2HtmlLabel3.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel3.ForeColor = SystemColors.ControlDark;
            guna2HtmlLabel3.Location = new Point(150, 370);
            guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            guna2HtmlLabel3.Size = new Size(71, 25);
            guna2HtmlLabel3.TabIndex = 8;
            guna2HtmlLabel3.Text = "FILTERS:";
            guna2HtmlLabel3.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // btnFilterCategory
            // 
            btnFilterCategory.BackColor = Color.White;
            btnFilterCategory.FlatAppearance.BorderSize = 0;
            btnFilterCategory.FlatStyle = FlatStyle.Flat;
            btnFilterCategory.ForeColor = Color.Black;
            btnFilterCategory.Location = new Point(253, 370);
            btnFilterCategory.Name = "btnFilterCategory";
            btnFilterCategory.Size = new Size(150, 40);
            btnFilterCategory.TabIndex = 9;
            btnFilterCategory.Text = "All Categories ▼";
            btnFilterCategory.UseVisualStyleBackColor = false;
            btnFilterCategory.MouseHover += btnFilterCategory_MouseHover;
            // 
            // btnFilterAll
            // 
            btnFilterAll.BackColor = Color.White;
            btnFilterAll.FlatAppearance.BorderSize = 0;
            btnFilterAll.FlatStyle = FlatStyle.Flat;
            btnFilterAll.ForeColor = Color.Black;
            btnFilterAll.Location = new Point(409, 370);
            btnFilterAll.Name = "btnFilterAll";
            btnFilterAll.Size = new Size(100, 40);
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
            btnFilterLow.Location = new Point(515, 370);
            btnFilterLow.Name = "btnFilterLow";
            btnFilterLow.Size = new Size(100, 40);
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
            btnFilterEmpty.Location = new Point(621, 370);
            btnFilterEmpty.Name = "btnFilterEmpty";
            btnFilterEmpty.Size = new Size(100, 40);
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
            btnFilterWell.Location = new Point(727, 370);
            btnFilterWell.Name = "btnFilterWell";
            btnFilterWell.Size = new Size(100, 40);
            btnFilterWell.TabIndex = 13;
            btnFilterWell.Text = "Well Stocked";
            btnFilterWell.UseVisualStyleBackColor = false;
            btnFilterWell.Click += btnFilterWell_Click;
            // 
            // LowStockItems
            // 
            LowStockItems.BackColor = Color.Transparent;
            LowStockItems.Location = new Point(150, 146);
            LowStockItems.Name = "LowStockItems";
            LowStockItems.Padding = new Padding(25, 10, 10, 10);
            LowStockItems.Size = new Size(450, 200);
            LowStockItems.TabIndex = 14;
            LowStockItems.TypeOfCard = Components.CardType.LowStockItems;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.White;
            btnRefresh.BorderRadius = 10;
            btnRefresh.CustomizableEdges = customizableEdges3;
            btnRefresh.DisabledState.BorderColor = Color.DarkGray;
            btnRefresh.DisabledState.CustomBorderColor = Color.DarkGray;
            btnRefresh.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnRefresh.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnRefresh.FillColor = Color.White;
            btnRefresh.Font = new Font("Segoe UI", 9F);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Image = Properties.Resources.icons8_sync_64;
            btnRefresh.ImageSize = new Size(40, 40);
            btnRefresh.Location = new Point(1094, 441);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnRefresh.Size = new Size(89, 35);
            btnRefresh.TabIndex = 17;
            btnRefresh.Click += btnRefresh_Click;
            btnRefresh.MouseHover += btnRefresh_MouseHover;
            // 
            // EmptyItems
            // 
            EmptyItems.BackColor = Color.Transparent;
            EmptyItems.Location = new Point(615, 146);
            EmptyItems.Name = "EmptyItems";
            EmptyItems.Size = new Size(450, 200);
            EmptyItems.TabIndex = 19;
            EmptyItems.TypeOfCard = Components.CardType.LowStockItems;
            // 
            // WellStockedItems
            // 
            WellStockedItems.BackColor = Color.Transparent;
            WellStockedItems.Location = new Point(1080, 146);
            WellStockedItems.Name = "WellStockedItems";
            WellStockedItems.Size = new Size(450, 200);
            WellStockedItems.TabIndex = 20;
            WellStockedItems.TypeOfCard = Components.CardType.LowStockItems;
            // 
            // restockTableuc1
            // 
            restockTableuc1.BackColor = Color.Transparent;
            restockTableuc1.Location = new Point(150, 425);
            restockTableuc1.Name = "restockTableuc1";
            restockTableuc1.Padding = new Padding(20);
            restockTableuc1.Size = new Size(1380, 451);
            restockTableuc1.TabIndex = 21;
            // 
            // RestockPage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnRefresh);
            Controls.Add(restockTableuc1);
            Controls.Add(WellStockedItems);
            Controls.Add(EmptyItems);
            Controls.Add(LowStockItems);
            Controls.Add(btnFilterWell);
            Controls.Add(btnFilterEmpty);
            Controls.Add(btnFilterLow);
            Controls.Add(btnFilterAll);
            Controls.Add(btnFilterCategory);
            Controls.Add(guna2HtmlLabel3);
            Controls.Add(guna2HtmlLabel1);
            Controls.Add(guna2HtmlLabel2);
            Name = "RestockPage";
            Size = new Size(1600, 900);
            Load += RestockPage_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private Components.PillButton btnFilterCategory;
        private Components.PillButton btnFilterAll;
        private Components.PillButton btnFilterLow;
        private Components.PillButton btnFilterEmpty;
        private Components.PillButton btnFilterWell;
        private Components.StatusCardUC LowStockItems;
        private Guna.UI2.WinForms.Guna2Button btnRefresh;
        private Components.StatusCardUC EmptyItems;
        private Components.StatusCardUC WellStockedItems;
        private Table.RestockTableUC restockTableuc1;
    }
}
