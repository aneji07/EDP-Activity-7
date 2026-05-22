using BankManagementSystem.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BankManagementSystem.Forms
{
    public class LoginForm : Form
    {
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnExit;
        private LinkLabel lnkForgotPassword;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel headerPanel;
        private CheckBox chkShowPassword;   // NEW: Show password toggle

        public LoginForm()
        {
            InitializeForm();
            CreateControls();
        }

        private void InitializeForm()
        {
            this.Text = "Bank Management System - Login";
            this.Size = new Size(600, 560);   // Slightly taller to fit checkbox
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(245, 247, 250);
        }

        private void CreateControls()
        {
            // Header Panel (same as before)
            headerPanel = new Panel();
            headerPanel.BackColor = Color.FromArgb(44, 62, 80);
            headerPanel.Size = new Size(600, 120);
            headerPanel.Location = new Point(0, 0);

            lblTitle = new Label();
            lblTitle.Text = "BANK MANAGEMENT SYSTEM";
            lblTitle.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Size = new Size(580, 45);
            lblTitle.Location = new Point(10, 30);
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            lblSubtitle = new Label();
            lblSubtitle.Text = "Please log in to continue";
            lblSubtitle.Font = new Font("Segoe UI", 11);
            lblSubtitle.ForeColor = Color.FromArgb(200, 200, 200);
            lblSubtitle.Size = new Size(580, 25);
            lblSubtitle.Location = new Point(10, 80);
            lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;

            headerPanel.Controls.Add(lblTitle);
            headerPanel.Controls.Add(lblSubtitle);
            this.Controls.Add(headerPanel);

            // Username
            Label lblUser = new Label();
            lblUser.Text = "Username";
            lblUser.ForeColor = Color.FromArgb(44, 62, 80);
            lblUser.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblUser.Location = new Point(100, 160);
            lblUser.Size = new Size(100, 25);
            this.Controls.Add(lblUser);

            txtUsername = new TextBox();
            txtUsername.Location = new Point(100, 190);
            txtUsername.Size = new Size(400, 30);
            txtUsername.Font = new Font("Segoe UI", 11);
            txtUsername.BackColor = Color.White;
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(txtUsername);

            // Password
            Label lblPass = new Label();
            lblPass.Text = "Password";
            lblPass.ForeColor = Color.FromArgb(44, 62, 80);
            lblPass.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblPass.Location = new Point(100, 240);
            lblPass.Size = new Size(100, 25);
            this.Controls.Add(lblPass);

            txtPassword = new TextBox();
            txtPassword.Location = new Point(100, 270);
            txtPassword.Size = new Size(400, 30);
            txtPassword.Font = new Font("Segoe UI", 11);
            txtPassword.PasswordChar = '*';
            txtPassword.BackColor = Color.White;
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(txtPassword);

            // NEW: Show Password CheckBox
            chkShowPassword = new CheckBox();
            chkShowPassword.Text = "👁 Show Password";
            chkShowPassword.ForeColor = Color.FromArgb(44, 62, 80);
            chkShowPassword.Font = new Font("Segoe UI", 9);
            chkShowPassword.Location = new Point(100, 310);
            chkShowPassword.Size = new Size(130, 25);
            chkShowPassword.CheckedChanged += ChkShowPassword_CheckedChanged;
            this.Controls.Add(chkShowPassword);

            // Login Button
            btnLogin = new Button();
            btnLogin.Text = "LOGIN";
            btnLogin.BackColor = Color.FromArgb(52, 152, 219);
            btnLogin.ForeColor = Color.White;
            btnLogin.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Size = new Size(180, 45);
            btnLogin.Location = new Point(130, 360);
            btnLogin.Click += BtnLogin_Click;
            this.Controls.Add(btnLogin);

            // Exit Button
            btnExit = new Button();
            btnExit.Text = "EXIT";
            btnExit.BackColor = Color.FromArgb(149, 165, 166);
            btnExit.ForeColor = Color.White;
            btnExit.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.Size = new Size(180, 45);
            btnExit.Location = new Point(320, 360);
            btnExit.Click += (s, e) => Application.Exit();
            this.Controls.Add(btnExit);

            // Forgot Password Link
            lnkForgotPassword = new LinkLabel();
            lnkForgotPassword.Text = "Forgot Password?";
            lnkForgotPassword.LinkColor = Color.FromArgb(52, 152, 219);
            lnkForgotPassword.Font = new Font("Segoe UI", 9);
            lnkForgotPassword.Location = new Point(240, 430);
            lnkForgotPassword.Size = new Size(120, 20);
            lnkForgotPassword.TextAlign = ContentAlignment.MiddleCenter;
            lnkForgotPassword.LinkClicked += LnkForgotPassword_LinkClicked;
            this.Controls.Add(lnkForgotPassword);
        }

        // NEW: Toggle password visibility
        private void ChkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chkShowPassword.Checked ? '\0' : '*';
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter username and password!", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (AuthService.Login(txtUsername.Text, txtPassword.Text))
            {
                MessageBox.Show($"Welcome, {AuthService.CurrentUser?.FullName}!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                DashboardForm dashboard = new DashboardForm();
                dashboard.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username, password, or your account may be inactive. Please check your credentials or contact admin.",
    "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private void LnkForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PasswordRecoveryForm recoverForm = new PasswordRecoveryForm();
            recoverForm.ShowDialog();
        }
    }
}