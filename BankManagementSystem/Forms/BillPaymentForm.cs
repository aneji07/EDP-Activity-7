using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using BankManagementSystem.Services;

namespace BankManagementSystem.Forms
{
    public partial class BillPaymentForm : Form
    {
        private ComboBox cboBillType;
        private NumericUpDown numAmount;
        private TextBox txtReferenceNo;
        private Button btnSave;
        private Button btnCancel;
        private Label lblTitle;

        public BillPaymentForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Pay Bill";
            this.Size = new Size(450, 370);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(245, 247, 250);

            // Title
            lblTitle = new Label();
            lblTitle.Text = "Bill Payment";
            lblTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitle.Size = new Size(400, 30);
            lblTitle.Location = new Point(25, 20);
            this.Controls.Add(lblTitle);

            // Bill Type
            Label lblType = new Label();
            lblType.Text = "Bill Type:";
            lblType.Location = new Point(30, 80);
            lblType.Size = new Size(100, 25);
            this.Controls.Add(lblType);

            cboBillType = new ComboBox();
            cboBillType.DropDownStyle = ComboBoxStyle.DropDownList;
            cboBillType.Items.AddRange(new object[] { "Electricity", "Water", "Internet", "Phone", "Credit Card" });
            cboBillType.SelectedIndex = 0;
            cboBillType.Location = new Point(140, 78);
            cboBillType.Size = new Size(220, 25);
            this.Controls.Add(cboBillType);

            // Amount
            Label lblAmount = new Label();
            lblAmount.Text = "Amount (PHP):";
            lblAmount.Location = new Point(30, 130);
            lblAmount.Size = new Size(100, 25);
            this.Controls.Add(lblAmount);

            numAmount = new NumericUpDown();
            numAmount.DecimalPlaces = 2;
            numAmount.Minimum = 0.01M;
            numAmount.Maximum = 999999.99M;
            numAmount.Location = new Point(140, 128);
            numAmount.Size = new Size(220, 25);
            this.Controls.Add(numAmount);

            // Reference Number (optional)
            Label lblRef = new Label();
            lblRef.Text = "Reference No.:";
            lblRef.Location = new Point(30, 180);
            lblRef.Size = new Size(100, 25);
            this.Controls.Add(lblRef);

            txtReferenceNo = new TextBox();
            txtReferenceNo.Location = new Point(140, 178);
            txtReferenceNo.Size = new Size(220, 25);
            this.Controls.Add(txtReferenceNo);

            // Buttons
            btnSave = new Button();
            btnSave.Text = "Pay Bill";
            btnSave.BackColor = Color.FromArgb(46, 204, 113);
            btnSave.ForeColor = Color.White;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Size = new Size(120, 35);
            btnSave.Location = new Point(80, 250);
            btnSave.Click += BtnSave_Click;
            this.Controls.Add(btnSave);

            btnCancel = new Button();
            btnCancel.Text = "Cancel";
            btnCancel.BackColor = Color.FromArgb(149, 165, 166);
            btnCancel.ForeColor = Color.White;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Size = new Size(100, 35);
            btnCancel.Location = new Point(240, 250);
            btnCancel.Click += (s, e) => this.Close();
            this.Controls.Add(btnCancel);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (cboBillType.SelectedItem == null)
            {
                MessageBox.Show("Please select a bill type.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (numAmount.Value <= 0)
            {
                MessageBox.Show("Amount must be greater than zero.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = @"INSERT INTO bill_payments (user_id, bill_type, amount, reference_no)
                             VALUES (@uid, @type, @amount, @ref)";

            var parameters = new Dictionary<string, object>
            {
                { "@uid", AuthService.CurrentUser.UserId },
                { "@type", cboBillType.SelectedItem.ToString() },
                { "@amount", numAmount.Value },
                { "@ref", txtReferenceNo.Text.Trim() }
            };

            try
            {
                DatabaseManager.ExecuteNonQuery(query, parameters);
                MessageBox.Show("Bill payment recorded!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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