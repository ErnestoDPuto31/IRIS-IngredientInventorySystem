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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            guna2BorderlessForm1 = new Guna2BorderlessForm(components);
            guna2Panel2 = new Guna2Panel();
            lblError = new Label();
            btnLogin = new Guna2GradientButton();
            label1 = new Label();
            label6 = new Label();
            txtPassword = new Guna2TextBox();
            label5 = new Label();
            txtUsername = new Guna2TextBox();
            label4 = new Label();
            label2 = new Label();
            label3 = new Label();
            guna2ImageButton1 = new Guna2ImageButton();
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
            // guna2Panel2
            // 
            guna2Panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            guna2Panel2.BackColor = Color.Transparent;
            guna2Panel2.BorderColor = Color.FromArgb(137, 65, 208);
            guna2Panel2.BorderRadius = 30;
            guna2Panel2.BorderThickness = 2;
            guna2Panel2.Controls.Add(lblError);
            guna2Panel2.Controls.Add(btnLogin);
            guna2Panel2.Controls.Add(label1);
            guna2Panel2.Controls.Add(label6);
            guna2Panel2.Controls.Add(txtPassword);
            guna2Panel2.Controls.Add(label5);
            guna2Panel2.Controls.Add(txtUsername);
            guna2Panel2.Controls.Add(label4);
            guna2Panel2.Controls.Add(label2);
            guna2Panel2.Controls.Add(label3);
            guna2Panel2.Controls.Add(guna2ImageButton1);
            guna2Panel2.CustomBorderColor = Color.FromArgb(137, 65, 208);
            guna2Panel2.CustomizableEdges = customizableEdges8;
            guna2Panel2.FillColor = Color.Transparent;
            guna2Panel2.Location = new Point(997, 40);
            guna2Panel2.Name = "guna2Panel2";
            guna2Panel2.ShadowDecoration.BorderRadius = 25;
            guna2Panel2.ShadowDecoration.CustomizableEdges = customizableEdges9;
            guna2Panel2.ShadowDecoration.Depth = 100;
            guna2Panel2.ShadowDecoration.Shadow = new Padding(0, 0, 0, 10);
            guna2Panel2.Size = new Size(549, 810);
            guna2Panel2.TabIndex = 1;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.Font = new Font("Poppins", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblError.ForeColor = Color.Red;
            lblError.Location = new Point(54, 578);
            lblError.Name = "lblError";
            lblError.Size = new Size(35, 30);
            lblError.TabIndex = 9;
            lblError.Text = "aa";
            lblError.Visible = false;
            // 
            // btnLogin
            // 
            btnLogin.Anchor = AnchorStyles.None;
            btnLogin.BorderRadius = 15;
            btnLogin.CustomizableEdges = customizableEdges1;
            btnLogin.DisabledState.BorderColor = Color.DarkGray;
            btnLogin.DisabledState.CustomBorderColor = Color.DarkGray;
            btnLogin.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnLogin.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            btnLogin.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnLogin.FillColor = Color.FromArgb(137, 65, 208);
            btnLogin.FillColor2 = Color.FromArgb(77, 10, 133);
            btnLogin.Font = new Font("Poppins", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLogin.ForeColor = Color.White;
            btnLogin.HoverState.FillColor = Color.FromArgb(155, 85, 225);
            btnLogin.HoverState.FillColor2 = Color.FromArgb(95, 25, 155);
            btnLogin.Location = new Point(54, 630);
            btnLogin.Name = "btnLogin";
            btnLogin.PressedColor = Color.FromArgb(60, 5, 110);
            btnLogin.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnLogin.Size = new Size(444, 67);
            btnLogin.TabIndex = 3;
            btnLogin.Text = "Sign In";
            btnLogin.Click += btnLogin_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Font = new Font("Poppins", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(221, 195);
            label1.Name = "label1";
            label1.Size = new Size(139, 36);
            label1.TabIndex = 0;
            label1.Text = "Welcome To";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.None;
            label6.AutoSize = true;
            label6.Font = new Font("Poppins", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Black;
            label6.Location = new Point(54, 484);
            label6.Name = "label6";
            label6.Size = new Size(94, 30);
            label6.TabIndex = 8;
            label6.Text = "Password";
            // 
            // txtPassword
            // 
            txtPassword.Anchor = AnchorStyles.None;
            txtPassword.BorderColor = Color.Gray;
            txtPassword.BorderRadius = 15;
            txtPassword.BorderThickness = 2;
            txtPassword.CustomizableEdges = customizableEdges3;
            txtPassword.DefaultText = "";
            txtPassword.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtPassword.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtPassword.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtPassword.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtPassword.FillColor = SystemColors.Window;
            txtPassword.FocusedState.BorderColor = Color.FromArgb(137, 65, 208);
            txtPassword.Font = new Font("Poppins", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPassword.ForeColor = Color.Black;
            txtPassword.HoverState.BorderColor = Color.FromArgb(137, 65, 208);
            txtPassword.IconLeft = Properties.Resources.icons8_password_24;
            txtPassword.IconLeftOffset = new Point(10, 0);
            txtPassword.Location = new Point(54, 514);
            txtPassword.Margin = new Padding(4, 9, 4, 9);
            txtPassword.Name = "txtPassword";
            txtPassword.PlaceholderText = "Enter Password";
            txtPassword.SelectedText = "";
            txtPassword.ShadowDecoration.CustomizableEdges = customizableEdges4;
            txtPassword.Size = new Size(444, 55);
            txtPassword.TabIndex = 1;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.None;
            label5.AutoSize = true;
            label5.Font = new Font("Poppins", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Black;
            label5.Location = new Point(54, 383);
            label5.Name = "label5";
            label5.Size = new Size(101, 30);
            label5.TabIndex = 6;
            label5.Text = "Username";
            // 
            // txtUsername
            // 
            txtUsername.Anchor = AnchorStyles.None;
            txtUsername.BorderColor = Color.Gray;
            txtUsername.BorderRadius = 15;
            txtUsername.BorderThickness = 2;
            txtUsername.CustomizableEdges = customizableEdges5;
            txtUsername.DefaultText = "";
            txtUsername.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtUsername.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtUsername.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtUsername.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtUsername.FillColor = SystemColors.Window;
            txtUsername.FocusedState.BorderColor = Color.FromArgb(137, 65, 208);
            txtUsername.Font = new Font("Poppins", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtUsername.ForeColor = Color.Black;
            txtUsername.HoverState.BorderColor = Color.FromArgb(137, 65, 208);
            txtUsername.IconLeft = Properties.Resources.icons8_user_24;
            txtUsername.IconLeftOffset = new Point(10, 0);
            txtUsername.Location = new Point(54, 413);
            txtUsername.Margin = new Padding(4, 8, 4, 8);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "Enter Username";
            txtUsername.SelectedText = "";
            txtUsername.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtUsername.Size = new Size(444, 54);
            txtUsername.TabIndex = 0;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.Font = new Font("Poppins", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Gray;
            label4.Location = new Point(182, 336);
            label4.Name = "label4";
            label4.Size = new Size(229, 26);
            label4.TabIndex = 4;
            label4.Text = "Sign-In to Access the System";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Font = new Font("Poppins", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(77, 10, 133);
            label2.Location = new Point(99, 289);
            label2.Name = "label2";
            label2.Size = new Size(408, 36);
            label2.TabIndex = 2;
            label2.Text = "An Ingredient Request Inventory System";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.Font = new Font("Poppins", 48F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(77, 10, 133);
            label3.Location = new Point(143, 195);
            label3.Name = "label3";
            label3.Size = new Size(311, 141);
            label3.TabIndex = 3;
            label3.Text = "i  R  I  S";
            // 
            // guna2ImageButton1
            // 
            guna2ImageButton1.Anchor = AnchorStyles.None;
            guna2ImageButton1.BackgroundImageLayout = ImageLayout.Stretch;
            guna2ImageButton1.CheckedState.ImageSize = new Size(64, 64);
            guna2ImageButton1.HoverState.ImageSize = new Size(200, 200);
            guna2ImageButton1.Image = Properties.Resources.IRIS_Logo;
            guna2ImageButton1.ImageOffset = new Point(0, 0);
            guna2ImageButton1.ImageRotate = 0F;
            guna2ImageButton1.ImageSize = new Size(300, 300);
            guna2ImageButton1.Location = new Point(201, 19);
            guna2ImageButton1.Name = "guna2ImageButton1";
            guna2ImageButton1.PressedState.ImageSize = new Size(64, 64);
            guna2ImageButton1.ShadowDecoration.CustomizableEdges = customizableEdges7;
            guna2ImageButton1.Size = new Size(173, 182);
            guna2ImageButton1.TabIndex = 1;
            guna2ImageButton1.UseTransparentBackground = true;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1600, 900);
            Controls.Add(guna2Panel2);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "IRIS";
            TopMost = true;
            guna2Panel2.ResumeLayout(false);
            guna2Panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Guna2BorderlessForm guna2BorderlessForm1;
        private Guna2Panel guna2Panel2;
        private Label label1;
        private Label label3;
        private Label label2;
        private Guna2ImageButton guna2ImageButton1;
        private Guna2TextBox txtUsername;
        private Label label4;
        private Label label6;
        private Guna2TextBox txtPassword;
        private Label label5;
        private Guna2GradientButton btnLogin;
        private Label lblError;
    }
}
