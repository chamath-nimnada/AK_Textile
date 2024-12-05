using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AK_Textile
{
    public partial class SalesSales : Form
    {
        private MainForm mainForm;
        public SalesSales(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm (new SalesDashboard(mainForm));
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm (new LoginForm (mainForm));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm .LoadForm (new SalesOrder(mainForm));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm (new SalesPayment(mainForm));
        }

        private void button8_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SalesCustomer(mainForm));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mainForm .LoadForm(new SalesSchedule(mainForm));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            mainForm .LoadForm(new SalesBill(mainForm));
        }

        private void button13_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SalesReport(mainForm));
        }
    }
}
