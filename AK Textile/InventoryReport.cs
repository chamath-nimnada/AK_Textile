using AK_Textile.Reports;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AK_Textile
{
    public partial class InventoryReport : Form
    {
        private MainForm mainForm;

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");
        public InventoryReport(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;

            comboBoxReport.Items.Add("Weekly");
            comboBoxReport.Items.Add("Monthly");
            comboBoxReport.Items.Add("Yearly");
        }

        public void LoadForm(Form form)
        {
            ReportPanal.Controls.Clear();
            form.TopLevel = false;
            ReportPanal.Controls.Add(form);
            form.Show();
        }

        private void InventoryReport_Load(object sender, EventArgs e)
        {
           
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new InventoryDashboard(mainForm));
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new LoginForm(mainForm));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new InventoryProduct(mainForm));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new InventoryCategory(mainForm));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new InventorySupplier(mainForm));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new InventoryInventory(mainForm));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new InventoryGRN(mainForm));
        }

        //Generate button codes
        private void button10_Click(object sender, EventArgs e)
        {
            // Define Start and End Dates
            DateTime startDate;
            DateTime endDate = DateTime.Today;
            DateTime today = DateTime.Today;

            if (comboBoxReport.SelectedItem == null)
            {
                MessageBox.Show("Please select a time duration.");
                return;
            }
            string reportType = comboBoxReport.SelectedItem.ToString();

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
                string query = @"
            SELECT 
                inv.InvID, 
                cat.InvCategory, 
                inv.InvItemName, 
                inv.InvQty, 
                inv.DateAdded, 
                inv.InvStockLevel 
            FROM 
                Inventory AS inv
            INNER JOIN 
                InventoryCategory AS cat ON inv.InvCatID = cat.InvCatID
            WHERE 
                inv.DateAdded >= @startDate AND inv.DateAdded <= @endDate";

                DataTable dt = new DataTable("InventoryReportData");

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }

                // Load the Crystal Report file
                ReportDocument cryRpt = new ReportDocument();
                string reportPath = Path.Combine(Application.StartupPath, "Reports\\InventoryReport.rpt");
                cryRpt.Load(reportPath);

                //  Push the DataTable into the Report
                cryRpt.SetDataSource(dt);

                //  Show the Report in the Viewer 
                crystalReportViewer1.ReportSource = cryRpt;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading report: " + ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Clear the ReportPanal controls
            ReportPanal.Controls.Clear();

            // Clear the comboBoxReport items
            comboBoxReport.Items.Clear();
        }
    }
}