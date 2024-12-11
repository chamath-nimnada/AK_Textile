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
    public partial class ProductSchedule : Form
    {
        private MainForm mainForm;
        public ProductSchedule(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ProductScheduleAdd schadd = new ProductScheduleAdd();
            schadd.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new ProductProduction(mainForm));
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

        private void updatebtn_Click(object sender, EventArgs e)
        {
            ProductScheduleUpdate schupd = new ProductScheduleUpdate();
            schupd.ShowDialog();
        }

        private void removebtn_Click(object sender, EventArgs e)
        {
            ProductProductionRemove schrem = new ProductProductionRemove();
            schrem.ShowDialog();
        }
    }
}