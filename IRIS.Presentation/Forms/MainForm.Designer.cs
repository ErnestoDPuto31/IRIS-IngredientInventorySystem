using System.Drawing;
using System.Windows.Forms;

namespace IRIS.Presentation.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();

                _clockTimer?.Dispose();
                _notificationTimer?.Dispose();
                _badgeTimer?.Dispose();
                _pageIntroTimer?.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdgesTop = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdgesTopShadow = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdgesRole = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdgesRoleShadow = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdgesExit = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdgesBar = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdgesBarShadow = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));

            guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            pnlMainContent = new Panel();
            pnlTop = new Guna.UI2.WinForms.Guna2Panel();
            notificationBadge = new IRIS.Presentation.UserControls.Components.NotificationBadge();
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
            guna2BorderlessForm1.BorderRadius = 24;
            guna2BorderlessForm1.ContainerControl = this;
            guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            guna2BorderlessForm1.TransparentWhileDrag = true;
            guna2BorderlessForm1.ResizeForm = false;

            // 
            // guna2Panel1
            // 
            guna2Panel1.BackColor = Color.LightGray;
            guna2Panel1.BorderColor = SystemColors.ControlDark;
            guna2Panel1.BorderThickness = 1;
            guna2Panel1.Controls.Add(btnExit);
            guna2Panel1.CustomBorderThickness = new Padding(0, 0, 0, 1);
            guna2Panel1.CustomizableEdges = customizableEdgesBar;
            guna2Panel1.Dock = DockStyle.Top;
            guna2Panel1.FillColor = Color.LightGray;
            guna2Panel1.Location = new Point(0, 0);
            guna2Panel1.Margin = new Padding(0);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdgesBarShadow;
            guna2Panel1.Size = new Size(1600, 42);
            guna2Panel1.TabIndex = 0;

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
            btnExit.Location = new Point(1566, 7);
            btnExit.Margin = new Padding(2);
            btnExit.Name = "btnExit";
            btnExit.PressedState.ImageSize = new Size(24, 24);
            btnExit.ShadowDecoration.CustomizableEdges = customizableEdgesExit;
            btnExit.Size = new Size(24, 24);
            btnExit.TabIndex = 1;
            btnExit.Visible = false;
            btnExit.Click += btnExit_Click;

            // 
            // pnlTop
            // 
            pnlTop.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlTop.BackColor = Color.White;
            pnlTop.BorderColor = SystemColors.ControlDark;
            pnlTop.BorderThickness = 1;
            pnlTop.Controls.Add(notificationBadge);
            pnlTop.Controls.Add(lblDate);
            pnlTop.Controls.Add(txtRole);
            pnlTop.CustomBorderThickness = new Padding(0, 0, 0, 1);
            pnlTop.CustomizableEdges = customizableEdgesTop;
            pnlTop.FillColor = Color.White;
            pnlTop.Location = new Point(240, 58);
            pnlTop.Margin = new Padding(2);
            pnlTop.Name = "pnlTop";
            pnlTop.ShadowDecoration.CustomizableEdges = customizableEdgesTopShadow;
            pnlTop.Size = new Size(1344, 80);
            pnlTop.TabIndex = 1;

            // 
            // notificationBadge
            // 
            notificationBadge.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            notificationBadge.BackColor = Color.Transparent;
            notificationBadge.Location = new Point(1268, 10);
            notificationBadge.Margin = new Padding(2);
            notificationBadge.Name = "notificationBadge";
            notificationBadge.Size = new Size(56, 56);
            notificationBadge.TabIndex = 2;
            notificationBadge.Click += notificationBadge_Click;

            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Font = new Font("Poppins", 11F, FontStyle.Regular, GraphicsUnit.Point);
            lblDate.ForeColor = SystemColors.ControlDarkDark;
            lblDate.Location = new Point(240, 21);
            lblDate.Margin = new Padding(4, 0, 4, 0);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(397, 39);
            lblDate.TabIndex = 1;
            lblDate.Text = "Saturday, March 31, 2006 - 10:00 PM";

            // 
            // txtRole
            // 
            txtRole.BorderColor = Color.Indigo;
            txtRole.BorderRadius = 14;
            txtRole.BorderThickness = 2;
            txtRole.CustomizableEdges = customizableEdgesRole;
            txtRole.DefaultText = "OFFICE STAFF";
            txtRole.DisabledState.BorderColor = Color.Indigo;
            txtRole.DisabledState.FillColor = Color.White;
            txtRole.DisabledState.ForeColor = Color.Indigo;
            txtRole.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtRole.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtRole.Font = new Font("Poppins", 10F, FontStyle.Bold, GraphicsUnit.Point);
            txtRole.ForeColor = Color.Indigo;
            txtRole.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtRole.Location = new Point(18, 16);
            txtRole.Margin = new Padding(8);
            txtRole.Name = "txtRole";
            txtRole.PlaceholderText = "";
            txtRole.ReadOnly = true;
            txtRole.SelectedText = "";
            txtRole.ShadowDecoration.CustomizableEdges = customizableEdgesRoleShadow;
            txtRole.Size = new Size(206, 46);
            txtRole.TabIndex = 0;
            txtRole.TextAlign = HorizontalAlignment.Center;

            // 
            // pnlMainContent
            // 
            pnlMainContent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlMainContent.BackColor = Color.White;
            pnlMainContent.Location = new Point(240, 150);
            pnlMainContent.Margin = new Padding(4);
            pnlMainContent.Name = "pnlMainContent";
            pnlMainContent.Size = new Size(1344, 734);
            pnlMainContent.TabIndex = 2;

            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1600, 900);
            Controls.Add(pnlMainContent);
            Controls.Add(pnlTop);
            Controls.Add(guna2Panel1);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MaximumSize = new Size(1600, 900);
            MinimumSize = new Size(1600, 900);
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
        private Guna.UI2.WinForms.Guna2Panel pnlTop;
        private Label lblDate;
        private Guna.UI2.WinForms.Guna2TextBox txtRole;
        private Guna.UI2.WinForms.Guna2ImageButton btnExit;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private UserControls.Components.NotificationBadge notificationBadge;
    }
}