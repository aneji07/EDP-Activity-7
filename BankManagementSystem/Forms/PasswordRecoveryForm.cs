using BankManagementSystem.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BankManagementSystem.Forms
{
    public class PasswordRecoveryForm : Form
    {
        private TextBox txtUsername;
        private TextBox txtSecurityAnswer;
        private TextBox txtNewPassword;
        private TextBox txtConfirmPassword;
        private Label lblQuestion;
        private Button btnVerify;
        private Button btnReset;
        private Button btnCancel;
        private Label lblTitle;
        private string securityQuestion = null;
        private string verifiedUsername = null;

        public PasswordRecoveryForm()
        {
            InitializeForm();
            CreateControls();
        }

        private void InitializeForm()
        {
            this.Text = "Password Recovery";
            this.Size = new Size(500, 480);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
        }

        private void CreateControls()
        {
            int yPos = 25;
            // Title
            lblTitle = new Label();
            lblTitle.Text = "PASSWORD RECOVERY";
            lblTitle.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitle.Size = new Size(460, 40);
            lblTitle.Location = new Point(20, yPos);
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            yPos += 60;

            // Username
            AddLabel("Username:", 30, yPos);
            txtUsername = new TextBox();
            txtUsername.Location = new Point(160, yPos);
            txtUsername.Size = new Size(280, 30);
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            yPos += 45;

            // Verify Button
            btnVerify = new Button();
            btnVerify.Text = "VERIFY";
            btnVerify.BackColor = Color.FromArgb(52, 152, 219);
            btnVerify.ForeColor = Color.White;
            btnVerify.FlatStyle = FlatStyle.Flat;
            btnVerify.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnVerify.Size = new Size(100, 30 );
            btnVerify.Location = new Point(340, yPos - 35);
            btnVerify.Click += BtnVerify_Click;

            // Security Question Label
            lblQuestion = new Label();
            lblQuestion.Text = "Security Question: ";
            lblQuestion.ForeColor = Color.FromArgb(44, 62, 80);
            lblQuestion.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            lblQuestion.Location = new Point(30, yPos);
            lblQuestion.Size = new Size(420, 30);
            lblQuestion.Visible = false;
            yPos += 45;

            // Security Answer
            AddLabel("Answer:", 30, yPos);
            txtSecurityAnswer = new TextBox();
            txtSecurityAnswer.Location = new Point(160, yPos);
            txtSecurityAnswer.Size = new Size(280, 30);
            txtSecurityAnswer.BorderStyle = BorderStyle.FixedSingle;
            txtSecurityAnswer.Visible = false;
            yPos += 45;

            // New Password
            AddLabel("New Password:", 30, yPos);
            txtNewPassword = new TextBox();
            txtNewPassword.Location = new Point(160, yPos);
            txtNewPassword.Size = new Size(280, 30);
            txtNewPassword.PasswordChar = '*';
            txtNewPassword.BorderStyle = BorderStyle.FixedSingle;
            txtNewPassword.Visible = false;
            yPos += 45;

            // Confirm Password
            AddLabel("Confirm:", 30, yPos);
            txtConfirmPassword = new TextBox();
            txtConfirmPassword.Location = new Point(160, yPos);
            txtConfirmPassword.Size = new Size(280, 30);
            txtConfirmPassword.PasswordChar = '*';
            txtConfirmPassword.BorderStyle = BorderStyle.FixedSingle;
            txtConfirmPassword.Visible = false;
            yPos += 55;

            // Reset Button
            btnReset = new Button();
            btnReset.Text = "RESET PASSWORD";
            btnReset.BackColor = Color.FromArgb(231, 76, 60);
            btnReset.ForeColor = Color.White;
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnReset.Size = new Size(140, 40);
            btnReset.Location = new Point(70, yPos);
            btnReset.Visible = false;
            btnReset.Click += BtnReset_Click;

            // Cancel Button
            btnCancel = new Button();
            btnCancel.Text = "CANCEL";
            btnCancel.BackColor = Color.FromArgb(149, 165, 166);
            btnCancel.ForeColor = Color.White;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnCancel.Size = new Size(100, 40);
            btnCancel.Location = new Point(290, yPos);
            btnCancel.Click += (s, e) => this.Close();

            // Add controls
            this.Controls.Add(lblTitle);
            this.Controls.Add(txtUsername);
            this.Controls.Add(btnVerify);
            this.Controls.Add(lblQuestion);
            this.Controls.Add(txtSecurityAnswer);
            this.Controls.Add(txtNewPassword);
            this.Controls.Add(txtConfirmPassword);
            this.Controls.Add(btnReset);
            this.Controls.Add(btnCancel);
        }

        private void AddLabel(string text, int x, int y)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.ForeColor = Color.FromArgb(44, 62, 80);
            lbl.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            lbl.Location = new Point(x, y);
            lbl.Size = new Size(100, 30);
            this.Controls.Add(lbl);
        }

        private void BtnVerify_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Please enter username!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            securityQuestion = AuthService.GetSecurityQuestion(txtUsername.Text);

            if (securityQuestion != null)
            {
                verifiedUsername = txtUsername.Text;
                lblQuestion.Text = "Security Question: " + securityQuestion;
                lblQuestion.Visible = true;
                txtSecurityAnswer.Visible = true;
                txtNewPassword.Visible = true;
                txtConfirmPassword.Visible = true;
                btnReset.Visible = true;
                btnVerify.Enabled = false;
                txtUsername.Enabled = false;
            }
            else
            {
                MessageBox.Show("Username not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSecurityAnswer.Text))
            {
                MessageBox.Show("Please answer the security question!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
            {
                MessageBox.Show("Please enter new password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Passwords do not match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (AuthService.ResetPassword(verifiedUsername, txtSecurityAnswer.Text, txtNewPassword.Text))
            {
                MessageBox.Show("Password reset successful! Please login with your new password.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Incorrect security answer!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}