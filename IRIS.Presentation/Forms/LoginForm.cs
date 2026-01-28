using IRIS.Domain.Entities;
using IRIS.Infrastructure.Data;
using IRIS.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;

namespace IRIS.Presentation
{
    public partial class LoginForm : Form
    {
        private readonly IrisDbContext _context;
        public LoginForm()
        {
            InitializeComponent();
            var options = new DbContextOptionsBuilder<IrisDbContext>()
            .UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=IRIS_DB;Trusted_Connection=True;")
            .Options;

            _context = new IrisDbContext(options);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Look for active user
            var user = _context.Users
                .FirstOrDefault(u => u.Username.ToLower() == username.ToLower() && u.IsActive);

            if (user == null)
            {
                MessageBox.Show("Invalid username or account inactive!", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verify hashed password
            if (user.PasswordHash is null || !PasswordHasher.VerifyPassword(password, user.PasswordHash))
            {
                MessageBox.Show("Invalid password!", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Show a simple message instead of dashboard forms
            MessageBox.Show($"Login successful!\nRole: {user.Role}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void panelCard_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void MinimizeBtn_Click_1(object sender, EventArgs e)
        {
            for (double i = 1; i >= 0.8; i -= 0.05)
            {
                this.Opacity = i;
                await Task.Delay(10);
            }

            this.WindowState = FormWindowState.Minimized;
            this.Opacity = 1;
        }


    }
}
