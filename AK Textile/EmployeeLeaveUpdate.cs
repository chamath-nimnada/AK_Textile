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
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AK_Textile
{
    public partial class EmployeeLeaveUpdate : Form
    {
        private Employee employeeForm; // Reference to Employee Leave

        //database connection
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-KLQEI3V0;Initial Catalog=AKTextilesDatabase;Integrated Security=True");

        // Variable to store the ID of the leave being updated
        private string currentLeaveID = null;

        public EmployeeLeaveUpdate(Employee employeeForm)
        {
            InitializeComponent();
            this.employeeForm = employeeForm;
        }

        private void EmployeeLeaveUpdate_Load(object sender, EventArgs e)
        {
            LoadLeaveTypes();
            ClearDetails();
        }

        //load method
        private void LoadLeaveTypes()
        {
            try
            {
                string query = "SELECT LeaveTypeID, LTName FROM LeaveType";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataTable leaveTypeTable = new DataTable();
                adapter.Fill(leaveTypeTable);

                // Configure the Leave Type ComboBox
                comboBox2.DataSource = leaveTypeTable;
                comboBox2.DisplayMember = "LTName"; 
                comboBox2.ValueMember = "LeaveTypeID";
                comboBox2.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load leave types: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // --- "Select" / "Search" Button Click ---
        private void button5_Click(object sender, EventArgs e)
        {
            string searchID = textBox1.Text.Trim();
            string loggedInEmpID = LoginForm.LoggedInUser.UserId;

            if (string.IsNullOrEmpty(searchID))
            {
                MessageBox.Show("Please enter a Leave ID to search.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(loggedInEmpID))
            {
                MessageBox.Show("Error: Could not identify logged-in user.", "User Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                con.Open();
                string query = @"SELECT LeaveTypeID, LReason, LStartDate, LEndDate, LStatus
                                 FROM Leave
                                 WHERE LeaveID = @LeaveID AND EmpID = @EmpID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@LeaveID", searchID);
                cmd.Parameters.AddWithValue("@EmpID", loggedInEmpID);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string status = reader["LStatus"].ToString();
                    if (status.ToLower() != "pending")
                    {
                        MessageBox.Show($"This leave request (Status: {status}) cannot be updated.", "Update Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        ClearDetails();
                        reader.Close(); // Close reader before returning
                        return;
                    }

                    // Store the ID of the found leave for the update query
                    currentLeaveID = searchID;

                    // Populate fields
                    dateTimePicker1.Value = Convert.ToDateTime(reader["LStartDate"]);
                    dateTimePicker2.Value = Convert.ToDateTime(reader["LEndDate"]);
                    textBox2.Text = reader["LReason"].ToString();
                    comboBox2.SelectedValue = reader["LeaveTypeID"];
                }
                else
                {
                    MessageBox.Show("Leave request with that ID not found for your account, or it's not pending.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearDetails(); // Clear fields if not found
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching leave request: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClearDetails();
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        private void LoadLeaveData()
        {
            string query2 = "SELECT LeaveID FROM Leave";
            using(SqlCommand cmd = new SqlCommand(query2, con))
            using(SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                con.Open();
                DataTable dt = new DataTable();
                da.Fill(dt);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearDetails();

        }

        // ClearMethod
        private void ClearDetails()
        {
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = DateTime.Today;
            comboBox2.SelectedIndex = -1;
            textBox2.Clear();
            currentLeaveID = null; 
            textBox1.Focus();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Check if a leave request is loaded
            if (currentLeaveID == null)
            {
                MessageBox.Show("Please search for and load a leave request first.", "No Leave Loaded", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validation
            if (comboBox2.SelectedValue == null)
            {
                MessageBox.Show("Please select a leave type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Please enter a reason.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dateTimePicker2.Value < dateTimePicker1.Value)
            {
                MessageBox.Show("End date cannot be before the start date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            // Get Updated Data
            string newLeaveTypeID = comboBox2.SelectedValue.ToString();
            string reason = textBox2.Text;
            DateTime startDate = dateTimePicker1.Value;
            DateTime endDate = dateTimePicker2.Value;
            string loggedInEmpID = LoginForm.LoggedInUser.UserId;

            //Database Update
            try
            {
                con.Open();
                string query = @"UPDATE Leave
                                 SET LeaveTypeID = @LeaveTypeID,
                                     LReason = @LReason,
                                     LStartDate = @LStartDate,
                                     LEndDate = @LEndDate
                                     -- Status remains 'Pending' when employee updates
                                 WHERE LeaveID = @CurrentLeaveID AND EmpID = @EmpID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@LeaveTypeID", newLeaveTypeID);
                cmd.Parameters.AddWithValue("@LReason", reason);
                cmd.Parameters.AddWithValue("@LStartDate", startDate);
                cmd.Parameters.AddWithValue("@LEndDate", endDate);
                cmd.Parameters.AddWithValue("@CurrentLeaveID", currentLeaveID);
                cmd.Parameters.AddWithValue("@EmpID", loggedInEmpID);


                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Leave request updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    employeeForm.RefreshDataGrid(); // Refresh parent
                    this.Close(); // Close this form
                }
                else
                {
                    MessageBox.Show("Update failed. Leave not found or no changes made.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating leave request: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                MessageBox.Show("Please search for a leave record first before updating.", "Search Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if Leave Type is selected and Reason is entered
            if (comboBox2.SelectedIndex == -1) // -1 means no item is selected in the ComboBox
            {
                MessageBox.Show("Please select a Leave Type.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                MessageBox.Show("Please enter a Reason for the leave.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Optional: Check if Start Date is before End Date
            if (dateTimePicker1.Value.Date > dateTimePicker2.Value.Date)
            {
                MessageBox.Show("Start Date cannot be after the End Date.", "Date Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- 2. Database Update Logic ---
            try
            {
                // Define the SQL query to update the leave record based on its unique ID.
                // Replace 'LeaveTable' and column names with your actual table and column names.
                string query = "UPDATE LeaveTable SET " +
                               "StartDate = @StartDate, " +
                               "EndDate = @EndDate, " +
                               "LeaveType = @LeaveType, " +
                               "Reason = @Reason " +
                               "WHERE LeaveID = @LeaveID";

                using (SqlConnection con = new SqlConnection(/* Your Connection String Here */))
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameters for the update
                    cmd.Parameters.AddWithValue("@StartDate", dateTimePicker1.Value.Date);
                    cmd.Parameters.AddWithValue("@EndDate", dateTimePicker2.Value.Date);
                    cmd.Parameters.AddWithValue("@LeaveType", comboBox2.SelectedItem.ToString()); // Get the selected Leave Type
                    cmd.Parameters.AddWithValue("@Reason", textBox2.Text.Trim());
                    cmd.Parameters.AddWithValue("@LeaveID", textBox1.Text.Trim()); // The unique ID of the leave record to update

                    con.Open();

                    // Execute the UPDATE query
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Leave details updated successfully.", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // --- 3. Post-Update Actions ---
                        // Example: Clear the form or refresh a DataGridView on the main form
                        // ClearFormControls(); 
                        // parentForm.RefreshLeaveDataGrid(); 
                    }
                    else
                    {
                        MessageBox.Show("No leave record was updated. Please check the Leave ID.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
