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
using System.IO;

namespace AK_Textile
{
    public partial class ProductReport : Form
    {
        private MainForm mainForm;

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");
        public ProductReport(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            // Populate ComboBox with options
            comboBox2.Items.Add("Weekly");
            comboBox2.Items.Add("Monthly");
            comboBox2.Items.Add("Yearly");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = -1;
            crystalReportViewer1.ReportSource = null;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new LoginForm(mainForm));
        }

        private void ProductReport_Load(object sender, EventArgs e)
        {
              
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new ProductDashboard(mainForm));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new ProductSchedule(mainForm));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //mainForm.LoadForm(new ProductProduction(mainForm));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new ProductRaw(mainForm));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new ProductionProduct(mainForm));
        }

        private void button8_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new ProductOrder(mainForm));
        }

        //generate button
        private void button10_Click(object sender, EventArgs e)
        {
            // Define Start and End Dates
            DateTime startDate;
            DateTime endDate = DateTime.Today; // We always end on today's date
            DateTime today = DateTime.Today;

            if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Please select a time duration.");
                return;
            }
            string reportType = comboBox2.SelectedItem.ToString();

            // Calculate Dates Based on ComboBox
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
                //This is the Data-Fetching Code
                string query = @"
            SELECT 
                PScheduleID, 
                PScheduleName, 
                ProdType, 
                ProdQty, 
                ProdStartDate, 
                ProdEndDate 
            FROM 
                ProductionSchedule
            WHERE 
                ProdStartDate >= @startDate AND ProdStartDate <= @endDate"; // Filter by ProdStartDate

                // Give the DataTable the SAME name as in the .xsd
                DataTable dt = new DataTable("ProductionReportData");

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }

                // Load the Crystal Report file ---
                ReportDocument cryRpt = new ReportDocument();
                string reportPath = Path.Combine(Application.StartupPath, "Reports\\ProductionReport.rpt");
                cryRpt.Load(reportPath);

                // Push the DataTable into the Report ---
                cryRpt.SetDataSource(dt);

                // Show the Report in the Viewer ---
                crystalReportViewer1.ReportSource = cryRpt;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading report: " + ex.Message);
            }
        }
    }
}
