namespace IRIS.Presentation.Window_Forms
{
    partial class frmAddIngredient
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            lblIngredientTitle = new Label();
            label1 = new Label();
            btnExitForm = new Guna.UI2.WinForms.Guna2ImageButton();
            label2 = new Label();
            txtIngredientName = new Guna.UI2.WinForms.Guna2TextBox();
            label3 = new Label();
            cmbCategory = new Guna.UI2.WinForms.Guna2ComboBox();
            label4 = new Label();
            numCurrentStock = new Guna.UI2.WinForms.Guna2NumericUpDown();
            numMinimumThreshold = new Guna.UI2.WinForms.Guna2NumericUpDown();
            label5 = new Label();
            cmbUnit = new Guna.UI2.WinForms.Guna2ComboBox();
            label6 = new Label();
            btnAddIngredient = new Guna.UI2.WinForms.Guna2GradientButton();
            btnCancel = new Guna.UI2.WinForms.Guna2GradientButton();
            ((System.ComponentModel.ISupportInitialize)numCurrentStock).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMinimumThreshold).BeginInit();
            SuspendLayout();
            // 
            // guna2BorderlessForm1
            // 
            guna2BorderlessForm1.BorderRadius = 50;
            guna2BorderlessForm1.ContainerControl = this;
            guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // lblIngredientTitle
            // 
            lblIngredientTitle.AutoSize = true;
            lblIngredientTitle.Font = new Font("Poppins", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblIngredientTitle.Location = new Point(25, 27);
            lblIngredientTitle.Name = "lblIngredientTitle";
            lblIngredientTitle.Size = new Size(323, 53);
            lblIngredientTitle.TabIndex = 0;
            lblIngredientTitle.Text = "Add New Ingredient";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Poppins", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ControlDarkDark;
            label1.Location = new Point(25, 68);
            label1.Name = "label1";
            label1.Size = new Size(372, 31);
            label1.TabIndex = 1;
            label1.Text = "Enter the details of thew new Ingredient";
            // 
            // btnExitForm
            // 
            btnExitForm.CheckedState.ImageSize = new Size(64, 64);
            btnExitForm.HoverState.ImageSize = new Size(25, 25);
            btnExitForm.Image = Properties.Resources.icons8_exit_100;
            btnExitForm.ImageOffset = new Point(0, 0);
            btnExitForm.ImageRotate = 0F;
            btnExitForm.ImageSize = new Size(24, 24);
            btnExitForm.Location = new Point(559, 27);
            btnExitForm.Name = "btnExitForm";
            btnExitForm.ShadowDecoration.CustomizableEdges = customizableEdges15;
            btnExitForm.Size = new Size(29, 26);
            btnExitForm.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Poppins", 10.8F, FontStyle.Bold);
            label2.ForeColor = SystemColors.ActiveCaptionText;
            label2.Location = new Point(55, 118);
            label2.Name = "label2";
            label2.Size = new Size(169, 31);
            label2.TabIndex = 3;
            label2.Text = "Ingredient Name";
            // 
            // txtIngredientName
            // 
            txtIngredientName.BorderColor = SystemColors.ControlDark;
            txtIngredientName.BorderRadius = 10;
            txtIngredientName.CustomizableEdges = customizableEdges13;
            txtIngredientName.DefaultText = "";
            txtIngredientName.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtIngredientName.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtIngredientName.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtIngredientName.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtIngredientName.FocusedState.BorderColor = Color.FromArgb(77, 10, 133);
            txtIngredientName.Font = new Font("Poppins", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtIngredientName.HoverState.BorderColor = Color.FromArgb(77, 10, 133);
            txtIngredientName.Location = new Point(55, 150);
            txtIngredientName.Margin = new Padding(5, 12, 5, 12);
            txtIngredientName.Name = "txtIngredientName";
            txtIngredientName.PlaceholderText = "e.g. Chicken Breast";
            txtIngredientName.SelectedText = "";
            txtIngredientName.ShadowDecoration.CustomizableEdges = customizableEdges14;
            txtIngredientName.Size = new Size(490, 38);
            txtIngredientName.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Poppins", 10.8F, FontStyle.Bold);
            label3.ForeColor = SystemColors.ActiveCaptionText;
            label3.Location = new Point(55, 222);
            label3.Name = "label3";
            label3.Size = new Size(100, 31);
            label3.TabIndex = 5;
            label3.Text = "Category";
            // 
            // cmbCategory
            // 
            cmbCategory.BackColor = Color.Transparent;
            cmbCategory.BorderColor = SystemColors.ControlDark;
            cmbCategory.BorderRadius = 10;
            cmbCategory.CustomizableEdges = customizableEdges11;
            cmbCategory.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.FocusedColor = Color.FromArgb(77, 10, 133);
            cmbCategory.FocusedState.BorderColor = Color.FromArgb(77, 10, 133);
            cmbCategory.Font = new Font("Poppins", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbCategory.ForeColor = Color.FromArgb(68, 88, 112);
            cmbCategory.HoverState.BorderColor = Color.FromArgb(77, 10, 133);
            cmbCategory.ItemHeight = 30;
            cmbCategory.Items.AddRange(new object[] { "Produce", "Protein", "Dairy & Eggs", "Pantry Staples", "Spices & Seasonings", "Condiments & Oils", "Grains & Legumes", "Bakery & Sweets", "Beverages", "Frozen & Prepared" });
            cmbCategory.ItemsAppearance.BackColor = Color.White;
            cmbCategory.ItemsAppearance.ForeColor = Color.Black;
            cmbCategory.ItemsAppearance.SelectedBackColor = Color.FromArgb(77, 10, 133);
            cmbCategory.ItemsAppearance.SelectedForeColor = Color.White;
            cmbCategory.Location = new Point(55, 256);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.ShadowDecoration.CustomizableEdges = customizableEdges12;
            cmbCategory.Size = new Size(490, 36);
            cmbCategory.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Poppins", 10.8F, FontStyle.Bold);
            label4.ForeColor = SystemColors.ActiveCaptionText;
            label4.Location = new Point(55, 327);
            label4.Name = "label4";
            label4.Size = new Size(141, 31);
            label4.TabIndex = 7;
            label4.Text = "Current Stock";
            // 
            // numCurrentStock
            // 
            numCurrentStock.BackColor = Color.Transparent;
            numCurrentStock.BorderColor = SystemColors.ControlDark;
            numCurrentStock.BorderRadius = 10;
            numCurrentStock.CustomizableEdges = customizableEdges9;
            numCurrentStock.FocusedState.BorderColor = Color.FromArgb(137, 65, 208);
            numCurrentStock.FocusedState.UpDownButtonFillColor = Color.FromArgb(155, 86, 226);
            numCurrentStock.Font = new Font("Poppins", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numCurrentStock.Location = new Point(51, 362);
            numCurrentStock.Margin = new Padding(3, 4, 3, 4);
            numCurrentStock.Maximum = new decimal(new int[] { 5000000, 0, 0, 0 });
            numCurrentStock.Name = "numCurrentStock";
            numCurrentStock.ShadowDecoration.CustomizableEdges = customizableEdges10;
            numCurrentStock.Size = new Size(226, 39);
            numCurrentStock.TabIndex = 8;
            numCurrentStock.TextOffset = new Point(5, 0);
            numCurrentStock.ThousandsSeparator = true;
            numCurrentStock.UpDownButtonFillColor = Color.Gray;
            numCurrentStock.UpDownButtonForeColor = Color.White;
            // 
            // numMinimumThreshold
            // 
            numMinimumThreshold.BackColor = Color.Transparent;
            numMinimumThreshold.BorderColor = SystemColors.ControlDark;
            numMinimumThreshold.BorderRadius = 10;
            numMinimumThreshold.CustomizableEdges = customizableEdges7;
            numMinimumThreshold.FocusedState.BorderColor = Color.FromArgb(137, 65, 208);
            numMinimumThreshold.FocusedState.UpDownButtonFillColor = Color.FromArgb(155, 86, 226);
            numMinimumThreshold.Font = new Font("Poppins", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numMinimumThreshold.Location = new Point(318, 362);
            numMinimumThreshold.Margin = new Padding(3, 4, 3, 4);
            numMinimumThreshold.Maximum = new decimal(new int[] { 5000000, 0, 0, 0 });
            numMinimumThreshold.Name = "numMinimumThreshold";
            numMinimumThreshold.ShadowDecoration.CustomizableEdges = customizableEdges8;
            numMinimumThreshold.Size = new Size(227, 39);
            numMinimumThreshold.TabIndex = 10;
            numMinimumThreshold.TextOffset = new Point(5, 0);
            numMinimumThreshold.UpDownButtonFillColor = Color.Gray;
            numMinimumThreshold.UpDownButtonForeColor = Color.White;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Poppins", 10.8F, FontStyle.Bold);
            label5.ForeColor = SystemColors.ActiveCaptionText;
            label5.Location = new Point(318, 327);
            label5.Name = "label5";
            label5.Size = new Size(199, 31);
            label5.TabIndex = 9;
            label5.Text = "Minimum Threshold";
            // 
            // cmbUnit
            // 
            cmbUnit.BackColor = Color.Transparent;
            cmbUnit.BorderColor = SystemColors.ControlDark;
            cmbUnit.BorderRadius = 10;
            cmbUnit.CustomizableEdges = customizableEdges5;
            cmbUnit.DrawMode = DrawMode.OwnerDrawFixed;
            cmbUnit.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUnit.FocusedColor = Color.FromArgb(77, 10, 133);
            cmbUnit.FocusedState.BorderColor = Color.FromArgb(77, 10, 133);
            cmbUnit.Font = new Font("Poppins", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbUnit.ForeColor = Color.FromArgb(68, 88, 112);
            cmbUnit.HoverState.BorderColor = Color.FromArgb(77, 10, 133);
            cmbUnit.ItemHeight = 30;
            cmbUnit.Items.AddRange(new object[] { "Grams", "Kilograms", "Pieces" });
            cmbUnit.ItemsAppearance.BackColor = Color.White;
            cmbUnit.ItemsAppearance.ForeColor = Color.Black;
            cmbUnit.ItemsAppearance.SelectedBackColor = Color.FromArgb(77, 10, 133);
            cmbUnit.ItemsAppearance.SelectedForeColor = Color.White;
            cmbUnit.Location = new Point(51, 469);
            cmbUnit.Name = "cmbUnit";
            cmbUnit.ShadowDecoration.CustomizableEdges = customizableEdges6;
            cmbUnit.Size = new Size(490, 36);
            cmbUnit.TabIndex = 12;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Poppins", 10.8F, FontStyle.Bold);
            label6.ForeColor = SystemColors.ActiveCaptionText;
            label6.Location = new Point(51, 435);
            label6.Name = "label6";
            label6.Size = new Size(51, 31);
            label6.TabIndex = 11;
            label6.Text = "Unit";
            // 
            // btnAddIngredient
            // 
            btnAddIngredient.BorderRadius = 10;
            btnAddIngredient.CustomizableEdges = customizableEdges3;
            btnAddIngredient.DialogResult = DialogResult.Continue;
            btnAddIngredient.DisabledState.BorderColor = Color.DarkGray;
            btnAddIngredient.DisabledState.CustomBorderColor = Color.DarkGray;
            btnAddIngredient.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnAddIngredient.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            btnAddIngredient.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnAddIngredient.FillColor = Color.FromArgb(77, 10, 133);
            btnAddIngredient.FillColor2 = Color.FromArgb(137, 65, 208);
            btnAddIngredient.Font = new Font("Poppins", 10.2F, FontStyle.Bold);
            btnAddIngredient.ForeColor = Color.White;
            btnAddIngredient.HoverState.FillColor = Color.FromArgb(95, 20, 161);
            btnAddIngredient.HoverState.FillColor2 = Color.FromArgb(155, 86, 226);
            btnAddIngredient.Location = new Point(349, 591);
            btnAddIngredient.Name = "btnAddIngredient";
            btnAddIngredient.PressedColor = Color.FromArgb(111, 49, 171);
            btnAddIngredient.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnAddIngredient.Size = new Size(220, 60);
            btnAddIngredient.TabIndex = 13;
            btnAddIngredient.Text = "Add Ingredient";
            btnAddIngredient.Click += btnAddIngredient_Click;
            // 
            // btnCancel
            // 
            btnCancel.BorderColor = Color.FromArgb(77, 10, 133);
            btnCancel.BorderRadius = 10;
            btnCancel.BorderThickness = 1;
            btnCancel.CustomizableEdges = customizableEdges1;
            btnCancel.DialogResult = DialogResult.Continue;
            btnCancel.DisabledState.BorderColor = Color.DarkGray;
            btnCancel.DisabledState.CustomBorderColor = Color.DarkGray;
            btnCancel.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnCancel.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            btnCancel.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnCancel.FillColor = Color.White;
            btnCancel.FillColor2 = Color.White;
            btnCancel.Font = new Font("Poppins", 10.2F, FontStyle.Bold);
            btnCancel.ForeColor = Color.Black;
            btnCancel.HoverState.FillColor = Color.Gray;
            btnCancel.HoverState.FillColor2 = Color.Silver;
            btnCancel.Location = new Point(179, 591);
            btnCancel.Name = "btnCancel";
            btnCancel.PressedColor = Color.Gray;
            btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnCancel.Size = new Size(150, 60);
            btnCancel.TabIndex = 14;
            btnCancel.Text = "Cancel";
            // 
            // frmAddIngredient
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 700);
            Controls.Add(btnCancel);
            Controls.Add(btnAddIngredient);
            Controls.Add(cmbUnit);
            Controls.Add(label6);
            Controls.Add(numMinimumThreshold);
            Controls.Add(label5);
            Controls.Add(numCurrentStock);
            Controls.Add(label4);
            Controls.Add(cmbCategory);
            Controls.Add(label3);
            Controls.Add(txtIngredientName);
            Controls.Add(label2);
            Controls.Add(btnExitForm);
            Controls.Add(label1);
            Controls.Add(lblIngredientTitle);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmAddIngredient";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmAddIngredient";
            ((System.ComponentModel.ISupportInitialize)numCurrentStock).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMinimumThreshold).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2ImageButton btnExitForm;
        private Label label1;
        private Label lblIngredientTitle;
        private Guna.UI2.WinForms.Guna2TextBox txtIngredientName;
        private Label label2;
        private Guna.UI2.WinForms.Guna2ComboBox cmbCategory;
        private Label label3;
        private Guna.UI2.WinForms.Guna2NumericUpDown numCurrentStock;
        private Label label4;
        private Guna.UI2.WinForms.Guna2GradientButton btnAddIngredient;
        private Guna.UI2.WinForms.Guna2ComboBox cmbUnit;
        private Label label6;
        private Guna.UI2.WinForms.Guna2NumericUpDown numMinimumThreshold;
        private Label label5;
        private Guna.UI2.WinForms.Guna2GradientButton btnCancel;
    }
}