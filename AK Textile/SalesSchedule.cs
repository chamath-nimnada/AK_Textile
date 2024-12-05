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
    public partial class SalesSchedule : Form
    {
        private MainForm mainForm;
        public SalesSchedule(MainForm mainForm)
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
            mainForm.LoadForm(new LoginForm (mainForm));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm .LoadForm (new SalesOrder(mainForm));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SalesSales(mainForm));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm (new SalesPayment(mainForm));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SalesCustomer(mainForm));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SalesBill(mainForm));
        }

        private void button10_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SalesReport(mainForm));
        }
    }
}
