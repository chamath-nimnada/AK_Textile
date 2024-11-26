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
    public partial class SupplierInvoice : Form
    {
        private MainForm mainForm;
        public SupplierInvoice(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void SupplierInvoice_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

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

        private void button9_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SupplierReport(mainForm));
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new LoginForm(mainForm));
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            mainForm.LoadForm(new LoginForm(mainForm));
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SupplierDashboard(mainForm));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            supidtxt.Text = string.Empty;
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            SupplierInvoiceAdd supplierinvoiceadd = new SupplierInvoiceAdd();
            supplierinvoiceadd.ShowDialog();
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            SupplierInvoiceUpdate supplierinvoiceupdate = new SupplierInvoiceUpdate();
            supplierinvoiceupdate.ShowDialog();
        }
    }
}
