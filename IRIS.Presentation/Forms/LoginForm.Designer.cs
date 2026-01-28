using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IRIS.Presentation
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panelCard = new Guna2ShadowPanel();
            guna2CirclePictureBox1 = new Guna2CirclePictureBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            lblTitle = new Label();
            lblSubtitle = new Label();
            txtUsername = new Guna2TextBox();
            txtPassword = new Guna2TextBox();
            btnLogin = new Guna2Button();
            guna2BorderlessForm1 = new Guna2BorderlessForm(components);
            imageList1 = new ImageList(components);
            panel1 = new Panel();
            MinimizeBtn = new Guna2CircleButton();
            ExitBtn = new Guna2CircleButton();
            panelCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)guna2CirclePictureBox1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panelCard
            // 
            panelCard.BackColor = Color.Transparent;
            panelCard.BorderStyle = BorderStyle.Fixed3D;
            panelCard.Controls.Add(guna2CirclePictureBox1);
            panelCard.Controls.Add(label4);
            panelCard.Controls.Add(label3);
            panelCard.Controls.Add(label2);
            panelCard.Controls.Add(label1);
            panelCard.Controls.Add(lblTitle);
            panelCard.Controls.Add(lblSubtitle);
            panelCard.Controls.Add(txtUsername);
            panelCard.Controls.Add(txtPassword);
            panelCard.Controls.Add(btnLogin);
            panelCard.FillColor = Color.White;
            panelCard.Location = new Point(879, 99);
            panelCard.Margin = new Padding(4);
            panelCard.Name = "panelCard";
            panelCard.Radius = 12;
            panelCard.ShadowColor = Color.Black;
            panelCard.ShadowShift = 10;
            panelCard.Size = new Size(648, 761);
            panelCard.TabIndex = 0;
            panelCard.Paint += panelCard_Paint;
            // 
            // guna2CirclePictureBox1
            // 
            guna2CirclePictureBox1.BackColor = Color.Transparent;
            guna2CirclePictureBox1.FillColor = Color.Transparent;
            guna2CirclePictureBox1.Image = Properties.Resources.logoIRIS;
            guna2CirclePictureBox1.ImageRotate = 0F;
            guna2CirclePictureBox1.Location = new Point(196, -9);
            guna2CirclePictureBox1.Margin = new Padding(4);
            guna2CirclePictureBox1.Name = "guna2CirclePictureBox1";
            guna2CirclePictureBox1.ShadowDecoration.CustomizableEdges = customizableEdges1;
            guna2CirclePictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            guna2CirclePictureBox1.Size = new Size(271, 226);
            guna2CirclePictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            guna2CirclePictureBox1.TabIndex = 9;
            guna2CirclePictureBox1.TabStop = false;
            guna2CirclePictureBox1.UseTransparentBackground = true;
            // 
            // label4
            // 
            label4.FlatStyle = FlatStyle.Flat;
            label4.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(40, 497);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(124, 36);
            label4.TabIndex = 8;
            label4.Text = "Password";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(40, 389);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(124, 36);
            label3.TabIndex = 7;
            label3.Text = "Username";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 10F);
            label2.ForeColor = Color.FromArgb(107, 114, 128);
            label2.Location = new Point(149, 185);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(375, 36);
            label2.TabIndex = 6;
            label2.Text = "Welcome To";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.Font = new Font("Arial Unicode MS", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(17, 24, 39);
            label1.Location = new Point(188, 201);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(282, 82);
            label1.TabIndex = 5;
            label1.Text = "I R I S";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            lblTitle.FlatStyle = FlatStyle.Popup;
            lblTitle.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.FromArgb(17, 24, 39);
            lblTitle.Location = new Point(50, 284);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(546, 50);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Ingredient Request Inventory System";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSubtitle
            // 
            lblSubtitle.Font = new Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = Color.FromArgb(107, 114, 128);
            lblSubtitle.Location = new Point(149, 334);
            lblSubtitle.Margin = new Padding(4, 0, 4, 0);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(375, 36);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Sign in to access the system";
            lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtUsername
            // 
            txtUsername.BorderColor = Color.MediumPurple;
            txtUsername.BorderRadius = 8;
            txtUsername.CustomizableEdges = customizableEdges2;
            txtUsername.DefaultText = "";
            txtUsername.Font = new Font("Segoe UI", 10F);
            txtUsername.Location = new Point(40, 430);
            txtUsername.Margin = new Padding(4, 5, 4, 5);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "Username";
            txtUsername.SelectedText = "";
            txtUsername.ShadowDecoration.CustomizableEdges = customizableEdges3;
            txtUsername.Size = new Size(556, 50);
            txtUsername.TabIndex = 2;
            // 
            // txtPassword
            // 
            txtPassword.BorderColor = Color.MediumPurple;
            txtPassword.BorderRadius = 8;
            txtPassword.CustomizableEdges = customizableEdges4;
            txtPassword.DefaultText = "";
            txtPassword.Font = new Font("Segoe UI", 10F);
            txtPassword.Location = new Point(40, 538);
            txtPassword.Margin = new Padding(4, 5, 4, 5);
            txtPassword.Name = "txtPassword";
            txtPassword.PlaceholderText = "Password";
            txtPassword.SelectedText = "";
            txtPassword.ShadowDecoration.CustomizableEdges = customizableEdges5;
            txtPassword.Size = new Size(556, 50);
            txtPassword.TabIndex = 3;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            btnLogin.BorderRadius = 10;
            btnLogin.CustomizableEdges = customizableEdges6;
            btnLogin.FillColor = Color.MediumPurple;
            btnLogin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLogin.ForeColor = SystemColors.Window;
            btnLogin.Location = new Point(40, 656);
            btnLogin.Margin = new Padding(4);
            btnLogin.Name = "btnLogin";
            btnLogin.ShadowDecoration.CustomizableEdges = customizableEdges7;
            btnLogin.Size = new Size(556, 56);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "SIGN IN";
            btnLogin.Click += btnLogin_Click;
            // 
            // guna2BorderlessForm1
            // 
            guna2BorderlessForm1.BorderRadius = 30;
            guna2BorderlessForm1.ContainerControl = this;
            guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "icons8-lock-50.png");
            // 
            // panel1
            // 
            panel1.BackColor = Color.Gainsboro;
            panel1.Controls.Add(MinimizeBtn);
            panel1.Controls.Add(ExitBtn);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1600, 34);
            panel1.TabIndex = 1;
            // 
            // MinimizeBtn
            // 
            MinimizeBtn.BackColor = Color.Transparent;
            MinimizeBtn.DisabledState.BorderColor = Color.DarkGray;
            MinimizeBtn.DisabledState.CustomBorderColor = Color.DarkGray;
            MinimizeBtn.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            MinimizeBtn.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            MinimizeBtn.FillColor = Color.Transparent;
            MinimizeBtn.Font = new Font("Segoe UI", 9F);
            MinimizeBtn.ForeColor = Color.White;
            MinimizeBtn.Image = Properties.Resources.minimizeBtn;
            MinimizeBtn.Location = new Point(1526, 0);
            MinimizeBtn.Name = "MinimizeBtn";
            MinimizeBtn.PressedColor = Color.DimGray;
            MinimizeBtn.ShadowDecoration.CustomizableEdges = customizableEdges8;
            MinimizeBtn.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            MinimizeBtn.Size = new Size(25, 31);
            MinimizeBtn.TabIndex = 3;
            MinimizeBtn.Click += MinimizeBtn_Click_1;
            // 
            // ExitBtn
            // 
            ExitBtn.BackColor = Color.Transparent;
            ExitBtn.DisabledState.BorderColor = Color.DarkGray;
            ExitBtn.DisabledState.CustomBorderColor = Color.DarkGray;
            ExitBtn.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            ExitBtn.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            ExitBtn.FillColor = Color.Transparent;
            ExitBtn.Font = new Font("Segoe UI", 9F);
            ExitBtn.ForeColor = Color.White;
            ExitBtn.Image = Properties.Resources.exitBtn;
            ExitBtn.Location = new Point(1557, 0);
            ExitBtn.Name = "ExitBtn";
            ExitBtn.PressedColor = Color.DimGray;
            ExitBtn.ShadowDecoration.CustomizableEdges = customizableEdges9;
            ExitBtn.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            ExitBtn.Size = new Size(25, 31);
            ExitBtn.TabIndex = 2;
            ExitBtn.Click += ExitBtn_Click;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            BackgroundImage = Properties.Resources.background;
            ClientSize = new Size(1600, 900);
            Controls.Add(panel1);
            Controls.Add(panelCard);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            panelCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)guna2CirclePictureBox1).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna2ShadowPanel panelCard;
        private Label lblTitle;
        private Label lblSubtitle;
        private Guna2TextBox txtUsername;
        private Guna2TextBox txtPassword;
        private Guna2Button btnLogin;
        private Guna2BorderlessForm guna2BorderlessForm1;
        private Label label4;
        private Label label3;
        private Label label1;
        private ImageList imageList1;
        private Panel panel1;
        private Guna2CircleButton MinimizeBtn;
        private Guna2CircleButton ExitBtn;
        private Guna2CirclePictureBox guna2CirclePictureBox1;
        private Label label2;
    }
}
