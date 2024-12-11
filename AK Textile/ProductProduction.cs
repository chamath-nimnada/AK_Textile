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
            mainForm.LoadForm(new ProductionProduct(mainForm));
        }

        private void button8_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new ProductOrder(mainForm));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new ProductReport(mainForm));
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            ProductProductionAdd productionadd = new ProductProductionAdd();
            productionadd.ShowDialog();
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            ProductProductionUpdate productionupdate = new ProductProductionUpdate();
            productionupdate.ShowDialog();
        }

        private void removebtn_Click(object sender, EventArgs e)
        {
            ProductProductionRemove productionremove = new ProductProductionRemove();
            productionremove.ShowDialog();
        }
    }
}
