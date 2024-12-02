using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AK_Textile
{
    public partial class EmpManagerLeave : Form
    {
        private MainForm mainForm;

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");
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

        private void EmpManagerLeave_Load(object sender, EventArgs e)
        {

            con.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT * FROM LeaveType", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();

            try
            {
                da.Fill(dt);

                // Bind the data to the DataGridView
                dataGridView2.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }


            con.Open();
            SqlCommand cmd2 = new SqlCommand("SELECT * FROM LeaveTable WHERE LStatus IS NULL", con);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataTable dt2 = new DataTable();

            try
            {
                da2.Fill(dt);

                // Bind the data to the DataGridView
                dataGridView1.DataSource = dt2;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
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
            mainForm.LoadForm(new LoginForm(mainForm));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new EmpManagerEmployee(mainForm));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new EmpManagerSalary(mainForm));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new EmpManagerDepartment(mainForm));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new EmpManagerReport(mainForm));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            EmpManagerLeaveAdd empmanagerleaveadd = new EmpManagerLeaveAdd();
            empmanagerleaveadd.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            EmpManagerLeaveUpdate empmanagerleaveupdate = new EmpManagerLeaveUpdate();
            empmanagerleaveupdate.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            EmpManagerLeaveRemove empmanagerleaveremove = new EmpManagerLeaveRemove();
            empmanagerleaveremove.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Assuming the Leave ID is in the first column of the DataGridView
                sleave.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            }
        }

        //method to reload the data grid after the approval or decline of the leave
        private void LoadPendingLeaves()
        {
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM LeaveTable WHERE LStatus IS NULL" , con);
            DataTable dt3 = new DataTable();
            adapter.Fill(dt3);
            dataGridView1.DataSource = dt3;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            UpdateLeaveStatus("Declined");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            UpdateLeaveStatus("Approved");
        }
        private void UpdateLeaveStatus(string status)
        {
            if (string.IsNullOrEmpty(sleave.Text))
            {
                MessageBox.Show("Please select a leave from the list.");
                return;
            }

            string leaveId = sleave.Text;

            // Update leave status in the database
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE LeaveTable SET Status = @Status WHERE LeaveID = @LeaveID", con);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@LeaveID", leaveId);

                int r1 = cmd.ExecuteNonQuery();
                con.Close();

                if (r1 > 0)
                {
                    MessageBox.Show($"Leave ID {leaveId} has been {status.ToLower()}.");
                    //calling the method to refresh the data grid 
                    LoadPendingLeaves();
                }
                else
                {
                    MessageBox.Show("Failed to update the leave status. Please try again.");
                }
            }

        }
    }
}
