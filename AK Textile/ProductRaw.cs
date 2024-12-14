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

    public partial class ProductRaw : Form
    {
        private MainForm mainForm;
        public ProductRaw(MainForm mainForm)
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

        private void button2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new ProductProduction(mainForm));
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
    }
}
