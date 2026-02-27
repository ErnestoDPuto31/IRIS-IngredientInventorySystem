namespace IRIS.Presentation.UserControls.PagesUC
{
    partial class HistoryControl
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
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            btnNext = new Guna.UI2.WinForms.Guna2Button();
            btnPrevious = new Guna.UI2.WinForms.Guna2Button();
            lblPageInfo = new Label();
            historyTableuc1 = new IRIS.Presentation.UserControls.Components.HistoryTableUC();
            label2 = new Label();
            label1 = new Label();
            guna2Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // guna2BorderlessForm1
            // 
            guna2BorderlessForm1.ContainerControl = this;
            guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // guna2Panel1
            // 
            guna2Panel1.BackColor = Color.White;
            guna2Panel1.Controls.Add(btnNext);
            guna2Panel1.Controls.Add(btnPrevious);
            guna2Panel1.Controls.Add(lblPageInfo);
            guna2Panel1.Controls.Add(historyTableuc1);
            guna2Panel1.Controls.Add(label2);
            guna2Panel1.Controls.Add(label1);
            guna2Panel1.CustomizableEdges = customizableEdges5;
            guna2Panel1.Dock = DockStyle.Fill;
            guna2Panel1.Location = new Point(0, 0);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges6;
            guna2Panel1.Size = new Size(1600, 900);
            guna2Panel1.TabIndex = 0;
            // 
            // btnNext
            // 
            btnNext.BorderColor = Color.Indigo;
            btnNext.BorderRadius = 10;
            btnNext.BorderThickness = 1;
            btnNext.CustomizableEdges = customizableEdges1;
            btnNext.DisabledState.BorderColor = Color.DarkGray;
            btnNext.DisabledState.CustomBorderColor = Color.DarkGray;
            btnNext.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnNext.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnNext.FillColor = Color.White;
            btnNext.Font = new Font("Poppins", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnNext.ForeColor = Color.Indigo;
            btnNext.HoverState.FillColor = Color.Indigo;
            btnNext.HoverState.ForeColor = Color.White;
            btnNext.Location = new Point(913, 677);
            btnNext.Name = "btnNext";
            btnNext.PressedColor = Color.FromArgb(63, 24, 97);
            btnNext.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnNext.Size = new Size(48, 38);
            btnNext.TabIndex = 10;
            btnNext.Text = ">";
            btnNext.TextOffset = new Point(0, -10);
            btnNext.Click += btnNext_Click;
            // 
            // btnPrevious
            // 
            btnPrevious.BorderColor = Color.Indigo;
            btnPrevious.BorderRadius = 10;
            btnPrevious.BorderThickness = 1;
            btnPrevious.CustomizableEdges = customizableEdges3;
            btnPrevious.DisabledState.BorderColor = Color.DarkGray;
            btnPrevious.DisabledState.CustomBorderColor = Color.DarkGray;
            btnPrevious.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnPrevious.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnPrevious.FillColor = Color.White;
            btnPrevious.Font = new Font("Poppins", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPrevious.ForeColor = Color.Indigo;
            btnPrevious.HoverState.FillColor = Color.Indigo;
            btnPrevious.HoverState.ForeColor = Color.White;
            btnPrevious.Location = new Point(667, 677);
            btnPrevious.Name = "btnPrevious";
            btnPrevious.PressedColor = Color.FromArgb(63, 24, 97);
            btnPrevious.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnPrevious.Size = new Size(48, 38);
            btnPrevious.TabIndex = 9;
            btnPrevious.Text = "<";
            btnPrevious.TextOffset = new Point(0, -10);
            btnPrevious.Click += btnPrevious_Click;
            // 
            // lblPageInfo
            // 
            lblPageInfo.AutoSize = true;
            lblPageInfo.Font = new Font("Poppins", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPageInfo.Location = new Point(750, 677);
            lblPageInfo.Name = "lblPageInfo";
            lblPageInfo.Size = new Size(120, 36);
            lblPageInfo.TabIndex = 7;
            lblPageInfo.Text = "Page 1 of 2";
            // 
            // historyTableuc1
            // 
            historyTableuc1.BackColor = Color.White;
            historyTableuc1.Location = new Point(106, 152);
            historyTableuc1.Name = "historyTableuc1";
            historyTableuc1.Padding = new Padding(25);
            historyTableuc1.Size = new Size(1429, 508);
            historyTableuc1.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Poppins", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ControlDarkDark;
            label2.Location = new Point(106, 75);
            label2.Name = "label2";
            label2.Size = new Size(533, 40);
            label2.TabIndex = 5;
            label2.Text = "View all inventory transactions and changes";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Poppins", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(96, 23);
            label1.Name = "label1";
            label1.Size = new Size(502, 70);
            label1.TabIndex = 4;
            label1.Text = "TRANSACTION HISTORY";
            // 
            // HistoryControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(guna2Panel1);
            Name = "HistoryControl";
            Size = new Size(1600, 900);
            guna2Panel1.ResumeLayout(false);
            guna2Panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Label label2;
        private Label label1;
        private Components.HistoryTableUC historyTableuc1;
        private Label lblPageInfo;
        private Guna.UI2.WinForms.Guna2Button btnPrevious;
        private Guna.UI2.WinForms.Guna2Button btnNext;
    }
}
