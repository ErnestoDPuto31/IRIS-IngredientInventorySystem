namespace IRIS.Presentation.Window_Forms
{
    partial class frmIngredientSelector
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIngredientSelector));
            guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            ucIngredientTable = new IRIS.Presentation.UserControls.Components.Tables.IngredientTableUC();
            btnCancel = new Guna.UI2.WinForms.Guna2GradientButton();
            btnSave = new Guna.UI2.WinForms.Guna2GradientButton();
            pnlInput = new Guna.UI2.WinForms.Guna2Panel();
            label2 = new Label();
            label1 = new Label();
            numRequestedQty = new Guna.UI2.WinForms.Guna2NumericUpDown();
            lblSelectedName = new Label();
            label3 = new Label();
            lstSummary = new ListBox();
            btnAdd = new Guna.UI2.WinForms.Guna2GradientButton();
            btnRemove = new Guna.UI2.WinForms.Guna2GradientButton();
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            pnlInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numRequestedQty).BeginInit();
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
            // ucIngredientTable
            // 
            ucIngredientTable.BackColor = Color.White;
            ucIngredientTable.Location = new Point(27, 27);
            ucIngredientTable.Name = "ucIngredientTable";
            ucIngredientTable.Padding = new Padding(25);
            ucIngredientTable.Size = new Size(809, 426);
            ucIngredientTable.TabIndex = 0;
            // 
            // btnCancel
            // 
            btnCancel.BorderColor = Color.FromArgb(77, 10, 133);
            btnCancel.BorderRadius = 10;
            btnCancel.BorderThickness = 1;
            btnCancel.CustomizableEdges = customizableEdges11;
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
            btnCancel.Location = new Point(260, 595);
            btnCancel.Name = "btnCancel";
            btnCancel.PressedColor = Color.Gray;
            btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnCancel.Size = new Size(150, 49);
            btnCancel.TabIndex = 16;
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.BorderRadius = 10;
            btnSave.CustomizableEdges = customizableEdges13;
            btnSave.DialogResult = DialogResult.Continue;
            btnSave.DisabledState.BorderColor = Color.DarkGray;
            btnSave.DisabledState.CustomBorderColor = Color.DarkGray;
            btnSave.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnSave.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            btnSave.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnSave.FillColor = Color.FromArgb(77, 10, 133);
            btnSave.FillColor2 = Color.FromArgb(137, 65, 208);
            btnSave.Font = new Font("Poppins", 10.2F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.HoverState.FillColor = Color.FromArgb(95, 20, 161);
            btnSave.HoverState.FillColor2 = Color.FromArgb(155, 86, 226);
            btnSave.Image = Properties.Resources.icons8_approve_24;
            btnSave.ImageOffset = new Point(-2, 0);
            btnSave.Location = new Point(426, 595);
            btnSave.Name = "btnSave";
            btnSave.PressedColor = Color.FromArgb(111, 49, 171);
            btnSave.ShadowDecoration.CustomizableEdges = customizableEdges14;
            btnSave.Size = new Size(161, 49);
            btnSave.TabIndex = 15;
            btnSave.Text = "Save List";
            btnSave.Click += btnSave_Click;
            // 
            // pnlInput
            // 
            pnlInput.BorderColor = SystemColors.ControlDark;
            pnlInput.BorderRadius = 15;
            pnlInput.BorderThickness = 1;
            pnlInput.Controls.Add(label2);
            pnlInput.Controls.Add(label1);
            pnlInput.Controls.Add(numRequestedQty);
            pnlInput.Controls.Add(lblSelectedName);
            pnlInput.CustomizableEdges = customizableEdges9;
            pnlInput.Enabled = false;
            pnlInput.Location = new Point(27, 480);
            pnlInput.Name = "pnlInput";
            pnlInput.ShadowDecoration.CustomizableEdges = customizableEdges10;
            pnlInput.Size = new Size(809, 91);
            pnlInput.TabIndex = 17;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Poppins", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(33, 5);
            label2.Name = "label2";
            label2.Size = new Size(92, 30);
            label2.TabIndex = 14;
            label2.Text = "Selected:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Poppins", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(553, 5);
            label1.Name = "label1";
            label1.Size = new Size(186, 30);
            label1.TabIndex = 13;
            label1.Text = "Requested Quantity";
            // 
            // numRequestedQty
            // 
            numRequestedQty.BackColor = Color.Transparent;
            numRequestedQty.BorderColor = SystemColors.ControlDark;
            numRequestedQty.BorderRadius = 10;
            numRequestedQty.CustomizableEdges = customizableEdges7;
            numRequestedQty.FocusedState.BorderColor = Color.FromArgb(137, 65, 208);
            numRequestedQty.FocusedState.UpDownButtonFillColor = Color.FromArgb(155, 86, 226);
            numRequestedQty.Font = new Font("Poppins", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numRequestedQty.Location = new Point(553, 35);
            numRequestedQty.Margin = new Padding(3, 4, 3, 4);
            numRequestedQty.Maximum = new decimal(new int[] { 5000000, 0, 0, 0 });
            numRequestedQty.Name = "numRequestedQty";
            numRequestedQty.ShadowDecoration.CustomizableEdges = customizableEdges8;
            numRequestedQty.Size = new Size(221, 39);
            numRequestedQty.TabIndex = 12;
            numRequestedQty.TextOffset = new Point(5, 0);
            numRequestedQty.UpDownButtonFillColor = Color.Gray;
            numRequestedQty.UpDownButtonForeColor = Color.White;
            // 
            // lblSelectedName
            // 
            lblSelectedName.AutoSize = true;
            lblSelectedName.Font = new Font("Poppins", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSelectedName.ForeColor = Color.Indigo;
            lblSelectedName.Location = new Point(33, 35);
            lblSelectedName.Name = "lblSelectedName";
            lblSelectedName.Size = new Size(209, 30);
            lblSelectedName.TabIndex = 0;
            lblSelectedName.Text = "No Ingredient Selected";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Poppins", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(62, 17);
            label3.Name = "label3";
            label3.Size = new Size(290, 30);
            label3.TabIndex = 14;
            label3.Text = "Ingredient Requested Summary";
            // 
            // lstSummary
            // 
            lstSummary.BorderStyle = BorderStyle.None;
            lstSummary.Font = new Font("Poppins", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lstSummary.FormattingEnabled = true;
            lstSummary.Location = new Point(24, 50);
            lstSummary.Name = "lstSummary";
            lstSummary.Size = new Size(348, 450);
            lstSummary.TabIndex = 0;
            // 
            // btnAdd
            // 
            btnAdd.BorderRadius = 10;
            btnAdd.CustomizableEdges = customizableEdges5;
            btnAdd.DialogResult = DialogResult.Continue;
            btnAdd.DisabledState.BorderColor = Color.DarkGray;
            btnAdd.DisabledState.CustomBorderColor = Color.DarkGray;
            btnAdd.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnAdd.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            btnAdd.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnAdd.FillColor = Color.FromArgb(77, 10, 133);
            btnAdd.FillColor2 = Color.FromArgb(137, 65, 208);
            btnAdd.Font = new Font("Poppins", 10.2F, FontStyle.Bold);
            btnAdd.ForeColor = Color.White;
            btnAdd.HoverState.FillColor = Color.FromArgb(95, 20, 161);
            btnAdd.HoverState.FillColor2 = Color.FromArgb(155, 86, 226);
            btnAdd.Image = Properties.Resources.icons8_add_100;
            btnAdd.ImageOffset = new Point(-5, -2);
            btnAdd.Location = new Point(599, 595);
            btnAdd.Name = "btnAdd";
            btnAdd.PressedColor = Color.FromArgb(111, 49, 171);
            btnAdd.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnAdd.Size = new Size(237, 49);
            btnAdd.TabIndex = 19;
            btnAdd.Text = "Add To List";
            // 
            // btnRemove
            // 
            btnRemove.BorderColor = Color.Red;
            btnRemove.BorderRadius = 10;
            btnRemove.BorderThickness = 1;
            btnRemove.CustomizableEdges = customizableEdges3;
            btnRemove.DialogResult = DialogResult.Continue;
            btnRemove.DisabledState.BorderColor = Color.DarkGray;
            btnRemove.DisabledState.CustomBorderColor = Color.DarkGray;
            btnRemove.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnRemove.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            btnRemove.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnRemove.FillColor = Color.White;
            btnRemove.FillColor2 = Color.White;
            btnRemove.Font = new Font("Poppins", 10.2F, FontStyle.Bold);
            btnRemove.ForeColor = Color.Red;
            btnRemove.HoverState.FillColor = Color.Red;
            btnRemove.HoverState.FillColor2 = Color.Red;
            btnRemove.HoverState.ForeColor = Color.White;
            btnRemove.Location = new Point(993, 595);
            btnRemove.Name = "btnRemove";
            btnRemove.PressedColor = Color.DarkRed;
            btnRemove.PressedDepth = 15;
            btnRemove.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnRemove.Size = new Size(167, 49);
            btnRemove.TabIndex = 20;
            btnRemove.Text = "Remove Item";
            btnRemove.Click += btnRemove_Click;
            // 
            // guna2Panel1
            // 
            guna2Panel1.BorderColor = SystemColors.ControlDark;
            guna2Panel1.BorderRadius = 20;
            guna2Panel1.BorderThickness = 1;
            guna2Panel1.Controls.Add(lstSummary);
            guna2Panel1.Controls.Add(label3);
            guna2Panel1.CustomizableEdges = customizableEdges1;
            guna2Panel1.Location = new Point(865, 27);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2Panel1.Size = new Size(395, 544);
            guna2Panel1.TabIndex = 21;
            // 
            // frmIngredientSelector
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1300, 670);
            Controls.Add(guna2Panel1);
            Controls.Add(btnRemove);
            Controls.Add(btnAdd);
            Controls.Add(pnlInput);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(ucIngredientTable);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "frmIngredientSelector";
            StartPosition = FormStartPosition.CenterParent;
            Text = "frmIngredientSelector";
            Load += frmIngredientSelector_Load;
            pnlInput.ResumeLayout(false);
            pnlInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numRequestedQty).EndInit();
            guna2Panel1.ResumeLayout(false);
            guna2Panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private UserControls.Components.Tables.IngredientTableUC ucIngredientTable;
        private Guna.UI2.WinForms.Guna2GradientButton btnCancel;
        private Guna.UI2.WinForms.Guna2GradientButton btnSave;
        private Guna.UI2.WinForms.Guna2Panel pnlInput;
        private Label lblSelectedName;
        private Guna.UI2.WinForms.Guna2NumericUpDown numRequestedQty;
        private Label label1;
        private Label label2;
        private Label label3;
        private ListBox lstSummary;
        private Guna.UI2.WinForms.Guna2GradientButton btnAdd;
        private Guna.UI2.WinForms.Guna2GradientButton btnRemove;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
    }
}