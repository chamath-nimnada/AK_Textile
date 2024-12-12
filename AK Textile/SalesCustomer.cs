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
    public partial class SalesCustomer : Form
    {
        private MainForm mainForm;
        public SalesCustomer(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void SalesCustomer_Load(object sender, EventArgs e)
        {

        }
    }
}
