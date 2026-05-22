using BankManagementSystem.Services;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BankManagementSystem.Forms
{
    public partial class DepositWithdrawalForm : Form
    {
        private ComboBox cboTransactionType;
        private NumericUpDown numAmount;
        private TextBox txtDescription;
        private Button btnSave;
        private Button btnCancel;
        private Label lblTitle;

        public DepositWithdrawalForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Deposit / Withdrawal";
            this.Size = new Size(450, 350);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(245, 247, 250);

            // Title
            lblTitle = new Label();
            lblTitle.Text = "New Bank Transaction";
            lblTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitle.Size = new Size(400, 30);
            lblTitle.Location = new Point(25, 20);
            this.Controls.Add(lblTitle);

            // Transaction type
            Label lblType = new Label();
            lblType.Text = "Transaction Type:";
            lblType.Location = new Point(30, 80);
            lblType.Size = new Size(120, 25);
            this.Controls.Add(lblType);

            cboTransactionType = new ComboBox();
            cboTransactionType.Items.AddRange(new object[] { "Deposit", "Withdrawal" });
            cboTransactionType.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTransactionType.Location = new Point(160, 78);
            cboTransactionType.Size = new Size(200, 25);
            this.Controls.Add(cboTransactionType);

            // Amount
            Label lblAmount = new Label();
            lblAmount.Text = "Amount (PHP):";
            lblAmount.Location = new Point(30, 130);
            lblAmount.Size = new Size(120, 25);
            this.Controls.Add(lblAmount);

            numAmount = new NumericUpDown();
            numAmount.DecimalPlaces = 2;
            numAmount.Minimum = 0.01M;
            numAmount.Maximum = 999999.99M;
            numAmount.Location = new Point(160, 128);
            numAmount.Size = new Size(200, 25);
            this.Controls.Add(numAmount);

            // Description
            Label lblDesc = new Label();
            lblDesc.Text = "Description:";
            lblDesc.Location = new Point(30, 180);
            lblDesc.Size = new Size(120, 25);
            this.Controls.Add(lblDesc);

            txtDescription = new TextBox();
            txtDescription.Location = new Point(160, 178);
            txtDescription.Size = new Size(200, 25);
            this.Controls.Add(txtDescription);

            // Buttons
            btnSave = new Button();
            btnSave.Text = "Save Transaction";
            btnSave.BackColor = Color.FromArgb(46, 204, 113);
            btnSave.ForeColor = Color.White;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Size = new Size(140, 35);
            btnSave.Location = new Point(80, 250);
            btnSave.Click += BtnSave_Click;
            this.Controls.Add(btnSave);

            btnCancel = new Button();
            btnCancel.Text = "Cancel";
            btnCancel.BackColor = Color.FromArgb(149, 165, 166);
            btnCancel.ForeColor = Color.White;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Size = new Size(100, 35);
            btnCancel.Location = new Point(250, 250);
            btnCancel.Click += (s, e) => this.Close();
            this.Controls.Add(btnCancel);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (cboTransactionType.SelectedItem == null)
            {
                MessageBox.Show("Please select a transaction type.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = @"INSERT INTO bank_transactions (user_id, transaction_type, amount, description)
                             VALUES (@uid, @type, @amount, @desc)";

            var parameters = new Dictionary<string, object>
            {
                { "@uid", AuthService.CurrentUser.UserId },
                { "@type", cboTransactionType.SelectedItem.ToString() },
                { "@amount", numAmount.Value },
                { "@desc", txtDescription.Text.Trim() }
            };

            try
            {
                DatabaseManager.ExecuteNonQuery(query, parameters);
                MessageBox.Show("Transaction saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}