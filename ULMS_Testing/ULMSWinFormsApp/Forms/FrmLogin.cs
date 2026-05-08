using ULMSWinFormsApp.Forms;

namespace ULMSWinFormsApp
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (!ValidateInputs(username, password)) return;

            if (AuthenticateUser(username, password))
            {
                OnLoginSuccess();
            }
            else
            {
                OnLoginFailure();
            }
        }

        private bool ValidateInputs(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Please enter your username.", "Login Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter your password.", "Login Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            return true;
        }

        private bool AuthenticateUser(string username, string password)
        {
            const string ValidUsername = "admin";
            const string ValidPassword = "1234";

            // The username comparison is case-insensitive, while the password comparison is case-sensitive for better security practices
            return string.Equals(username, ValidUsername, StringComparison.OrdinalIgnoreCase)
                && string.Equals(password, ValidPassword, StringComparison.Ordinal);
        }

        private void OnLoginSuccess()
        {
            MessageBox.Show("Valid login. Access granted! Welcome.", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            FrmDashboard dashboard = new FrmDashboard();
            dashboard.Show();
            this.Hide();
        }

        private void OnLoginFailure()
        {
            MessageBox.Show("Oops! That username or password doesn’t match. Please try again.", "Login Failed",
                MessageBoxButtons.OK, MessageBoxIcon.Error);

            txtPassword.Clear();
            txtPassword.Focus();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtUsername.Focus();
        }
    }
}