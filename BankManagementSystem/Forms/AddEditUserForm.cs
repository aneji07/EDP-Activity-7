using BankManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BankManagementSystem.Forms
{
    public class AddEditUserForm : Form
    {
        private int? userId = null;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private TextBox txtConfirmPassword;
        private TextBox txtEmail;
        private TextBox txtFullName;
        private TextBox txtSecurityQuestion;
        private TextBox txtSecurityAnswer;
        private ComboBox cboRole;
        private CheckBox chkActive;
        private Button btnSave;
        private Button btnCancel;
        private Label lblTitle;

        public AddEditUserForm(int? id = null)
        {
            userId = id;
            InitializeForm();
            CreateControls();

            if (userId.HasValue)
            {
                LoadUserData();
                this.Text = "Edit User";
                lblTitle.Text = "EDIT USER";
            }
            else
            {
                this.Text = "Add New User";
                lblTitle.Text = "ADD NEW USER";
            }
        }

        private void InitializeForm()
        {
            this.Size = new Size(550, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void CreateControls()
        {
            int yPos = 25;
            int spacing = 40;

            // Title
            lblTitle = new Label();
            lblTitle.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitle.Size = new Size(500, 40);
            lblTitle.Location = new Point(25, yPos);
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            yPos += 55;

            // Username
            AddLabel("Username:", 30, yPos);
            txtUsername = new TextBox();
            txtUsername.Location = new Point(160, yPos);
            txtUsername.Size = new Size(340, 30);
            txtUsername.Font = new Font("Segoe UI", 10);
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            yPos += spacing;

            // Password fields for new user
            if (!userId.HasValue)
            {
                AddLabel("Password:", 30, yPos);
                txtPassword = new TextBox();
                txtPassword.Location = new Point(160, yPos);
                txtPassword.Size = new Size(340, 30);
                txtPassword.PasswordChar = '*';
                txtPassword.BorderStyle = BorderStyle.FixedSingle;
                yPos += spacing;

                AddLabel("Confirm:", 30, yPos);
                txtConfirmPassword = new TextBox();
                txtConfirmPassword.Location = new Point(160, yPos);
                txtConfirmPassword.Size = new Size(340, 30);
                txtConfirmPassword.PasswordChar = '*';
                txtConfirmPassword.BorderStyle = BorderStyle.FixedSingle;
                yPos += spacing;
            }

            // Email
            AddLabel("Email:", 30, yPos);
            txtEmail = new TextBox();
            txtEmail.Location = new Point(160, yPos);
            txtEmail.Size = new Size(340, 30);
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            yPos += spacing;

            // Full Name
            AddLabel("Full Name:", 30, yPos);
            txtFullName = new TextBox();
            txtFullName.Location = new Point(160, yPos);
            txtFullName.Size = new Size(340, 30);
            txtFullName.BorderStyle = BorderStyle.FixedSingle;
            yPos += spacing;

            // Security Question
            AddLabel("Security Q:", 30, yPos);
            txtSecurityQuestion = new TextBox();
            txtSecurityQuestion.Location = new Point(160, yPos);
            txtSecurityQuestion.Size = new Size(340, 30);
            txtSecurityQuestion.BorderStyle = BorderStyle.FixedSingle;
            yPos += spacing;

            // Security Answer
            AddLabel("Security A:", 30, yPos);
            txtSecurityAnswer = new TextBox();
            txtSecurityAnswer.Location = new Point(160, yPos);
            txtSecurityAnswer.Size = new Size(340, 30);
            txtSecurityAnswer.BorderStyle = BorderStyle.FixedSingle;
            yPos += spacing;

            // Role
            AddLabel("Role:", 30, yPos);
            cboRole = new ComboBox();
            cboRole.Location = new Point(160, yPos);
            cboRole.Size = new Size(150, 30);
            cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRole.Items.AddRange(new string[] { "Staff", "Admin" });
            cboRole.SelectedIndex = 0;
            yPos += spacing;

            // Active (only for edit)
            if (userId.HasValue)
            {
                AddLabel("Active:", 30, yPos);
                chkActive = new CheckBox();
                chkActive.Location = new Point(160, yPos);
                chkActive.Size = new Size(60, 30);
                yPos += spacing;
            }

            // Buttons
            btnSave = new Button();
            btnSave.Text = "SAVE";
            btnSave.BackColor = Color.FromArgb(46, 204, 113);
            btnSave.ForeColor = Color.White;
            btnSave.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Size = new Size(100, 40);
            btnSave.Location = new Point(140, yPos + 20);
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button();
            btnCancel.Text = "CANCEL";
            btnCancel.BackColor = Color.FromArgb(149, 165, 166);
            btnCancel.ForeColor = Color.White;
            btnCancel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Size = new Size(100, 40);
            btnCancel.Location = new Point(270, yPos + 20);
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            // Add controls
            this.Controls.Add(lblTitle);
            this.Controls.Add(txtUsername);
            this.Controls.Add(txtEmail);
            this.Controls.Add(txtFullName);
            this.Controls.Add(txtSecurityQuestion);
            this.Controls.Add(txtSecurityAnswer);
            this.Controls.Add(cboRole);
            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);

            if (!userId.HasValue)
            {
                this.Controls.Add(txtPassword);
                this.Controls.Add(txtConfirmPassword);
            }
            else
            {
                this.Controls.Add(chkActive);
            }
        }

        private void AddLabel(string text, int x, int y)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.ForeColor = Color.FromArgb(44, 62, 80);
            lbl.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lbl.Location = new Point(x, y);
            lbl.Size = new Size(110, 30);
            this.Controls.Add(lbl);
        }

        private void LoadUserData()
        {
            string query = "SELECT * FROM users WHERE user_id = @userId";
            var parameters = new Dictionary<string, object>();
            parameters.Add("@userId", userId.Value);

            using (var reader = DatabaseManager.ExecuteReader(query, parameters))
            {
                if (reader.Read())
                {
                    txtUsername.Text = reader.GetString("username");
                    txtEmail.Text = reader.GetString("email");
                    txtFullName.Text = reader.GetString("full_name");
                    txtSecurityQuestion.Text = reader.GetString("security_question");
                    txtSecurityAnswer.Text = reader.GetString("security_answer");
                    cboRole.SelectedItem = reader.GetString("role");
                    chkActive.Checked = reader.GetBoolean("is_active");
                    txtUsername.Enabled = false;
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // --- Common validation ---
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Username is required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Email is required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Full name is required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtSecurityQuestion.Text))
            {
                MessageBox.Show("Security question is required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtSecurityAnswer.Text))
            {
                MessageBox.Show("Security answer is required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // --- EDIT MODE ---
                if (userId.HasValue)
                {
                    // Ask for current admin's password
                    string adminPassword = "";
                    using (var pwdForm = new Form())
                    {
                        pwdForm.Text = "Confirm Password";
                        pwdForm.Size = new Size(350, 170);
                        pwdForm.StartPosition = FormStartPosition.CenterParent;
                        pwdForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                        pwdForm.MaximizeBox = false;
                        pwdForm.MinimizeBox = false;
                        pwdForm.BackColor = Color.FromArgb(245, 247, 250);

                        Label lbl = new Label();
                        lbl.Text = "Enter your password to confirm changes:";
                        lbl.Location = new Point(20, 20);
                        lbl.Size = new Size(300, 30);
                        lbl.Font = new Font("Segoe UI", 9);
                        pwdForm.Controls.Add(lbl);

                        TextBox txtPwd = new TextBox();
                        txtPwd.Location = new Point(20, 60);
                        txtPwd.Size = new Size(250, 30);
                        txtPwd.PasswordChar = '*';
                        txtPwd.BorderStyle = BorderStyle.FixedSingle;
                        pwdForm.Controls.Add(txtPwd);

                        Button btnOk = new Button();
                        btnOk.Text = "OK";
                        btnOk.BackColor = Color.FromArgb(52, 152, 219);
                        btnOk.ForeColor = Color.White;
                        btnOk.FlatStyle = FlatStyle.Flat;
                        btnOk.Location = new Point(110, 100);
                        btnOk.Size = new Size(80, 30);
                        btnOk.DialogResult = DialogResult.OK;
                        pwdForm.Controls.Add(btnOk);

                        if (pwdForm.ShowDialog() == DialogResult.OK)
                            adminPassword = txtPwd.Text;
                        else
                            return;
                    }

                    if (!AuthService.VerifyCurrentPassword(adminPassword))
                    {
                        MessageBox.Show("Incorrect password. Changes not saved.", "Security Check",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Proceed with update
                    string query = @"UPDATE users SET email = @email, full_name = @fullName, 
                                   security_question = @question, security_answer = @answer, 
                                   role = @role, is_active = @isActive WHERE user_id = @userId";
                    var parameters = new Dictionary<string, object>();
                    parameters.Add("@email", txtEmail.Text);
                    parameters.Add("@fullName", txtFullName.Text);
                    parameters.Add("@question", txtSecurityQuestion.Text);
                    parameters.Add("@answer", txtSecurityAnswer.Text);
                    parameters.Add("@role", cboRole.SelectedItem.ToString());
                    parameters.Add("@isActive", chkActive.Checked);
                    parameters.Add("@userId", userId.Value);
                    DatabaseManager.ExecuteNonQuery(query, parameters);
                    MessageBox.Show("User updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                // --- ADD NEW USER MODE ---
                else
                {
                    if (AuthService.UserExists(txtUsername.Text))
                    {
                        MessageBox.Show("Username already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(txtPassword.Text))
                    {
                        MessageBox.Show("Password is required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (txtPassword.Text != txtConfirmPassword.Text)
                    {
                        MessageBox.Show("Passwords do not match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string query = @"INSERT INTO users (username, password_hash, email, full_name, 
                                   security_question, security_answer, role) 
                                   VALUES (@username, @password, @email, @fullName, @question, @answer, @role)";
                    var parameters = new Dictionary<string, object>();
                    parameters.Add("@username", txtUsername.Text);
                    parameters.Add("@password", txtPassword.Text);
                    parameters.Add("@email", txtEmail.Text);
                    parameters.Add("@fullName", txtFullName.Text);
                    parameters.Add("@question", txtSecurityQuestion.Text);
                    parameters.Add("@answer", txtSecurityAnswer.Text);
                    parameters.Add("@role", cboRole.SelectedItem.ToString());
                    DatabaseManager.ExecuteNonQuery(query, parameters);
                    MessageBox.Show("User added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving user: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}