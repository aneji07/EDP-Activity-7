using BankManagementSystem.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BankManagementSystem.Forms
{
    public class DashboardForm : Form
    {
        private Panel headerPanel;
        private Label lblTitle;
        private Label lblWelcome;
        private Panel mainPanel;
        private Panel footerPanel;

        // Main action buttons
        private Button btnUserManagement;
        private Button btnDepositWithdraw;
        private Button btnLoanApplication;
        private Button btnBillPayment;
        private Button btnReports;

        // Footer buttons
        private Button btnPasswordRecovery;
        private Button btnAbout;
        private Button btnLogout;

        public DashboardForm()
        {
            InitializeForm();
            CreateControls();
        }

        private void InitializeForm()
        {
            this.Text = "Bank Management System - Dashboard";
            this.Size = new Size(1100, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.MinimumSize = new Size(900, 600);
        }

        private void CreateControls()
        {
            // ================= HEADER =================
            headerPanel = new Panel();
            headerPanel.BackColor = Color.FromArgb(44, 62, 80);
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 100;

            lblTitle = new Label();
            lblTitle.Text = "BANK MANAGEMENT SYSTEM";
            lblTitle.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Height = 55;

            string welcomeText = AuthService.CurrentUser != null
                ? $"Welcome, {AuthService.CurrentUser.FullName} ({AuthService.CurrentUser.Role})"
                : "Welcome!";

            lblWelcome = new Label();
            lblWelcome.Text = welcomeText;
            lblWelcome.Font = new Font("Segoe UI", 11);
            lblWelcome.ForeColor = Color.FromArgb(220, 220, 220);
            lblWelcome.TextAlign = ContentAlignment.MiddleCenter;
            lblWelcome.Dock = DockStyle.Bottom;
            lblWelcome.Height = 30;

            headerPanel.Controls.Add(lblTitle);
            headerPanel.Controls.Add(lblWelcome);

            // ================= MAIN PANEL =================
            mainPanel = new Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.BackColor = Color.FromArgb(245, 247, 250);
            mainPanel.AutoScroll = true;

            // USER MANAGEMENT
            btnUserManagement = CreateModernButton(
                "👥 USER MANAGEMENT",
                Color.FromArgb(52, 152, 219),
                280,
                100
            );
            btnUserManagement.Click += BtnUserManagement_Click;

            // DEPOSIT / WITHDRAW
            btnDepositWithdraw = CreateModernButton(
                "💵 DEPOSIT / WITHDRAW",
                Color.FromArgb(46, 204, 113),
                280,
                100
            );
            btnDepositWithdraw.Click += (s, e) =>
            {
                new DepositWithdrawalForm().ShowDialog();
            };

            // LOAN APPLICATION
            btnLoanApplication = CreateModernButton(
                "💰 LOAN APPLICATION",
                Color.FromArgb(155, 89, 182),
                280,
                100
            );
            btnLoanApplication.Click += (s, e) =>
            {
                new LoanApplicationForm().ShowDialog();
            };

            // BILL PAYMENT
            btnBillPayment = CreateModernButton(
                "🧾 BILL PAYMENT",
                Color.FromArgb(241, 196, 15),
                280,
                100
            );

            btnBillPayment.ForeColor = Color.FromArgb(44, 62, 80);

            btnBillPayment.Click += (s, e) =>
            {
                new BillPaymentForm().ShowDialog();
            };

            // REPORTS BUTTON
            btnReports = CreateModernButton(
                "📊 GENERATE REPORTS",
                Color.FromArgb(52, 73, 94),
                320,
                65
            );

            btnReports.Font = new Font("Segoe UI", 12, FontStyle.Bold);

            btnReports.Click += (s, e) =>
            {
                new ReportModuleForm().ShowDialog();
            };

            // Add buttons to main panel
            mainPanel.Controls.Add(btnUserManagement);
            mainPanel.Controls.Add(btnDepositWithdraw);
            mainPanel.Controls.Add(btnLoanApplication);
            mainPanel.Controls.Add(btnBillPayment);
            mainPanel.Controls.Add(btnReports);

            // ================= FOOTER =================
            footerPanel = new Panel();
            footerPanel.Dock = DockStyle.Bottom;
            footerPanel.Height = 65;
            footerPanel.BackColor = Color.FromArgb(236, 240, 241);

            // PASSWORD RECOVERY
            btnPasswordRecovery = new Button();
            btnPasswordRecovery.Text = "🔐 PASSWORD RECOVERY";
            btnPasswordRecovery.Size = new Size(190, 40);
            btnPasswordRecovery.BackColor = Color.FromArgb(52, 152, 219);
            btnPasswordRecovery.ForeColor = Color.White;
            btnPasswordRecovery.FlatStyle = FlatStyle.Flat;
            btnPasswordRecovery.FlatAppearance.BorderSize = 0;
            btnPasswordRecovery.Cursor = Cursors.Hand;

            btnPasswordRecovery.Click += (s, e) =>
            {
                new PasswordRecoveryForm().ShowDialog();
            };

            // ABOUT BUTTON
            btnAbout = new Button();
            btnAbout.Text = "ℹ️ ABOUT";
            btnAbout.Size = new Size(120, 40);
            btnAbout.BackColor = Color.FromArgb(149, 165, 166);
            btnAbout.ForeColor = Color.White;
            btnAbout.FlatStyle = FlatStyle.Flat;
            btnAbout.FlatAppearance.BorderSize = 0;
            btnAbout.Cursor = Cursors.Hand;

            btnAbout.Click += (s, e) =>
            {
                new AboutForm().ShowDialog();
            };

            // LOGOUT BUTTON
            btnLogout = new Button();
            btnLogout.Text = "🚪 LOGOUT";
            btnLogout.Size = new Size(120, 40);
            btnLogout.BackColor = Color.FromArgb(231, 76, 60);
            btnLogout.ForeColor = Color.White;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Cursor = Cursors.Hand;

            btnLogout.Click += BtnLogout_Click;

            footerPanel.Controls.Add(btnPasswordRecovery);
            footerPanel.Controls.Add(btnAbout);
            footerPanel.Controls.Add(btnLogout);

            // ================= FORM CONTROLS =================
            this.Controls.Add(mainPanel);
            this.Controls.Add(footerPanel);
            this.Controls.Add(headerPanel);

            // Arrange controls properly
            ArrangeButtons();

            // Resize events
            this.Resize += (s, e) =>
            {
                ArrangeButtons();
                ArrangeFooterButtons();
            };

            footerPanel.Resize += (s, e) =>
            {
                ArrangeFooterButtons();
            };
        }

        // ================= BUTTON LAYOUT =================
        private void ArrangeButtons()
        {
            int btnWidth = 280;
            int btnHeight = 100;
            int spacing = 40;

            int totalWidth = (2 * btnWidth) + spacing;

            int startX = Math.Max(
                20,
                (mainPanel.ClientSize.Width - totalWidth) / 2
            );

            int row1Y = 40;
            int row2Y = row1Y + btnHeight + spacing;

            // Row 1
            btnUserManagement.Location = new Point(startX, row1Y);

            btnDepositWithdraw.Location = new Point(
                startX + btnWidth + spacing,
                row1Y
            );

            // Row 2
            btnLoanApplication.Location = new Point(startX, row2Y);

            btnBillPayment.Location = new Point(
                startX + btnWidth + spacing,
                row2Y
            );

            // Reports button
            btnReports.Location = new Point(
                (mainPanel.ClientSize.Width - btnReports.Width) / 2,
                row2Y + btnHeight + 50
            );
        }

        // ================= FOOTER LAYOUT =================
        private void ArrangeFooterButtons()
        {
            btnPasswordRecovery.Location = new Point(20, 12);

            btnAbout.Location = new Point(
                (footerPanel.Width - btnAbout.Width) / 2,
                12
            );

            btnLogout.Location = new Point(
                footerPanel.Width - btnLogout.Width - 20,
                12
            );
        }

        // ================= MODERN BUTTON CREATOR =================
        private Button CreateModernButton(
            string text,
            Color bgColor,
            int width,
            int height)
        {
            Button btn = new Button();

            btn.Text = text;
            btn.Size = new Size(width, height);

            btn.BackColor = bgColor;
            btn.ForeColor = Color.White;

            btn.Font = new Font(
                "Segoe UI",
                11,
                FontStyle.Bold
            );

            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;

            btn.Cursor = Cursors.Hand;

            return btn;
        }

        // ================= EVENT HANDLERS =================
        private void BtnUserManagement_Click(object sender, EventArgs e)
        {
            if (AuthService.IsAdmin)
            {
                UserManagementForm userForm = new UserManagementForm();
                userForm.ShowDialog();
            }
            else
            {
                MessageBox.Show(
                    "Access Denied! Admin only.",
                    "Unauthorized",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            AuthService.Logout();

            MessageBox.Show(
                "Logged out successfully!",
                "Info",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            LoginForm login = new LoginForm();
            login.Show();

            this.Close();
        }
    }
}