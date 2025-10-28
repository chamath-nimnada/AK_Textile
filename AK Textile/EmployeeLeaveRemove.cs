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

        //database connection
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-KLQEI3V0;Initial Catalog=AKTextilesDatabase;Integrated Security=True");

        public EmployeeLeaveRemove(Employee employeeForm)
        {
            InitializeComponent();
            this.employeeForm = employeeForm;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                MessageBox.Show("Please enter the Leave ID to remove.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- 2. Confirmation ---
            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to permanently remove the leave record with ID: " + textBox1.Text.Trim() + "?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
            {
                // --- 3. Database Deletion Logic ---
                try
                {
                    // Replace 'LeaveTable' and 'LeaveID' with your actual table and column names.
                    string deleteQuery = "DELETE FROM LeaveTable WHERE LeaveID = @LeaveID";

                    // Assuming 'con' is a globally accessible or defined SqlConnection object/variable 
                    // from your connection setup (as implied by your sample code).
                    using (SqlConnection connection = new SqlConnection(/* Your Connection String Here or use 'con' */))
                    {
                        connection.Open();

                        using (SqlCommand cmd = new SqlCommand(deleteQuery, connection))
                        {
                            // Add parameter using the ID entered in textBox1
                            cmd.Parameters.AddWithValue("@LeaveID", textBox1.Text.Trim());

                            // Execute the delete command
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Leave record deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Clear the input and display area after successful deletion
                                textBox1.Clear();
                                // Assuming the big text box displaying "Selected Leave" is textBox2
                                // textBox2.Clear(); 

                                // Refresh the main DataGridView to show the updated list
                                // Replace 'employeeLeaveForm' with the actual object name of your parent form/class
                                // employeeLeaveForm.RefreshLeaveDataGrid();
                            }
                            else
                            {
                                MessageBox.Show("No leave record found with the specified ID.", "Deletion Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting record: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

        /*private void button5_Click(object sender, EventArgs e)
        {

                string leaveId = txtLeaveId.Text; // Assuming txtLeaveId is the TextBox for entering Leave ID.

                if (!string.IsNullOrEmpty(leaveId))
            try
            {
                using (SqlConnection connection = new SqlConnection(con.ConnectionString))

                {
                    connection.Open();
                    // SQL Query to search data
                    string query = "SELECT * FROM Leave WHERE LeaveID ";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@search", "%" + textBox1.Text.Trim() + "%");

                        // Use SqlDataAdapter to fetch and display data
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Display the result in DataGridView
                        dataGridView1.DataSource = dt;
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                }
            }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
                txtLeaveId.Clear();        // Clear the Leave ID input box.
                rtbSelectedLeave.Clear();  // Clear the result display area.
        
            textBox1.Text = string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
                this.Close(); // Close the current form or dialog.
            
            this.Close();
            employeeForm.RefreshDataGrid();
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
                string leaveId = txtLeaveId.Text;

            if (dataGridView1.SelectedRows.Count > 0) // Check if a row is selected
            {
                // Get the CategoryID of the selected row (as a string)
                string selectedCategoryID = dataGridView1.SelectedRows[0].Cells["LeaveID"].Value.ToString();

                try
                {
                    using (SqlConnection connection = new SqlConnection(con.ConnectionString))
                    {
                        connection.Open();

                        // SQL Query to delete data
                        string deleteQuery = "DELETE FROM Leave WHERE LeaveID = @LeaveID";

                        using (SqlCommand cmd = new SqlCommand(deleteQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@LeaveID", selectedCategoryID);

                            // Execute the delete command
                            int result = cmd.ExecuteNonQuery();

                            if (result > 0)
                            {
                                MessageBox.Show("Record deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Refresh DataGridView after deletion
                                button9.PerformClick();

                                // Call the public method from InventoryCategory
                                employeeForm.RefreshDataGrid();

                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete the record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show("Please enter a valid Leave ID to remove.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                    MessageBox.Show("Error: " + ex.Message, "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a record to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void EmployeeLeaveRemove_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
*/