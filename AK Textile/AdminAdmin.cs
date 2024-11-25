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

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            // Create and configure the overlay form
            Form overlay = new Form
            {
                BackColor = Color.Black,
                Opacity = 0.5, // 50% transparent
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.Manual,
                ShowInTaskbar = false,
                Bounds = this.Bounds, // Match the size and position of the MainForm
                Owner = this // Set the MainForm as the owner
            };

            // Show the overlay form
            overlay.Show();

            // Create and show the subform as a dialog
            using (AdminAdminRemove adminAdminRemove = new AdminAdminRemove())
            {
                adminAdminRemove.ShowDialog();
            }

            // Close the overlay form after the subform is closed
            overlay.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            AdminAdminUpdate adminadminupdate = new AdminAdminUpdate();
            adminadminupdate.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AdminAdminAdd adminadminadd = new AdminAdminAdd();
            adminadminadd.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            input.Text = string.Empty; //Clear the text box
        }
    }
}
