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
    public partial class SupplierDashboard : Form
    {
        private MainForm mainForm;
        public SupplierDashboard(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
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

        private void button4_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SupplierOrder(mainForm));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SupplierInvoice(mainForm));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SupplierReport(mainForm));
        }
    }
}
