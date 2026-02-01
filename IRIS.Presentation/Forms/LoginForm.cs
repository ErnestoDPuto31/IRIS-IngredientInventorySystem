using IRIS.Domain.Entities;
using IRIS.Infrastructure.Data;
using IRIS.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using IRIS.Presentation.Forms;

namespace IRIS.Presentation
{
    public partial class LoginForm : Form
    {
        private readonly IrisDbContext _context;
        public LoginForm()
        {
            InitializeComponent();

            this.AcceptButton = btnLogin;
            var options = new DbContextOptionsBuilder<IrisDbContext>()
            .UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;
                        Database=IRIS_DB;
                        Trusted_Connection=True;")
            .Options;

            _context = new IrisDbContext(options);
            SeedData.Initialize(_context);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();

                var user = _context.Users
                    .FirstOrDefault(u => u.Username.ToLower() == username.ToLower() &&
                    u.IsActive);

                // Validate user existence and active status
                if (user == null)
                {
                    lblError.Visible = true;
                    lblError.Text = "User does not exist or is inactive.";
                    return;
                }

                // Validate password
                if (user.PasswordHash is null || !PasswordHasher.VerifyPassword(password, user.PasswordHash))
                {
                    lblError.Visible = true;
                    lblError.Text = "Incorrect password.";
                    return;
                }

                // Successful login
                UserSession.CurrentUser = user;

                this.Hide();

                using (MainForm mainApp = new MainForm())
                {
                    mainApp.ShowDialog();
                }

                txtPassword.Clear();
                lblError.Visible = false;
                this.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during login: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

    }
}
