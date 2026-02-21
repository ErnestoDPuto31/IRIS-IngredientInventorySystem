namespace IRIS.Presentation.Window_Forms
{
    partial class frmRestockIngredient
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRestockIngredient));
            guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            pnlMainContent = new Guna.UI2.WinForms.Guna2Panel();
            lblError = new Label();
            btnCancel = new Guna.UI2.WinForms.Guna2Button();
            btnConfirm = new Guna.UI2.WinForms.Guna2Button();
            pnlResult = new Guna.UI2.WinForms.Guna2Panel();
            lblNewStockText = new Label();
            label6 = new Label();
            txtRemarks = new Guna.UI2.WinForms.Guna2TextBox();
            label5 = new Label();
            numQuantity = new Guna.UI2.WinForms.Guna2NumericUpDown();
            label4 = new Label();
            pnlInfo = new Guna.UI2.WinForms.Guna2Panel();
            lblSuggestedValue = new Label();
            lblMinValue = new Label();
            lblCurrentValue = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            lblSubtitle = new Label();
            lblTitle = new Label();
            pnlMainContent.SuspendLayout();
            pnlResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numQuantity).BeginInit();
            pnlInfo.SuspendLayout();
            SuspendLayout();
            // 
            // guna2BorderlessForm1
            // 
            guna2BorderlessForm1.BorderRadius = 30;
            guna2BorderlessForm1.ContainerControl = this;
            guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // pnlMainContent
            // 
            pnlMainContent.BackColor = Color.White;
            pnlMainContent.Controls.Add(lblError);
            pnlMainContent.Controls.Add(btnCancel);
            pnlMainContent.Controls.Add(btnConfirm);
            pnlMainContent.Controls.Add(pnlResult);
            pnlMainContent.Controls.Add(txtRemarks);
            pnlMainContent.Controls.Add(label5);
            pnlMainContent.Controls.Add(numQuantity);
            pnlMainContent.Controls.Add(label4);
            pnlMainContent.Controls.Add(pnlInfo);
            pnlMainContent.Controls.Add(lblSubtitle);
            pnlMainContent.Controls.Add(lblTitle);
            pnlMainContent.CustomizableEdges = customizableEdges13;
            pnlMainContent.Dock = DockStyle.Fill;
            pnlMainContent.Location = new Point(0, 0);
            pnlMainContent.Name = "pnlMainContent";
            pnlMainContent.ShadowDecoration.CustomizableEdges = customizableEdges14;
            pnlMainContent.Size = new Size(500, 624);
            pnlMainContent.TabIndex = 0;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.Font = new Font("Poppins", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblError.ForeColor = Color.Red;
            lblError.Location = new Point(34, 495);
            lblError.Name = "lblError";
            lblError.Size = new Size(57, 26);
            lblError.TabIndex = 9;
            lblError.Text = "label7";
            lblError.Visible = false;
            // 
            // btnCancel
            // 
            btnCancel.BorderColor = Color.Gray;
            btnCancel.BorderRadius = 15;
            btnCancel.BorderThickness = 2;
            btnCancel.CustomizableEdges = customizableEdges1;
            btnCancel.DisabledState.BorderColor = Color.DarkGray;
            btnCancel.DisabledState.CustomBorderColor = Color.DarkGray;
            btnCancel.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnCancel.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnCancel.FillColor = Color.White;
            btnCancel.FocusedColor = Color.Red;
            btnCancel.Font = new Font("Poppins", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancel.ForeColor = Color.Gray;
            btnCancel.HoverState.BorderColor = Color.Red;
            btnCancel.HoverState.FillColor = Color.Red;
            btnCancel.HoverState.ForeColor = Color.White;
            btnCancel.ImageAlign = HorizontalAlignment.Left;
            btnCancel.Location = new Point(107, 549);
            btnCancel.Name = "btnCancel";
            btnCancel.PressedColor = Color.DarkRed;
            btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnCancel.Size = new Size(126, 46);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "Cancel";
            btnCancel.Click += BtnCancel_Click;
            // 
            // btnConfirm
            // 
            btnConfirm.BorderRadius = 15;
            btnConfirm.CustomizableEdges = customizableEdges3;
            btnConfirm.DisabledState.BorderColor = Color.DarkGray;
            btnConfirm.DisabledState.CustomBorderColor = Color.DarkGray;
            btnConfirm.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnConfirm.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnConfirm.FillColor = Color.Indigo;
            btnConfirm.FocusedColor = Color.FromArgb(100, 22, 168);
            btnConfirm.Font = new Font("Poppins", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnConfirm.ForeColor = Color.White;
            btnConfirm.HoverState.FillColor = Color.FromArgb(100, 22, 168);
            btnConfirm.Image = Properties.Resources.icons8_arrow_up_24;
            btnConfirm.ImageAlign = HorizontalAlignment.Left;
            btnConfirm.Location = new Point(239, 549);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.PressedColor = Color.FromArgb(58, 14, 97);
            btnConfirm.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnConfirm.Size = new Size(225, 46);
            btnConfirm.TabIndex = 7;
            btnConfirm.Text = "Confirm Restock";
            btnConfirm.TextOffset = new Point(10, 0);
            btnConfirm.Click += BtnConfirm_Click;
            // 
            // pnlResult
            // 
            pnlResult.BackColor = Color.FromArgb(238, 255, 242);
            pnlResult.BorderColor = Color.FromArgb(238, 255, 242);
            pnlResult.BorderRadius = 15;
            pnlResult.BorderThickness = 5;
            pnlResult.Controls.Add(lblNewStockText);
            pnlResult.Controls.Add(label6);
            pnlResult.CustomizableEdges = customizableEdges5;
            pnlResult.Location = new Point(34, 431);
            pnlResult.Name = "pnlResult";
            pnlResult.ShadowDecoration.CustomizableEdges = customizableEdges6;
            pnlResult.Size = new Size(430, 50);
            pnlResult.TabIndex = 6;
            // 
            // lblNewStockText
            // 
            lblNewStockText.AutoSize = true;
            lblNewStockText.Font = new Font("Poppins", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNewStockText.ForeColor = Color.FromArgb(15, 110, 45);
            lblNewStockText.Location = new Point(220, 11);
            lblNewStockText.Name = "lblNewStockText";
            lblNewStockText.Size = new Size(119, 30);
            lblNewStockText.TabIndex = 4;
            lblNewStockText.Text = "2000 Grams";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Poppins", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.FromArgb(15, 110, 45);
            label6.Location = new Point(14, 11);
            label6.Name = "label6";
            label6.Size = new Size(207, 30);
            label6.TabIndex = 3;
            label6.Text = "New stock level will be:";
            // 
            // txtRemarks
            // 
            txtRemarks.BorderColor = SystemColors.ControlDark;
            txtRemarks.BorderRadius = 10;
            txtRemarks.CustomizableEdges = customizableEdges7;
            txtRemarks.DefaultText = "";
            txtRemarks.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtRemarks.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtRemarks.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtRemarks.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtRemarks.FocusedState.BorderColor = Color.FromArgb(77, 10, 133);
            txtRemarks.Font = new Font("Poppins", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtRemarks.HoverState.BorderColor = Color.FromArgb(77, 10, 133);
            txtRemarks.Location = new Point(34, 368);
            txtRemarks.Margin = new Padding(5, 12, 5, 12);
            txtRemarks.Name = "txtRemarks";
            txtRemarks.PlaceholderText = "e.g., Supplier deliver";
            txtRemarks.SelectedText = "";
            txtRemarks.ShadowDecoration.CustomizableEdges = customizableEdges8;
            txtRemarks.Size = new Size(431, 38);
            txtRemarks.TabIndex = 2;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Poppins", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Black;
            label5.Location = new Point(34, 338);
            label5.Name = "label5";
            label5.Size = new Size(187, 30);
            label5.TabIndex = 4;
            label5.Text = "Remarks (Optional)";
            // 
            // numQuantity
            // 
            numQuantity.BackColor = Color.Transparent;
            numQuantity.BorderColor = SystemColors.ControlDark;
            numQuantity.BorderRadius = 10;
            numQuantity.CustomizableEdges = customizableEdges9;
            numQuantity.FocusedState.BorderColor = Color.FromArgb(137, 65, 208);
            numQuantity.FocusedState.UpDownButtonFillColor = Color.FromArgb(155, 86, 226);
            numQuantity.Font = new Font("Poppins", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numQuantity.Location = new Point(34, 284);
            numQuantity.Margin = new Padding(3, 4, 3, 4);
            numQuantity.Maximum = new decimal(new int[] { 5000000, 0, 0, 0 });
            numQuantity.Name = "numQuantity";
            numQuantity.ShadowDecoration.CustomizableEdges = customizableEdges10;
            numQuantity.Size = new Size(431, 39);
            numQuantity.TabIndex = 1;
            numQuantity.TextOffset = new Point(5, 0);
            numQuantity.ThousandsSeparator = true;
            numQuantity.UpDownButtonFillColor = Color.Gray;
            numQuantity.UpDownButtonForeColor = Color.White;
            numQuantity.ValueChanged += NumQuantity_ValueChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Poppins", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(34, 250);
            label4.Name = "label4";
            label4.Size = new Size(218, 30);
            label4.TabIndex = 3;
            label4.Text = "Restock Quantity (Unit)";
            // 
            // pnlInfo
            // 
            pnlInfo.BackColor = SystemColors.ControlLight;
            pnlInfo.BorderColor = SystemColors.ControlLight;
            pnlInfo.BorderRadius = 20;
            pnlInfo.BorderThickness = 10;
            pnlInfo.Controls.Add(lblSuggestedValue);
            pnlInfo.Controls.Add(lblMinValue);
            pnlInfo.Controls.Add(lblCurrentValue);
            pnlInfo.Controls.Add(label3);
            pnlInfo.Controls.Add(label2);
            pnlInfo.Controls.Add(label1);
            pnlInfo.CustomizableEdges = customizableEdges11;
            pnlInfo.Location = new Point(34, 100);
            pnlInfo.Name = "pnlInfo";
            pnlInfo.ShadowDecoration.CustomizableEdges = customizableEdges12;
            pnlInfo.Size = new Size(431, 128);
            pnlInfo.TabIndex = 2;
            // 
            // lblSuggestedValue
            // 
            lblSuggestedValue.AutoSize = true;
            lblSuggestedValue.Font = new Font("Poppins", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSuggestedValue.ForeColor = Color.Indigo;
            lblSuggestedValue.Location = new Point(191, 85);
            lblSuggestedValue.Name = "lblSuggestedValue";
            lblSuggestedValue.RightToLeft = RightToLeft.No;
            lblSuggestedValue.Size = new Size(102, 26);
            lblSuggestedValue.TabIndex = 5;
            lblSuggestedValue.Text = "1000 Grams";
            lblSuggestedValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblMinValue
            // 
            lblMinValue.AutoSize = true;
            lblMinValue.Font = new Font("Poppins", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMinValue.ForeColor = Color.Gray;
            lblMinValue.Location = new Point(191, 49);
            lblMinValue.Name = "lblMinValue";
            lblMinValue.Size = new Size(105, 26);
            lblMinValue.TabIndex = 4;
            lblMinValue.Text = "2000 Grams";
            lblMinValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblCurrentValue
            // 
            lblCurrentValue.AutoSize = true;
            lblCurrentValue.Font = new Font("Poppins", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCurrentValue.ForeColor = Color.Black;
            lblCurrentValue.Location = new Point(191, 13);
            lblCurrentValue.Name = "lblCurrentValue";
            lblCurrentValue.Size = new Size(102, 26);
            lblCurrentValue.TabIndex = 3;
            lblCurrentValue.Text = "1500 Grams";
            lblCurrentValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Poppins", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Gray;
            label3.Location = new Point(14, 85);
            label3.Name = "label3";
            label3.Size = new Size(157, 26);
            label3.TabIndex = 2;
            label3.Text = "Suggested Restock:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Poppins", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Gray;
            label2.Location = new Point(14, 49);
            label2.Name = "label2";
            label2.Size = new Size(121, 26);
            label2.TabIndex = 1;
            label2.Text = "Min Threshold:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Poppins", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Gray;
            label1.Location = new Point(14, 13);
            label1.Name = "label1";
            label1.Size = new Size(118, 26);
            label1.TabIndex = 0;
            label1.Text = "Current Stock:";
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Poppins", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSubtitle.ForeColor = Color.FromArgb(120, 120, 120);
            lblSubtitle.Location = new Point(12, 57);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(335, 30);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Add stock to increase inventory levels";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Poppins", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.FromArgb(50, 50, 50);
            lblTitle.Location = new Point(12, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(381, 50);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Restock All Purpose Flour";
            // 
            // frmRestockIngredient
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(500, 624);
            Controls.Add(pnlMainContent);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "frmRestockIngredient";
            StartPosition = FormStartPosition.CenterParent;
            Text = "frmRestockIngredient";
            pnlMainContent.ResumeLayout(false);
            pnlMainContent.PerformLayout();
            pnlResult.ResumeLayout(false);
            pnlResult.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numQuantity).EndInit();
            pnlInfo.ResumeLayout(false);
            pnlInfo.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2Panel pnlMainContent;
        private Guna.UI2.WinForms.Guna2Panel pnlInfo;
        private Label label1;
        private Label lblSubtitle;
        private Label lblTitle;
        private Label lblSuggestedValue;
        private Label lblMinValue;
        private Label lblCurrentValue;
        private Label label3;
        private Label label2;
        private Label label4;
        private Guna.UI2.WinForms.Guna2NumericUpDown numQuantity;
        private Label label5;
        private Guna.UI2.WinForms.Guna2Panel pnlResult;
        private Guna.UI2.WinForms.Guna2TextBox txtRemarks;
        private Guna.UI2.WinForms.Guna2Button btnConfirm;
        private Label lblNewStockText;
        private Label label6;
        private Guna.UI2.WinForms.Guna2Button btnCancel;
        private Label lblError;
    }
}