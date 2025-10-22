using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AK_Textile
{
    public partial class EmpManagerLeave : Form
    {
        private MainForm mainForm;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");

        private int selectedLeaveId = 0;


        public EmpManagerLeave(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
           
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new EmpManagerReport(mainForm));
        }

        private void EmpManagerLeave_Load(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form formBackground = new Form();
            try
            {
                using (EmpManagerLeaveAdd empManagerLeaveAdd = new EmpManagerLeaveAdd())
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

                    empManagerLeaveAdd.Owner = formBackground;
                    empManagerLeaveAdd.ShowDialog();

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
                using (EmpManagerLeaveUpdate empManagerLeaveUpdate = new EmpManagerLeaveUpdate())
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

                    empManagerLeaveUpdate.Owner = formBackground;
                    empManagerLeaveUpdate.ShowDialog();

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
                using (EmpManagerLeaveRemove empManagerLeaveRemove = new EmpManagerLeaveRemove())
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

                    empManagerLeaveRemove.Owner = formBackground;
                    empManagerLeaveRemove.ShowDialog();

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

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LoadPendingLeaves()
        {
            try
            {
                {
                    con.Open();
                    string query = "SELECT LeaveID, EmpName, LeaveTypeID, LStartDate, LEndDate, Status FROM Leave WHERE Status = 'Pending'";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading pending leaves: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }


        private void button10_Click(object sender, EventArgs e)
        {
            if (selectedLeaveId > 0)
            {
                try
                {
                        con.Open();
                        string query2 = "UPDATE Leave SET Status = 'Approved' WHERE LeaveID = @LeaveID";
                        SqlCommand command = new SqlCommand(query2, con);
                        command.Parameters.AddWithValue("@LeaveID", selectedLeaveId);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Leave Approved Successfully!");

                        // Refresh Pending Leaves Grid
                        LoadPendingLeaves();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("Please select a leave to approve.");
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (selectedLeaveId > 0) // Ensure a leave is selected
            {
                try
                {
                        con.Open();
                        string query3 = "UPDATE Leave SET Status = 'Declined' WHERE LeaveID = @LeaveID";
                        SqlCommand command = new SqlCommand(query3, con);
                        command.Parameters.AddWithValue("@LeaveID", selectedLeaveId);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Leave Declined Successfully!");

                        // Refresh Pending Leaves Grid
                        LoadPendingLeaves();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("Please select a leave to decline.");
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new EmpManagerSalary(mainForm));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new EmpManagerEmployee(mainForm));
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new LoginForm(mainForm));
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new EmpManagerDashboard(mainForm));
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
