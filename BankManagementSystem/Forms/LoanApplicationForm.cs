using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using BankManagementSystem.Services;

namespace BankManagementSystem.Forms
{
    public partial class LoanApplicationForm : Form
    {
        private NumericUpDown numLoanAmount;
        private NumericUpDown numLoanTerm;
        private TextBox txtPurpose;
        private ComboBox cboStatus;
        private Button btnSave;
        private Button btnCancel;
        private Label lblTitle;

        public LoanApplicationForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Apply for Loan";
            this.Size = new Size(450, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(245, 247, 250);

            // Title
            lblTitle = new Label();
            lblTitle.Text = "New Loan Application";
            lblTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitle.Size = new Size(400, 30);
            lblTitle.Location = new Point(25, 20);
            this.Controls.Add(lblTitle);

            // Loan Amount
            Label lblAmount = new Label();
            lblAmount.Text = "Loan Amount (PHP):";
            lblAmount.Location = new Point(30, 80);
            lblAmount.Size = new Size(130, 25);
            this.Controls.Add(lblAmount);

            numLoanAmount = new NumericUpDown();
            numLoanAmount.DecimalPlaces = 2;
            numLoanAmount.Minimum = 1000.00M;
            numLoanAmount.Maximum = 1000000.00M;
            numLoanAmount.Increment = 1000.00M;
            numLoanAmount.Location = new Point(170, 78);
            numLoanAmount.Size = new Size(200, 25);
            this.Controls.Add(numLoanAmount);

            // Loan Term (months)
            Label lblTerm = new Label();
            lblTerm.Text = "Loan Term (months):";
            lblTerm.Location = new Point(30, 130);
            lblTerm.Size = new Size(130, 25);
            this.Controls.Add(lblTerm);

            numLoanTerm = new NumericUpDown();
            numLoanTerm.Minimum = 1;
            numLoanTerm.Maximum = 60;
            numLoanTerm.Location = new Point(170, 128);
            numLoanTerm.Size = new Size(200, 25);
            this.Controls.Add(numLoanTerm);

            // Purpose
            Label lblPurpose = new Label();
            lblPurpose.Text = "Purpose:";
            lblPurpose.Location = new Point(30, 180);
            lblPurpose.Size = new Size(130, 25);
            this.Controls.Add(lblPurpose);

            txtPurpose = new TextBox();
            txtPurpose.Location = new Point(170, 178);
            txtPurpose.Size = new Size(200, 25);
            this.Controls.Add(txtPurpose);

            // Status (default = Pending)
            Label lblStatus = new Label();
            lblStatus.Text = "Status:";
            lblStatus.Location = new Point(30, 230);
            lblStatus.Size = new Size(130, 25);
            this.Controls.Add(lblStatus);

            cboStatus = new ComboBox();
            cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboStatus.Items.AddRange(new object[] { "Pending", "Approved", "Rejected" });
            cboStatus.SelectedIndex = 0;   // Pending
            cboStatus.Location = new Point(170, 228);
            cboStatus.Size = new Size(200, 25);
            this.Controls.Add(cboStatus);

            // Buttons
            btnSave = new Button();
            btnSave.Text = "Submit Application";
            btnSave.BackColor = Color.FromArgb(46, 204, 113);
            btnSave.ForeColor = Color.White;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Size = new Size(150, 35);
            btnSave.Location = new Point(70, 290);
            btnSave.Click += BtnSave_Click;
            this.Controls.Add(btnSave);

            btnCancel = new Button();
            btnCancel.Text = "Cancel";
            btnCancel.BackColor = Color.FromArgb(149, 165, 166);
            btnCancel.ForeColor = Color.White;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Size = new Size(100, 35);
            btnCancel.Location = new Point(250, 290);
            btnCancel.Click += (s, e) => this.Close();
            this.Controls.Add(btnCancel);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (numLoanAmount.Value <= 0)
            {
                MessageBox.Show("Please enter a valid loan amount.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (numLoanTerm.Value <= 0)
            {
                MessageBox.Show("Please enter a valid loan term.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPurpose.Text))
            {
                MessageBox.Show("Please enter the purpose of the loan.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = @"INSERT INTO loan_applications (user_id, loan_amount, loan_term, purpose, status)
                             VALUES (@uid, @amount, @term, @purpose, @status)";

            var parameters = new Dictionary<string, object>
            {
                { "@uid", AuthService.CurrentUser.UserId },
                { "@amount", numLoanAmount.Value },
                { "@term", (int)numLoanTerm.Value },
                { "@purpose", txtPurpose.Text.Trim() },
                { "@status", cboStatus.SelectedItem.ToString() }
            };

            try
            {
                DatabaseManager.ExecuteNonQuery(query, parameters);
                MessageBox.Show("Loan application submitted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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