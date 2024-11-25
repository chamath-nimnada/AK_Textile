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
    public partial class AdminDashboard : Form
    {
        private MainForm mainForm; //Step 01
        public AdminDashboard(MainForm mainForm/*Step 02*/)
        {
            InitializeComponent();
            this.mainForm = mainForm; //Step 03
        }

        private void AdminDashboard_Load(object sender, EventArgs e)
        {

        }
    }
}
