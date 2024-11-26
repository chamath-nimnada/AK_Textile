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
    public partial class ProductProduction : Form
    {
        private MainForm mainForm;
        public ProductProduction(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void ProductProduction_Load(object sender, EventArgs e)
        {

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
            mainForm.LoadForm(new ProductDashboard(mainForm));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new ProductSchedule(mainForm));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new ProductRaw(mainForm));
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new ProductOrder(mainForm));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new ProductReport(mainForm));
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            productidtxtbox.Text = string.Empty;
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            ProductProductionAdd productproductionadd = new ProductProductionAdd();
            productproductionadd.ShowDialog();
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            ProductProductionUpdate productproductionupdate = new ProductProductionUpdate();
            productproductionupdate.ShowDialog();
        }

        private void removebtn_Click(object sender, EventArgs e)
        {
            ProductProductionRemove productproductionremove = new ProductProductionRemove();
            productproductionremove.ShowDialog();
        }
    }
}
