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
    public partial class AdminAdmin : Form
    {
        private MainForm mainForm; //Step 01
        public AdminAdmin(MainForm mainForm/*Step 02*/)
        {
            InitializeComponent();
            this.mainForm = mainForm; //Step 03
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Step 04
            //Load Employee Form in Main Panal
            mainForm.LoadForm(new AdminEmployee(mainForm));
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AdminAdmin_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new AdminDashboard(mainForm));
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new LoginForm(mainForm));
        }
    }
}
