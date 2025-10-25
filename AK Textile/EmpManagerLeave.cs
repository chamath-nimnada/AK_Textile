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

        Form formBackground = null; // Declare outside to access in 'finally'

        private string selectedLeaveId = null;


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
            LoadPendingLeaves();
            LoadLeaveTypes();
        }
        
        //to load leave types to the data grid
        private void LoadLeaveTypes()
        {
            try
            {
                con.Open();
                string query = "SELECT * FROM LeaveType";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView2.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading leave types: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }


        private void button7_Click(object sender, EventArgs e)
        {
            // Create an instance of the form and pass it to the method
            OpenSubForm(new EmpManagerLeaveAdd(this));
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // Create an instance of the form and pass it to the method
            OpenSubForm(new EmpManagerLeaveUpdate(this));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Create an instance of the form and pass it to the method
            OpenSubForm(new EmpManagerLeaveRemove(this));
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LoadPendingLeaves()
        {
            try
            {
                con.Open();
                // We JOIN Employee to get EmpName, and show LReason
                string query = @"SELECT 
                                    L.LeaveID, 
                                    E.EmpName, 
                                    L.LeaveTypeID, 
                                    L.LReason, 
                                    L.LStartDate, 
                                    L.LEndDate 
                                 FROM Leave L
                                 INNER JOIN Employee E ON L.EmpID = E.EmpID
                                 WHERE L.LStatus = 'Pending'";

                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable; // Assumes dataGridView1 is Pending Leaves
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

        //Approvve button code
        private void button10_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedLeaveId))
            {
                try
                {
                    con.Open();
                    string query2 = "UPDATE Leave SET LStatus = 'Approved' WHERE LeaveID = @LeaveID";
                    SqlCommand command = new SqlCommand(query2, con);
                    command.Parameters.AddWithValue("@LeaveID", selectedLeaveId);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Leave Approved Successfully!");

                    // Refresh Pending Leaves Grid
                    LoadPendingLeaves();
                    textBox1.Text = ""; // Clear textbox
                    selectedLeaveId = null;
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

        //decline button code
        private void button6_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedLeaveId))
            {
                try
                {
                    con.Open();
                    string query3 = "UPDATE Leave SET LStatus = 'Declined' WHERE LeaveID = @LeaveID";
                    SqlCommand command = new SqlCommand(query3, con);
                    command.Parameters.AddWithValue("@LeaveID", selectedLeaveId);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Leave Declined Successfully!");

                    // Refresh Pending Leaves Grid
                    LoadPendingLeaves();
                    textBox1.Text = ""; 
                    selectedLeaveId = null;
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
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                selectedLeaveId = row.Cells["LeaveID"].Value.ToString();
                textBox1.Text = selectedLeaveId;
            }
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

        private void OpenSubForm(Form subForm)
        {
            Form formBackground = new Form(); // Initialize background form

            try
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

                // Set the background form as the owner of the subform
                subForm.Owner = formBackground;

                // Show the subform as a dialog
                subForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Dispose both forms
                formBackground.Dispose();
                subForm.Dispose();
            }
        }
    }
}









    