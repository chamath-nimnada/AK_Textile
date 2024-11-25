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
    }
}
