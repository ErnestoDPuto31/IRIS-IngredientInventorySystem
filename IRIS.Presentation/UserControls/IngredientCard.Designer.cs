namespace IRIS.Presentation.UserControls
{
    partial class IngredientCard
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            lblUpdatedAt = new Label();
            btnDeleteIngredient = new Guna.UI2.WinForms.Guna2ImageButton();
            lblMinThreshold = new Label();
            lblCurrentStock = new Label();
            label2 = new Label();
            label1 = new Label();
            guna2ProgressBar1 = new Guna.UI2.WinForms.Guna2ProgressBar();
            btnEditIngredient = new Guna.UI2.WinForms.Guna2ImageButton();
            txtStatus = new Guna.UI2.WinForms.Guna2TextBox();
            txtCategoryLabel = new Guna.UI2.WinForms.Guna2TextBox();
            lblIngredientName = new Label();
            guna2Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // guna2BorderlessForm1
            // 
            guna2BorderlessForm1.BorderRadius = 30;
            guna2BorderlessForm1.ContainerControl = this;
            guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // guna2Panel1
            // 
            guna2Panel1.BorderColor = Color.FromArgb(77, 10, 133);
            guna2Panel1.BorderRadius = 15;
            guna2Panel1.BorderThickness = 1;
            guna2Panel1.Controls.Add(lblUpdatedAt);
            guna2Panel1.Controls.Add(btnDeleteIngredient);
            guna2Panel1.Controls.Add(lblMinThreshold);
            guna2Panel1.Controls.Add(lblCurrentStock);
            guna2Panel1.Controls.Add(label2);
            guna2Panel1.Controls.Add(label1);
            guna2Panel1.Controls.Add(guna2ProgressBar1);
            guna2Panel1.Controls.Add(btnEditIngredient);
            guna2Panel1.Controls.Add(txtStatus);
            guna2Panel1.Controls.Add(txtCategoryLabel);
            guna2Panel1.Controls.Add(lblIngredientName);
            guna2Panel1.CustomizableEdges = customizableEdges9;
            guna2Panel1.Dock = DockStyle.Fill;
            guna2Panel1.Location = new Point(0, 0);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges10;
            guna2Panel1.Size = new Size(300, 260);
            guna2Panel1.TabIndex = 0;
            // 
            // lblUpdatedAt
            // 
            lblUpdatedAt.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblUpdatedAt.AutoSize = true;
            lblUpdatedAt.Font = new Font("Poppins", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblUpdatedAt.ForeColor = SystemColors.ControlDarkDark;
            lblUpdatedAt.Location = new Point(22, 214);
            lblUpdatedAt.Name = "lblUpdatedAt";
            lblUpdatedAt.Size = new Size(159, 23);
            lblUpdatedAt.TabIndex = 10;
            lblUpdatedAt.Text = "Updated At 01-20-2026";
            lblUpdatedAt.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnDeleteIngredient
            // 
            btnDeleteIngredient.CheckedState.ImageSize = new Size(64, 64);
            btnDeleteIngredient.HoverState.Image = Properties.Resources.icons8_trash_48;
            btnDeleteIngredient.HoverState.ImageSize = new Size(30, 30);
            btnDeleteIngredient.Image = Properties.Resources.icons8_trash_48;
            btnDeleteIngredient.ImageOffset = new Point(0, 0);
            btnDeleteIngredient.ImageRotate = 0F;
            btnDeleteIngredient.ImageSize = new Size(30, 30);
            btnDeleteIngredient.Location = new Point(243, 20);
            btnDeleteIngredient.Name = "btnDeleteIngredient";
            btnDeleteIngredient.PressedState.Image = Properties.Resources.icons8_trash_48;
            btnDeleteIngredient.PressedState.ImageSize = new Size(30, 30);
            btnDeleteIngredient.ShadowDecoration.CustomizableEdges = customizableEdges1;
            btnDeleteIngredient.Size = new Size(33, 29);
            btnDeleteIngredient.TabIndex = 9;
            btnDeleteIngredient.Click += btnDeleteIngredient_Click;
            // 
            // lblMinThreshold
            // 
            lblMinThreshold.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblMinThreshold.AutoSize = true;
            lblMinThreshold.Font = new Font("Poppins", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMinThreshold.ForeColor = SystemColors.ControlDarkDark;
            lblMinThreshold.Location = new Point(233, 168);
            lblMinThreshold.Name = "lblMinThreshold";
            lblMinThreshold.Size = new Size(43, 23);
            lblMinThreshold.TabIndex = 8;
            lblMinThreshold.Text = "500g";
            lblMinThreshold.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblCurrentStock
            // 
            lblCurrentStock.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblCurrentStock.AutoSize = true;
            lblCurrentStock.Font = new Font("Poppins", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCurrentStock.ForeColor = SystemColors.ControlDarkDark;
            lblCurrentStock.Location = new Point(223, 147);
            lblCurrentStock.Name = "lblCurrentStock";
            lblCurrentStock.Size = new Size(53, 23);
            lblCurrentStock.TabIndex = 7;
            lblCurrentStock.Text = "2,000g";
            lblCurrentStock.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Poppins", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ControlDarkDark;
            label2.Location = new Point(22, 168);
            label2.Name = "label2";
            label2.Size = new Size(101, 23);
            label2.TabIndex = 6;
            label2.Text = "Min Threshold:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Poppins", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ControlDarkDark;
            label1.Location = new Point(22, 147);
            label1.Name = "label1";
            label1.Size = new Size(101, 23);
            label1.TabIndex = 5;
            label1.Text = "Current Stock:";
            // 
            // guna2ProgressBar1
            // 
            guna2ProgressBar1.BorderRadius = 10;
            guna2ProgressBar1.CustomizableEdges = customizableEdges2;
            guna2ProgressBar1.Location = new Point(22, 197);
            guna2ProgressBar1.Name = "guna2ProgressBar1";
            guna2ProgressBar1.ShadowDecoration.CustomizableEdges = customizableEdges3;
            guna2ProgressBar1.Size = new Size(254, 14);
            guna2ProgressBar1.TabIndex = 4;
            guna2ProgressBar1.Text = "guna2ProgressBar1";
            guna2ProgressBar1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // btnEditIngredient
            // 
            btnEditIngredient.CheckedState.ImageSize = new Size(64, 64);
            btnEditIngredient.HoverState.Image = Properties.Resources.icons8_pencil_hover_52;
            btnEditIngredient.HoverState.ImageSize = new Size(24, 24);
            btnEditIngredient.Image = Properties.Resources.icons8_pencil_52;
            btnEditIngredient.ImageOffset = new Point(0, 0);
            btnEditIngredient.ImageRotate = 0F;
            btnEditIngredient.ImageSize = new Size(24, 24);
            btnEditIngredient.Location = new Point(204, 20);
            btnEditIngredient.Name = "btnEditIngredient";
            btnEditIngredient.PressedState.Image = Properties.Resources.icons8_pencil_hover_52;
            btnEditIngredient.PressedState.ImageSize = new Size(24, 24);
            btnEditIngredient.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnEditIngredient.Size = new Size(33, 29);
            btnEditIngredient.TabIndex = 3;
            btnEditIngredient.Click += btnEditIngredient_Click;
            // 
            // txtStatus
            // 
            txtStatus.BorderColor = Color.FromArgb(39, 174, 96);
            txtStatus.BorderRadius = 10;
            txtStatus.CustomizableEdges = customizableEdges5;
            txtStatus.DefaultText = "Full";
            txtStatus.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtStatus.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtStatus.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtStatus.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtStatus.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtStatus.Font = new Font("Poppins", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtStatus.ForeColor = Color.Black;
            txtStatus.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtStatus.Location = new Point(36, 94);
            txtStatus.Margin = new Padding(3, 5, 3, 5);
            txtStatus.Name = "txtStatus";
            txtStatus.PlaceholderForeColor = Color.Black;
            txtStatus.PlaceholderText = "";
            txtStatus.ReadOnly = true;
            txtStatus.SelectedText = "";
            txtStatus.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtStatus.Size = new Size(60, 26);
            txtStatus.TabIndex = 2;
            txtStatus.TextAlign = HorizontalAlignment.Center;
            // 
            // txtCategoryLabel
            // 
            txtCategoryLabel.BorderRadius = 10;
            txtCategoryLabel.BorderThickness = 0;
            txtCategoryLabel.CustomizableEdges = customizableEdges7;
            txtCategoryLabel.DefaultText = "Spices & Seasonings";
            txtCategoryLabel.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtCategoryLabel.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtCategoryLabel.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtCategoryLabel.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtCategoryLabel.FillColor = Color.FromArgb(230, 126, 34);
            txtCategoryLabel.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtCategoryLabel.Font = new Font("Poppins", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCategoryLabel.ForeColor = Color.White;
            txtCategoryLabel.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtCategoryLabel.Location = new Point(36, 67);
            txtCategoryLabel.Margin = new Padding(3, 5, 3, 5);
            txtCategoryLabel.Name = "txtCategoryLabel";
            txtCategoryLabel.PlaceholderForeColor = Color.Black;
            txtCategoryLabel.PlaceholderText = "";
            txtCategoryLabel.ReadOnly = true;
            txtCategoryLabel.SelectedText = "";
            txtCategoryLabel.ShadowDecoration.CustomizableEdges = customizableEdges8;
            txtCategoryLabel.Size = new Size(177, 26);
            txtCategoryLabel.TabIndex = 1;
            txtCategoryLabel.TextAlign = HorizontalAlignment.Center;
            // 
            // lblIngredientName
            // 
            lblIngredientName.AutoSize = true;
            lblIngredientName.Font = new Font("Poppins", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblIngredientName.Location = new Point(22, 36);
            lblIngredientName.Name = "lblIngredientName";
            lblIngredientName.Size = new Size(108, 40);
            lblIngredientName.TabIndex = 0;
            lblIngredientName.Text = "Carrots";
            // 
            // IngredientCard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(guna2Panel1);
            Name = "IngredientCard";
            Size = new Size(300, 260);
            guna2Panel1.ResumeLayout(false);
            guna2Panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Label lblIngredientName;
        private Guna.UI2.WinForms.Guna2TextBox txtCategoryLabel;
        private Guna.UI2.WinForms.Guna2TextBox txtStatus;
        private Guna.UI2.WinForms.Guna2ProgressBar guna2ProgressBar1;
        private Guna.UI2.WinForms.Guna2ImageButton btnEditIngredient;
        private Label lblMinThreshold;
        private Label lblCurrentStock;
        private Label label2;
        private Label label1;
        private Label lblUpdatedAt;
        private Guna.UI2.WinForms.Guna2ImageButton btnDeleteIngredient;
    }
}