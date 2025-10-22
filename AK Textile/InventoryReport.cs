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

        private void button10_Click(object sender, EventArgs e)
        {
            // Check if a report type is selected
            if (comboBoxReport.SelectedItem == null)
            {
                MessageBox.Show("Please select a report type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Load the appropriate report form based on the selected item
            switch (comboBoxReport.SelectedItem.ToString())
            {
                case "Weekly":
                    LoadForm(new InventoryWeeklyReport());
                    break;
                case "Monthly":
                    LoadForm(new InventoryMonthlyReport());
                    break;
                case "Yearly":
                    LoadForm(new InventoryYearlyReport());
                    break;
                default:
                    MessageBox.Show("Invalid report type selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
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