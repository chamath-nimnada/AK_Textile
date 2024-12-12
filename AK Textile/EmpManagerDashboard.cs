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
    public partial class EmpManagerDashboard : Form
    {
        private MainForm mainForm; //Step 01
        public EmpManagerDashboard(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void EmpManagerDashboard_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new EmpManagerDepartment(mainForm));
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
