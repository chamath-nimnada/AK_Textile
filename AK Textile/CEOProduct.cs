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
    public partial class CEOProduct : Form
    {
        private MainForm mainForm; //Step 01
        public CEOProduct(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void CEOProduct_Load(object sender, EventArgs e)
        {

        }
    }
}
