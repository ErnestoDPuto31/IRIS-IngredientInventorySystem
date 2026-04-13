using IRIS.Domain.Entities;
using IRIS.Infrastructure.Data;
using IRIS.Infrastructure.Services;
using IRIS.Presentation.Helpers;

namespace IRIS.Presentation.Forms
{
    public partial class ForgotPasswordForm : Form
    {
        private readonly IrisDbContext _context;
        private User _targetUser;

        private System.Windows.Forms.Timer fadeInTimer;
        private double opacityStep = 0.05; 

        public ForgotPasswordForm(IrisDbContext context)
        {
            InitializeComponent();
            FontManager.ApplyFont(this);
            _context = context;


            fadeInTimer = new System.Windows.Forms.Timer();
            fadeInTimer.Interval = 10; 
            fadeInTimer.Tick += FadeInTimer_Tick;

            this.Opacity = 0;

            this.Load += new EventHandler(ForgotPasswordForm_Load);

            ResetUI();
        }

        private void ForgotPasswordForm_Load(object sender, EventArgs e)
        {
            fadeInTimer.Start();
        }

        private void FadeInTimer_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1)
            {
                this.Opacity += opacityStep;
            }
            else
            {
                fadeInTimer.Stop();
            }
        }

        private void ResetUI()
        {
            lblError.Visible = false;

            lblSecurityDisplay.Visible = false;
            lblSecurityQuestion.Visible = false;
            txtSecurityAnswer.Visible = false;
            btnVerifyAnswer.Visible = false;

            txtUsername.Enabled = true;
            btnFindUser.Enabled = true;
            txtUsername.Clear();
            txtSecurityAnswer.Clear();

            this.AcceptButton = btnFindUser;
        }

        private void ShowError(string message)
        {
            lblError.Text = message;
            lblError.Visible = true;
        }

        private void btnFindUser_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;
            string username = txtUsername.Text.Trim();

            if (string.IsNullOrEmpty(username))
            {
                ShowError("Please enter your username.");
                return;
            }

            _targetUser = _context.Users.FirstOrDefault(u => u.Username.ToLower() == username.ToLower() && u.IsActive);

            if (_targetUser == null)
            {
                ShowError("User not found or inactive.");
                return;
            }
            if (string.IsNullOrEmpty(_targetUser.SecurityQuestion) || string.IsNullOrEmpty(_targetUser.SecurityAnswerHash))
            {
                MessageBox.Show("This account has not set up a security question. Please contact your system administrator to reset your password.",
                                "Setup Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lblSecurityQuestion.Text = _targetUser.SecurityQuestion;
            lblSecurityQuestion.Visible = true;
            txtSecurityAnswer.Visible = true;
            btnVerifyAnswer.Visible = true;
            lblSecurityDisplay.Visible = true;

            txtUsername.Enabled = false;
            btnFindUser.Enabled = false;

            this.AcceptButton = btnVerifyAnswer;
            txtSecurityAnswer.Focus();
        }

        private void btnVerifyAnswer_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;
            string inputAnswer = txtSecurityAnswer.Text.Trim();

            if (string.IsNullOrEmpty(inputAnswer))
            {
                ShowError("Please enter an answer.");
                return;
            }

            var securityService = new SecurityService();
            bool isVerified = securityService.VerifySecurityAnswer(_targetUser, _targetUser.SecurityAnswerHash, inputAnswer);

            if (isVerified)
            {
                MessageBox.Show("Identity verified! You may now reset your password.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide();

                using (var changePasswordForm = new ChangePasswordForm(_targetUser, _context))
                {
                    var result = changePasswordForm.ShowDialog(this);

                    if (result == DialogResult.OK)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        ResetUI();
                        this.Show();
                    }
                }
            }
            else
            {
                ShowError("Incorrect security answer. Please try again.");
                txtSecurityAnswer.Clear();
                txtSecurityAnswer.Focus();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}