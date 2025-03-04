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

namespace AK_Textile
{
    public partial class InventoryReport : Form
    {
        private MainForm mainForm;
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
            
        }
    }
}