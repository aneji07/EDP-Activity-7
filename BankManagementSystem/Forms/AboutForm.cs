using System;
using System.Drawing;
using System.Windows.Forms;

namespace BankManagementSystem.Forms
{
    public class AboutForm : Form
    {
        private Label lblTitle;
        private Label lblDetails;
        private Button btnClose;
        private PictureBox pictureBox;

        public AboutForm()
        {
            InitializeForm();
            CreateControls();
        }

        private void InitializeForm()
        {
            this.Text = "About Bank Management System";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void CreateControls()
        {
            // Title
            lblTitle = new Label();
            lblTitle.Text = "BANK MANAGEMENT SYSTEM";
            lblTitle.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitle.Size = new Size(460, 40);
            lblTitle.Location = new Point(20, 20);
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // Details
            lblDetails = new Label();
            lblDetails.Text = 
                "IT 120 Event Driven Programming\n" +
                "Activity 5: User Authentication & Management\n\n" +
                "Developed by: Developed by: Barrameda, Ane Christel - BSIT 3A\n\nCollaborator: achi0725\n\n" +
                "Features:\n" +
                "✓ User Authentication\n" +
                "✓ Password Recovery\n" +
                "✓ User Management (CRUD)\n" +
                "✓ Active/Inactive Account Toggle\n" +
                "✓ Search Users\n\n" +
                "© 2024 - All Rights Reserved";
            lblDetails.Font = new Font("Segoe UI", 10);
            lblDetails.ForeColor = Color.FromArgb(44, 62, 80);
            lblDetails.Size = new Size(440, 240);
            lblDetails.Location = new Point(30, 80);
            lblDetails.TextAlign = ContentAlignment.MiddleLeft;

            // Close button
            btnClose = new Button();
            btnClose.Text = "CLOSE";
            btnClose.BackColor = Color.FromArgb(52, 152, 219);
            btnClose.ForeColor = Color.White;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnClose.Size = new Size(100, 35);
            btnClose.Location = new Point(190, 320);
            btnClose.Click += (s, e) => this.Close();

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblDetails);
            this.Controls.Add(btnClose);
        }
    }
}