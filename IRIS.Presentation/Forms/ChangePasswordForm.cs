using IRIS.Domain.Entities;
using IRIS.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace IRIS.Presentation.Forms
{
    public partial class ChangePasswordForm : Form
    {
        private readonly User _user;
        private readonly IrisDbContext _context;

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

            if (newPassword.Length < 6)
            {
                ShowError("Password must be at least 6 characters long.");
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
