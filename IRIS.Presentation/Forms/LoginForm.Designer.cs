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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            guna2BorderlessForm1 = new Guna2BorderlessForm(components);
            guna2ImageButton1 = new Guna2ImageButton();
            label4 = new Label();
            txtUsername = new Guna2TextBox();
            label5 = new Label();
            txtPassword = new Guna2TextBox();
            label6 = new Label();
            label1 = new Label();
            btnLogin = new Guna2GradientButton();
            lblError = new Label();
            label2 = new Label();
            btnForgotPassword = new Guna2Button();
            chkShowPassword = new Guna2ImageCheckBox();
            guna2Panel2 = new Guna2Panel();
            guna2Panel2.SuspendLayout();
            SuspendLayout();
            // 
            // guna2BorderlessForm1
            // 
            guna2BorderlessForm1.BorderRadius = 30;
            guna2BorderlessForm1.ContainerControl = this;
            guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // guna2ImageButton1
            // 
            guna2ImageButton1.Anchor = AnchorStyles.None;
            guna2ImageButton1.BackColor = Color.Transparent;
            guna2ImageButton1.BackgroundImageLayout = ImageLayout.Stretch;
            guna2ImageButton1.CheckedState.ImageSize = new Size(64, 64);
            guna2ImageButton1.HoverState.ImageSize = new Size(300, 300);
            guna2ImageButton1.Image = Properties.Resources.IRIS_Logo;
            guna2ImageButton1.ImageOffset = new Point(0, 0);
            guna2ImageButton1.ImageRotate = 0F;
            guna2ImageButton1.ImageSize = new Size(300, 300);
            guna2ImageButton1.ImeMode = ImeMode.Off;
            guna2ImageButton1.Location = new Point(137, 11);
            guna2ImageButton1.Name = "guna2ImageButton1";
            guna2ImageButton1.PressedState.ImageSize = new Size(300, 300);
            guna2ImageButton1.ShadowDecoration.CustomizableEdges = customizableEdges10;
            guna2ImageButton1.Size = new Size(230, 191);
            guna2ImageButton1.TabIndex = 1;
            guna2ImageButton1.UseTransparentBackground = true;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.Font = new Font("Poppins", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.FromArgb(125, 125, 125);
            label4.Location = new Point(137, 288);
            label4.Name = "label4";
            label4.Size = new Size(241, 30);
            label4.TabIndex = 4;
            label4.Text = "Please enter your account.";
            // 
            // txtUsername
            // 
            txtUsername.Anchor = AnchorStyles.None;
            txtUsername.Animated = true;
            txtUsername.BorderColor = Color.FromArgb(225, 225, 235);
            txtUsername.BorderRadius = 20;
            txtUsername.BorderThickness = 2;
            txtUsername.CustomizableEdges = customizableEdges8;
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
            txtUsername.Location = new Point(82, 370);
            txtUsername.Margin = new Padding(4, 8, 4, 8);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderForeColor = Color.FromArgb(170, 170, 170);
            txtUsername.PlaceholderText = "Enter Username";
            txtUsername.SelectedText = "";
            txtUsername.ShadowDecoration.CustomizableEdges = customizableEdges9;
            txtUsername.Size = new Size(350, 45);
            txtUsername.TabIndex = 0;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.None;
            label5.AutoSize = true;
            label5.Font = new Font("Poppins", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.FromArgb(70, 70, 70);
            label5.Location = new Point(82, 334);
            label5.Name = "label5";
            label5.Size = new Size(102, 30);
            label5.TabIndex = 6;
            label5.Text = "Username";
            // 
            // txtPassword
            // 
            txtPassword.Anchor = AnchorStyles.None;
            txtPassword.Animated = true;
            txtPassword.BorderColor = Color.FromArgb(225, 225, 235);
            txtPassword.BorderRadius = 20;
            txtPassword.BorderThickness = 2;
            txtPassword.CustomizableEdges = customizableEdges6;
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
            txtPassword.Location = new Point(82, 458);
            txtPassword.Margin = new Padding(4, 9, 4, 9);
            txtPassword.Name = "txtPassword";
            txtPassword.PlaceholderForeColor = Color.FromArgb(170, 170, 170);
            txtPassword.PlaceholderText = "Enter Password";
            txtPassword.SelectedText = "";
            txtPassword.ShadowDecoration.CustomizableEdges = customizableEdges7;
            txtPassword.Size = new Size(350, 45);
            txtPassword.TabIndex = 1;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.None;
            label6.AutoSize = true;
            label6.Font = new Font("Poppins", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.FromArgb(70, 70, 70);
            label6.Location = new Point(82, 423);
            label6.Name = "label6";
            label6.Size = new Size(99, 30);
            label6.TabIndex = 8;
            label6.Text = "Password";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Font = new Font("Poppins", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(25, 25, 35);
            label1.Location = new Point(127, 218);
            label1.Name = "label1";
            label1.Size = new Size(251, 70);
            label1.TabIndex = 0;
            label1.Text = "Hey There!";
            // 
            // btnLogin
            // 
            btnLogin.Anchor = AnchorStyles.None;
            btnLogin.Animated = true;
            btnLogin.BorderRadius = 20;
            btnLogin.CustomizableEdges = customizableEdges4;
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
            btnLogin.Location = new Point(96, 568);
            btnLogin.Name = "btnLogin";
            btnLogin.PressedColor = Color.FromArgb(60, 5, 110);
            btnLogin.ShadowDecoration.CustomizableEdges = customizableEdges5;
            btnLogin.Size = new Size(320, 48);
            btnLogin.TabIndex = 3;
            btnLogin.Text = "Sign In";
            btnLogin.TextOffset = new Point(-6, 0);
            btnLogin.Click += btnLogin_Click;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.Font = new Font("Poppins", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblError.ForeColor = Color.FromArgb(220, 53, 69);
            lblError.Location = new Point(82, 512);
            lblError.Name = "lblError";
            lblError.Size = new Size(32, 26);
            lblError.TabIndex = 9;
            lblError.Text = "aa";
            lblError.Visible = false;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Font = new Font("Poppins", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(137, 65, 208);
            label2.Location = new Point(165, 205);
            label2.Name = "label2";
            label2.Size = new Size(154, 30);
            label2.TabIndex = 2;
            label2.Text = "IRIS Secure Login";
            // 
            // btnForgotPassword
            // 
            btnForgotPassword.CustomizableEdges = customizableEdges2;
            btnForgotPassword.DisabledState.BorderColor = Color.DarkGray;
            btnForgotPassword.DisabledState.CustomBorderColor = Color.DarkGray;
            btnForgotPassword.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnForgotPassword.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnForgotPassword.FillColor = Color.White;
            btnForgotPassword.Font = new Font("Poppins", 9F, FontStyle.Underline, GraphicsUnit.Point, 0);
            btnForgotPassword.ForeColor = Color.Indigo;
            btnForgotPassword.HoverState.FillColor = Color.White;
            btnForgotPassword.HoverState.ForeColor = Color.FromArgb(104, 4, 179);
            btnForgotPassword.Location = new Point(175, 622);
            btnForgotPassword.Name = "btnForgotPassword";
            btnForgotPassword.PressedColor = Color.FromArgb(66, 3, 112);
            btnForgotPassword.PressedDepth = 0;
            btnForgotPassword.ShadowDecoration.CustomizableEdges = customizableEdges3;
            btnForgotPassword.Size = new Size(171, 30);
            btnForgotPassword.TabIndex = 11;
            btnForgotPassword.Text = "Forgot Password?";
            btnForgotPassword.Click += btnForgotPassword_Click;
            // 
            // chkShowPassword
            // 
            chkShowPassword.CheckedState.Image = Properties.Resources.eye_icon;
            chkShowPassword.Image = Properties.Resources.eye_icon;
            chkShowPassword.ImageOffset = new Point(0, 0);
            chkShowPassword.ImageRotate = 0F;
            chkShowPassword.Location = new Point(390, 465);
            chkShowPassword.Name = "chkShowPassword";
            chkShowPassword.ShadowDecoration.CustomizableEdges = customizableEdges1;
            chkShowPassword.Size = new Size(30, 30);
            chkShowPassword.TabIndex = 0;
            chkShowPassword.CheckedChanged += chkShowPassword_CheckedChanged;
            // 
            // guna2Panel2
            // 
            guna2Panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            guna2Panel2.BackColor = Color.Transparent;
            guna2Panel2.BorderColor = Color.Indigo;
            guna2Panel2.BorderRadius = 36;
            guna2Panel2.BorderThickness = 2;
            guna2Panel2.Controls.Add(chkShowPassword);
            guna2Panel2.Controls.Add(btnForgotPassword);
            guna2Panel2.Controls.Add(label2);
            guna2Panel2.Controls.Add(lblError);
            guna2Panel2.Controls.Add(btnLogin);
            guna2Panel2.Controls.Add(label1);
            guna2Panel2.Controls.Add(label6);
            guna2Panel2.Controls.Add(txtPassword);
            guna2Panel2.Controls.Add(label5);
            guna2Panel2.Controls.Add(txtUsername);
            guna2Panel2.Controls.Add(label4);
            guna2Panel2.Controls.Add(guna2ImageButton1);
            guna2Panel2.CustomBorderColor = Color.Transparent;
            guna2Panel2.CustomizableEdges = customizableEdges11;
            guna2Panel2.FillColor = Color.White;
            guna2Panel2.Location = new Point(998, 84);
            guna2Panel2.Name = "guna2Panel2";
            guna2Panel2.ShadowDecoration.BorderRadius = 36;
            guna2Panel2.ShadowDecoration.CustomizableEdges = customizableEdges12;
            guna2Panel2.ShadowDecoration.Depth = 15;
            guna2Panel2.ShadowDecoration.Enabled = true;
            guna2Panel2.ShadowDecoration.Shadow = new Padding(0, 0, 0, 10);
            guna2Panel2.Size = new Size(512, 739);
            guna2Panel2.TabIndex = 1;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            BackgroundImage = Properties.Resources.unnamed;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1600, 900);
            Controls.Add(guna2Panel2);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(1278, 688);
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "iRIS";
            guna2Panel2.ResumeLayout(false);
            guna2Panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Guna2BorderlessForm guna2BorderlessForm1;
        private Guna2Panel guna2Panel2;
        private Guna2ImageCheckBox chkShowPassword;
        private Guna2Button btnForgotPassword;
        private Label label2;
        private Label lblError;
        private Guna2GradientButton btnLogin;
        private Label label1;
        private Label label6;
        private Guna2TextBox txtPassword;
        private Label label5;
        private Guna2TextBox txtUsername;
        private Label label4;
        private Guna2ImageButton guna2ImageButton1;
    }
}