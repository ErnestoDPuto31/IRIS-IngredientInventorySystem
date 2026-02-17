using IRIS.Domain.Entities;
using IRIS.Infrastructure.Data;
using Microsoft.AspNetCore.Identity; 
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
                    .FirstOrDefault(u => u.Username.ToLower() == username.ToLower() && u.IsActive);

                // Validate user existence and active status
                if (user == null)
                {
                    ShowError("User does not exist or is inactive.");
                    return;
                }

                // Validate password using ASP.NET Core Hasher
                var hasher = new PasswordHasher<User>();
                var passwordResult = hasher.VerifyHashedPassword(user, user.PasswordHash, password);

                if (passwordResult == PasswordVerificationResult.Failed)
                {
                    ShowError("Incorrect password.");
                    return;
                }

                if (!string.IsNullOrEmpty(user.SessionToken))
                {
                    var overrideResult = MessageBox.Show(
                        "This account is currently active on another device, or the app closed unexpectedly during your last session.\n\nWould you like to force logout the other session and log in here?",
                        "Account in Use",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (overrideResult == DialogResult.No)
                    {
                        return;
                    }
                }

                // Prevent multiple logins on different devices
                if (user.IsLoggedIn)
                {
                    ShowError("This user is already logged in on another device.");
                    return;
                }

                // 4. Handle First Time Login
                if (user.isFirstLogin)
                {
                    // Open the Change Password form and pass the user and database context to it
                    using (var changePasswordForm = new IRIS.Presentation.Forms.ChangePasswordForm(user, _context))
                    {
                        var result = changePasswordForm.ShowDialog(this);

                        if (result != DialogResult.OK)
                        {
                            return; 
                        }
                    }
                }

                // SUCCESSFUL LOGIN
                string newToken = Guid.NewGuid().ToString();

                user.SessionToken = newToken;
                _context.SaveChanges();

                UserSession.CurrentUser = user;
                UserSession.CurrentSessionToken = newToken;

                this.Hide();

                using (MainForm mainApp = new MainForm())
                {
                    mainApp.ShowDialog();
                }


                var loggedOutUser = _context.Users.Find(user.UserId);
                if (loggedOutUser != null)
                {
                    if (loggedOutUser.SessionToken == UserSession.CurrentSessionToken)
                    {
                        loggedOutUser.SessionToken = null; 
                        _context.SaveChanges();
                    }
                }

                txtPassword.Clear();
                lblError.Visible = false;
                UserSession.CurrentUser = null;
                UserSession.CurrentSessionToken = null;
                this.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during login: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowError(string message)
        {
            lblError.Visible = true;
            lblError.Text = message;
        }
    }
}