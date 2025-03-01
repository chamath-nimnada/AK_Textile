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
    public partial class EmployeeLeaveAdd : Form
    {
        public EmployeeLeaveAdd()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            private void buttonAdd_Click(object sender, EventArgs e)
            {
                try
                {
                    // Get values from input fields
                    string leaveID = textBoxLeaveID.Text;
                    DateTime startDate = dateTimePickerStart.Value;
                    DateTime endDate = dateTimePickerEnd.Value;
                    string leaveType = comboBoxLeaveType.SelectedItem?.ToString();
                    string reason = textBoxReason.Text;

                    // Validate input
                    if (string.IsNullOrWhiteSpace(leaveID) || string.IsNullOrWhiteSpace(leaveType) || string.IsNullOrWhiteSpace(reason))
                    {
                        MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (startDate > endDate)
                    {
                        MessageBox.Show("Start date cannot be after the end date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Database connection
                    using (SqlConnection con = new SqlConnection("your_connection_string"))
                    {
                        con.Open();
                        string query = "INSERT INTO EmployeeLeaves (LeaveID, StartDate, EndDate, LeaveType, Reason) VALUES (@LeaveID, @StartDate, @EndDate, @LeaveType, @Reason)";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@LeaveID", leaveID);
                            cmd.Parameters.AddWithValue("@StartDate", startDate);
                            cmd.Parameters.AddWithValue("@EndDate", endDate);
                            cmd.Parameters.AddWithValue("@LeaveType", leaveType);
                            cmd.Parameters.AddWithValue("@Reason", reason);

                            int result = cmd.ExecuteNonQuery();
                            if (result > 0)
                            {
                                MessageBox.Show("Leave added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close(); // Close the form after successful addition
                            }
                            else
                            {
                                MessageBox.Show("Failed to add leave. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
