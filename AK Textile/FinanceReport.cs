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
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

namespace AK_Textile
{
    public partial class FinanceReport : Form
    {
        private MainForm mainForm;

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");
        public FinanceReport(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            // Populate ComboBox with options
            comboBox2.Items.Add("Weekly");
            comboBox2.Items.Add("Monthly");
            comboBox2.Items.Add("Yearly");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new FinanceSale(mainForm));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mainForm .LoadForm (new FinanceSalary(mainForm));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new FinanceOrder(mainForm));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new FinanceSupplierPayment(mainForm));
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            mainForm .LoadForm (new FinanceDashboard(mainForm));
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mainForm .LoadForm (new LoginForm (mainForm));
        }

        //generate button
        private void button10_Click(object sender, EventArgs e)
        {
            // Define Start and End Dates ---
            DateTime startDate;
            DateTime endDate = DateTime.Today; // We always end on today's date
            DateTime today = DateTime.Today;

            if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Please select a time duration.");
                return;
            }
            string reportType = comboBox2.SelectedItem.ToString();

            // --- 2. Calculate Dates Based on ComboBox ---
            switch (reportType)
            {
                case "Weekly":
                    startDate = today.AddDays(-7);
                    break;

                case "Monthly":
                    startDate = new DateTime(today.Year, today.Month, 1);
                    break;

                case "Yearly":
                    startDate = new DateTime(today.Year, 1, 1);
                    break;

                default:
                    MessageBox.Show("Please select a valid duration.");
                    return;
            }

            try
            {
                // This is the Data-Fetching Code 
                string query = @"
            SELECT 
                pay.SupPID, 
                sup.SupName, 
                pay.SupPDate, 
                pay.SupPAmount 
            FROM 
                SupplierPayment AS pay
            INNER JOIN 
                Supplier AS sup ON pay.SupID = sup.SupID
            WHERE 
                pay.SupPDate >= @startDate AND pay.SupPDate <= @endDate";

                // Give the DataTable the SAME name as in your .xsd
                DataTable dt = new DataTable("SupplierPayments");

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
 
                ReportDocument cryRpt = new ReportDocument();
                string reportPath = Path.Combine(Application.StartupPath, "Reports\\SupplierPaymentReport.rpt");
                cryRpt.Load(reportPath);

                // Push the DataTable into the Report ---
                cryRpt.SetDataSource(dt);

                //  Show the Report in the Viewer ---
                crystalReportViewer1.ReportSource = cryRpt;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading report: " + ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            crystalReportViewer1.ReportSource = null;
            comboBox2.SelectedIndex = -1;
        }
    }
}
