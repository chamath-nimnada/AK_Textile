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
    public partial class Employee : Form
    {
        private MainForm mainForm;
        public Employee(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            SalaryId.Text=string.Empty;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
           mainForm.LoadForm(new LoginForm(mainForm));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form formBackground = new Form();
            try
            {
                using (EmployeeLeaveAdd emplpoyeeleaveadd = new EmployeeLeaveAdd())
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

                    emplpoyeeleaveadd.Owner = formBackground;
                    emplpoyeeleaveadd.ShowDialog();

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
                using (EmployeeLeaveUpdate employeeleaveupdate = new EmployeeLeaveUpdate())
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

                    employeeleaveupdate.Owner = formBackground;
                    employeeleaveupdate.ShowDialog();

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

        private void button9_Click(object sender, EventArgs e)
        {
            Form formBackground = new Form();
            try
            {
                using (EmployeeLeaveRemove employeeleaveremove = new EmployeeLeaveRemove())
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

                    employeeleaveremove.Owner = formBackground;
                    employeeleaveremove.ShowDialog();

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

        private void button10_Click(object sender, EventArgs e)
        {
                /*string salaryId = txtSalaryId.Text;

                if (!string.IsNullOrEmpty(salaryId))
                {
                    // Example: Replace this with actual search logic (e.g., database query)
                    rtbResult.Text = $"Searching for Salary ID: {salaryId}\nResult: [Sample Data]";
                }
                else
                {
                    rtbResult.Text = "Please enter a Salary ID.";
                }*/
        }

    }
}
