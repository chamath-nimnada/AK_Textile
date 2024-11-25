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
    public partial class CEODashboard : Form
    {
        private MainForm mainForm; //Step 01
        public CEODashboard(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void CEODashboard_Load(object sender, EventArgs e)
        {

        }
    }
}
