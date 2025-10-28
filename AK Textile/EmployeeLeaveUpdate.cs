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

        public EmployeeLeaveUpdate(Employee employeeForm)
        {
            InitializeComponent();
            this.employeeForm = employeeForm;
        }

        private void EmployeeLeaveUpdate_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                MessageBox.Show("Please enter an Employee ID to search.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                string query1 = "SELECT * FROM Leave WHERE LeaveID = @LeaveID";
                {
                    using (SqlCommand cmd = new SqlCommand(query1, con))
                    {
                        cmd.Parameters.AddWithValue("@LeaveID", textBox1.Text.Trim());
                        con.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            textBox2.Text = result.ToString();
                        }
                        else
                        {
                            MessageBox.Show("No record found for the given Employee ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                //textBox1.DataSource = dt;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearFields();

        }

        private void ClearFields()
        {
            textBox1.Clear();
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            comboBox2.Text = String.Empty;
            textBox2.Clear();
        }

        private void button8_Click(object sender, EventArgs e)
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
