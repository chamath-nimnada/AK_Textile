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
    public partial class EmployeeLeaveRemove : Form
    {
        private Employee employeeForm; // Reference to Employee Leave

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");

        // Store the LeaveID shown in the TextBox below the grid
        private string selectedLeaveID = null;

        public EmployeeLeaveRemove(Employee employeeForm)
        {
            InitializeComponent();
            this.employeeForm = employeeForm;
        }

        private void EmployeeLeaveRemove_Load(object sender, EventArgs e)
        {

        }

        //search button
        private void button5_Click_1(object sender, EventArgs e)
        {
            string searchID = textBox1.Text.Trim();
            string loggedInEmpID = LoginForm.LoggedInUser.UserId;

            if (string.IsNullOrWhiteSpace(searchID))
            {
                MessageBox.Show("Please enter a Leave ID to search.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(loggedInEmpID))
            {
                MessageBox.Show("Error: Could not identify logged-in user.", "User Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ClearSelection();

            try
            {
                string query = @"SELECT LeaveID, LeaveTypeID, LReason, LStartDate, LEndDate, LStatus
                                 FROM Leave
                                 WHERE LeaveID = @LeaveID AND EmpID = @EmpID";

                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                adapter.SelectCommand.Parameters.AddWithValue("@LeaveID", searchID);
                adapter.SelectCommand.Parameters.AddWithValue("@EmpID", loggedInEmpID);

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; 
                }
                else
                {
                    MessageBox.Show("No leave request found with that ID for your account.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching leave requests: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView1.DataSource = null;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Make sure it's not the header row
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                //stores the ID of the clicked row
                selectedLeaveID = row.Cells["LeaveID"].Value.ToString();
            }
        }

        //Clear method
        private void ClearSelection()
        {
            selectedLeaveID = null;
        }

        //clear button
        private void button6_Click_1(object sender, EventArgs e)
        {
            textBox1.Clear();
            ClearSelection();
            dataGridView1.DataSource = null;
        }


        //remove button
        private void button9_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedLeaveID))
            {
                MessageBox.Show("Please search for and select a leave request from the grid to remove.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string loggedInEmpID = LoginForm.LoggedInUser.UserId;
            if (string.IsNullOrEmpty(loggedInEmpID))
            {
                MessageBox.Show("Error: Could not identify logged-in user.", "User Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            // Confirm delete dialog box 
            DialogResult confirm = MessageBox.Show($"Are you sure you want to delete leave request {selectedLeaveID}?",
                                                   "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.No)
            {
                //return id user cancels
                return; 
            }

            // Execute Delete Query
            try
            {
                con.Open();
                string query = "DELETE FROM Leave WHERE LeaveID = @LeaveID AND EmpID = @EmpID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@LeaveID", selectedLeaveID);
                cmd.Parameters.AddWithValue("@EmpID", loggedInEmpID);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Leave request removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    employeeForm.RefreshDataGrid();
                    this.Close(); 
                }
                else
                {
                    MessageBox.Show("Deletion failed. Leave might have been removed already or does not belong to you.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error removing leave request: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        //cancel button
        private void button1_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}