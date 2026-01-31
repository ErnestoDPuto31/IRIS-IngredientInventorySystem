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
                    MessageBox.Show("Invalid username or account inactive!", 
                        "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate password
                if (user.PasswordHash is null || !PasswordHasher.VerifyPassword(password, user.PasswordHash))
                {
                    MessageBox.Show("Invalid password!", 
                        "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Successful login
                MessageBox.Show($"Login successful!\nWelcome: {user.Username}\nRole: {user.Role.ToString()}", 
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during login: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
