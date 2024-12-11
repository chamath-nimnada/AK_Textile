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
        public MainForm mainForm; //Step 01
        public ProductionProduct(MainForm mainForm/*Step 02*/)
        {
            InitializeComponent();
            this.mainForm = mainForm; //Step 03
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ProductProductAdd prodadd = new ProductProductAdd();
            prodadd.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ProductProductUpdate produpdate = new ProductProductUpdate();
            produpdate.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ProductProductRemove prodremove = new ProductProductRemove();
            prodremove.ShowDialog();
        }
    }
}
