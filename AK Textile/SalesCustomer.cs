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
    public partial class SalesCustomer : Form
    {
        private MainForm mainForm;
        public SalesCustomer(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
           mainForm.LoadForm(new SalesDashboard(mainForm));
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new LoginForm(mainForm));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SalesOrder(mainForm));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SalesSales(mainForm));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SalesPayment(mainForm));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SalesSchedule(mainForm));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SalesBill(mainForm));
        }

        private void button13_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SalesReport(mainForm));
        }
    }
}
