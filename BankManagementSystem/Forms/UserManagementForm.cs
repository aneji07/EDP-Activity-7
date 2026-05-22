using MySql.Data.MySqlClient;
using BankManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BankManagementSystem.Forms
{
    public class UserManagementForm : Form
    {
        private DataGridView dgvUsers;
        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnToggleStatus;
        private Button btnDelete;
        private Button btnRefresh;
        private Button btnClose;
        private Label lblTitle;
        private Panel headerPanel;

        public UserManagementForm()
        {
            InitializeForm();
            CreateControls();
            LoadUsers();
        }

        private void InitializeForm()
        {
            this.Text = "User Management";
            this.Size = new Size(1100, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(245, 247, 250);
        }

        private void CreateControls()
        {
            // Header Panel
            headerPanel = new Panel();
            headerPanel.BackColor = Color.FromArgb(44, 62, 80);
            headerPanel.Size = new Size(1100, 70);
            headerPanel.Location = new Point(0, 0);
            headerPanel.Dock = DockStyle.Top;

            // Title
            lblTitle = new Label();
            lblTitle.Text = "USER MANAGEMENT";
            lblTitle.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Size = new Size(400, 40);
            lblTitle.Location = new Point(20, 15);

            // Search Box
            Label lblSearch = new Label();
            lblSearch.Text = "Search:";
            lblSearch.ForeColor = Color.FromArgb(44, 62, 80);
            lblSearch.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblSearch.Location = new Point(30, 100);
            lblSearch.Size = new Size(60, 25);

            txtSearch = new TextBox();
            txtSearch.Location = new Point(100, 95);
            txtSearch.Size = new Size(220, 30);
            txtSearch.Font = new Font("Segoe UI", 10);
            txtSearch.BorderStyle = BorderStyle.FixedSingle;

            btnSearch = new Button();
            btnSearch.Text = "🔍 SEARCH";
            btnSearch.BackColor = Color.FromArgb(52, 152, 219);
            btnSearch.ForeColor = Color.White;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Size = new Size(90, 30);
            btnSearch.Location = new Point(335, 95);
            btnSearch.Click += BtnSearch_Click;

            // DataGridView
            dgvUsers = new DataGridView();
            dgvUsers.Location = new Point(30, 150);
            dgvUsers.Size = new Size(1040, 420);
            dgvUsers.BackgroundColor = Color.White;
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.MultiSelect = false;
            dgvUsers.ReadOnly = true;
            dgvUsers.BorderStyle = BorderStyle.None;
            dgvUsers.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvUsers.RowHeadersVisible = false;
            dgvUsers.EnableHeadersVisualStyles = false;
            dgvUsers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            dgvUsers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            // Buttons
            btnAdd = new Button();
            btnAdd.Text = "➕ ADD USER";
            btnAdd.BackColor = Color.FromArgb(46, 204, 113);
            btnAdd.ForeColor = Color.White;
            btnAdd.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.Size = new Size(120, 40);
            btnAdd.Location = new Point(30, 600);
            btnAdd.Click += BtnAdd_Click;

            btnEdit = new Button();
            btnEdit.Text = "✏️ EDIT";
            btnEdit.BackColor = Color.FromArgb(52, 152, 219);
            btnEdit.ForeColor = Color.White;
            btnEdit.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.FlatAppearance.BorderSize = 0;
            btnEdit.Size = new Size(100, 40);
            btnEdit.Location = new Point(170, 600);
            btnEdit.Click += BtnEdit_Click;

            btnToggleStatus = new Button();
            btnToggleStatus.Text = "🔄 TOGGLE STATUS";
            btnToggleStatus.BackColor = Color.FromArgb(243, 156, 18);
            btnToggleStatus.ForeColor = Color.White;
            btnToggleStatus.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnToggleStatus.FlatStyle = FlatStyle.Flat;
            btnToggleStatus.FlatAppearance.BorderSize = 0;
            btnToggleStatus.Size = new Size(130, 40);
            btnToggleStatus.Location = new Point(290, 600);
            btnToggleStatus.Click += BtnToggleStatus_Click;

            btnDelete = new Button();
            btnDelete.Text = "🗑️ DELETE";
            btnDelete.BackColor = Color.FromArgb(231, 76, 60);
            btnDelete.ForeColor = Color.White;
            btnDelete.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.Size = new Size(100, 40);
            btnDelete.Location = new Point(440, 600);
            btnDelete.Click += BtnDelete_Click;

            btnRefresh = new Button();
            btnRefresh.Text = "🔄 REFRESH";
            btnRefresh.BackColor = Color.FromArgb(149, 165, 166);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Size = new Size(100, 40);
            btnRefresh.Location = new Point(850, 600);
            btnRefresh.Click += (s, e) => LoadUsers();

            btnClose = new Button();
            btnClose.Text = "CLOSE";
            btnClose.BackColor = Color.FromArgb(149, 165, 166);
            btnClose.ForeColor = Color.White;
            btnClose.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Size = new Size(100, 40);
            btnClose.Location = new Point(970, 600);
            btnClose.Click += (s, e) => this.Close();

            // Add controls
            headerPanel.Controls.Add(lblTitle);
            this.Controls.Add(headerPanel);
            this.Controls.Add(lblSearch);
            this.Controls.Add(txtSearch);
            this.Controls.Add(btnSearch);
            this.Controls.Add(dgvUsers);
            this.Controls.Add(btnAdd);
            this.Controls.Add(btnEdit);
            this.Controls.Add(btnToggleStatus);
            this.Controls.Add(btnDelete);
            this.Controls.Add(btnRefresh);
            this.Controls.Add(btnClose);
        }

        private void LoadUsers(string searchKeyword = null)
        {
            string query;
            var parameters = new Dictionary<string, object>();

            if (string.IsNullOrWhiteSpace(searchKeyword))
            {
                query = "SELECT user_id, username, full_name, email, role, is_active FROM users ORDER BY user_id";
            }
            else
            {
                query = @"SELECT user_id, username, full_name, email, role, is_active 
                         FROM users 
                         WHERE username LIKE @keyword OR full_name LIKE @keyword OR email LIKE @keyword
                         ORDER BY user_id";
                parameters.Add("@keyword", "%" + searchKeyword + "%");
            }

            try
            {
                using (var reader = DatabaseManager.ExecuteReader(query, parameters))
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    dgvUsers.DataSource = dt;
                }

                // Format columns
                if (dgvUsers.Columns.Contains("user_id"))
                    dgvUsers.Columns["user_id"].HeaderText = "ID";
                if (dgvUsers.Columns.Contains("username"))
                    dgvUsers.Columns["username"].HeaderText = "Username";
                if (dgvUsers.Columns.Contains("full_name"))
                    dgvUsers.Columns["full_name"].HeaderText = "Full Name";
                if (dgvUsers.Columns.Contains("email"))
                    dgvUsers.Columns["email"].HeaderText = "Email";
                if (dgvUsers.Columns.Contains("role"))
                    dgvUsers.Columns["role"].HeaderText = "Role";
                if (dgvUsers.Columns.Contains("is_active"))
                    dgvUsers.Columns["is_active"].HeaderText = "Active";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading users: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
                LoadUsers();
            else
                LoadUsers(txtSearch.Text.Trim());
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            AddEditUserForm addForm = new AddEditUserForm();
            if (addForm.ShowDialog() == DialogResult.OK)
                LoadUsers();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user to edit.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int userId = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["user_id"].Value);
            AddEditUserForm editForm = new AddEditUserForm(userId);
            if (editForm.ShowDialog() == DialogResult.OK)
                LoadUsers();
        }

        private void BtnToggleStatus_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int userId = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["user_id"].Value);
            string username = dgvUsers.SelectedRows[0].Cells["username"].Value.ToString();
            bool currentStatus = Convert.ToBoolean(dgvUsers.SelectedRows[0].Cells["is_active"].Value);
            string newStatus = !currentStatus ? "Active" : "Inactive";

            DialogResult result = MessageBox.Show($"Toggle user '{username}' to {newStatus}?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string query = "UPDATE users SET is_active = @newStatus WHERE user_id = @userId";
                var parameters = new Dictionary<string, object>();
                parameters.Add("@newStatus", !currentStatus);
                parameters.Add("@userId", userId);
                DatabaseManager.ExecuteNonQuery(query, parameters);
                LoadUsers();
                MessageBox.Show($"User is now {newStatus}.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user to delete.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int userId = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["user_id"].Value);
            string username = dgvUsers.SelectedRows[0].Cells["username"].Value.ToString();

            if (username == "admin")
            {
                MessageBox.Show("Cannot delete the admin account!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show($"Delete user '{username}'? This cannot be undone.", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                string query = "DELETE FROM users WHERE user_id = @userId";
                var parameters = new Dictionary<string, object>();
                parameters.Add("@userId", userId);
                DatabaseManager.ExecuteNonQuery(query, parameters);
                LoadUsers();
                MessageBox.Show("User deleted successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}