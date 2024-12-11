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
    public partial class ProductOrder : Form
    {
        public MainForm mainForm; //Step 01
        public ProductOrder(MainForm mainForm/*Step 02*/)
        {
            InitializeComponent();
            this.mainForm = mainForm; //Step 03
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            this.textBox1.Clear();
        }

        private void shedulebtn_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new ProductSchedule(mainForm));
        }

        private void productionbtn_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new ProductProduction(mainForm));
        }

        private void rmbtn_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new ProductRaw(mainForm));
        }

        private void productbtn_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new ProductionProduct(mainForm));
        }

        private void reportbtn_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new ProductReport(mainForm));
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new ProductDashboard(mainForm));
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new LoginForm(mainForm));
        }
    }
}
