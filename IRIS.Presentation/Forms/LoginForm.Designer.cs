using System;
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            slidePictureBox = new Guna2PictureBox();
            slideshowTimer = new System.Windows.Forms.Timer(components);
            btnHoverTimer = new System.Windows.Forms.Timer(components);
            guna2BorderlessForm1 = new Guna2BorderlessForm(components);
            guna2Panel2 = new Guna2Panel();
            chkShowPassword = new Guna2CheckBox();
            lblError = new Label();
            btnLogin = new Guna2GradientButton();
            label1 = new Label();
            label6 = new Label();
            txtPassword = new Guna2TextBox();
            label5 = new Label();
            txtUsername = new Guna2TextBox();
            label4 = new Label();
            label2 = new Label();
            guna2ImageButton1 = new Guna2ImageButton();
            guna2Panel1 = new Guna2Panel();
            ((System.ComponentModel.ISupportInitialize)slidePictureBox).BeginInit();
            guna2Panel2.SuspendLayout();
            guna2Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // slidePictureBox
            // 
            slidePictureBox.BackColor = Color.Transparent;
            slidePictureBox.BorderRadius = 40;
            slidePictureBox.CustomizableEdges = customizableEdges1;
            slidePictureBox.Dock = DockStyle.Fill;
            slidePictureBox.FillColor = Color.Transparent;
            slidePictureBox.ImageRotate = 0F;
            slidePictureBox.Location = new Point(0, 0);
            slidePictureBox.Margin = new Padding(2);
            slidePictureBox.Name = "slidePictureBox";
            slidePictureBox.ShadowDecoration.CustomizableEdges = customizableEdges2;
            slidePictureBox.Size = new Size(616, 624);
            slidePictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            slidePictureBox.TabIndex = 0;
            slidePictureBox.TabStop = false;
            slidePictureBox.UseTransparentBackground = true;
            // 
            // slideshowTimer
            // 
            slideshowTimer.Interval = 2400;
            // 
            // btnHoverTimer
            // 
            btnHoverTimer.Interval = 10;
            // 
            // guna2BorderlessForm1
            // 
            guna2BorderlessForm1.BorderRadius = 30;
            guna2BorderlessForm1.ContainerControl = this;
            guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // guna2Panel2
            // 
            guna2Panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            guna2Panel2.BackColor = Color.Transparent;
            guna2Panel2.BorderColor = Color.Transparent;
            guna2Panel2.BorderRadius = 36;
            guna2Panel2.Controls.Add(chkShowPassword);
            guna2Panel2.Controls.Add(lblError);
            guna2Panel2.Controls.Add(btnLogin);
            guna2Panel2.Controls.Add(label1);
            guna2Panel2.Controls.Add(label6);
            guna2Panel2.Controls.Add(txtPassword);
            guna2Panel2.Controls.Add(label5);
            guna2Panel2.Controls.Add(txtUsername);
            guna2Panel2.Controls.Add(label4);
            guna2Panel2.Controls.Add(label2);
            guna2Panel2.Controls.Add(guna2ImageButton1);
            guna2Panel2.CustomBorderColor = Color.Transparent;
            guna2Panel2.CustomizableEdges = customizableEdges12;
            guna2Panel2.FillColor = Color.White;
            guna2Panel2.Location = new Point(56, 48);
            guna2Panel2.Name = "guna2Panel2";
            guna2Panel2.ShadowDecoration.BorderRadius = 36;
            guna2Panel2.ShadowDecoration.CustomizableEdges = customizableEdges13;
            guna2Panel2.ShadowDecoration.Depth = 15;
            guna2Panel2.ShadowDecoration.Enabled = true;
            guna2Panel2.ShadowDecoration.Shadow = new Padding(0, 0, 0, 10);
            guna2Panel2.Size = new Size(512, 624);
            guna2Panel2.TabIndex = 1;
            // 
            // chkShowPassword
            // 
            chkShowPassword.Anchor = AnchorStyles.None;
            chkShowPassword.AutoSize = true;
            chkShowPassword.BackColor = Color.Transparent;
            chkShowPassword.CheckedState.BorderColor = Color.FromArgb(137, 65, 208);
            chkShowPassword.CheckedState.BorderRadius = 6;
            chkShowPassword.CheckedState.BorderThickness = 1;
            chkShowPassword.CheckedState.FillColor = Color.White;
            chkShowPassword.CheckMarkColor = Color.FromArgb(137, 65, 208);
            chkShowPassword.Cursor = Cursors.Hand;
            chkShowPassword.Font = new Font("Poppins", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkShowPassword.ForeColor = Color.FromArgb(110, 110, 110);
            chkShowPassword.Location = new Point(96, 468);
            chkShowPassword.Margin = new Padding(0);
            chkShowPassword.Name = "chkShowPassword";
            chkShowPassword.Size = new Size(152, 30);
            chkShowPassword.TabIndex = 10;
            chkShowPassword.Text = "Show Password";
            chkShowPassword.UncheckedState.BorderColor = Color.FromArgb(210, 210, 220);
            chkShowPassword.UncheckedState.BorderRadius = 6;
            chkShowPassword.UncheckedState.BorderThickness = 1;
            chkShowPassword.UncheckedState.FillColor = Color.White;
            chkShowPassword.UseVisualStyleBackColor = false;
            chkShowPassword.CheckedChanged += chkShowPassword_CheckedChanged;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.Font = new Font("Poppins", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblError.ForeColor = Color.FromArgb(220, 53, 69);
            lblError.Location = new Point(96, 500);
            lblError.Name = "lblError";
            lblError.Size = new Size(32, 26);
            lblError.TabIndex = 9;
            lblError.Text = "aa";
            lblError.Visible = false;
            // 
            // btnLogin
            // 
            btnLogin.Anchor = AnchorStyles.None;
            btnLogin.Animated = true;
            btnLogin.BorderRadius = 20;
            btnLogin.CustomizableEdges = customizableEdges5;
            btnLogin.DisabledState.BorderColor = Color.DarkGray;
            btnLogin.DisabledState.CustomBorderColor = Color.DarkGray;
            btnLogin.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnLogin.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            btnLogin.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnLogin.FillColor = Color.FromArgb(137, 65, 208);
            btnLogin.FillColor2 = Color.FromArgb(77, 10, 133);
            btnLogin.Font = new Font("Poppins", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLogin.ForeColor = Color.White;
            btnLogin.HoverState.FillColor = Color.FromArgb(155, 85, 225);
            btnLogin.HoverState.FillColor2 = Color.FromArgb(95, 25, 155);
            btnLogin.ImageAlign = HorizontalAlignment.Right;
            btnLogin.ImageOffset = new Point(-18, 0);
            btnLogin.ImageSize = new Size(22, 22);
            btnLogin.Location = new Point(96, 532);
            btnLogin.Name = "btnLogin";
            btnLogin.PressedColor = Color.FromArgb(60, 5, 110);
            btnLogin.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnLogin.Size = new Size(320, 48);
            btnLogin.TabIndex = 3;
            btnLogin.Text = "Sign In";
            btnLogin.TextOffset = new Point(-6, 0);
            btnLogin.Click += btnLogin_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Font = new Font("Poppins", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(25, 25, 35);
            label1.Location = new Point(82, 191);
            label1.Name = "label1";
            label1.Size = new Size(251, 70);
            label1.TabIndex = 0;
            label1.Text = "Hey There!";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.None;
            label6.AutoSize = true;
            label6.Font = new Font("Poppins", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.FromArgb(70, 70, 70);
            label6.Location = new Point(96, 392);
            label6.Name = "label6";
            label6.Size = new Size(88, 28);
            label6.TabIndex = 8;
            label6.Text = "Password";
            // 
            // txtPassword
            // 
            txtPassword.Anchor = AnchorStyles.None;
            txtPassword.Animated = true;
            txtPassword.BorderColor = Color.FromArgb(225, 225, 235);
            txtPassword.BorderRadius = 20;
            txtPassword.BorderThickness = 2;
            txtPassword.CustomizableEdges = customizableEdges7;
            txtPassword.DefaultText = "";
            txtPassword.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtPassword.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtPassword.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtPassword.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtPassword.FillColor = Color.FromArgb(252, 252, 255);
            txtPassword.FocusedState.BorderColor = Color.FromArgb(137, 65, 208);
            txtPassword.Font = new Font("Poppins", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPassword.ForeColor = Color.Black;
            txtPassword.HoverState.BorderColor = Color.FromArgb(160, 95, 225);
            txtPassword.IconLeft = Properties.Resources.icons8_password_24;
            txtPassword.IconLeftOffset = new Point(10, 0);
            txtPassword.Location = new Point(96, 420);
            txtPassword.Margin = new Padding(4, 9, 4, 9);
            txtPassword.Name = "txtPassword";
            txtPassword.PlaceholderForeColor = Color.FromArgb(170, 170, 170);
            txtPassword.PlaceholderText = "Enter Password";
            txtPassword.SelectedText = "";
            txtPassword.ShadowDecoration.CustomizableEdges = customizableEdges8;
            txtPassword.Size = new Size(320, 45);
            txtPassword.TabIndex = 1;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.None;
            label5.AutoSize = true;
            label5.Font = new Font("Poppins", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.FromArgb(70, 70, 70);
            label5.Location = new Point(96, 304);
            label5.Name = "label5";
            label5.Size = new Size(94, 28);
            label5.TabIndex = 6;
            label5.Text = "Username";
            // 
            // txtUsername
            // 
            txtUsername.Anchor = AnchorStyles.None;
            txtUsername.Animated = true;
            txtUsername.BorderColor = Color.FromArgb(225, 225, 235);
            txtUsername.BorderRadius = 20;
            txtUsername.BorderThickness = 2;
            txtUsername.CustomizableEdges = customizableEdges9;
            txtUsername.DefaultText = "";
            txtUsername.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtUsername.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtUsername.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtUsername.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtUsername.FillColor = Color.FromArgb(252, 252, 255);
            txtUsername.FocusedState.BorderColor = Color.FromArgb(137, 65, 208);
            txtUsername.Font = new Font("Poppins", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtUsername.ForeColor = Color.Black;
            txtUsername.HoverState.BorderColor = Color.FromArgb(160, 95, 225);
            txtUsername.IconLeft = Properties.Resources.icons8_user_24;
            txtUsername.IconLeftOffset = new Point(10, 0);
            txtUsername.Location = new Point(96, 332);
            txtUsername.Margin = new Padding(4, 8, 4, 8);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderForeColor = Color.FromArgb(170, 170, 170);
            txtUsername.PlaceholderText = "Enter Username";
            txtUsername.SelectedText = "";
            txtUsername.ShadowDecoration.CustomizableEdges = customizableEdges10;
            txtUsername.Size = new Size(320, 45);
            txtUsername.TabIndex = 0;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.Font = new Font("Poppins", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.FromArgb(125, 125, 125);
            label4.Location = new Point(96, 253);
            label4.Name = "label4";
            label4.Size = new Size(241, 30);
            label4.TabIndex = 4;
            label4.Text = "Please enter your account.";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Font = new Font("Poppins", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(137, 65, 208);
            label2.Location = new Point(96, 168);
            label2.Name = "label2";
            label2.Size = new Size(154, 30);
            label2.TabIndex = 2;
            label2.Text = "IRIS Secure Login";
            // 
            // guna2ImageButton1
            // 
            guna2ImageButton1.Anchor = AnchorStyles.None;
            guna2ImageButton1.BackColor = Color.Transparent;
            guna2ImageButton1.BackgroundImageLayout = ImageLayout.Stretch;
            guna2ImageButton1.CheckedState.ImageSize = new Size(64, 64);
            guna2ImageButton1.HoverState.ImageSize = new Size(140, 140);
            guna2ImageButton1.Image = Properties.Resources.IRIS_Logo;
            guna2ImageButton1.ImageOffset = new Point(0, 0);
            guna2ImageButton1.ImageRotate = 0F;
            guna2ImageButton1.ImageSize = new Size(200, 200);
            guna2ImageButton1.ImeMode = ImeMode.Off;
            guna2ImageButton1.Location = new Point(175, 19);
            guna2ImageButton1.Name = "guna2ImageButton1";
            guna2ImageButton1.PressedState.ImageSize = new Size(120, 120);
            guna2ImageButton1.ShadowDecoration.CustomizableEdges = customizableEdges11;
            guna2ImageButton1.Size = new Size(133, 126);
            guna2ImageButton1.TabIndex = 1;
            guna2ImageButton1.UseTransparentBackground = true;
            // 
            // guna2Panel1
            // 
            guna2Panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            guna2Panel1.BackColor = Color.Transparent;
            guna2Panel1.BackgroundImageLayout = ImageLayout.Zoom;
            guna2Panel1.BorderRadius = 40;
            guna2Panel1.Controls.Add(slidePictureBox);
            guna2Panel1.CustomizableEdges = customizableEdges3;
            guna2Panel1.FillColor = Color.FromArgb(240, 235, 245);
            guna2Panel1.Location = new Point(608, 48);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.BorderRadius = 40;
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges4;
            guna2Panel1.ShadowDecoration.Depth = 15;
            guna2Panel1.ShadowDecoration.Enabled = true;
            guna2Panel1.ShadowDecoration.Shadow = new Padding(0, 0, 0, 10);
            guna2Panel1.Size = new Size(616, 624);
            guna2Panel1.TabIndex = 2;
            guna2Panel1.UseTransparentBackground = true;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(1280, 720);
            Controls.Add(guna2Panel1);
            Controls.Add(guna2Panel2);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(1278, 688);
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "iRIS";
            ((System.ComponentModel.ISupportInitialize)slidePictureBox).EndInit();
            guna2Panel2.ResumeLayout(false);
            guna2Panel2.PerformLayout();
            guna2Panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Guna2PictureBox slidePictureBox;
        private System.Windows.Forms.Timer slideshowTimer;
        private System.Windows.Forms.Timer btnHoverTimer;
        private Guna2BorderlessForm guna2BorderlessForm1;
        private Guna2Panel guna2Panel2;
        private Label label1;
        private Label label2;
        private Guna2ImageButton guna2ImageButton1;
        private Guna2TextBox txtUsername;
        private Label label6;
        private Guna2TextBox txtPassword;
        private Label label5;
        private Guna2GradientButton btnLogin;
        private Label lblError;
        private Guna2Panel guna2Panel1;
        private Guna2CheckBox chkShowPassword;
        private Label label4;
    }
}