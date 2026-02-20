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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnFilterAll = new IRIS.Presentation.UserControls.Components.PillButton();
            btnFilterLow = new IRIS.Presentation.UserControls.Components.PillButton();
            btnFilterEmpty = new IRIS.Presentation.UserControls.Components.PillButton();
            btnFilterWell = new IRIS.Presentation.UserControls.Components.PillButton();
            LowStockItems = new IRIS.Presentation.UserControls.Components.StatusCardUC();
            btnRefresh = new Guna.UI2.WinForms.Guna2Button();
            EmptyItems = new IRIS.Presentation.UserControls.Components.StatusCardUC();
            WellStockedItems = new IRIS.Presentation.UserControls.Components.StatusCardUC();
            cmbFilter = new Guna.UI2.WinForms.Guna2ComboBox();
            guna2ImageButton1 = new Guna.UI2.WinForms.Guna2ImageButton();
            sfDataGrid1 = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            ((System.ComponentModel.ISupportInitialize)sfDataGrid1).BeginInit();
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
            // btnFilterAll
            // 
            btnFilterAll.BackColor = Color.White;
            btnFilterAll.FlatAppearance.BorderSize = 0;
            btnFilterAll.FlatStyle = FlatStyle.Flat;
            btnFilterAll.Font = new Font("Poppins", 9F);
            btnFilterAll.ForeColor = Color.Black;
            btnFilterAll.Location = new Point(617, 370);
            btnFilterAll.Name = "btnFilterAll";
            btnFilterAll.Size = new Size(100, 36);
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
            btnFilterLow.Font = new Font("Poppins", 9F);
            btnFilterLow.ForeColor = Color.Black;
            btnFilterLow.Location = new Point(723, 370);
            btnFilterLow.Name = "btnFilterLow";
            btnFilterLow.Size = new Size(100, 36);
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
            btnFilterEmpty.Font = new Font("Poppins", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnFilterEmpty.ForeColor = Color.Black;
            btnFilterEmpty.Location = new Point(829, 370);
            btnFilterEmpty.Name = "btnFilterEmpty";
            btnFilterEmpty.Size = new Size(100, 36);
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
            btnFilterWell.Font = new Font("Poppins", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnFilterWell.ForeColor = Color.Black;
            btnFilterWell.Location = new Point(935, 370);
            btnFilterWell.Name = "btnFilterWell";
            btnFilterWell.Size = new Size(124, 36);
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
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.Transparent;
            btnRefresh.BorderRadius = 10;
            btnRefresh.CustomizableEdges = customizableEdges6;
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
            btnRefresh.ShadowDecoration.CustomizableEdges = customizableEdges7;
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
            cmbFilter.CustomizableEdges = customizableEdges8;
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
            cmbFilter.ShadowDecoration.CustomizableEdges = customizableEdges9;
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
            guna2ImageButton1.ShadowDecoration.CustomizableEdges = customizableEdges10;
            guna2ImageButton1.Size = new Size(29, 36);
            guna2ImageButton1.TabIndex = 23;
            // 
            // sfDataGrid1
            // 
            sfDataGrid1.AccessibleName = "Table";
            sfDataGrid1.Location = new Point(150, 445);
            sfDataGrid1.Name = "sfDataGrid1";
            sfDataGrid1.PreviewRowHeight = 35;
            sfDataGrid1.Size = new Size(1380, 389);
            sfDataGrid1.Style.BorderColor = Color.FromArgb(100, 100, 100);
            sfDataGrid1.Style.CheckBoxStyle.CheckedBackColor = Color.FromArgb(0, 120, 215);
            sfDataGrid1.Style.CheckBoxStyle.CheckedBorderColor = Color.FromArgb(0, 120, 215);
            sfDataGrid1.Style.CheckBoxStyle.IndeterminateBorderColor = Color.FromArgb(0, 120, 215);
            sfDataGrid1.Style.DragPreviewRowStyle.Font = new Font("Segoe UI", 9F);
            sfDataGrid1.Style.DragPreviewRowStyle.RowCountIndicatorBackColor = Color.FromArgb(0, 120, 215);
            sfDataGrid1.Style.DragPreviewRowStyle.RowCountIndicatorTextColor = Color.FromArgb(255, 255, 255);
            sfDataGrid1.Style.HyperlinkStyle.DefaultLinkColor = Color.FromArgb(0, 120, 215);
            sfDataGrid1.TabIndex = 24;
            sfDataGrid1.Text = "sfDataGrid1";
            // 
            // RestockPage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(sfDataGrid1);
            Controls.Add(guna2ImageButton1);
            Controls.Add(cmbFilter);
            Controls.Add(guna2HtmlLabel2);
            Controls.Add(guna2HtmlLabel1);
            Controls.Add(btnRefresh);
            Controls.Add(WellStockedItems);
            Controls.Add(EmptyItems);
            Controls.Add(LowStockItems);
            Controls.Add(btnFilterWell);
            Controls.Add(btnFilterEmpty);
            Controls.Add(btnFilterLow);
            Controls.Add(btnFilterAll);
            Name = "RestockPage";
            Size = new Size(1600, 900);
            Load += RestockPage_Load;
            ((System.ComponentModel.ISupportInitialize)sfDataGrid1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private Components.PillButton btnFilterAll;
        private Components.PillButton btnFilterLow;
        private Components.PillButton btnFilterEmpty;
        private Components.PillButton btnFilterWell;
        private Components.StatusCardUC LowStockItems;
        private Guna.UI2.WinForms.Guna2Button btnRefresh;
        private Components.StatusCardUC EmptyItems;
        private Components.StatusCardUC WellStockedItems;
        private Guna.UI2.WinForms.Guna2ComboBox cmbFilter;
        private Guna.UI2.WinForms.Guna2ImageButton guna2ImageButton1;
        private Syncfusion.WinForms.DataGrid.SfDataGrid sfDataGrid1;
    }
}
