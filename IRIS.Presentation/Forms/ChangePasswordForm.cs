using IRIS.Domain.Entities;
using IRIS.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Windows.Forms;

namespace IRIS.Presentation.Forms
{
    public partial class ChangePasswordForm : Form
    {
        private readonly User _user;
        private readonly IrisDbContext _context;
        private System.Windows.Forms.Timer fadeInTimer;
        private double opacityStep = 0.05; // Controls the speed of the fade-in effect

        public ChangePasswordForm()
        {
            InitializeComponent();
        }

        public ChangePasswordForm(User user, IrisDbContext context)
        {
            InitializeComponent();
            _user = user;
            _context = context;

            this.AcceptButton = btnSave;
            lblError.Visible = false;

            // Initialize and configure the timer for the fade-in effect
            fadeInTimer = new System.Windows.Forms.Timer();
            fadeInTimer.Interval = 10; // The interval in milliseconds for smoother fade
            fadeInTimer.Tick += FadeInTimer_Tick;

            // Set initial opacity to 0 (fully transparent)
            this.Opacity = 0;

            // Hook up form load event to start the fade-in effect
            this.Load += new EventHandler(ChangePasswordForm_Load);
        }

        private void ChangePasswordForm_Load(object sender, EventArgs e)
        {
            // Start the fade-in animation when the form is loaded
            fadeInTimer.Start();
        }

        private void FadeInTimer_Tick(object sender, EventArgs e)
        {
            // Increase opacity gradually by opacityStep
            if (this.Opacity < 1)
            {
                this.Opacity += opacityStep;
            }
            else
            {
                // Stop the timer once the opacity reaches 100%
                fadeInTimer.Stop();
            }
        }

        private void ShowError(string message)
        {
            lblError.Text = message;
            lblError.Visible = true;
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            bool showText = !chkShowPassword.Checked;

            txtNewPassword.UseSystemPasswordChar = showText;
            txtConfirmPassword.UseSystemPasswordChar = showText;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string newPassword = txtNewPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                ShowError("Passwords cannot be empty.");
                return;
            }

            if (newPassword != confirmPassword)
            {
                ShowError("Passwords do not match.");
                return;
            }

            if (newPassword.Length < 8)
            {
                ShowError("Password must be at least 8 characters long.");
                return;
            }

            bool hasLetter = newPassword.Any(char.IsLetter);
            bool hasDigit = newPassword.Any(char.IsDigit);

            if (!hasLetter || !hasDigit)
            {
                ShowError("Password must contain at least one letter and one number.");
                return;
            }

            try
            {
                var hasher = new PasswordHasher<User>();
                _user.PasswordHash = hasher.HashPassword(_user, newPassword);

                _user.isFirstLogin = false;

                _context.Users.Update(_user);
                _context.SaveChanges();

                MessageBox.Show("Password updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred saving your password: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}