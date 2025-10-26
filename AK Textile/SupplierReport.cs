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
using System.IO;

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
            crystalReportViewer1.ReportSource = null;

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

        //Generate button
        private void button10_Click(object sender, EventArgs e)
        {
            // --- 1. Define Start and End Dates ---
            DateTime startDate;
            DateTime endDate = DateTime.Today; // We always end on today's date
            DateTime today = DateTime.Today;

            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a time duration.");
                return;
            }
            string reportType = comboBox1.SelectedItem.ToString();

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
                // --- 3. This is the Data-Fetching Code ---
                string query = @"
            SELECT 
                spo.SPOrderID,
                s.SupName,
                po.PItemName,
                po.PItemQty,
                spo.IssuedDate
            FROM 
                SupplierPurchaseOrder AS spo
            INNER JOIN 
                Supplier AS s ON spo.SupID = s.SupID
            INNER JOIN
                PurchaseOrder AS po ON spo.POrderID = po.POrderID
            WHERE 
                spo.IssuedDate >= @startDate AND spo.IssuedDate <= @endDate"; // Filter by IssuedDate

                // Give the DataTable the SAME name as in the .xsd
                DataTable dt = new DataTable("SupplierPurchaseOrderData");

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }

                // --- 4. Load the Crystal Report file ---
                ReportDocument cryRpt = new ReportDocument();
                string reportPath = Path.Combine(Application.StartupPath, "Reports\\SupplierPurchaseOrderReport.rpt");
                cryRpt.Load(reportPath);

                // --- 5. Push the DataTable into the Report ---
                cryRpt.SetDataSource(dt);

                // --- 6. Show the Report in the Viewer ---
                crystalReportViewer1.ReportSource = cryRpt;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading report: " + ex.Message);
            }
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void SupplierReport_Load(object sender, EventArgs e)
        {

        }
    }
}
