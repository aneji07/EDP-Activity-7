using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Drawing.Chart;
using BankManagementSystem.Services;

namespace BankManagementSystem.Forms
{
    public partial class ReportModuleForm : Form
    {
        private ComboBox cboReportType;
        private DataGridView dgvReport;
        private Button btnLoad;
        private Button btnExport;

        public ReportModuleForm()
        {
            InitializeComponent();
            LoadReportData();
        }

        private void InitializeComponent()
        {
            this.Text = "Report Module";
            this.Size = new Size(1000, 650);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(245, 247, 250);

            // ===== TITLE =====
            Label lblTitle = new Label();
            lblTitle.Text = "Generate Excel Reports";
            lblTitle.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitle.Location = new Point(20, 20);
            lblTitle.Size = new Size(400, 35);
            this.Controls.Add(lblTitle);

            // ===== REPORT LABEL =====
            Label lblType = new Label();
            lblType.Text = "Select Report:";
            lblType.Font = new Font("Segoe UI", 10);
            lblType.Location = new Point(30, 80);
            lblType.Size = new Size(120, 25);
            this.Controls.Add(lblType);

            // ===== COMBOBOX =====
            cboReportType = new ComboBox();
            cboReportType.DropDownStyle = ComboBoxStyle.DropDownList;
            cboReportType.Font = new Font("Segoe UI", 10);

            cboReportType.Items.AddRange(new object[]
            {
                "Bank Transactions",
                "Loan Applications",
                "Bill Payments"
            });

            cboReportType.SelectedIndex = 0;
            cboReportType.Location = new Point(150, 78);
            cboReportType.Size = new Size(250, 30);

            this.Controls.Add(cboReportType);

            // ===== LOAD BUTTON =====
            btnLoad = new Button();
            btnLoad.Text = "Load Report";
            btnLoad.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnLoad.BackColor = Color.FromArgb(52, 152, 219);
            btnLoad.ForeColor = Color.White;
            btnLoad.FlatStyle = FlatStyle.Flat;
            btnLoad.FlatAppearance.BorderSize = 0;
            btnLoad.Cursor = Cursors.Hand;
            btnLoad.Location = new Point(430, 76);
            btnLoad.Size = new Size(130, 35);

            btnLoad.Click += BtnLoad_Click;

            this.Controls.Add(btnLoad);

            // ===== EXPORT BUTTON =====
            btnExport = new Button();
            btnExport.Text = "📊 Export to Excel";
            btnExport.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnExport.BackColor = Color.FromArgb(46, 204, 113);
            btnExport.ForeColor = Color.White;
            btnExport.FlatStyle = FlatStyle.Flat;
            btnExport.FlatAppearance.BorderSize = 0;
            btnExport.Cursor = Cursors.Hand;
            btnExport.Location = new Point(780, 76);
            btnExport.Size = new Size(170, 35);

            btnExport.Click += BtnExport_Click;

            this.Controls.Add(btnExport);

            // ===== DATAGRIDVIEW =====
            dgvReport = new DataGridView();

            dgvReport.Location = new Point(20, 140);
            dgvReport.Size = new Size(940, 440);

            dgvReport.BackgroundColor = Color.White;
            dgvReport.BorderStyle = BorderStyle.None;

            dgvReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReport.ReadOnly = true;
            dgvReport.AllowUserToAddRows = false;

            dgvReport.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10, FontStyle.Bold);

            dgvReport.DefaultCellStyle.Font =
                new Font("Segoe UI", 10);

            dgvReport.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(245, 245, 245);

            this.Controls.Add(dgvReport);
        }

        // =====================================================
        // LOAD REPORT DATA
        // =====================================================
        private void BtnLoad_Click(object sender, EventArgs e)
        {
            LoadReportData();
        }

