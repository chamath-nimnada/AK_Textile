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
    public partial class EmpManagerLeaveUpdate : Form
    {

        private EmpManagerLeave leaveform; // Reference to Supplier
                                           //database connection
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-KLQEI3V0;Initial Catalog=AKTextilesDatabase;Integrated Security=True");

        public EmpManagerLeaveUpdate(EmpManagerLeave leaveform)
        {
            InitializeComponent();
            this.leaveform = leaveform;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string selectedLeaveID = comboBox1.SelectedItem?.ToString(); // Assuming comboBoxLeaveID is the dropdown control
            string leaveName = textBox3.Text.Trim();
            string description = textBox4.Text.Trim();
            string leaveAmountPerMonth = textBox8.Text.Trim();

            if (string.IsNullOrEmpty(selectedLeaveID) || string.IsNullOrEmpty(leaveName) || string.IsNullOrEmpty(leaveAmountPerMonth))
            {
                MessageBox.Show("Please fill in all required fields and select a leave type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } 

            //query to update leaves
            string query = "UPDATE LeaveType SET LeaveName = @LTName, LTDescription = @LTDescription, LTAmount = @LeaveAmountPerMonth WHERE LeaveID = @LeaveID";

            try
            {
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@LeaveID", selectedLeaveID);
                    command.Parameters.AddWithValue("@LTName", leaveName);
                    command.Parameters.AddWithValue("@LTDescription", description);
                    command.Parameters.AddWithValue("@LTAmount", leaveAmountPerMonth);

                    con.Open();
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Leave type updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to update leave type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1; // Reset the dropdown
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox8.Text = string.Empty;

            MessageBox.Show("All fields have been cleared.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
