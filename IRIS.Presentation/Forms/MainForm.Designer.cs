namespace IRIS.Presentation.Forms
{
    partial class MainForm
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            pnlMainContent = new Panel();
            navigationPanel1 = new IRIS.Presentation.UserControls.NavigationPanel();
            pnlTop = new Guna.UI2.WinForms.Guna2Panel();
            notificationBadge1 = new IRIS.Presentation.UserControls.Components.NotificationBadge();
            lblDate = new Label();
            txtRole = new Guna.UI2.WinForms.Guna2TextBox();
            btnExit = new Guna.UI2.WinForms.Guna2ImageButton();
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            pnlTop.SuspendLayout();
            guna2Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // guna2BorderlessForm1
            // 
            guna2BorderlessForm1.BorderRadius = 50;
            guna2BorderlessForm1.ContainerControl = this;
            guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // pnlMainContent
            // 
            pnlMainContent.BackColor = Color.White;
            pnlMainContent.Location = new Point(0, 96);
            pnlMainContent.Name = "pnlMainContent";
            pnlMainContent.Size = new Size(1600, 804);
            pnlMainContent.TabIndex = 1;
            // 
            // navigationPanel1
            // 
            navigationPanel1.BackColor = Color.Indigo;
            navigationPanel1.Location = new Point(0, 30);
            navigationPanel1.Margin = new Padding(5, 4, 5, 4);
            navigationPanel1.Name = "navigationPanel1";
            navigationPanel1.Size = new Size(59, 870);
            navigationPanel1.TabIndex = 0;
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.White;
            pnlTop.BorderColor = SystemColors.ControlDark;
            pnlTop.BorderThickness = 2;
            pnlTop.Controls.Add(notificationBadge1);
            pnlTop.Controls.Add(lblDate);
            pnlTop.Controls.Add(txtRole);
            pnlTop.CustomBorderThickness = new Padding(0, 0, 0, 2);
            pnlTop.CustomizableEdges = customizableEdges6;
            pnlTop.Location = new Point(57, 30);
            pnlTop.Name = "pnlTop";
            pnlTop.ShadowDecoration.CustomizableEdges = customizableEdges7;
            pnlTop.Size = new Size(1543, 72);
            pnlTop.TabIndex = 0;
            // 
            // notificationBadge1
            // 
            notificationBadge1.BackColor = Color.Transparent;
            notificationBadge1.Location = new Point(1485, 17);
            notificationBadge1.Name = "notificationBadge1";
            notificationBadge1.Size = new Size(34, 43);
            notificationBadge1.TabIndex = 3;
            notificationBadge1.Click += notificationBadge1_Click;
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Font = new Font("Poppins", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDate.ForeColor = SystemColors.ControlDarkDark;
            lblDate.Location = new Point(224, 19);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(366, 36);
            lblDate.TabIndex = 1;
            lblDate.Text = "Saturday, March 31, 2006 - 10:00 PM";
            // 
            // txtRole
            // 
            txtRole.BorderColor = Color.Indigo;
            txtRole.BorderRadius = 15;
            txtRole.BorderThickness = 2;
            txtRole.CustomizableEdges = customizableEdges4;
            txtRole.DefaultText = "OFFICE STAFF";
            txtRole.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtRole.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtRole.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtRole.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtRole.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtRole.Font = new Font("Poppins", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtRole.ForeColor = Color.Indigo;
            txtRole.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtRole.Location = new Point(26, 16);
            txtRole.Margin = new Padding(6, 9, 6, 9);
            txtRole.Name = "txtRole";
            txtRole.PlaceholderText = "";
            txtRole.ReadOnly = true;
            txtRole.SelectedText = "";
            txtRole.ShadowDecoration.CustomizableEdges = customizableEdges5;
            txtRole.Size = new Size(189, 39);
            txtRole.TabIndex = 0;
            txtRole.TextAlign = HorizontalAlignment.Center;
            // 
            // btnExit
            // 
            btnExit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExit.BackColor = Color.Transparent;
            btnExit.CheckedState.ImageSize = new Size(24, 24);
            btnExit.HoverState.ImageSize = new Size(30, 30);
            btnExit.Image = Properties.Resources.exitBtn;
            btnExit.ImageOffset = new Point(0, 0);
            btnExit.ImageRotate = 0F;
            btnExit.ImageSize = new Size(24, 24);
            btnExit.Location = new Point(1560, 0);
            btnExit.Name = "btnExit";
            btnExit.ShadowDecoration.CustomizableEdges = customizableEdges1;
            btnExit.Size = new Size(28, 32);
            btnExit.TabIndex = 2;
            btnExit.Click += btnExit_Click;
            // 
            // guna2Panel1
            // 
            guna2Panel1.BackColor = Color.FromArgb(206, 195, 214);
            guna2Panel1.BorderColor = SystemColors.ControlDark;
            guna2Panel1.BorderThickness = 2;
            guna2Panel1.Controls.Add(btnExit);
            guna2Panel1.CustomBorderThickness = new Padding(0, 0, 0, 2);
            guna2Panel1.CustomizableEdges = customizableEdges2;
            guna2Panel1.Location = new Point(0, 0);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges3;
            guna2Panel1.Size = new Size(1600, 32);
            guna2Panel1.TabIndex = 4;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1600, 900);
            Controls.Add(navigationPanel1);
            Controls.Add(guna2Panel1);
            Controls.Add(pnlTop);
            Controls.Add(pnlMainContent);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MainForm";
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            guna2Panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Panel pnlMainContent;
        private UserControls.NavigationPanel navigationPanel1;
        private Guna.UI2.WinForms.Guna2Panel pnlTop;
        private Label lblDate;
        private Guna.UI2.WinForms.Guna2TextBox txtRole;
        private Guna.UI2.WinForms.Guna2ImageButton btnExit;
        private UserControls.Components.NotificationBadge notificationBadge1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
    }
}