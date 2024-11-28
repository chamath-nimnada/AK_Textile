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
    public partial class AdminEmployee : Form
    {
        private MainForm mainForm;

        public AdminEmployee(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new AdminAdmin(mainForm));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //Dark the back main window and open sub window
            Form formBackground = new Form();
            try
            {
                using (AdminEmployeeRemove adminEmployeeRemove = new AdminEmployeeRemove())
                {
                    formBackground.StartPosition = FormStartPosition.Manual;
                    formBackground.FormBorderStyle = FormBorderStyle.None;
                    formBackground.Opacity = .50d;
                    formBackground.BackColor = Color.Black;
                    formBackground.WindowState = FormWindowState.Maximized;
                    formBackground.TopMost = true;
                    formBackground.Location = this.Location;
                    formBackground.ShowInTaskbar = false;
                    formBackground.Show();

                    adminEmployeeRemove.Owner = formBackground;
                    adminEmployeeRemove.ShowDialog();

                    formBackground.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                formBackground.Dispose();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Dark the back main window and open sub window
            Form formBackground = new Form();
            try
            {
                using (AdminEmployeeUpdate adminEmployeeUpdate = new AdminEmployeeUpdate())
                {
                    formBackground.StartPosition = FormStartPosition.Manual;
                    formBackground.FormBorderStyle = FormBorderStyle.None;
                    formBackground.Opacity = .50d;
                    formBackground.BackColor = Color.Black;
                    formBackground.WindowState = FormWindowState.Maximized;
                    formBackground.TopMost = true;
                    formBackground.Location = this.Location;
                    formBackground.ShowInTaskbar = false;
                    formBackground.Show();

                    adminEmployeeUpdate.Owner = formBackground;
                    adminEmployeeUpdate.ShowDialog();

                    formBackground.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                formBackground.Dispose();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //Dark the back main window and open sub window
            Form formBackground = new Form();
            try
            {
                using (AdminEmployeeAdd adminEmployeeAdd = new AdminEmployeeAdd())
                {
                    formBackground.StartPosition = FormStartPosition.Manual;
                    formBackground.FormBorderStyle = FormBorderStyle.None;
                    formBackground.Opacity = .50d;
                    formBackground.BackColor = Color.Black;
                    formBackground.WindowState = FormWindowState.Maximized;
                    formBackground.TopMost = true;
                    formBackground.Location = this.Location;
                    formBackground.ShowInTaskbar = false;
                    formBackground.Show();

                    adminEmployeeAdd.Owner = formBackground;
                    adminEmployeeAdd.ShowDialog();

                    formBackground.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                formBackground.Dispose();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            input.Text = string.Empty; // Clear the text in the textbox
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new AdminDashboard(mainForm));
        }
    }
}
