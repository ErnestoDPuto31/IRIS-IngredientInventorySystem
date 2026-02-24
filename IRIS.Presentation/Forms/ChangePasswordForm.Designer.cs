namespace IRIS.Presentation.Forms
{
    partial class ChangePasswordForm
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangePasswordForm));
            guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            btnCancel = new Guna.UI2.WinForms.Guna2GradientButton();
            btnSave = new Guna.UI2.WinForms.Guna2GradientButton();
            lblError = new Label();
            chkShowPassword = new Guna.UI2.WinForms.Guna2CheckBox();
            txtConfirmPassword = new Guna.UI2.WinForms.Guna2TextBox();
            guna2HtmlLabel4 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtNewPassword = new Guna.UI2.WinForms.Guna2TextBox();
            guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
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
            // guna2Panel1
            // 
            guna2Panel1.BackgroundImageLayout = ImageLayout.Stretch;
            guna2Panel1.Controls.Add(btnCancel);
            guna2Panel1.Controls.Add(btnSave);
            guna2Panel1.Controls.Add(lblError);
            guna2Panel1.Controls.Add(chkShowPassword);
            guna2Panel1.Controls.Add(txtConfirmPassword);
            guna2Panel1.Controls.Add(guna2HtmlLabel4);
            guna2Panel1.Controls.Add(txtNewPassword);
            guna2Panel1.Controls.Add(guna2HtmlLabel3);
            guna2Panel1.Controls.Add(guna2HtmlLabel2);
            guna2Panel1.Controls.Add(guna2HtmlLabel1);
            guna2Panel1.CustomizableEdges = customizableEdges9;
            guna2Panel1.Dock = DockStyle.Fill;
            guna2Panel1.Location = new Point(0, 0);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges10;
            guna2Panel1.Size = new Size(400, 428);
            guna2Panel1.TabIndex = 0;
            // 
            // btnCancel
            // 
            btnCancel.BorderColor = Color.Indigo;
            btnCancel.BorderRadius = 10;
            btnCancel.BorderThickness = 2;
            btnCancel.CustomizableEdges = customizableEdges1;
            btnCancel.DisabledState.BorderColor = Color.DarkGray;
            btnCancel.DisabledState.CustomBorderColor = Color.DarkGray;
            btnCancel.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnCancel.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            btnCancel.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnCancel.FillColor = Color.White;
            btnCancel.FillColor2 = Color.White;
            btnCancel.FocusedColor = Color.White;
            btnCancel.Font = new Font("Poppins", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancel.ForeColor = Color.Indigo;
            btnCancel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            btnCancel.HoverState.FillColor = Color.BlueViolet;
            btnCancel.HoverState.FillColor2 = Color.MediumPurple;
            btnCancel.Location = new Point(124, 356);
            btnCancel.Name = "btnCancel";
            btnCancel.PressedColor = Color.DarkSlateBlue;
            btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnCancel.Size = new Size(106, 41);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "CANCEL";
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.BorderRadius = 10;
            btnSave.CustomizableEdges = customizableEdges3;
            btnSave.DisabledState.BorderColor = Color.DarkGray;
            btnSave.DisabledState.CustomBorderColor = Color.DarkGray;
            btnSave.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnSave.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            btnSave.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnSave.FillColor = Color.Indigo;
            btnSave.FillColor2 = Color.BlueViolet;
            btnSave.FocusedColor = Color.MediumPurple;
            btnSave.Font = new Font("Poppins", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSave.ForeColor = Color.White;
            btnSave.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            btnSave.HoverState.FillColor = Color.BlueViolet;
            btnSave.HoverState.FillColor2 = Color.MediumPurple;
            btnSave.Location = new Point(236, 356);
            btnSave.Name = "btnSave";
            btnSave.PressedColor = Color.DarkSlateBlue;
            btnSave.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnSave.Size = new Size(128, 41);
            btnSave.TabIndex = 3;
            btnSave.Text = "SAVE";
            btnSave.Click += btnSave_Click;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.Font = new Font("Poppins", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblError.ForeColor = Color.Red;
            lblError.Location = new Point(36, 276);
            lblError.Name = "lblError";
            lblError.Size = new Size(202, 26);
            lblError.TabIndex = 7;
            lblError.Text = "Passwords do not match.";
            lblError.Visible = false;
            // 
            // chkShowPassword
            // 
            chkShowPassword.AutoSize = true;
            chkShowPassword.CheckedState.BorderColor = Color.Ivory;
            chkShowPassword.CheckedState.BorderRadius = 2;
            chkShowPassword.CheckedState.BorderThickness = 0;
            chkShowPassword.CheckedState.FillColor = Color.Indigo;
            chkShowPassword.Font = new Font("Poppins", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkShowPassword.ForeColor = SystemColors.ControlDarkDark;
            chkShowPassword.Location = new Point(32, 305);
            chkShowPassword.Name = "chkShowPassword";
            chkShowPassword.Size = new Size(152, 30);
            chkShowPassword.TabIndex = 6;
            chkShowPassword.Text = "Show Password";
            chkShowPassword.UncheckedState.BorderColor = Color.Indigo;
            chkShowPassword.UncheckedState.BorderRadius = 2;
            chkShowPassword.UncheckedState.BorderThickness = 1;
            chkShowPassword.UncheckedState.FillColor = Color.White;
            chkShowPassword.CheckedChanged += chkShowPassword_CheckedChanged;
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.BackColor = Color.Transparent;
            txtConfirmPassword.BorderColor = SystemColors.ControlDark;
            txtConfirmPassword.BorderRadius = 10;
            txtConfirmPassword.BorderThickness = 2;
            txtConfirmPassword.CustomizableEdges = customizableEdges5;
            txtConfirmPassword.DefaultText = "";
            txtConfirmPassword.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtConfirmPassword.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtConfirmPassword.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtConfirmPassword.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtConfirmPassword.FocusedState.BorderColor = Color.Indigo;
            txtConfirmPassword.Font = new Font("Poppins", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtConfirmPassword.HoverState.BorderColor = Color.Indigo;
            txtConfirmPassword.IconLeft = Properties.Resources.icons8_password_24;
            txtConfirmPassword.IconLeftOffset = new Point(5, 0);
            txtConfirmPassword.Location = new Point(32, 227);
            txtConfirmPassword.Margin = new Padding(4, 6, 4, 6);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.PlaceholderText = "Confirm Password";
            txtConfirmPassword.SelectedText = "";
            txtConfirmPassword.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtConfirmPassword.Size = new Size(332, 39);
            txtConfirmPassword.TabIndex = 2;
            txtConfirmPassword.UseSystemPasswordChar = true;
            // 
            // guna2HtmlLabel4
            // 
            guna2HtmlLabel4.BackColor = Color.Transparent;
            guna2HtmlLabel4.Font = new Font("Poppins", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel4.ForeColor = SystemColors.ControlDarkDark;
            guna2HtmlLabel4.Location = new Point(32, 198);
            guna2HtmlLabel4.Name = "guna2HtmlLabel4";
            guna2HtmlLabel4.Size = new Size(206, 32);
            guna2HtmlLabel4.TabIndex = 4;
            guna2HtmlLabel4.Text = "Confirm New Password";
            // 
            // txtNewPassword
            // 
            txtNewPassword.BackColor = Color.Transparent;
            txtNewPassword.BorderColor = SystemColors.ControlDark;
            txtNewPassword.BorderRadius = 10;
            txtNewPassword.BorderThickness = 2;
            txtNewPassword.CustomizableEdges = customizableEdges7;
            txtNewPassword.DefaultText = "";
            txtNewPassword.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtNewPassword.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtNewPassword.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtNewPassword.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtNewPassword.FocusedState.BorderColor = Color.Indigo;
            txtNewPassword.Font = new Font("Poppins", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNewPassword.HoverState.BorderColor = Color.Indigo;
            txtNewPassword.IconLeft = Properties.Resources.icons8_password_24;
            txtNewPassword.IconLeftOffset = new Point(5, 0);
            txtNewPassword.Location = new Point(32, 143);
            txtNewPassword.Margin = new Padding(4, 6, 4, 6);
            txtNewPassword.Name = "txtNewPassword";
            txtNewPassword.PlaceholderText = "Enter New Password";
            txtNewPassword.SelectedText = "";
            txtNewPassword.ShadowDecoration.CustomizableEdges = customizableEdges8;
            txtNewPassword.Size = new Size(332, 39);
            txtNewPassword.TabIndex = 1;
            txtNewPassword.UseSystemPasswordChar = true;
            // 
            // guna2HtmlLabel3
            // 
            guna2HtmlLabel3.BackColor = Color.Transparent;
            guna2HtmlLabel3.Font = new Font("Poppins", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel3.ForeColor = SystemColors.ControlDarkDark;
            guna2HtmlLabel3.Location = new Point(32, 114);
            guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            guna2HtmlLabel3.Size = new Size(131, 32);
            guna2HtmlLabel3.TabIndex = 2;
            guna2HtmlLabel3.Text = "New Password";
            // 
            // guna2HtmlLabel2
            // 
            guna2HtmlLabel2.AutoSize = false;
            guna2HtmlLabel2.BackColor = Color.Transparent;
            guna2HtmlLabel2.Font = new Font("Poppins", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel2.ForeColor = Color.Gray;
            guna2HtmlLabel2.Location = new Point(12, 44);
            guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            guna2HtmlLabel2.Size = new Size(376, 45);
            guna2HtmlLabel2.TabIndex = 1;
            guna2HtmlLabel2.Text = "For security reasons, you must change your default password before continuing.";
            guna2HtmlLabel2.TextAlignment = ContentAlignment.TopCenter;
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Font = new Font("Poppins", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel1.ForeColor = Color.Black;
            guna2HtmlLabel1.Location = new Point(55, 12);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(278, 42);
            guna2HtmlLabel1.TabIndex = 0;
            guna2HtmlLabel1.Text = "Set Your New Password";
            // 
            // ChangePasswordForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 428);
            Controls.Add(guna2Panel1);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "ChangePasswordForm";
            StartPosition = FormStartPosition.CenterParent;
            guna2Panel1.ResumeLayout(false);
            guna2Panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2TextBox txtNewPassword;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2CheckBox chkShowPassword;
        private Guna.UI2.WinForms.Guna2TextBox txtConfirmPassword;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel4;
        private Guna.UI2.WinForms.Guna2GradientButton btnCancel;
        private Guna.UI2.WinForms.Guna2GradientButton btnSave;
        private Label lblError;
    }
}