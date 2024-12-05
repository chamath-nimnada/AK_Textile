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
    public partial class SalesOrder : Form
    {
        private MainForm mainForm;
        public SalesOrder(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SalesSales(mainForm));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SalesPayment(mainForm));
        }

        private void button8_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SalesCustomer(mainForm));
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

        private void button10_Click(object sender, EventArgs e)
        {
            orderId.Text=string.Empty;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            SalesOrderUpdate salesorderupdate=new SalesOrderUpdate();
           salesorderupdate.ShowDialog();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            SalesOrderAdd salesorderadd=new SalesOrderAdd();
            salesorderadd.ShowDialog();
           
        }

        private void button12_Click(object sender, EventArgs e)
        {
            SalesOrderRemove salesorderremove=new SalesOrderRemove();
            salesorderremove.ShowDialog();
        }
    }
}
