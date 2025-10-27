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
    public partial class EmpManagerLeaveAdd : Form
    {
        private EmpManagerLeave leaveform; // Reference to Supplier

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");
        public EmpManagerLeaveAdd(EmpManagerLeave leaveform)
        {
            InitializeComponent();
            this.leaveform = leaveform;
        }

        private void AddCategory()
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
                this.Close();
        }

        private void ClearForm()
        {
            textBox3.Clear();
            textBox4.Clear();
            textBox8.Clear();
            textBox3.Focus(); // Set focus
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearForm();

        }

        //add button
        private void button8_Click(object sender, EventArgs e)
        {

            string leaveID = textBox2.Text; 
            string ltName = textBox3.Text.Trim();
            string description = textBox4.Text.Trim();  

            if (string.IsNullOrEmpty(ltName))
            {
                MessageBox.Show("Please enter the Leave Type Name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Validate Amount - Assumes LTAmount is numeric in DB (e.g., int, decimal, float)
            if (!decimal.TryParse(textBox8.Text, out decimal leaveAmount) || leaveAmount < 0)
            {
                MessageBox.Show("Please enter a valid non-negative number for Leave Amount Annually.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                con.Open();
                string query1 = "INSERT INTO LeaveType (LeaveTypeID, LTName, LTDescription, LTAmount) VALUES (@LeaveID, @LTName, @LTDescription, @LTAmount)";

                using (SqlCommand command = new SqlCommand(query1, con))
                {
                    command.Parameters.AddWithValue("@LeaveID", leaveID);
                    command.Parameters.AddWithValue("@LTName", ltName);

                    command.Parameters.AddWithValue("@LTDescription", string.IsNullOrEmpty(description) ? (object)DBNull.Value : description);
                    command.Parameters.AddWithValue("@LTAmount", leaveAmount); 

                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Leave type added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        leaveform.RefreshDataGrid(); // Refresh parent grid
                        ClearForm(); // Clear fields
                        AutoGenerateID(); // Generate next ID
                    }
                    else
                    {
                        MessageBox.Show("Failed to add leave type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Handle potential primary key violation
                if (sqlEx.Number == 2627 || sqlEx.Number == 2601)
                {
                    MessageBox.Show($"Error: Leave Type ID '{leaveID}' already exists.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Consider regenerating ID if this happens often due to concurrent adds
                    AutoGenerateID();
                }
                else
                {
                    MessageBox.Show("Database error occurred: " + sqlEx.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void EmpManagerLeaveAdd_Load(object sender, EventArgs e)
        {
            AutoGenerateID();
            ClearForm();
        }

        private void AutoGenerateID()
        {
            try
            {
                con.Open();
                // Find the highest numeric part of LeaveTypeID
                SqlCommand cmd1 = new SqlCommand("SELECT MAX(CAST(SUBSTRING(LeaveTypeID, 3, LEN(LeaveTypeID)) AS INT)) FROM LeaveType WHERE LeaveTypeID LIKE 'LT%'", con);
                object result = cmd1.ExecuteScalar(); // Use ExecuteScalar for single value

                if (result == DBNull.Value || result == null)
                {
                    textBox2.Text = "LT001"; // Start from 001
                }
                else
                {
                    int numericPart = Convert.ToInt32(result);
                    string newID = "LT" + (numericPart + 1).ToString("D3"); // Increment and format
                    textBox2.Text = newID;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating Leave Type ID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Text = "LT-ERR"; // Indicate error
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }
    }
}
