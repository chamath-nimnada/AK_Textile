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
    public partial class EmpManagerDepartment : Form
    {
        private MainForm mainForm;
        public EmpManagerDepartment(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form formBackground = new Form();
            try
            {
                using (EmpManagerDepartmentAdd empManagerDepartmentAdd = new EmpManagerDepartmentAdd())
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

                    empManagerDepartmentAdd.Owner = formBackground;
                    empManagerDepartmentAdd.ShowDialog();

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
            Form formBackground = new Form();
            try
            {
                using (EmpManagerDepartmentUpdate empManagerDepartmentUpdate = new EmpManagerDepartmentUpdate())
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

                    empManagerDepartmentUpdate.Owner = formBackground;
                    empManagerDepartmentUpdate.ShowDialog();

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

        private void button5_Click(object sender, EventArgs e)
        {
            Form formBackground = new Form();
            try
            {
                using (EmpManagerDepartmentRemove empManagerDepartmentRemove = new EmpManagerDepartmentRemove())
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

                    empManagerDepartmentRemove.Owner = formBackground;
                    empManagerDepartmentRemove.ShowDialog();

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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