        private void LoadReportData()
        {
            string selected = cboReportType.SelectedItem.ToString();
            string query = "";

            switch (selected)
            {
                case "Bank Transactions":
                    query = @"
                        SELECT
                            transaction_id AS 'ID',
                            transaction_type AS 'Transaction Type',
                            amount AS 'Amount',
                            transaction_date AS 'Date',
                            description AS 'Description'
                        FROM bank_transactions
                        ORDER BY transaction_date DESC";
                    break;

                case "Loan Applications":
                    query = @"
                        SELECT
                            loan_id AS 'ID',
                            loan_amount AS 'Loan Amount',
                            loan_term AS 'Loan Term',
                            purpose AS 'Purpose',
                            status AS 'Status',
                            application_date AS 'Application Date'
                        FROM loan_applications
                        ORDER BY application_date DESC";
                    break;

                case "Bill Payments":
                    query = @"
                        SELECT
                            payment_id AS 'ID',
                            bill_type AS 'Bill Type',
                            amount AS 'Amount',
                            payment_date AS 'Payment Date',
                            reference_no AS 'Reference No'
                        FROM bill_payments
                        ORDER BY payment_date DESC";
                    break;
            }

            try
            {
                using (var reader = DatabaseManager.ExecuteReader(query))
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    dgvReport.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error loading report:\n{ex.Message}",
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =====================================================
        // EXPORT BUTTON
        // =====================================================
        private void BtnExport_Click(object sender, EventArgs e)
        {
            if (dgvReport.Rows.Count == 0)
            {
                MessageBox.Show(
                    "No data to export.",
                    "Export",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            SaveFileDialog saveDlg = new SaveFileDialog();

            saveDlg.Filter = "Excel Files|*.xlsx";

            saveDlg.FileName =
                $"{cboReportType.SelectedItem}_Report_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

            if (saveDlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    GenerateExcelReport(saveDlg.FileName);

                    MessageBox.Show(
                        "Excel report exported successfully!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Export failed:\n{ex.Message}",
                        "Export Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }

        // =====================================================
        // GENERATE EXCEL REPORT
        // =====================================================
        private void GenerateExcelReport(string filePath)
        {
            // EPPlus 8 License
            ExcelPackage.License.SetNonCommercialPersonal("EDP6");

            using (ExcelPackage package = new ExcelPackage())
            {
                // =====================================================
                // SHEET 1 - REPORT DATA
                // =====================================================
                var wsData =
                    package.Workbook.Worksheets.Add("Report Data");

                // ===== HEADER STYLING =====
                wsData.Row(1).Height = 45;
                wsData.Row(2).Height = 25;
                wsData.Row(3).Height = 15;

                wsData.Column(1).Width = 18;
                wsData.Column(2).Width = 25;
                wsData.Column(3).Width = 20;

                wsData.Cells["B1:F1"].Merge = true;
                wsData.Cells["B2:F2"].Merge = true;

                // ===== LOGO =====
                string logoPath =
                    Path.Combine(
                        Application.StartupPath,
                        "Resources",
                        "logo.png"
                    );

                if (File.Exists(logoPath))
                {
                    var logo =
                        wsData.Drawings.AddPicture(
                            "CompanyLogo",
                            new FileInfo(logoPath)
                        );

                    logo.SetPosition(0, 5, 0, 5);
                    logo.SetSize(55, 55);
                }

                // ===== TITLE =====
                wsData.Cells["B1"].Value =
                    "BANK MANAGEMENT SYSTEM";

                wsData.Cells["B1"].Style.Font.Size = 20;
                wsData.Cells["B1"].Style.Font.Bold = true;

                wsData.Cells["B1"].Style.HorizontalAlignment =
                    ExcelHorizontalAlignment.Left;

                wsData.Cells["B1"].Style.VerticalAlignment =
                    ExcelVerticalAlignment.Center;

                wsData.Cells["B2"].Value =
                    "OFFICIAL TRANSACTION REPORT";

                wsData.Cells["B2"].Style.Font.Size = 12;
                wsData.Cells["B2"].Style.Font.Italic = true;

                wsData.Cells["B2"].Style.Font.Color.SetColor(
                    Color.DarkSlateGray
                );

                // ===== BORDER =====
                using (var range = wsData.Cells["A3:F3"])
                {
                    range.Style.Border.Bottom.Style =
                        ExcelBorderStyle.Thick;
                }

                // ===== REPORT INFO =====
                wsData.Cells["A5"].Value =
                    $"Report Type: {cboReportType.SelectedItem}";

                wsData.Cells["A5"].Style.Font.Bold = true;

                wsData.Cells["A6"].Value =
                    $"Generated On: {DateTime.Now:MMMM dd, yyyy hh:mm tt}";

                // =====================================================
                // TABLE
                // =====================================================
                int startRow = 8;

                // COLUMN HEADERS
                for (int col = 0; col < dgvReport.Columns.Count; col++)
                {
                    wsData.Cells[startRow, col + 1].Value =
                        dgvReport.Columns[col].HeaderText;

                    wsData.Cells[startRow, col + 1].Style.Font.Bold = true;

                    wsData.Cells[startRow, col + 1]
                        .Style.Fill.PatternType =
                        ExcelFillStyle.Solid;

                    wsData.Cells[startRow, col + 1]
                        .Style.Fill.BackgroundColor
                        .SetColor(Color.FromArgb(52, 73, 94));

                    wsData.Cells[startRow, col + 1]
                        .Style.Font.Color
                        .SetColor(Color.White);
                }

                // DATA ROWS
                for (int row = 0; row < dgvReport.Rows.Count; row++)
                {
                    for (int col = 0; col < dgvReport.Columns.Count; col++)
                    {
                        wsData.Cells[startRow + row + 1, col + 1].Value =
                            dgvReport.Rows[row].Cells[col].Value;
                    }
                }

                // AUTO FIT
                wsData.Cells.AutoFitColumns();

                // =====================================================
                // SIGNATURE AREA
                // =====================================================
                int signatureRow =
                    startRow + dgvReport.Rows.Count + 4;

                wsData.Cells[$"A{signatureRow}"].Value =
                    "Prepared By:";

                wsData.Cells[$"A{signatureRow}"].Style.Font.Bold = true;

                wsData.Cells[$"B{signatureRow}"].Value =
                    AuthService.CurrentUser?.FullName ?? "System User";

                wsData.Cells[$"A{signatureRow + 2}"].Value =
                    "Signature:";

                wsData.Cells[$"A{signatureRow + 2}"].Style.Font.Bold = true;

                wsData.Cells[$"B{signatureRow + 2}"].Value =
                    "____________________________";

                // =====================================================
                // SHEET 2 - CHART
                // =====================================================
                var wsChart =
                    package.Workbook.Worksheets.Add("Chart");

                DataTable summary =
                    BuildChartData(
                        cboReportType.SelectedItem.ToString()
                    );

                if (summary.Rows.Count > 0)
                {
                    // HEADERS
                    wsChart.Cells[1, 1].Value = "Category";
                    wsChart.Cells[1, 2].Value = "Total Amount";

                    // DATA
                    for (int i = 0; i < summary.Rows.Count; i++)
                    {
                        wsChart.Cells[i + 2, 1].Value =
                            summary.Rows[i]["Category"];

                        wsChart.Cells[i + 2, 2].Value =
                            summary.Rows[i]["Total Amount"];
                    }

                    // CREATE CHART
                    var chart =
                        wsChart.Drawings.AddChart(
                            "SummaryChart",
                            eChartType.ColumnClustered
                        );

                    chart.Title.Text = "Transaction Summary";

                    chart.SetPosition(1, 0, 3, 0);

                    chart.SetSize(700, 450);

                    var series =
                        chart.Series.Add(
                            wsChart.Cells[
                                2,
                                2,
                                summary.Rows.Count + 1,
                                2
                            ],
                            wsChart.Cells[
                                2,
                                1,
                                summary.Rows.Count + 1,
                                1
                            ]
                        );

                    series.Header = "Amount";
                }

                // SAVE FILE
                package.SaveAs(new FileInfo(filePath));
            }
        }

        // =====================================================
        // BUILD CHART DATA
        // =====================================================
        private DataTable BuildChartData(string reportType)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Category", typeof(string));
            dt.Columns.Add("Total Amount", typeof(decimal));

            try
            {
                string query = "";

                if (reportType == "Bank Transactions")
                {
                    query = @"
                        SELECT
                            transaction_type AS Category,
                            SUM(amount) AS TotalAmount
                        FROM bank_transactions
                        GROUP BY transaction_type";
                }
                else if (reportType == "Loan Applications")
                {
                    query = @"
                        SELECT
                            status AS Category,
                            SUM(loan_amount) AS TotalAmount
                        FROM loan_applications
                        GROUP BY status";
                }
                else if (reportType == "Bill Payments")
                {
                    query = @"
                        SELECT
                            bill_type AS Category,
                            SUM(amount) AS TotalAmount
                        FROM bill_payments
                        GROUP BY bill_type";
                }

                using (var reader =
                    DatabaseManager.ExecuteReader(query))
                {
                    while (reader.Read())
                    {
                        dt.Rows.Add(
                            reader["Category"].ToString(),
                            Convert.ToDecimal(
                                reader["TotalAmount"]
                            )
                        );
                    }
                }
            }
            catch
            {
            }

            return dt;
        }
    }
}