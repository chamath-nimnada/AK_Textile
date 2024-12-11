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
    public partial class SupplierOrder : Form
    {
        private MainForm mainForm;
        public SupplierOrder(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            this.textBox1.Clear();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new LoginForm(mainForm));
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SupplierDashboard(mainForm));
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
            mainForm.LoadForm(new SupplierInvoice(mainForm));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SupplierReport(mainForm));
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            SupplierOrderAdd orderadd = new SupplierOrderAdd();
            orderadd.ShowDialog();
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            SupplierOrderUpdate orderupdate = new SupplierOrderUpdate();
            orderupdate.ShowDialog();
        }
    }
}
