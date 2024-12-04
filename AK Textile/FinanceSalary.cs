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
    public partial class FinanceSalary : Form
    {
        private MainForm mainForm;
        public FinanceSalary(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new FinanceDashboard(mainForm));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new FinanceOrder(mainForm));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new FinanceSupplierPayment(mainForm));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new FinanceSale(mainForm));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new FinanceCustomerPayment(mainForm));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new FinanceReport(mainForm));
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            mainForm.LoadForm(new LoginForm(mainForm));
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new LoginForm(mainForm));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            EmployeeId.Text=string.Empty;
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
