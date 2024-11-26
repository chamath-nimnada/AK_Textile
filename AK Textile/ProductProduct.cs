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
    public partial class ProductionProduct : Form
    {
        private MainForm mainForm;
        public ProductionProduct(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void ProductionProduct_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            mainForm.LoadForm(new LoginForm(mainForm));
        }

        private void pictureBox2_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new ProductProduction(mainForm));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new ProductRaw(mainForm));
        }

        private void button8_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new ProductOrder(mainForm));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new ProductReport(mainForm));
        }

        private void clearbutton_Click(object sender, EventArgs e)
        {
            productIDtxtbox.Text = string.Empty; // to clear the textbox
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            ProductProductUpdate productproductupdate = new ProductProductUpdate();
            productproductupdate.ShowDialog();
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            ProductProductAdd productproductadd = new ProductProductAdd();
            productproductadd.ShowDialog();
        }

        private void removebtn_Click(object sender, EventArgs e)
        {
            ProductProductRemove productproductremove = new ProductProductRemove();
            productproductremove.ShowDialog();
        }
    }
}
