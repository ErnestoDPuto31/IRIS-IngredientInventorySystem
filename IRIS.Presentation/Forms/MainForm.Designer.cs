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
            btnExit = new Guna.UI2.WinForms.Guna2ImageButton();
            lblDate = new Label();
            txtRole = new Guna.UI2.WinForms.Guna2TextBox();
            pnlTop.SuspendLayout();
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
            pnlMainContent.Location = new Point(0, 64);
            pnlMainContent.Name = "pnlMainContent";
            pnlMainContent.Size = new Size(1600, 830);
            pnlMainContent.TabIndex = 1;
            // 
            // navigationPanel1
            // 
            navigationPanel1.BackColor = Color.Indigo;
            navigationPanel1.Dock = DockStyle.Left;
            navigationPanel1.Location = new Point(0, 0);
            navigationPanel1.Margin = new Padding(5, 4, 5, 4);
            navigationPanel1.Name = "navigationPanel1";
            navigationPanel1.Size = new Size(59, 900);
            navigationPanel1.TabIndex = 0;
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.White;
            pnlTop.BorderColor = SystemColors.ControlDark;
            pnlTop.BorderThickness = 2;
            pnlTop.Controls.Add(notificationBadge1);
            pnlTop.Controls.Add(btnExit);
            pnlTop.Controls.Add(lblDate);
            pnlTop.Controls.Add(txtRole);
            pnlTop.CustomBorderThickness = new Padding(0, 0, 0, 2);
            pnlTop.CustomizableEdges = customizableEdges4;
            pnlTop.Location = new Point(59, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.ShadowDecoration.CustomizableEdges = customizableEdges5;
            pnlTop.Size = new Size(1540, 70);
            pnlTop.TabIndex = 0;
            // 
            // notificationBadge1
            // 
            notificationBadge1.BackColor = Color.Transparent;
            notificationBadge1.Location = new Point(1388, 3);
            notificationBadge1.Name = "notificationBadge1";
            notificationBadge1.Size = new Size(50, 50);
            notificationBadge1.TabIndex = 3;
            notificationBadge1.Click += notificationBadge1_Click;
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
            btnExit.Location = new Point(1505, 12);
            btnExit.Name = "btnExit";
            btnExit.ShadowDecoration.CustomizableEdges = customizableEdges1;
            btnExit.Size = new Size(24, 24);
            btnExit.TabIndex = 2;
            btnExit.Click += btnExit_Click;
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDate.ForeColor = SystemColors.ControlDarkDark;
            lblDate.Location = new Point(224, 19);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(339, 25);
            lblDate.TabIndex = 1;
            lblDate.Text = "Saturday, March 31, 2006 - 10:00 PM";
            // 
            // txtRole
            // 
            txtRole.BorderColor = Color.Indigo;
            txtRole.BorderRadius = 15;
            txtRole.BorderThickness = 2;
            txtRole.CustomizableEdges = customizableEdges2;
            txtRole.DefaultText = "OFFICE STAFF";
            txtRole.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtRole.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtRole.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtRole.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtRole.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtRole.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtRole.ForeColor = Color.Indigo;
            txtRole.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtRole.Location = new Point(19, 20);
            txtRole.Margin = new Padding(4, 6, 4, 6);
            txtRole.Name = "txtRole";
            txtRole.PlaceholderText = "";
            txtRole.ReadOnly = true;
            txtRole.SelectedText = "";
            txtRole.ShadowDecoration.CustomizableEdges = customizableEdges3;
            txtRole.Size = new Size(184, 35);
            txtRole.TabIndex = 0;
            txtRole.TextAlign = HorizontalAlignment.Center;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1600, 900);
            Controls.Add(pnlTop);
            Controls.Add(navigationPanel1);
            Controls.Add(pnlMainContent);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MainForm";
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
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
    }
}