using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace AK_Textile
{
    public partial class SupplierReport : Form
    {
        //database connection
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");

        private MainForm mainForm;
        public SupplierReport(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            // Populate ComboBox with options
            comboBox1.Items.Add("Weekly");
            comboBox1.Items.Add("Monthly");
            comboBox1.Items.Add("Yearly");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SupplierDashboard(mainForm));
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new LoginForm(mainForm));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SupplierSupplier(mainForm));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SupplierPayment(mainForm));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SupplierOrder(mainForm));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SupplierInvoice(mainForm));
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a report type!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {

                // Get Date Range Based on Selection
                string reportType = comboBox1.SelectedItem.ToString();
                DateTime endDate = DateTime.Today;
                DateTime startDate;

                if (reportType == "Weekly")
                    startDate = endDate.AddDays(-7);
                else if (reportType == "Monthly")
                    startDate = endDate.AddMonths(-1);
                else // Yearly
                    startDate = endDate.AddYears(-1);


                SqlDataAdapter adapter = new SqlDataAdapter("SELECT spo.SPOrderID, spo.PitemName, spo.PItemQty, s.SupName, spo.OrderDate" +
                    "FROM SupplierPurchaseOrder spo" +
                    "INNER JOIN Supplier s ON spo.SupID = s.SupID" +
                    "WHERE spo.OrderDate >= @StartDate AND spo.OrderDate <= @EndDate;", con);

                adapter.SelectCommand.Parameters.AddWithValue("@StartDate", startDate);
                adapter.SelectCommand.Parameters.AddWithValue("@EndDate", endDate);

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No data found for the selected report type.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Load Crystal Report
                ReportDocument report = new ReportDocument();
                string reportPath = Application.StartupPath + @"\SupplierPurchaseReport.rpt";

                if (!System.IO.File.Exists(reportPath))
                {
                    MessageBox.Show("Report file not found: " + reportPath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                report.Load(reportPath);
                report.SetDataSource(dt);

                // Assign report to CrystalReportViewer
                //crystalReportViewer1.ReportSource = report;
                //crystalReportViewer1.Refresh();
            }
            
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Report Generation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
